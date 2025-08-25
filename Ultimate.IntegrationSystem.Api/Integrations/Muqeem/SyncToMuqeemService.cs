using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Ultimate.IntegrationSystem.Api.Common.Enum;
using Ultimate.IntegrationSystem.Api.DBMangers;
using Ultimate.IntegrationSystem.Api.Models;
using Ultimate.IntegrationSystem.Api.Models.SqlLite;

namespace Ultimate.IntegrationSystem.Api.Integrations.Muqeem
{

    public sealed class SyncToMuqeemService : ISyncToMuqeemService
    {
        private readonly ILogger<SyncToMuqeemService> _logger;
        private readonly IntegrationApiDbContext _db;
        private readonly MuqeemService _muqeemServes;

        private const string PlatformKey = "Muqeem";

        public SyncToMuqeemService(
            ILogger<SyncToMuqeemService> logger,
            IntegrationApiDbContext db,
            MuqeemService muqeemServes)
        {
            _logger = logger;
            _db = db;
            _muqeemServes = muqeemServes;
        }

        private async Task<ApiRequestSettings> GetEndpointSettingsAsync(int endpointId, CancellationToken ct)
        {
            var settings = await _db.ApiRequestSettings
                .AsNoTracking() // تحسين الأداء (لأننا لا نحتاج تتبع الكيان)
                .FirstOrDefaultAsync(s => s.ApiKey == PlatformKey && s.Id == endpointId, ct);

            if (settings is null)
            {
                var errorMessage = $"❌ لم يتم العثور على إعدادات API (Platform={PlatformKey}, EndpointId={endpointId}).";
                _logger.LogError(errorMessage);
                throw new InvalidOperationException(errorMessage);
            }

            return settings;
        }



