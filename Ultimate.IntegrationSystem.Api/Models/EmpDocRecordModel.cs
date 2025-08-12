using System.Text.Json.Serialization;

namespace Ultimate.IntegrationSystem.Api.Models
{
    public class EmpDocRecordModel
    {
        [JsonPropertyName("codeNo")] public long? CodeNo { get; set; }
        [JsonPropertyName("subCodeNo")] public long? SubCodeNo { get; set; }
        [JsonPropertyName("subCodeNameAR")] public string SubCodeNameAR { get; set; }
        [JsonPropertyName("subCodeNameEN")] public string SubCodeNameEN { get; set; }
        [JsonPropertyName("docNo")] public string DocNo { get; set; }
        [JsonPropertyName("empNo")] public long? EmpNo { get; set; }
        [JsonPropertyName("issueDate")] public string IssueDate { get; set; }   // "YYYY-MM-DD"
        [JsonPropertyName("expiryDate")] public string ExpiryDate { get; set; }
        [JsonPropertyName("renewalDate")] public string RenewalDate { get; set; }
        [JsonPropertyName("issuePlace")] public string IssuePlace { get; set; }
        [JsonPropertyName("ownerType")] public long? OwnerType { get; set; }
        [JsonPropertyName("dependentNo")] public long? DependentNo { get; set; }
        [JsonPropertyName("docTypeNo")] public long? DocTypeNo { get; set; }
    }
}
