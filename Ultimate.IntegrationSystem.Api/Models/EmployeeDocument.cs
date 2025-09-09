using Newtonsoft.Json;

namespace Ultimate.IntegrationSystem.Api.Models
{
    public class EmployeeDocument
    {
        [JsonProperty("empNo")]
        public int EmployeeNumber { get; set; }

        [JsonProperty("codeNo")]
        public int CodeNumber { get; set; }

        [JsonProperty("subCodeNo")]
        public int SubCodeNumber { get; set; }

        [JsonProperty("subCodeNameAR")]
        public string SubCodeNameArabic { get; set; }

        [JsonProperty("subCodeNameEN")]
        public string SubCodeNameEnglish { get; set; }

        [JsonProperty("docNo")]
        public string DocumentNumber { get; set; }

        [JsonProperty("issueDate")]
        public DateTime? IssueDate { get; set; }

        [JsonProperty("expiryDate")]
        public DateTime? ExpiryDate { get; set; }

        [JsonProperty("renewalDate")]
        public DateTime? RenewalDate { get; set; }

        [JsonProperty("issueDateHijri")]
        public string IssueDateHijri { get; set; }

        [JsonProperty("expiryDateHijri")]
        public string ExpiryDateHijri { get; set; }

        [JsonProperty("renewalDateHijri")]
        public string RenewalDateHijri { get; set; }

        [JsonProperty("issuePlace")]
        public string IssuePlace { get; set; }

        [JsonProperty("ownerType")]
        public int OwnerType { get; set; }

        [JsonProperty("dependentNo")]
        public int DependentNumber { get; set; }

        [JsonProperty("docTypeNo")]
        public int DocumentTypeNumber { get; set; }

        [JsonProperty("cityNo")]
        public int CityNumber { get; set; }

        [JsonProperty("isDefault")]
        public bool IsDefault { get; set; }

        [JsonProperty("nonRenewable")]
        public bool IsNonRenewable { get; set; }

        [JsonProperty("syncState")]
        public int SyncState { get; set; }

        [JsonProperty("inactive")]
        public bool IsInactive { get; set; }











       




    }

}