        public async Task<ApiResultModel> SendAsync(
       int endpointId,
       object payload = null,
       string templateVariable = null,
       TimeSpan? perAttemptTimeout = null,
       CancellationToken ct = default)
        {
            const int maxRetries = 3;

             var endpoint = await GetEndpointSettingsAsync(endpointId, ct);

            // platformKey من DB أو من endpoint
            var platformKey = endpoint.ApiKey ?? "Muqeem";

            var (client, token, baseUrl, appId, appKey) =
                await _muqeemServes.GetAuthorizedHttpClientAsync(platformKey: platformKey);

            // Build URL
            string fullUrl = endpoint.Endpoint;
            string paramPart = FormatTemplate(endpoint.Parametr, new { replaceExistingBarcode = templateVariable });
            if (!string.IsNullOrWhiteSpace(paramPart))
                fullUrl += (fullUrl.Contains("?") ? "&" : "?") + paramPart;

            var requestUri = Uri.TryCreate(fullUrl, UriKind.Absolute, out var abs)
                ? abs
                : new Uri(new Uri(baseUrl), fullUrl);

            var method = new HttpMethod(endpoint.HttpMethod?.ToUpperInvariant() ?? "GET");

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                var baseTimeout = perAttemptTimeout ?? TimeSpan.FromMinutes(5);
                var attemptTimeout = TimeSpan.FromMilliseconds(baseTimeout.TotalMilliseconds * Math.Pow(1.6, attempt - 1));

                using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
                linkedCts.CancelAfter(attemptTimeout);

                using var req = new HttpRequestMessage(method, requestUri)
                {
                    Version = HttpVersion.Version20,
                    VersionPolicy = HttpVersionPolicy.RequestVersionOrLower
                };

                // 🟢 الهيدرز الأساسية
                req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                if (appId != null) req.Headers.TryAddWithoutValidation("app-id", appId);
                if (appKey != null) req.Headers.TryAddWithoutValidation("app-key", appKey);

                if (payload != null && (method == HttpMethod.Post || method == HttpMethod.Put))
                {
                    if (payload is string raw)
                        req.Content = new StringContent(raw, Encoding.UTF8, "application/json");
                    else
                        req.Content = JsonContent.Create(payload);
                }

                try
                {
                    using var res = await client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, linkedCts.Token);
                    var body = await res.Content.ReadAsStringAsync(linkedCts.Token);

                    // 🟠 Unauthorized → جدد التوكن
                    if (res.StatusCode == HttpStatusCode.Unauthorized || res.StatusCode == HttpStatusCode.Forbidden)
                    {
                        _logger.LogWarning("Attempt {Attempt}: Unauthorized/Forbidden: {Url}. Refreshing token…", attempt, requestUri);
                        try
                        {
                            token = await _muqeemServes.RefreshTokenAsync(platformKey);
                        }
                        catch (Exception loginEx)
                        {
                            _logger.LogError(loginEx, "Token refresh failed.");
                            return new ApiResultModel(401, $"فشل تجديد التوكن: {requestUri}", loginEx.Message, "AuthFailed", platformKey);
                        }

                        if (attempt == maxRetries)
                            return new ApiResultModel(401, "فشل المصادقة بعد عدة محاولات", body, "AuthFailed", platformKey);

                        await Task.Delay(TimeSpan.FromSeconds(attempt), ct);
                        continue;
                    }

                    // 🟠 في حالة فشل آخر
                    if (!res.IsSuccessStatusCode)
                    {
                        // تمرير للـ Normalizer
                        var normalizer = ResponseNormalizerFactory.Get(platformKey);
                        var normalized = normalizer.Normalize(
                            MuqeemEndpointMapper.MapPathToEndpoint(requestUri.AbsolutePath),
                            body
                        );

                        if (normalized.Code != 0)
                            return normalized;

                        throw new Exception($"HTTP {(int)res.StatusCode} {res.ReasonPhrase}\n{body}");
                    }

                    // 🟢 نجاح → مرر الاستجابة للـ Normalizer
                    var normalizerOk = ResponseNormalizerFactory.Get(platformKey);
                    var normalizedOk = normalizerOk.Normalize(
                        MuqeemEndpointMapper.MapPathToEndpoint(requestUri.AbsolutePath),
                        body
                    );

                    return normalizedOk;
                }
                catch (OperationCanceledException oce) when (!ct.IsCancellationRequested)
                {
                    _logger.LogWarning(oce, "Attempt {Attempt}: Timeout after {Timeout}", attempt, attemptTimeout);
                    if (attempt == maxRetries)
                        return new ApiResultModel(500, $"انتهت المهلة بعد {attemptTimeout.TotalSeconds:N0} ثانية", oce.Message, "Timeout", platformKey);
                }
                catch (HttpRequestException hre)
                {
                    _logger.LogWarning(hre, "Attempt {Attempt}: HttpRequestException", attempt);
                    if (attempt == maxRetries)
                        return new ApiResultModel(500, "فشل الاتصال بالشبكة بعد عدة محاولات: " + hre.Message, hre.Message, "NetworkError", platformKey);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Attempt {Attempt}: Unexpected error", attempt);
                    if (attempt == maxRetries)
                        return new ApiResultModel(500, ex.Message, ex.InnerException?.Message, "Exception", platformKey);
                }

                await Task.Delay(TimeSpan.FromSeconds(attempt), ct);
            }

