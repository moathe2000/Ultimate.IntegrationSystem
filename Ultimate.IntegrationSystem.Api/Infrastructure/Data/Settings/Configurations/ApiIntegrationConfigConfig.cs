using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultimate.IntegrationSystem.Api.Models.SqlLite;

namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings.Configurations
{
    public sealed class ApiIntegrationConfigConfig : IEntityTypeConfiguration<ApiIntegrationConfig>
    {
        public void Configure(EntityTypeBuilder<ApiIntegrationConfig> e)
        {
            e.ToTable("integration_api_settings");
            e.HasKey(x => x.Id);

            e.Property(x => x.Token);
            e.Property(x => x.RefreshToken);
            e.Property(x => x.ExpiresOn);
            e.Property(x => x.RefreshTokenExpiration);

            e.Property(x => x.Email).HasMaxLength(256);
            e.Property(x => x.Password).HasMaxLength(256);  // ⚠️ يفضّل عدم تخزينها plain
            e.Property(x => x.ApiKey).HasMaxLength(256);
            e.Property(x => x.ApiUrl).HasMaxLength(1024);
            e.Property(x => x.PublicIPWithPort).HasMaxLength(64);
            e.Property(x => x.DateFormat).HasMaxLength(32);
            e.Property(x => x.DName).HasMaxLength(128);
            e.Property(x => x.RedirectUrl).HasMaxLength(1024);

            // أعلام/خيارات افتراضية (0/1)
            e.Property(x => x.FracWhenSyncPrice).HasDefaultValue(0);
            e.Property(x => x.ShowOrderDataWhenSync).HasDefaultValue(0);
            e.Property(x => x.SyncItemQuantityAndPrice).HasDefaultValue(0);
            e.Property(x => x.SyncItemQuantityAndPricePeriodInMinuts).HasDefaultValue(0);
            e.Property(x => x.ShowActiveSyncButton).HasDefaultValue(0);
            e.Property(x => x.DoLog).HasDefaultValue(0);
            e.Property(x => x.AddOnlySubProductsFromOrderWithPriceDistribution).HasDefaultValue(0);

            // فهرس مفيد إذا تبحث بالـ ApiUrl أو Email
            e.HasIndex(x => x.ApiUrl);
            e.HasIndex(x => x.Email);
        }
    }
}