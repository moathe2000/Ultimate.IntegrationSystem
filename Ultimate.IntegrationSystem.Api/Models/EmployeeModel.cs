using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Ultimate.IntegrationSystem.Api.Models
{ 
    public class EmployeeModel
    {
        [JsonProperty("employeeNumber")]
        [JsonPropertyName("employeeNumber")]

        public string EmployeeNumber;

        [JsonProperty("employeeName")]
        [JsonPropertyName("employeeName")]
        public string EmployeeName;

        [JsonProperty("employeeNameForeign")]
        [JsonPropertyName("employeeNameForeign")]
        public string EmployeeNameForeign;

        [JsonProperty("jobNo")]
        [JsonPropertyName("jobNo")]
        public string JobNo;

        [JsonProperty("hierarchyNumber")]
        [JsonPropertyName("hierarchyNumber")]
        public string HierarchyNumber;

        [JsonProperty("jobName")]
        [JsonPropertyName("jobName")]
        public string JobName;

        [JsonProperty("hierarchyName")]
        [JsonPropertyName("hierarchyName")]
        public string HierarchyName;

        [JsonProperty("telNo")]
        [JsonPropertyName("telNo")]
        public string TelNo;

        [JsonProperty("mobileNo")]
        [JsonPropertyName("mobileNo")]
        public string MobileNo;

        [JsonProperty("poBoxNo")]
        [JsonPropertyName("poBoxNo")]
        public string PoBoxNo;

        [JsonProperty("address")]
        [JsonPropertyName("address")]
        public string Address;

        [JsonProperty("website")]
        [JsonPropertyName("website")]
        public string Website;

        [JsonProperty("email")]
        [JsonPropertyName("email")]
        public string Email;

        [JsonProperty("inactive")]
        [JsonPropertyName("inactive")]
        public string Inactive;

        [JsonProperty("borderNumber")]
        [JsonPropertyName("borderNumber")]
        public string BorderNumber;

        [JsonProperty("birthCountry")]
        [JsonPropertyName("birthCountry")]
        public int BirthCountry;

        [JsonProperty("maritalStatus")]
        [JsonPropertyName("maritalStatus")]
        public int MaritalStatus;

        [JsonProperty("firstNameLocal")]
        [JsonPropertyName("firstNameLocal")]
        public string FirstNameLocal;

        [JsonProperty("firstNameForeign")]
        [JsonPropertyName("firstNameForeign")]
        public string FirstNameForeign;

        [JsonProperty("secondNameLocal")]
        [JsonPropertyName("secondNameLocal")]
        public string SecondNameLocal;

        [JsonProperty("secondNameForeign")]
        [JsonPropertyName("secondNameForeign")]
        public string SecondNameForeign;

        [JsonProperty("thirdNameLocal")]
        [JsonPropertyName("thirdNameLocal")]
        public string ThirdNameLocal;

        [JsonProperty("thirdNameForeign")]
        [JsonPropertyName("thirdNameForeign")]
        public string ThirdNameForeign;

        [JsonProperty("lastNameLocal")]
        [JsonPropertyName("lastNameLocal")]
        public string LastNameLocal;

        [JsonProperty("lastNameForeign")]
        [JsonPropertyName("lastNameForeign")]
        public string LastNameForeign;

        [JsonProperty("maritalStatusName")]
        [JsonPropertyName("maritalStatusName")]
        public string MaritalStatusName;

        [JsonProperty("birthCountryName")]
        [JsonPropertyName("birthCountryName")]
        public string BirthCountryName;

        [JsonProperty("cityName")]
        [JsonPropertyName("cityName")]
        public string CityName;
    }
}
