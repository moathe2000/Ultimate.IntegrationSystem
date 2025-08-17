using Microsoft.EntityFrameworkCore;
using Ultimate.IntegrationSystem.Api.Models.SqlLite;

namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings.Configurations
{
    public class IntegrationApiDbContext : DbContext
    {
        public IntegrationApiDbContext(DbContextOptions<IntegrationApiDbContext> options)
            : base(options) { }

        // إن أردت إبقاء أسماء DbSet كما هي فهذا لا يؤثر على EF
        public DbSet<ApiIntegrationConfig> IntegrationApiSettings { get; set; } = default!;
        public DbSet<ConnectionSetting> DBSetting { get; set; } = default!;
        public DbSet<ApiRequestSettings> ApiRequestSettings { get; set; } = default!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // لا تُعيد التكوين إذا تم حقنه من Program.cs
            if (!optionsBuilder.IsConfigured)
            {
                // كخطة طوارئ فقط؛ الأفضل تمرير السلسلة من Program.cs
                var dbPath = Path.Combine(AppContext.BaseDirectory, "dbSqlit.db");
                optionsBuilder.UseSqlite($"Data Source={dbPath}");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // فهرس مركّب فريد (Year, Activity) كما لديك
            modelBuilder.Entity<ConnectionSetting>()
                .HasIndex(s => new { s.Year, s.Activity })
                .IsUnique();

            // تكوينات إضافية موصى بها (قيود الطول/الإلزام—حسب خصائص موديلاتك)
            modelBuilder.ApplyConfiguration(new ApiIntegrationConfigConfig());
            modelBuilder.ApplyConfiguration(new ConnectionSettingConfig());
            modelBuilder.ApplyConfiguration(new ApiRequestSettingsConfig());
        }
    }
}
