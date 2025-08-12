using Ultimate.IntegrationSystem.Web.Models;

namespace Ultimate.IntegrationSystem.Web.Service
{
    public interface IEmployeeService
    {
       
       
        Task<List<EmployeeDto>> GetAllEmployeesAsync(EmployeePara para = null, CancellationToken ct = default);

        
    }
    
}
