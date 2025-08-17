using System.Text.Json;

namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings.Entities
{
    public sealed class PlatformConfig
    {
        public string Name { get; init; } = "";
        public string Account { get; init; } = "default";
        public string BaseUrl { get; init; } = "";
        public string? ApiKey { get; init; }
        public string? Secret { get; init; }
        public JsonDocument? Extra { get; init; } // أمثال: Username/Password/AppId/AppKey/IntegratorId
    }
}
