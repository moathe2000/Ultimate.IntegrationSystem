using Ultimate.IntegrationSystem.Web.Models;

namespace Ultimate.IntegrationSystem.Web.Service
{
    public class EmployeeService
    {
        public List<Employee> GetAllEmployees()
        {
            return new List<Employee>
            {
                new Employee { EmployeeNumber = 1345, Name = "إيهاب أبو عبيد", JobTitle = "الرئيس التنفيذي", Department = "الإدارة العامة", Branch = "السليمانية", JoinDate = new DateTime(2024, 3, 1), Status = "نشط" },
                new Employee { EmployeeNumber = 1336, Name = "محمد يسري", JobTitle = "محلل أعمال", Department = "تطوير المحتوى", Branch = "العليا", JoinDate = new DateTime(2024, 2, 4), Status = "نشط" },
                new Employee { EmployeeNumber = 1332, Name = "أحمد صالح", JobTitle = "مدير قسم التمكين", Department = "قسم إدارة المحتوى", Branch = "RCU Remote", JoinDate = new DateTime(2023, 1, 1), Status = "نشط" },
                new Employee { EmployeeNumber = 1330, Name = "محمد السبيعي", JobTitle = "صانع محتوى رقمي", Department = "التسويق الرقمي", Branch = "الرياض", JoinDate = new DateTime(2022, 1, 1), Status = "غير نشط" },
                new Employee { EmployeeNumber = 1325, Name = "صالح سعيد", JobTitle = "أخصائي نجاح العملاء", Department = "قسم التصميم", Branch = "السليمانية", JoinDate = new DateTime(2023, 2, 1), Status = "نشط" }
            };
        }
    }
}
