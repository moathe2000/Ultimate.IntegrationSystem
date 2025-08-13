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
            // احصل على مصفوفة العناصر من Content
            //JsonElement arrayEl;
            //if (envelope.Content.ValueKind == JsonValueKind.Array)
            //{
            //    arrayEl = envelope.Content;
            //}
            //else if (envelope.Content.ValueKind == JsonValueKind.String)
            //{
            //    var inner = envelope.Content.GetString() ?? "[]";
            //    using var doc = JsonDocument.Parse(inner);
            //    arrayEl = doc.RootElement.ValueKind == JsonValueKind.Array ? doc.RootElement : default;
            //    if (arrayEl.ValueKind != JsonValueKind.Array) return new List<EmployeeDto>();
            //}
            //else
            //{
            //    return new List<EmployeeDto>();
            //}

            //static string S(JsonElement el, string name)
            //    => el.TryGetProperty(name, out var v) && v.ValueKind != JsonValueKind.Null
            //       ? (v.ValueKind == JsonValueKind.String ? v.GetString() ?? "" : v.ToString())
            //       : "";

            //var list = new List<EmployeeDto>();
            //foreach (var e in arrayEl.EnumerateArray())
            //{
            //    var employeeNumber = S(e, "employeeNumber");
            //    var employeeName = S(e, "employeeName");
            //    var firstName = S(e, "firstName");
            //    var jobTitle = S(e, "jobName");    // من JSON
            //    var dept = S(e, "hrchyName");  // من JSON

            //    // Id نوعه Guid لديك: حوِّل أو أنشئ جديدًا
            //    //Guid id = Guid.Empty;
            //    //if (!Guid.TryParse(employeeNumber, out id))
            //    //    id = Guid.NewGuid();

            //    list.Add(new EmployeeDto
            //    {
            //     // Id = id, // << يحل CS0029
            //        EmployeeNumber = employeeNumber,
            //        FullName = !string.IsNullOrWhiteSpace(employeeName) ? employeeName : firstName,
            //        Department = dept,
            //        JobTitle = jobTitle,
            //        Inactive = true,
            //        HireDate = DateTime.MinValue, // << يحل CS0037 (أو اجعل الخاصية Nullable)
            //        EmployeeName = employeeName,
            //        Address = envelope.,
                    
            //    });
            //}

            return envelope.ToList();
        }

   
        


        

        
    }
    }
  
