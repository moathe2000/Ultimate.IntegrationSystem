using Ultimate.IntegrationSystem.Api.Models;

namespace Ultimate.IntegrationSystem.Api.Interface
{
    public interface IDbModelMappingService
    {
       // DbTransResultModel GetDbResultModel(string str);
        DbTransResultModel GetDbResultModelFromJson(string jsonString);
       
        List<T> MapJson<T>(DbTransResultModel model);
    }
}
