using System;
using System.Data;
using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;
using Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings;

namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings
{
    public sealed class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly ISettingsProvider _settings;

        public DbConnectionFactory(ISettingsProvider settings) => _settings = settings;

        public IDbConnection Create(string? name = null)
        {
            // استدعاء متزامن مقصود لأن التواقيع لا تسمح async في Construction عادةً
            var (provider, cs) = _settings.GetDataSourceAsync(name, default).GetAwaiter().GetResult();

            if (provider.Equals("Oracle", StringComparison.OrdinalIgnoreCase))
                return new OracleConnection(cs);

            if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
                return new SqlConnection(cs);

            // fallback
            if (cs.Contains("User Id=", StringComparison.OrdinalIgnoreCase) && cs.Contains("Data Source=", StringComparison.OrdinalIgnoreCase))
                return new OracleConnection(cs);

            return new SqlConnection(cs);
        }
    }
}
