namespace Ultimate.IntegrationSystem.Web.Models
{
    public class ResidencyRow
    {
        public string IqamaNumber { get; set; } = "";
        public string EmployeeNumber { get; set; } = "";
        public string EmployeeName { get; set; } = "";
        public string ActionType { get; set; } = "-";
        public string IqamaProfession { get; set; } = "";
        public string IssueNumber { get; set; } = "-";
        public DateTime? IssueDate { get; set; }
    }

}
