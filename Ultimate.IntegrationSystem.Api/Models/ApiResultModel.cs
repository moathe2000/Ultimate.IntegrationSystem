namespace Ultimate.IntegrationSystem.Api.Models
{
    public class ApiResultModel
    {
        public int Code { get; set; }        // 0 = Success, >0 = Business Error, <0 = System Error
        public string Message { get; set; }  // وصف النتيجة
        public object Content { get; set; }  // الكائن الموحد (DTO/Array/FilePayload...)
        public string Kind { get; set; }     // نوع الاستجابة (IqamaRenewal, LookupList::Cities, ExitReentryIssued ...)
        public string Platform { get; set; } // المنصة (Muqeem, OnyxERP, ...)

        public ApiResultModel() { }

        public ApiResultModel(int code, string message, object content,
                              string kind = null, string platform = null)
        {
            Code = code;
            Message = message;
            Content = content;
            Kind = kind;
            Platform = platform;
        }
        public void Deconstruct(out bool success, out string kind, out object content)
        {
            success = (Code == 0);
            kind = Kind;
            content = Content;
        }
        public ApiResultModel(object content) : this(0, "Success", content) { }

        public bool IsSuccess => Code == 0;
    }
}

