using Newtonsoft.Json.Linq;
using Ultimate.IntegrationSystem.Api.Common.Enum;
using Ultimate.IntegrationSystem.Api.Integrations.Muqeem;
using Ultimate.IntegrationSystem.Api.Models;

namespace Ultimate.IntegrationSystem.Api.Integrations
{
    public class DefaultResponseNormalizer : IResponseNormalizer
    {
        public ApiResultModel Normalize(MuqeemEndpoint endpoint, string rawResponse)
        {
            if (string.IsNullOrWhiteSpace(rawResponse))
                return new ApiResultModel(500, "استجابة فارغة", null);

            try
            {
                var token = JToken.Parse(rawResponse);
                return new ApiResultModel(0, "Success", token);
            }
            catch
            {
                // الاستجابة مش JSON → نرجعها كنص خام
                return new ApiResultModel(0, "Success (RawText)", rawResponse);
            }
        }
    }
}
