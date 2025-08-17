using Microsoft.EntityFrameworkCore;
using Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings.Entities;

namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings
{
    public sealed class SettingsDbContext : DbContext
    {
        public SettingsDbContext(DbContextOptions<SettingsDbContext> options) : base(options) { }

        public DbSet<DataSourceRow> DataSources => Set<DataSourceRow>();
        public DbSet<PlatformRow> Platforms => Set<PlatformRow>();

        protected override void OnModelCreating(ModelBuilder b)
        {
            // DataSources
            b.Entity<DataSourceRow>(e =>
            {
                e.ToTable("DataSources");
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(128);
                e.Property(x => x.Provider).IsRequired().HasMaxLength(32); // "SqlServer" | "Oracle"
                e.Property(x => x.ConnectionStringCipher).IsRequired();
                e.Property(x => x.IsDefault).HasDefaultValue(false);
                e.Property(x => x.IsEnabled).HasDefaultValue(true);
                e.Property(x => x.UpdatedAtUtc).IsRequired();

                e.HasIndex(x => x.Name).IsUnique();
                e.HasIndex(x => new { x.IsEnabled, x.IsDefault });
            });

            // Platforms
            b.Entity<PlatformRow>(e =>
            {
                e.ToTable("Platforms");
                e.HasKey(x => x.Id);
                e.Property(x => x.Name).IsRequired().HasMaxLength(64);      // "Muqeem" | "Shopify" | ...
                e.Property(x => x.Account).IsRequired().HasMaxLength(64);   // "default" أو اسم الحساب
                e.Property(x => x.BaseUrl).IsRequired().HasMaxLength(512);
                e.Property(x => x.IsEnabled).HasDefaultValue(true);
                e.Property(x => x.UpdatedAtUtc).IsRequired();
                // حقول حساسة مُشفّرة:
                e.Property(x => x.ApiKeyCipher);
                e.Property(x => x.SecretCipher);
                e.Property(x => x.ExtraJson);

                e.HasIndex(x => new { x.Name, x.Account }).IsUnique();
                e.HasIndex(x => x.IsEnabled);
            });
        }
    }

    }
