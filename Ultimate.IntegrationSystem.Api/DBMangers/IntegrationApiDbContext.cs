using Microsoft.EntityFrameworkCore;
using Ultimate.IntegrationSystem.Api.Models.SqlLite;

namespace Ultimate.IntegrationSystem.Api.DBMangers
{
    public class IntegrationApiDbContext : DbContext
    {
        private readonly string _dbPath;

        public IntegrationApiDbContext(DbContextOptions<IntegrationApiDbContext> options) : base(options)
        {
            _dbPath = Path.Combine(Directory.GetCurrentDirectory(), "dbSqlit.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlite($"Data Source={_dbPath}");
            }
        }

       
        public DbSet<ApiIntegrationConfig> integration_api_settings { get; set; }
        public DbSet<ConnectionSetting> DBSetting { get; set; }
     

        public DbSet<ApiRequestSettings> ApiRequestSettings { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // إضافة قيد `UNIQUE` بين Year و Activity لضمان عدم التكرار
            modelBuilder.Entity<ConnectionSetting>()
                .HasIndex(s => new { s.Year, s.Activity })
                .IsUnique();  // ضمان عدم تكرار Year و Activity معًا

           
        }
    }
}
