namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings.Entities
{
    public sealed class DataSourceRow
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";                 // "SqlServerMain" | "OracleErp"
        public string Provider { get; set; } = "";             // "SqlServer" | "Oracle"
        public string ConnectionStringCipher { get; set; } = "";// مُشفّر Base64
        public bool IsDefault { get; set; }
        public bool IsEnabled { get; set; } = true;
        public DateTime UpdatedAtUtc { get; set; }
    }
}
