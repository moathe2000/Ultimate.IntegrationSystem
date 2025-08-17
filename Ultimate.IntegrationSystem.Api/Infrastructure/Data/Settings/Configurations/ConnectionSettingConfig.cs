using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultimate.IntegrationSystem.Api.Models.SqlLite;

namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings.Configurations
{
    public sealed class ConnectionSettingConfig : IEntityTypeConfiguration<ConnectionSetting>
    {
        public void Configure(EntityTypeBuilder<ConnectionSetting> e)
        {
            e.ToTable("ConnectionSetting");
            e.HasKey(x => x.ID);

            // Requireds & قيود طول منطقية
            e.Property(x => x.SelectedSystem)
                .IsRequired()
                .HasMaxLength(64);
            e.Property(x => x.SchemaName)
                .HasMaxLength(128);
            e.Property(x => x.Password)
                .HasMaxLength(256); // 
            e.Property(x => x.Year)
                .IsRequired()
                .HasMaxLength(10);  // لأنها string عندك
            e.Property(x => x.Activity)
                .IsRequired();

            e.Property(x => x.Host)
                .IsRequired()
                .HasMaxLength(255);
            e.Property(x => x.Port)
                .IsRequired();
            e.Property(x => x.ServiceName)
                .IsRequired()
                .HasMaxLength(128);

            // فهرس مركّب فريد
            e.HasIndex(x => new { x.Year, x.Activity })
                .IsUnique();
        }
    }
}
