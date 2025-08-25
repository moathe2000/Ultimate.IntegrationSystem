using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Ultimate.IntegrationSystem.Api.Models.SqlLite;

namespace Ultimate.IntegrationSystem.Api.Integrations.Muqeem
{ 
    public sealed class MuqeemService : BasePlatformService
    {
        public override string PlatformKey => "Muqeem";

        protected override async Task<string> LoginAndGetTokenAsync(ApiIntegrationConfig settings)
        {
            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

            // لو في توكن صالح، ارجعه
            if (!string.IsNullOrWhiteSpace(settings.Token) && settings.ExpiresOn > now)
                return settings.Token;

            // غير صالح → سجل دخول
            return await LoginWithCredentialsAsync(settings);
        }

        private async Task<string> LoginWithCredentialsAsync(ApiIntegrationConfig settings)
        {
            // بناء عنوان المصادقة حسب Swagger: POST /api/authenticate
            var authUrl = $"{settings.ApiUrl.TrimEnd('/')}/api/authenticate";

            // وفق الـ Swagger: { username, password } في الجسم
            var body = new { username = settings.Email, password = settings.Password };

            using var req = new HttpRequestMessage(HttpMethod.Post, authUrl)
            {
                Content = JsonContent.Create(body)
            };

            // هيدرز مقيم الإلزامية (إن كانت محفوظة لديك ضمن ApiIntegrationConfig)
            // ملاحظة: في موديلك الحالي لا يوجد AppId/IntegratorId، فقط ApiKey.
            // إن كنت تحفظ AppId في ApiKey (أو بعمود لاحق)، عدّل السطور أدناه وفق تخزينك:
            req.Headers.TryAddWithoutValidation("app-id", settings.AppId); // TODO: خذها من جدولك عند إضافته
            req.Headers.TryAddWithoutValidation("app-key", settings.ApiKey);
            // IntegratorId (اختياري) إن توفر لديك في جدولك:
            // req.Headers.TryAddWithoutValidation("X-INTEGRATOR-ID", settings.IntegratorId);

            var res = await _http.SendAsync(req);
            var content = await res.Content.ReadAsStringAsync();

            if (!res.IsSuccessStatusCode)
                throw new Exception($"فشل المصادقة على Muqeem (Status: {(int)res.StatusCode} {res.ReasonPhrase}): {content}");

            // حسب الـ Swagger: الاستجابة { idToken: "..." }
            var authDto = JsonConvert.DeserializeObject<AuthDto>(content);
            var token = authDto?.idToken;

            if (string.IsNullOrWhiteSpace(token))
                throw new Exception("لم يتم استلام idToken من خدمة المقيم.");

            // مدة صلاحية التوكن ليست مذكورة صراحة في الـ Swagger لديك.
            // سنخزن مدة افتراضية (مثلاً 40 دقيقة) أو اجعلها من جدولك لاحقًا.
            var expiresInSeconds = 40 * 60;

            settings.Token = token;
            settings.ExpiresOn = (int)DateTimeOffset.UtcNow.AddSeconds(expiresInSeconds).ToUnixTimeSeconds();
            // RefreshTokenExpiration غير معروف من Muqeem؛ اتركه كما هو أو صفر.
            // settings.RefreshTokenExpiration = ...

            await _db.SaveChangesAsync();
            return token;
        }

        public async Task<string> RefreshTokenAsync(string PlatFormNo)
        {
            var settings = await _db.IntegrationApiSettings.FirstOrDefaultAsync(s => s.PlatformKey == PlatFormNo);
            if (settings == null)
                throw new Exception($"الإعدادات غير موجودة  رقم {PlatformKey}");

            return await LoginWithCredentialsAsync(settings);
        }

        private sealed class AuthDto
        {
            public string idToken { get; set; }
        }
    }
}