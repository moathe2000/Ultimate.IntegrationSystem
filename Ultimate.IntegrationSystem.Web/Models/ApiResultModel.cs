using System.Text.Json;
using System.Text.Json.Serialization;

namespace Ultimate.IntegrationSystem.Web.Models
{
    public class ApiResultModel
    {
        [JsonPropertyName("code")] public int Code { get; set; }
        [JsonPropertyName("message")] public string Message { get; set; }
        [JsonPropertyName("content")] public object Content { get; set; } // مهم
    }


    public class ApiResultModel<T>
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;

        [JsonPropertyName("content")]
        public T? Content { get; set; }
    }
}
