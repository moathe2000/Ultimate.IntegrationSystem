namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings.Entities
{
    public sealed class PlatformRow
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";                 // "Muqeem" | "Shopify" | "Woo"...
        public string Account { get; set; } = "default";       // لدعم تعدد الحسابات
        public string BaseUrl { get; set; } = "";
        public string? ApiKeyCipher { get; set; }              // مُشفّر Base64
        public string? SecretCipher { get; set; }              // مُشفّر Base64
        public string? ExtraJson { get; set; }                 // {"Username":"...","Password":"...","AppId":"...","AppKey":"...","IntegratorId":null}
        public bool IsEnabled { get; set; } = true;
        public DateTime UpdatedAtUtc { get; set; }
    }
}
