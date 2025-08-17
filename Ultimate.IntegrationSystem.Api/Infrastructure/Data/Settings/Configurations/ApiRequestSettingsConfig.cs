using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ultimate.IntegrationSystem.Api.Models.SqlLite;

namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings.Configurations
{
    public sealed class ApiRequestSettingsConfig : IEntityTypeConfiguration<ApiRequestSettings>
    {
        public void Configure(EntityTypeBuilder<ApiRequestSettings> e)
        {
            e.ToTable("ApiRequestSettings");
            e.HasKey(x => x.Id);

            e.Property(x => x.Endpoint)
                .IsRequired()
                .HasMaxLength(1024);
            e.Property(x => x.HttpMethod)
                .IsRequired()
                .HasMaxLength(8); // GET/POST/PUT/DELETE

            // JSON/Text columns
            e.Property(x => x.Headers);
            e.Property(x => x.Parametr);
            e.Property(x => x.BodyTemplate);

            e.Property(x => x.Description)
                .HasMaxLength(512);

            e.Property(x => x.ApiKey)
                .HasMaxLength(256); // ⚠️ يفضّل تشفيرها أو نقلها لجدول آمن
            e.Property(x => x.BodyFormat)
                .HasMaxLength(32);

            // CreatedAt محفوظ كسلسلة لديك؛ نحافظ عليها كما هي
            e.Property(x => x.CreatedAt)
                .HasMaxLength(40);

            // فهرس مساعد اختياري لتحسين البحث
            e.HasIndex(x => new { x.HttpMethod, x.Endpoint });
        }
    }
}
