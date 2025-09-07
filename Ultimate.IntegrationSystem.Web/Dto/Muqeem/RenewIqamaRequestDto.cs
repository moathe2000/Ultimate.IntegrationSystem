using Newtonsoft.Json;

namespace Ultimate.IntegrationSystem.Web.Dto.Muqeem
{
    public class RenewIqamaRequestDto
    {
        public string IqamaDuration { get; set; } // "12" أو "24"

        public string IqamaNumber { get; set; }
        public string EmployeeNumber { get; set; }
        public string BorderNumber { get; set; }
    }
}
