// File: Services/Muqeem/IqamaService.cs
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Ultimate.IntegrationSystem.Web.Dto;
using Ultimate.IntegrationSystem.Web.Dto.Muqeem;
using Ultimate.IntegrationSystem.Web.Models;

namespace Ultimate.IntegrationSystem.Web.Service.Muqeem
{
    public class IqamaService : IIqamaService
    {
        private readonly HttpClient _http;

        private static readonly JsonSerializerOptions JsonOpts = new(JsonSerializerDefaults.Web)
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        public IqamaService(HttpClient http)
        {
            _http = http ?? throw new ArgumentNullException(nameof(http));
            _http.DefaultRequestHeaders.Accept.Clear();
            _http.DefaultRequestHeaders.Accept.ParseAdd("application/json");
        }

        // =================== Iqama ===================
        public Task<ApiResultModel> RenewAsync(RenewIqamaRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/Iqama/Renew", dto, ct);

        public Task<ApiResultModel> IssueAsync(IssueIqamaRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/Iqama/Issue", dto, ct);

        public Task<ApiResultModel> TransferAsync(TransferIqamaRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/Iqama/Transfer", dto, ct);

        // =================== Exit Re-Entry ===================
        public Task<ApiResultModel> ExitReentryIssueAsync(FEVisaIssuanceRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/ExitReentry/Issue", dto, ct);

        public Task<ApiResultModel> ExitReentryCancelAsync(ERVisaCancellationRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/ExitReentry/Cancel", dto, ct);

        public Task<ApiResultModel> ExitReentryReprintAsync(ReprintERVisaRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/ExitReentry/Reprint", dto, ct);

        public Task<ApiResultModel> ExitReentryExtendAsync(ERVisaExtendRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/ExitReentry/Extend", dto, ct);

        // =================== Final Exit ===================
        public Task<ApiResultModel> FinalExitIssueAsync(FEVisaIssuanceRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/FinalExit/Issue", dto, ct);

        public Task<ApiResultModel> FinalExitCancelAsync(FEVisaCancellationRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/FinalExit/Cancel", dto, ct);

        public Task<ApiResultModel> FinalExitIssueDuringProbationAsync(IssueFEDuringTheProbationaryPeriodRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/FinalExit/IssueDuringProbation", dto, ct);

        // =================== UpdateInfo (Passport) ===================
        public Task<ApiResultModel> UpdateInfoExtendPassportAsync(UIExtendPassportValidityRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/UpdateInfo/ExtendPassport", dto, ct);

        public Task<ApiResultModel> UpdateInfoRenewPassportAsync(UIRenewPassportRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/UpdateInfo/RenewPassport", dto, ct);

        // =================== Visit Visa ===================
        public Task<ApiResultModel> VisitVisaExtendAsync(ExtendVisitVisaRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/VisitVisa/Extend", dto, ct);

        // =================== Reports ===================
        public Task<ApiResultModel> ReportInteractiveAsync(InteractiveServicesReportRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/Reports/Interactive", dto, ct);

        public Task<ApiResultModel> ReportMuqeemPrintAsync(MuqeemReportRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/Reports/Muqeem/Print", dto, ct);

        public Task<ApiResultModel> ReportVisitorPrintAsync(VisitorReportRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/Reports/Visitor/Print", dto, ct);

        // =================== Occupation ===================
        public Task<ApiResultModel> OccupationCheckApprovalAsync(ChangeOccupationApprovalRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/Occupation/CheckApproval", dto, ct);

        public Task<ApiResultModel> OccupationChangeAsync(ChangeOccupationRequestDto dto, CancellationToken ct = default)
            => PostAsync("api/Muqeem/Occupation/Change", dto, ct);

        // =================== Lookups (GET) ===================
        public Task<ApiResultModel> GetCitiesAsync(CancellationToken ct = default)
            => GetAsync("api/Muqeem/Lookups/Cities", ct);

        public Task<ApiResultModel> GetCountriesAsync(CancellationToken ct = default)
            => GetAsync("api/Muqeem/Lookups/Countries", ct);

        public Task<ApiResultModel> GetMaritalStatusesAsync(CancellationToken ct = default)
            => GetAsync("api/Muqeem/Lookups/MaritalStatuses", ct);

        // =================== Docs (للتابات) ===================
        public async Task<List<EmployeeDocumentDto>> GetEmpDocsAsync(EmpDocParaDto para, CancellationToken ct = default)
        {
            using var resp = await _http.PostAsJsonAsync("api/Docs/GetEmpDocs", para, JsonOpts, ct);

            ApiResultModel? api = null;
            try { api = await resp.Content.ReadFromJsonAsync<ApiResultModel>(JsonOpts, ct); } catch { /* ignore */ }

            if (api is null || api.Code != 0 || api.Content is null)
                return new List<EmployeeDocumentDto>();

            try
            {
                if (api.Content is JsonElement je)
                    return je.Deserialize<List<EmployeeDocumentDto>>(JsonOpts) ?? new List<EmployeeDocumentDto>();

                var raw = api.Content.ToString();
                if (!string.IsNullOrWhiteSpace(raw))
                {
                    using var doc = JsonDocument.Parse(raw);
                    return doc.RootElement.Deserialize<List<EmployeeDocumentDto>>(JsonOpts) ?? new List<EmployeeDocumentDto>();
                }
            }
            catch { /* ignore */ }

            return new List<EmployeeDocumentDto>();
        }

        // =================== Helpers ===================
        private async Task<ApiResultModel> PostAsync<TReq>(string url, TReq dto, CancellationToken ct)
        {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            linkedCts.CancelAfter(TimeSpan.FromSeconds(60));

            using var resp = await _http.PostAsJsonAsync(url, dto, JsonOpts, linkedCts.Token);

            ApiResultModel? api = null;
            try { api = await resp.Content.ReadFromJsonAsync<ApiResultModel>(JsonOpts, linkedCts.Token); } catch { }

            if (api is not null) return api;

            var text = await resp.Content.ReadAsStringAsync(linkedCts.Token);
            var (title, detail) = TryReadProblemDetails(text);

            return new ApiResultModel
            {
                Code = (int)resp.StatusCode,
                Message = string.IsNullOrWhiteSpace(detail) ? (title ?? text ?? $"HTTP {(int)resp.StatusCode}") : detail,
                Content = null
            };
        }

        private async Task<ApiResultModel> GetAsync(string url, CancellationToken ct)
        {
            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            linkedCts.CancelAfter(TimeSpan.FromSeconds(60));

            using var resp = await _http.GetAsync(url, linkedCts.Token);

            ApiResultModel? api = null;
            try { api = await resp.Content.ReadFromJsonAsync<ApiResultModel>(JsonOpts, linkedCts.Token); } catch { }

            if (api is not null) return api;

            var text = await resp.Content.ReadAsStringAsync(linkedCts.Token);
            var (title, detail) = TryReadProblemDetails(text);

            return new ApiResultModel
            {
                Code = (int)resp.StatusCode,
                Message = string.IsNullOrWhiteSpace(detail) ? (title ?? text ?? $"HTTP {(int)resp.StatusCode}") : detail,
                Content = null
            };
        }

        private static (string? title, string? detail) TryReadProblemDetails(string? json)
        {
            if (string.IsNullOrWhiteSpace(json)) return (null, null);
            try
            {
                var pd = JsonSerializer.Deserialize<ProblemLike>(json, JsonOpts);
                return (pd?.Title, pd?.Detail);
            }
            catch { return (null, null); }
        }

        private sealed class ProblemLike
        {
            public string? Title { get; set; }
            public string? Detail { get; set; }
        }
    }
}