            return new ApiResultModel(500, $"فشل إرسال الطلب بعد عدة محاولات {requestUri}", null, "Failed", platformKey);
        }





        //public async Task<ApiResultModel> SendAsync(
        //    int endpointId,
        //    object payload = null,
        //    string templateVariable = null,
        //    TimeSpan? perAttemptTimeout = null,
        //    CancellationToken ct = default)
        //{
        //    const int maxRetries = 3;

        //    var endpoint = await GetEndpointSettingsAsync(endpointId, ct);
        //    var (client, token, baseUrl, appId, appKey) = await _muqeemServes.GetAuthorizedHttpClientAsync(platformKey: "Muqeem");

        // //   client.Timeout = Timeout.InfiniteTimeSpan;

        //    // Build URL
        //    string fullUrl = endpoint.Endpoint;
        //    string paramPart = FormatTemplate(endpoint.Parametr, new { replaceExistingBarcode = templateVariable });
        //    if (!string.IsNullOrWhiteSpace(paramPart))
        //        fullUrl += (fullUrl.Contains("?") ? "&" : "?") + paramPart;

        //    var requestUri = Uri.TryCreate(fullUrl, UriKind.Absolute, out var abs)
        //        ? abs
        //        : new Uri(new Uri(baseUrl), fullUrl);

        //    // Headers (اختيارية من الجدول)
        //    var extraHeaders = string.IsNullOrWhiteSpace(endpoint.Headers)
        //        ? null
        //        : JsonConvert.DeserializeObject<Dictionary<string, string>>(endpoint.Headers);

        //    var method = new HttpMethod(endpoint.HttpMethod?.ToUpperInvariant() ?? "GET");

        //    for (int attempt = 1; attempt <= maxRetries; attempt++)
        //    {
        //        var baseTimeout = perAttemptTimeout ?? TimeSpan.FromMinutes(5);
        //        var attemptTimeout = TimeSpan.FromMilliseconds(baseTimeout.TotalMilliseconds * Math.Pow(1.6, attempt - 1));

        //        using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
        //        linkedCts.CancelAfter(attemptTimeout);

        //        using var req = new HttpRequestMessage(method, requestUri)
        //        {
        //            Version = HttpVersion.Version20,
        //            VersionPolicy = HttpVersionPolicy.RequestVersionOrLower
        //        };

        //        // هيدرز مقيم الأساسية
        //        req.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

        //        //req.Headers.TryAddWithoutValidation("app-id", "APP_ID"); // TODO: اجلبها من DB عندما تضيف العمود
        //        //                                                         // ApiKey من إعدادات الجهاز (integration_api_settings.ApiKey)
        //        //                                                         // هنا لا نملك settings الجهاز مباشرة، لذلك يمكن وضعها في extraHeaders إن رغبت.
        //        //                                                         // أو اجلب الصف من DB إن لزمك:
        //        //                                                         // var deviceSettings = await _db.integration_api_settings.FirstOrDefaultAsync(s => s.Id == 1, linkedCts.Token);
        //        //                                                         // req.Headers.TryAddWithoutValidation("app-key", deviceSettings?.ApiKey);
        //        ////                                                         // إن أحببت: مرّر "app-key" من ApiRequestSettings.Headers


        //        if(appId != null) {
        //            req.Headers.TryAddWithoutValidation("app-id", appId);
        //        }
        //        if (appKey != null)
        //        {

        //            req.Headers.TryAddWithoutValidation("app-key", appKey);
        //        }
        //        //if (extraHeaders is not null)
        //        //{
        //        //    foreach (var h in extraHeaders)
        //        //        req.Headers.TryAddWithoutValidation(h.Key, h.Value);
        //        //}

        //        if (payload != null && (method == HttpMethod.Post || method == HttpMethod.Put))
        //        {
        //            if (payload is string raw)
        //                req.Content = new StringContent(raw, Encoding.UTF8, "application/json");
        //            else
        //                req.Content = JsonContent.Create(payload);
        //        }

        //        try
        //        {
        //            using var res = await client.SendAsync(req, HttpCompletionOption.ResponseHeadersRead, linkedCts.Token);
        //            var body = await res.Content.ReadAsStringAsync(linkedCts.Token);

        //            if (res.StatusCode == HttpStatusCode.Unauthorized || res.StatusCode == HttpStatusCode.Forbidden)
        //            {
        //                _logger.LogWarning("Attempt {Attempt}: Unauthorized/Forbidden: {Url}. Refreshing token…", attempt, requestUri);
        //                try
        //                {
        //                    token = await _muqeemServes.RefreshTokenAsync("Muqeem");
        //                }
        //                catch (Exception loginEx)
        //                {
        //                    _logger.LogError(loginEx, "Token refresh failed.");
        //                    return new ApiResultModel(401, $"فشل تجديد التوكن: {requestUri}", loginEx.Message);
        //                }

        //                if (attempt == maxRetries)
        //                    return new ApiResultModel(401, "فشل المصادقة بعد عدة محاولات", body);

        //                await Task.Delay(TimeSpan.FromSeconds(attempt), ct);
        //                continue;
        //            }

        //            if (!res.IsSuccessStatusCode)
        //            {
        //                var parsed = TryParseApiResult(body);
        //                if (parsed is not null && parsed.Code != 0)
        //                    return parsed;

        //                throw new Exception($"HTTP {(int)res.StatusCode} {res.ReasonPhrase}\n{body}");
        //            }
        //            if(res.IsSuccessStatusCode)
        //            {
        //                if (body.Length > 0)
        //                {

        //                }
        //            }
        //           // var ok = TryParseApiResult(body);
        //            return  new ApiResultModel(0, "OK", body);
        //        }
        //        catch (OperationCanceledException oce) when (!ct.IsCancellationRequested)
        //        {
        //            _logger.LogWarning(oce, "Attempt {Attempt}: Timeout after {Timeout}", attempt, attemptTimeout);
        //            if (attempt == maxRetries)
        //                return new ApiResultModel(500, $"انتهت المهلة بعد {attemptTimeout.TotalSeconds:N0} ثانية", oce.Message);
        //        }
        //        catch (HttpRequestException hre)
        //        {
        //            _logger.LogWarning(hre, "Attempt {Attempt}: HttpRequestException", attempt);
        //            if (attempt == maxRetries)
        //                return new ApiResultModel(500, "فشل الاتصال بالشبكة بعد عدة محاولات: " + hre.Message, hre.Message);
        //        }
        //        catch (Exception ex)
        //        {
        //            _logger.LogError(ex, "Attempt {Attempt}: Unexpected error", attempt);
        //            if (attempt == maxRetries)
        //                return new ApiResultModel(500, ex.Message, ex.InnerException?.Message);
        //        }

        //        await Task.Delay(TimeSpan.FromSeconds(attempt), ct);
        //    }

        //    return new ApiResultModel(500, $"فشل إرسال الطلب بعد عدة محاولات {requestUri}", null);
        //}

        private static ApiResultModel TryParseApiResult(string json)
        {
            try { return JsonConvert.DeserializeObject<ApiResultModel>(json); }
            catch { return null; }
        }

        private static string FormatTemplate(string template, object values)
        {
            if (string.IsNullOrWhiteSpace(template))
                return template;

            var dict = values != null
                ? JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(values))
                : new Dictionary<string, object>();

            foreach (var kv in dict)
                template = template.Replace("{" + kv.Key + "}", kv.Value?.ToString());

            return template;
        }

        // واجهات عمليات مسماة (اختياري)
        public Task<ApiResultModel> IssueExitReentryAsync(int endpointId, object dto, CancellationToken ct = default)
            => SendAsync(endpointId, dto, null, null, ct);
        public Task<ApiResultModel> CancelExitReentryAsync(int endpointId, object dto, CancellationToken ct = default)
            => SendAsync(endpointId, dto, null, null, ct);
        public Task<ApiResultModel> ExtendExitReentryAsync(int endpointId, object dto, CancellationToken ct = default)
            => SendAsync(endpointId, dto, null, null, ct);
        public Task<ApiResultModel> IssueFinalExitAsync(int endpointId, object dto, CancellationToken ct = default)
            => SendAsync(endpointId, dto, null, null, ct);
        public Task<ApiResultModel> CancelFinalExitAsync(int endpointId, object dto, CancellationToken ct = default)
            => SendAsync(endpointId, dto, null, null, ct);
        public Task<ApiResultModel> RenewIqamaAsync(int endpointId, object dto, CancellationToken ct = default)
            => SendAsync(endpointId, dto, null, null, ct);
        public Task<ApiResultModel> UpdateInfoExtendPassportAsync(int endpointId, object dto, CancellationToken ct = default)
            => SendAsync(endpointId, dto, null, null, ct);
        public Task<ApiResultModel> UpdateInfoRenewPassportAsync(int endpointId, object dto, CancellationToken ct = default)
            => SendAsync(endpointId, dto, null, null, ct);
    }
}
