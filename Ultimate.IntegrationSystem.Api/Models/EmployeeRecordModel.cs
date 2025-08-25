using System.Text.Json.Serialization;

namespace Ultimate.IntegrationSystem.Api.Models
{
    public class EmployeeRecordModel
    {
        [JsonPropertyName("employeeNumber")]
        public string EmployeeNumber { get; set; }

        [JsonPropertyName("employeeName")]
        public string EmployeeName { get; set; }

        [JsonPropertyName("firstName")]
        public string FirstName { get; set; }

        [JsonPropertyName("jobNo")]
        public string JobNo { get; set; }

        [JsonPropertyName("hrchyNo")]
        public string HrchyNo { get; set; }

        [JsonPropertyName("jobName")]
        public string JobName { get; set; }

        [JsonPropertyName("hrchyName")]
        public string HrchyName { get; set; }

        [JsonPropertyName("telNo")]
        public string TelNo { get; set; }

        [JsonPropertyName("mobileNo")]
        public string MobileNo { get; set; }

        [JsonPropertyName("poBoxNo")]
        public string PoBoxNo { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("inactive")]
        public int? Inactive { get; set; }

        // --- الحقول الإضافية من s_emp ---
        [JsonPropertyName("borderNumber")]
        public string BorderNumber { get; set; }

        [JsonPropertyName("birthCountry")]
        public int? BirthCountry { get; set; }

        [JsonPropertyName("maritalStatus")]
        public int? MaritalStatus { get; set; }

        [JsonPropertyName("familyName")]
        public string FamilyName { get; set; }

        [JsonPropertyName("fatherName")]
        public string FatherName { get; set; }

        [JsonPropertyName("givenName")]
        public string GivenName { get; set; }

        [JsonPropertyName("grandFatherName")]
        public string GrandFatherName { get; set; }

        // أسماء مفهومة من الجداول المرجعية
        [JsonPropertyName("maritalStatusName")]
        public string MaritalStatusName { get; set; }

        [JsonPropertyName("birthCountryName")]
        public string BirthCountryName { get; set; }

        // إذا لاحقاً أضفت المدينة
        [JsonPropertyName("cityName")]
         public string CityName { get; set; }




    }
}
