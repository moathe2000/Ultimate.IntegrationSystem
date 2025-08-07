namespace Ultimate.IntegrationSystem.Web.Models
{
    public class ExitReentryRow
    {
        public DateTime? PassportExpiry { get; set; }
        public DateTime? IqamaExpiry { get; set; }
        public string? PdfPath { get; set; }
        public string? ActionBy { get; set; }
        public DateTime? ActionTime { get; set; }
        public string MuqeemStatus { get; set; } = "بانتظار الرد"; // تم إتمامها / تم رفضها / بانتظار الرد
    }

    public class FinalExitRow
    {
        public string? IqamaNumber { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? PassportExpiry { get; set; }
        public DateTime? IqamaExpiry { get; set; }
        public string? PdfPath { get; set; }
        public string? ActionBy { get; set; }
        public DateTime? ActionTime { get; set; }
        public string? Status { get; set; } // تم إتمامها / تم رفضها / بانتظار الرد
    }

    public class PassportRow
    {
        public string? PassportNumber { get; set; }
        public string? EmployeeName { get; set; }
        public DateTime? OldExpiry { get; set; }
        public DateTime? NewExpiry { get; set; }
        public string? RequestType { get; set; } // تجديد / تمديد ...
        public string? PdfPath { get; set; }
        public string? ActionBy { get; set; }
        public DateTime? ActionTime { get; set; }
        public string? Status { get; set; }
    }
}
