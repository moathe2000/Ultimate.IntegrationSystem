using System.Text.Json.Serialization;

namespace Ultimate.IntegrationSystem.Web.Models
{
    public class EmployeeDto
    {
       
        public string EmployeeNumber { get; set; }
        public string FullName { get; set; }
        public string JobTitle { get; set; }
        public string Department { get; set; } 
        public int? Inactive { get; set; }
        public DateTime HireDate { get; set; }



   
        public string EmployeeName { get; set; }
        public string FirstName { get; set; }
        public string JobNo { get; set; }
        public string HrchyNo { get; set; }
        public string JobName { get; set; }
        public string HrchyName { get; set; }
        public string TelNo { get; set; }
        public string MobileNo { get; set; }
        public string PoBoxNo { get; set; }
        public string Address { get; set; }
        public string Website { get; set; }
        public string Email { get; set; }


        public string Id { get; set; }



        // --- الحقول الإضافية من s_emp ---
   
        public string BorderNumber { get; set; }


        public int? BirthCountry { get; set; }
    public int? MaritalStatus { get; set; }


        public string FamilyName { get; set; }

 
        public string FatherName { get; set; }


        public string GivenName { get; set; }


        public string GrandFatherName { get; set; }

        // أسماء مفهومة من الجداول المرجعية

        public string MaritalStatusName { get; set; }


        public string BirthCountryName { get; set; }

        // إذا لاحقاً أضفت المدينة

        public string CityName { get; set; }



    }
}

