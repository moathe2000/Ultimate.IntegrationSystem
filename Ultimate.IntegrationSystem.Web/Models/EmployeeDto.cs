namespace Ultimate.IntegrationSystem.Web.Models
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }
        public string EmployeeNumber { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; }
        public bool IsActive { get; set; }
        public DateTime HireDate { get; set; }
    }
}
