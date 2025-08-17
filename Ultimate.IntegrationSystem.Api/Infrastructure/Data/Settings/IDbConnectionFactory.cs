using System.Data;

namespace Ultimate.IntegrationSystem.Api.Infrastructure.Data.Settings
{
    public interface IDbConnectionFactory
    {
        IDbConnection Create(string? name = null); // null => default
    }
}
