namespace Ultimate.IntegrationSystem.Web.Dto.Muqeem
{
    public class FinalExitDto
    {
        public string IqamaNumber { get; set; } = string.Empty;
        public string EmployeeNumber { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime? PassportExpiry { get; set; }
        public DateTime? IqamaExpiry { get; set; }
        public string? PdfPath { get; set; }
        public string? ActionBy { get; set; }
        public DateTime? ActionTime { get; set; }
        public string Status { get; set; } = "بانتظار الرد";
    }
}
