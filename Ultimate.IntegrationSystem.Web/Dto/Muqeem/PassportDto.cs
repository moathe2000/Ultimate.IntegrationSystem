namespace Ultimate.IntegrationSystem.Web.Dto.Muqeem
{
    public class PassportDto
    {
        public string PassportNumber { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime? OldExpiry { get; set; }
        public DateTime? NewExpiry { get; set; }
        public string RequestType { get; set; } = string.Empty;
        public string? PdfPath { get; set; }
        public string? ActionBy { get; set; }
        public DateTime? ActionTime { get; set; }
        public string Status { get; set; } = "بانتظار الرد";
    }
}
