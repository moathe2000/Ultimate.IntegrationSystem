using System.Text.Json.Serialization;

namespace Ultimate.IntegrationSystem.Api.Models
{
    public class EmployeeRecordModel
    {
        [JsonPropertyName("employeeNumber")] 
        public string EmployeeNumber { get; set; }
        [JsonPropertyName("employeeName")]
        public string EmployeeName { get; set; }
        [JsonPropertyName("firstName")] public string FirstName { get; set; }
        [JsonPropertyName("jobNo")] public string JobeNo { get; set; }
        [JsonPropertyName("hrchyNo")] public string HrchyNo { get; set; }
        [JsonPropertyName("jobName")] public string JobName { get; set; }
        [JsonPropertyName("hrchyName")] public string HrchyName { get; set; }
        [JsonPropertyName("telNo")] public string TelNo { get; set; }
        [JsonPropertyName("mobileNo")] public string MobileNo { get; set; }
        [JsonPropertyName("poBoxNo")] public string BoxNo { get; set; }
        [JsonPropertyName("address")] public string Address { get; set; }
        [JsonPropertyName("website")] 
        public string Website { get; set; }
        [JsonPropertyName("email")] 
        public string Email { get; set; }
        [JsonPropertyName("inactive")]
        public int? Inactive { get; set; }
        
    }
}
