using System.Data;
using Microsoft.Data.SqlClient;
using Oracle.ManagedDataAccess.Client;

namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings
{
    public sealed class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly ISettingsProvider _settings;
        public DbConnectionFactory(ISettingsProvider settings) => _settings = settings;

        public IDbConnection Create(string? name = null)
        {
            // NOTE: يتم الاستدعاء غالباً في سياقات غير async (Repositories)، لذلك نستخدم .Result هنا
            var (provider, cs) = _settings.GetDataSourceAsync(name, CancellationToken.None).GetAwaiter().GetResult();

            if (provider.Equals("Oracle", StringComparison.OrdinalIgnoreCase))
                return new OracleConnection(cs);

            if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase))
                return new SqlConnection(cs);

            // محاولة تخمين على أساس صيغة النص
            if (cs.Contains("User Id=", StringComparison.OrdinalIgnoreCase) &&
                cs.Contains("Data Source=", StringComparison.OrdinalIgnoreCase))
                return new OracleConnection(cs);

            return new SqlConnection(cs);
        }
    }
}
