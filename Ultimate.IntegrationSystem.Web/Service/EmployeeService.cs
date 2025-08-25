using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using Ultimate.IntegrationSystem.Web.Models;

namespace Ultimate.IntegrationSystem.Web.Service
{


    public class EmployeeService : IEmployeeService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _json;

       [ ActivatorUtilitiesConstructor]
        public EmployeeService(HttpClient http, IConfiguration cfg, IHttpContextAccessor? acc = null)
        {
            if (http.BaseAddress is null)
            {
                var baseUrl = cfg["ApiBaseUrl"];
                if (string.IsNullOrWhiteSpace(baseUrl) && acc?.HttpContext != null)
                    baseUrl = $"{acc.HttpContext.Request.Scheme}://{acc.HttpContext.Request.Host}/";

                if (!string.IsNullOrWhiteSpace(baseUrl))
                    http.BaseAddress = new Uri(baseUrl);
            }
            _http = http;
        }

        // Adjust the route to your controller route
        private const string Endpoint = "GetEmployees";

        public EmployeeService(HttpClient http)
        {
            _http = http;
            _json = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync(EmployeePara para = null, CancellationToken ct = default)
        {
            para ??= new EmployeePara { P_LNG_NO = 1 };


            //var response1 = await _http.PostAsJsonAsync(Endpoint, para).ConfigureAwait(false);

            //var response = JsonConvert.DeserializeObject<ApiResultModel>(await response1.Content.ReadAsStringAsync());


            using var resp = await _http.PostAsJsonAsync(Endpoint, para, _json, ct);
            resp.EnsureSuccessStatusCode();

            var raw = await resp.Content.ReadAsStringAsync(ct);
            var result = JsonConvert.DeserializeObject<ApiResultModel>(raw.ToString())
                          ?? throw new Exception("Empty response.");
            if (result.Code != 0)
                throw new Exception(result.Message ?? "API error.");

            var envelope = JsonConvert.DeserializeObject<EmployeeDto[]>(result.Content.ToString());
          

            return envelope.ToList();
        }

   
        


        

        
    }
    }
  
