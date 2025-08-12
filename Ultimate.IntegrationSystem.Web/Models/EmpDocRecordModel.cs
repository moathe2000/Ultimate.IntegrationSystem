using System.Text.Json.Serialization;

namespace Ultimate.IntegrationSystem.Web.Models
{
    public class EmpDocRecordModel
    {
     [JsonPropertyName("codeNo")] public int? CodeNo { get; set; }
    [JsonPropertyName("subCodeNo")] public int? SubCodeNo { get; set; }
    [JsonPropertyName("subCodeNameAR")] public string SubCodeNameAR { get; set; }
    [JsonPropertyName("subCodeNameEN")] public string SubCodeNameEN { get; set; }
    [JsonPropertyName("docNo")] public string DocNo { get; set; }
    [JsonPropertyName("empNo")] public long? EmpNo { get; set; }
    [JsonPropertyName("issueDate")] public string IssueDate { get; set; }
    [JsonPropertyName("expiryDate")] public string ExpiryDate { get; set; }
    [JsonPropertyName("renewalDate")] public string RenewalDate { get; set; }
    [JsonPropertyName("issuePlace")] public string IssuePlace { get; set; }
    [JsonPropertyName("ownerType")] public int? OwnerType { get; set; }
    [JsonPropertyName("dependentNo")] public int? DependentNo { get; set; }
    [JsonPropertyName("docTypeNo")] public int? DocTypeNo { get; set; }
}
}
