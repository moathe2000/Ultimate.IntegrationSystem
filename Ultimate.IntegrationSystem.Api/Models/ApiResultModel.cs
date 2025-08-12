namespace Ultimate.IntegrationSystem.Api.Models
{
    public class ApiResultModel
    {
        public int Code { get; set; }
        public string Message { get; set; }
        public object Content { get; set; }


        public ApiResultModel()
        {

        }

        public ApiResultModel(int code, string message, object content)
        {
            Code = code;
            Message = message;
            Content = content;
        }

        public ApiResultModel(object content) : this(0, "Success", content)
        {
        }
    }
}
