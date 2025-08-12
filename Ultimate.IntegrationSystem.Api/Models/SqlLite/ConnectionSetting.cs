using System.ComponentModel.DataAnnotations;

namespace Ultimate.IntegrationSystem.Api.Models.SqlLite
{
    public class ConnectionSetting
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "system requer ")]
        public string SelectedSystem { set; get; }
        public string SchemaName { get; set; }

        public string Password { get; set; }

        [Required(ErrorMessage = "السنة مطلوبة")]

        public string Year { get; set; }
        [Required(ErrorMessage = "حالة النشاط مطلوبة")]
        public int Activity { get; set; } = 1;

        [Required(ErrorMessage = "عنوان السيرفر مطلوب")]
        [RegularExpression(@"^[a-zA-Z0-9_\-\.]+$", ErrorMessage = "صيغة اسم السيرفر غير صحيحة")]
        public string Host { get; set; }


        [Required(ErrorMessage = "رقم المنفذ مطلوب")]
        [Range(1, 65535, ErrorMessage = "المنفذ يجب أن يكون بين 1 و 65535")]
        public int Port { get; set; }

        [Required(ErrorMessage = "اسم الخدمة مطلوب")]
        [RegularExpression(@"^[A-Za-z0-9_\.]+$", ErrorMessage = "اسم الخدمة يحتوي على رموز غير صالحة")]
        public string ServiceName { get; set; }

    }
}
