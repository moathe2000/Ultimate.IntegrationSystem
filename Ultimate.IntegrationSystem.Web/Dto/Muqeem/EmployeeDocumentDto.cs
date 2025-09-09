namespace Ultimate.IntegrationSystem.Web.Dto.Muqeem
{
    public class EmployeeDocumentDto
    {
        public int? DocTypeNo { get; set; }            // 1..4
        public string? DocNo { get; set; }             // رقم الوثيقة
        public int? EmployeeNo { get; set; }
        public string? EmployeeName { get; set; }
        public string? ActionType { get; set; }        // إصدار/تجديد/تمديد...
        public string? OccupationAr { get; set; }      // المهنة (عربي)
        public string? OrganizationMOINumber { get; set; } // رقم المنشأة
        public string? NationalityAr { get; set; }
        public string? ReligionAr { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? ExpiryDateG { get; set; }     // ميلادي
        public string? ExpiryDateH { get; set; }       // هجري (نصي)
        public string? PdfPath { get; set; }
        public string? Status { get; set; }            // تم إتمامها/بانتظار/تم رفضها
        public string? ActionBy { get; set; }
        public DateTime? ActionTime { get; set; }
    }
}

