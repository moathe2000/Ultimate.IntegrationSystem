using Ultimate.IntegrationSystem.Web.Models;

namespace Ultimate.IntegrationSystem.Web.Service
{
   
        public class EmployeeService
        {
            public List<Employee> GetAllEmployees() => new()
    {
        new Employee{ EmployeeNumber=1345, Name="إيهاب أبو عبيد", JobTitle="الرئيس التنفيذي", Department="الإدارة العامة", Branch="السلطانية", JoinDate=new(2024,3,1), Status="Active" },
        new Employee{ EmployeeNumber=1336, Name="محمد يسري", JobTitle="محلل أعمال", Department="تطوير المحتوى", Branch="العلّا", JoinDate=new(2024,2,4), Status="Active" },
        new Employee{ EmployeeNumber=1332, Name="أحمد صالح", JobTitle="مدير قسم التمكين", Department="الشؤون الإدارية", Branch="RCU Remote", JoinDate=new(2023,1,1), Status="Inactive" },
        new Employee{ EmployeeNumber=1330, Name="محمد العيسي", JobTitle="صانع محتوى رقمي", Department="التسويق الرقمي", Branch="الرياض", JoinDate=new(2022,1,1), Status="Active" },
        new Employee{ EmployeeNumber=1325, Name="صالح سعيد", JobTitle="أخصائي نجاح العملاء", Department="قسم التصميم", Branch="السلطانية", JoinDate=new(2023,2,1), Status="Inactive" },
    };
        }
    }
