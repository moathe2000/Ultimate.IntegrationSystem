using Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings.Entities;

namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings
{
    public interface ISettingsProvider
    {
        Task<(string provider, string connectionString)> GetDataSourceAsync(string? name, CancellationToken ct);
        Task<PlatformConfig> GetPlatformAsync(string name, string? account, CancellationToken ct);
        void Invalidate(string? key = null); // لمسح الكاش بعد أي تعديل
    }
}
