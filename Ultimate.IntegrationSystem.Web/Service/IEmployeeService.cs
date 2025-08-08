using Ultimate.IntegrationSystem.Web.Models;

namespace Ultimate.IntegrationSystem.Web.Service
{
    public interface IEmployeeService
    {
        List<Employee> GetAllEmployees();
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
    }
    
}
