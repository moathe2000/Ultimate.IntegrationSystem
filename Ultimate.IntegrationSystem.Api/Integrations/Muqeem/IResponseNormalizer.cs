using Ultimate.IntegrationSystem.Api.Common.Enum;
using Ultimate.IntegrationSystem.Api.Models;

namespace Ultimate.IntegrationSystem.Api.Integrations.Muqeem
{
    public interface IResponseNormalizer
    {
        ApiResultModel Normalize(MuqeemEndpoint endpoint, string rawResponse);
       // (bool Success, string Kind, object Content) Normalize(string body, string endpointPath = null);
    }
}
