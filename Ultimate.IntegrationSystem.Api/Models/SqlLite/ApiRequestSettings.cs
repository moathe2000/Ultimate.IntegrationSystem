using System.ComponentModel.DataAnnotations;

namespace Ultimate.IntegrationSystem.Api.Models.SqlLite
{
    public class ApiRequestSettings
    {
        [Key]
        public int Id { get; set; }

        public string Endpoint { get; set; }  // URL للنهاية (إما GET أو POST)
        public string HttpMethod { get; set; }  // GET, POST, PUT, DELETE (طريقة الطلب)

        // رؤوس الـ HTTP مثل { "Authorization": "Bearer token" }
        public string Headers { get; set; }

        public string Parametr { get; set; }

        // جسم الـ HTTP (إذا كان يوجد مثل البيانات المرسلة في POST أو PUT)
        public string BodyTemplate { get; set; }

        // شرح مختصر للإعداد
        public string Description { get; set; }

        // المفتاح الذي يميز إعدادات API (API Key)
        public string ApiKey { get; set; }

        // تنسيق البيانات (مثل JSON أو Form)
        public string BodyFormat { get; set; }

        // تاريخ إنشاء الإعدادات
        public string CreatedAt { get; set; }
    }
}
