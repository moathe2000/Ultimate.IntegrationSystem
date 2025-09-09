namespace Ultimate.IntegrationSystem.Api.Dto.Muqeem.Responses
{
    public class EmployeeDocumentDto
    {
        public int EmployeeNo { get; set; }
        public string DocNo { get; set; }
        public int DocTypeNo { get; set; }

        /// <summary>اسم نوع الوثيقة المقروء (إقامة، جواز سفر، خروج وعودة، خروج نهائي...)</summary>
        public string DocType { get; set; }

        /// <summary>التصنيف/الفرعي (يُفضّل العربي ثم الإنجليزي، وإلا Fallback على الكود)</summary>
        public string SubType { get; set; }

        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? RenewalDate { get; set; }

        /// <summary>تواريخ هجري بتوحيد الفاصل /</summary>
        public string IssueDateHijri { get; set; }
        public string ExpiryDateHijri { get; set; }
        public string RenewalDateHijri { get; set; }

        public string IssuePlace { get; set; }

        public bool IsDefault { get; set; }
        public bool IsInactive { get; set; }

        /// <summary>هل منتهي؟ يعتمد على ExpiryDate مقابل تاريخ اليوم</summary>
        public bool IsExpired { get; set; }

        /// <summary>أيام متبقية للانتهاء (قد تكون سالبة لو منتهي، أو null لو لا يوجد تاريخ)</summary>
        public int? DaysToExpire { get; set; }
    

    }

}
