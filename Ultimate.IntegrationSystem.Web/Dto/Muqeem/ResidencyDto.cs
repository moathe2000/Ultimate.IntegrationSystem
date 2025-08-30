namespace Ultimate.IntegrationSystem.Web.Dto.Muqeem
{
    public class ResidencyDto
    {
        public string IqamaNumber { get; set; } = string.Empty;
        public string EmployeeNumber { get; set; } = string.Empty;
        public string EmployeeName { get; set; } = string.Empty;
        public string ActionType { get; set; } = string.Empty;
        public string IqamaProfession { get; set; } = string.Empty;
        public string IssueNumber { get; set; } = string.Empty;
        public DateTime? IssueDate { get; set; }
    }
}
