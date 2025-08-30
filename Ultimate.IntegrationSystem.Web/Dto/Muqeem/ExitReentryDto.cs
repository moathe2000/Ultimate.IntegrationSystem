namespace Ultimate.IntegrationSystem.Web.Dto.Muqeem
{
    public class ExitReentryDto
    {
        public DateTime? PassportExpiry { get; set; }
        public DateTime? IqamaExpiry { get; set; }
        public string? PdfPath { get; set; }
        public string? ActionBy { get; set; }
        public DateTime? ActionTime { get; set; }
        public string MuqeemStatus { get; set; } = "بانتظار الرد";
    }
}
