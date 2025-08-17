using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;
using Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings.Entities;

namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings
{
    public sealed class DbSettingsProvider : ISettingsProvider
    {
        private readonly SettingsDbContext _db;
        private readonly ISecretCipher _cipher;
        private readonly IMemoryCache _cache;

        public DbSettingsProvider(SettingsDbContext db, ISecretCipher cipher, IMemoryCache cache)
        { _db = db; _cipher = cipher; _cache = cache; }

        public void Invalidate(string? key = null)
        {
            if (string.IsNullOrWhiteSpace(key)) _cache.Dispose(); // يمسح كل الكاش
            else _cache.Remove(key);
        }

        public async Task<(string provider, string connectionString)> GetDataSourceAsync(string? name, CancellationToken ct)
        {
            var key = $"ds::{(string.IsNullOrWhiteSpace(name) ? "default" : name)}";
            if (_cache.TryGetValue<(string, string)>(key, out var cached)) return cached;

            DataSourceRow row;
            if (string.IsNullOrWhiteSpace(name))
                row = await _db.DataSources.AsNoTracking().Where(x => x.IsEnabled)
                        .OrderByDescending(x => x.IsDefault).FirstAsync(ct);
            else
                row = await _db.DataSources.AsNoTracking()
                        .FirstAsync(x => x.Name == name && x.IsEnabled, ct);

            var cs = _cipher.Decrypt(row.ConnectionStringCipher);
            var result = (row.Provider, cs);
            _cache.Set(key, result, TimeSpan.FromMinutes(5));
            return result;
        }

        public async Task<PlatformConfig> GetPlatformAsync(string name, string? account, CancellationToken ct)
        {
            var acc = string.IsNullOrWhiteSpace(account) ? "default" : account!;
            var key = $"pf::{name}::{acc}";
            if (_cache.TryGetValue<PlatformConfig>(key, out var cached)) return cached;

            var row = await _db.Platforms.AsNoTracking()
                        .FirstAsync(x => x.Name == name && x.Account == acc && x.IsEnabled, ct);

            var cfg = new PlatformConfig
            {
                Name = row.Name,
                Account = row.Account,
                BaseUrl = row.BaseUrl,
                ApiKey = row.ApiKeyCipher is null ? null : _cipher.Decrypt(row.ApiKeyCipher),
                Secret = row.SecretCipher is null ? null : _cipher.Decrypt(row.SecretCipher),
                Extra = string.IsNullOrWhiteSpace(row.ExtraJson) ? null : JsonDocument.Parse(row.ExtraJson)
            };
            _cache.Set(key, cfg, TimeSpan.FromMinutes(5));
            return cfg;
        }
    }
}