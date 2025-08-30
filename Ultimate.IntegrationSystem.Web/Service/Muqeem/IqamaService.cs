using System.Text.Json;
using System.Text.Json.Serialization;
using Ultimate.IntegrationSystem.Api.Dto.Muqeem.Requests;
using Ultimate.IntegrationSystem.Web.Models;

namespace Ultimate.IntegrationSystem.Web.Service.Muqeem
{
    public class IqamaService : IIqamaService
    {
        private readonly HttpClient _http;
        private static readonly JsonSerializerOptions JsonOpts = new(JsonSerializerDefaults.Web)
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            NumberHandling = JsonNumberHandling.AllowReadingFromString
        };

        public IqamaService(HttpClient http) => _http = http;

        public Task<ApiResultModel> RenewAsync(RenewIqamaRequestDto dto, CancellationToken ct = default)
            => Post("api/Muqeem/Iqama/Renew", dto, ct);

        public Task<ApiResultModel> IssueAsync(IssueIqamaRequestDto dto, CancellationToken ct = default)
            => Post("api/Muqeem/Iqama/Issue", dto, ct);

        public Task<ApiResultModel> TransferAsync(TransferIqamaRequestDto dto, CancellationToken ct = default)
            => Post("api/Muqeem/Iqama/Transfer", dto, ct);

        private async Task<ApiResultModel> Post<TReq>(string url, TReq dto, CancellationToken ct)
        {
            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            cts.CancelAfter(TimeSpan.FromSeconds(60));

            using var resp = await _http.PostAsJsonAsync(url, dto, JsonOpts, cts.Token);
            var api = await resp.Content.ReadFromJsonAsync<ApiResultModel>(JsonOpts, cts.Token);

            if (api is null)
            {
                var text = await resp.Content.ReadAsStringAsync(cts.Token);
                return new ApiResultModel
                {
                    Code = (int)resp.StatusCode,
                    Message = $"Unexpected response: {text}"
                };
            }
            return api;
        }
    }
}
