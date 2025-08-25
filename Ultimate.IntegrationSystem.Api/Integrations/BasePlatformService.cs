using Microsoft.EntityFrameworkCore;
using Ultimate.IntegrationSystem.Api.DBMangers;
using Ultimate.IntegrationSystem.Api.Models.SqlLite;

namespace Ultimate.IntegrationSystem.Api.Integrations
{
    public abstract class BasePlatformService : IBasePlatformService
    {
        protected IntegrationApiDbContext _db;
        protected readonly HttpClient _http;
        public abstract string PlatformKey { get; }

        protected BasePlatformService()
        {
            var options = new DbContextOptionsBuilder<IntegrationApiDbContext>()
                .UseSqlite("Data Source=dbSqlit.db")
                .Options;

            _db = new IntegrationApiDbContext(options);
            _http = new HttpClient();
        }

        public async Task<(HttpClient client, string token, string baseUrl, string AppId, string AppKey)> GetAuthorizedHttpClientAsync(string platformKey)
        {
            var settings = await _db.IntegrationApiSettings
                .FirstOrDefaultAsync(s => s.PlatformKey == platformKey);

            if (settings == null)
                throw new Exception($"\u0625\u0639\u062f\u0627\u062f\u0627\u062a {PlatformKey} \u063a\u064a\u0631 \u0645\u0648\u062c\u0648\u062f\u0629 \u0641\u064a \u0642\u0627\u0639\u062f\u0629 \u0627\u0644\u0628\u064a\u0627\u0646\u0627\u062a");

            var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            string token = settings.Token;

            if (string.IsNullOrEmpty(token) || settings.ExpiresOn <= now)
            {
                token = await LoginAndGetTokenAsync(settings);
            }


            return (_http, token, settings.ApiUrl,settings.AppId,settings.ApiKey);
        }

        protected abstract Task<string> LoginAndGetTokenAsync(ApiIntegrationConfig settings);
    }
}
