using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Ultimate.IntegrationSystem.Web.Dto;
using Ultimate.IntegrationSystem.Web.Models;

namespace Ultimate.IntegrationSystem.Web.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _json;

        public EmployeeService(HttpClient http)
        {
            _http = http;
            _json = new(JsonSerializerDefaults.Web)
            {
                PropertyNameCaseInsensitive = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                NumberHandling = JsonNumberHandling.AllowReadingFromString
            };
        }

        // عدّل هذا ليتوافق مع الراوت الفعلي لديك
        private const string Endpoint = "GetEmployees";

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync(EmployeePara? para = null, CancellationToken ct = default)
        {
            para ??= new EmployeePara { P_LNG_NO = 1 };

            using var cts = CancellationTokenSource.CreateLinkedTokenSource(ct);
            cts.CancelAfter(TimeSpan.FromSeconds(60));

            using var resp = await _http.PostAsJsonAsync(Endpoint, para, _json, cts.Token);

            var api = await resp.Content.ReadFromJsonAsync<ApiResultModel<List<EmployeeDto>>>(_json, cts.Token);
            if (api is null)
            {
                var txt = await resp.Content.ReadAsStringAsync(cts.Token);
                throw new InvalidOperationException($"Unexpected response ({(int)resp.StatusCode}): {txt}");
            }

            if (api.Code != 0)
                throw new InvalidOperationException(api.Message?.Trim().Length > 0 ? api.Message! : "API error.");

            return api.Content ?? new List<EmployeeDto>();
        }
    }
}
