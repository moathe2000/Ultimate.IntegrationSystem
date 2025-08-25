using Microsoft.EntityFrameworkCore;
using Ultimate.IntegrationSystem.Api.Models.SqlLite;
using Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings.Configurations;

namespace Ultimate.IntegrationSystem.Api.DBMangers
{
    public class IntegrationApiDbContext : DbContext
    {
        public IntegrationApiDbContext(DbContextOptions<IntegrationApiDbContext> options)
            : base(options) { }

        // DbSets (أسماء منطقية؛ الربط للجداول يتم في التكوينات)
        public DbSet<ApiIntegrationConfig> IntegrationApiSettings { get; set; } = default!;
        public DbSet<ConnectionSetting> ConnectionSettings { get; set; } = default!;
        public DbSet<ApiRequestSettings> ApiRequestSettings { get; set; } = default!;



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // كوِّن فقط إذا لم يُحقن من Program.cs
            if (!optionsBuilder.IsConfigured)
            {
                var dbPath = Path.Combine(AppContext.BaseDirectory, "dbSqlit.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // فهرس مركب فريد على (Year, Activity)
            modelBuilder.Entity<ConnectionSetting>()
                        .HasIndex(s => new { s.Year, s.Activity })
                        .IsUnique();

            // طبّق التكوينات المنفصلة
            modelBuilder.ApplyConfiguration(new ApiIntegrationConfigConfig());
            modelBuilder.ApplyConfiguration(new ConnectionSettingConfig());
            modelBuilder.ApplyConfiguration(new ApiRequestSettingsConfig());
        }
    }
}
