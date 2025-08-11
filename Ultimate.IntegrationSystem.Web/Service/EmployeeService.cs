using Ultimate.IntegrationSystem.Web.Models;

namespace Ultimate.IntegrationSystem.Web.Service
{


    public class EmployeeService : IEmployeeService
    {
        private static readonly List<EmployeeDto> _employees = new()
        {
            new EmployeeDto { Id=Guid.NewGuid(), FullName="أحمد محمد ناجي  ل صالح", EmployeeNumber="EMP-001", JobTitle="مطور واجهات", Department="التطوير", IsActive=true,  HireDate=DateTime.Now.AddDays(-10)},
            new EmployeeDto { Id=Guid.NewGuid(), FullName="سارة البب علي",  EmployeeNumber="EMP-002", JobTitle="موارد بشرية", Department="الموارد البشرية", IsActive=true, HireDate=DateTime.Now.AddDays(-25)},
            new EmployeeDto { Id=Guid.NewGuid(), FullName="محمدففف 555 عادل", EmployeeNumber="EMP-003", JobTitle="مسؤول مبيعات", Department="المبيعات", IsActive=false, HireDate=DateTime.Now.AddMonths(-3)},
             new EmployeeDto { Id=Guid.NewGuid(), FullName="محمد عادل", EmployeeNumber="EMP-004", JobTitle="مسؤول مبيعات", Department="المبيعات", IsActive=false, HireDate=DateTime.Now.AddMonths(-3)},
          new EmployeeDto { Id=Guid.NewGuid(), FullName="محمد عادل", EmployeeNumber="EMP-005", JobTitle="مسؤول مبيعات", Department="المبيعات", IsActive=false, HireDate=DateTime.Now.AddMonths(-3)},
            new EmployeeDto { Id=Guid.NewGuid(), FullName="محمد عادل4", EmployeeNumber="EMP-006", JobTitle="مسؤول مبيعات", Department="المبيعات", IsActive=false, HireDate=DateTime.Now.AddMonths(-3)},
              new EmployeeDto { Id=Guid.NewGuid(), FullName="أحمد صالح", EmployeeNumber="EMP-007", JobTitle="مطور واجهات", Department="التطوير", IsActive=true,  HireDate=DateTime.Now.AddDays(-10)},
                new EmployeeDto { Id=Guid.NewGuid(), FullName="أحمد صالح", EmployeeNumber="EMP-008", JobTitle="مطور واجهات", Department="التطوير", IsActive=true,  HireDate=DateTime.Now.AddDays(-10)},
                  new EmployeeDto { Id=Guid.NewGuid(), FullName="أحمد صالح52", EmployeeNumber="EMP-009", JobTitle="مطور واجهات", Department="التطوير", IsActive=true,  HireDate=DateTime.Now.AddDays(-10)},
                    new EmployeeDto { Id=Guid.NewGuid(), FullName="أحمد صالح", EmployeeNumber="EMP-010", JobTitle="مطور واجهات", Department="التطوير", IsActive=true,  HireDate=DateTime.Now.AddDays(-10)},

        };

        public Task<List<EmployeeDto>> GetAllEmployeesAsync()
            => Task.FromResult(_employees.OrderBy(e => e.FullName).ToList());

        public Task AddEmployeeAsync(EmployeeDto employee)
        {
            employee.Id = _employees.Any() ? Guid.NewGuid(): Guid.NewGuid();
            _employees.Add(employee);
            return Task.CompletedTask;
        }

        public List<Employee> GetAllEmployees()
        {
            throw new NotImplementedException();
        }
    }
    }
    //    public List<Employee> GetAllEmployees() => new()
    //    {
    //        new Employee{ EmployeeNumber=1501, Name="أحمد العتيبي",     JobTitle="محلل أعمال",          Department="تطوير الأعمال",     Branch="الرياض",   JoinDate=new(2024,3,1),  Status="Active" },
    //        new Employee{ EmployeeNumber=1502, Name=" محمد السبيعي",    JobTitle="مطور برمجيات",        Department="تقنية المعلومات",    Branch="جدة",      JoinDate=new(2023,12,10), Status="Active" },
    //        new Employee{ EmployeeNumber=1503, Name="خالد الغامدي",     JobTitle="أخصائي موارد بشرية",  Department="الموارد البشرية",    Branch="الدمام",   JoinDate=new(2022,6,20),  Status="Inactive" },
    //        new Employee{ EmployeeNumber=1504, Name="سارة القحطاني",    JobTitle="مصممة واجهات",        Department="تقنية المعلومات",    Branch="الرياض",   JoinDate=new(2024,1,5),  Status="Active" },
    //        new Employee{ EmployeeNumber=1505, Name="نورة الشهري",      JobTitle="محاسبة",              Department="المالية",           Branch="الخبر",     JoinDate=new(2021,11,2),  Status="Active" },
    //        new Employee{ EmployeeNumber=1506, Name="عبدالله المطيري",  JobTitle="مدير مشروع",          Department="التطوير",           Branch="المدينة",  JoinDate=new(2023,3,15),  Status="Inactive" },
    //        new Employee{ EmployeeNumber=1507, Name="فهد العبدالله",    JobTitle="مهندس شبكات",         Department="تقنية المعلومات",    Branch="الرياض",   JoinDate=new(2022,9,8),   Status="Active" },
    //        new Employee{ EmployeeNumber=1508, Name="ريم العنزي",       JobTitle="أخصائية تسويق",       Department="التسويق الرقمي",     Branch="جدة",      JoinDate=new(2024,2,1),  Status="Active" },
    //        new Employee{ EmployeeNumber=1509, Name="ليان الحربي",      JobTitle="كاتبة محتوى",         Department="التسويق الرقمي",     Branch="الطائف",   JoinDate=new(2022,1,25),  Status="Inactive" },
    //        new Employee{ EmployeeNumber=1510, Name="مشعل الزهراني",    JobTitle="أخصائي دعم فني",      Department="الدعم الفني",        Branch="تبوك",     JoinDate=new(2023,5,3),   Status="Active" },

//        new Employee{ EmployeeNumber=1511, Name="تركي الدوسري",     JobTitle="محلل نظم",            Department="تقنية المعلومات",    Branch="الرياض",   JoinDate=new(2021,8,19),  Status="Active" },
//        new Employee{ EmployeeNumber=1512, Name="جود القحطاني",     JobTitle="مسؤولة مشتريات",      Department="المشتريات",          Branch="الدمام",   JoinDate=new(2023,10,2),  Status="Inactive" },
//        new Employee{ EmployeeNumber=1513, Name="راكان الحارثي",    JobTitle="مدير منتج",           Department="التطوير",           Branch="جدة",      JoinDate=new(2022,7,14),  Status="Active" },
//        new Employee{ EmployeeNumber=1514, Name="نواف البلوي",      JobTitle="مدقق جودة",           Department="الجودة",            Branch="حائل",     JoinDate=new(2024,4,10),  Status="Active" },
//        new Employee{ EmployeeNumber=1515, Name="مها السالم",       JobTitle="أخصائية خدمة عملاء",  Department="خدمة العملاء",       Branch="الرياض",   JoinDate=new(2021,12,28), Status="Inactive" },
//        new Employee{ EmployeeNumber=1516, Name="شهد العوفي",       JobTitle="مصممة جرافيك",        Department="التسويق الرقمي",     Branch="القصيم",   JoinDate=new(2023,2,6),   Status="Active" },
//        new Employee{ EmployeeNumber=1517, Name="عبدالرحمن السبيعي",JobTitle="محاسب",              Department="المالية",           Branch="المدينة",  JoinDate=new(2022,10,1),  Status="Active" },
//        new Employee{ EmployeeNumber=1518, Name="هناء العنزي",      JobTitle="أخصائية توظيف",       Department="الموارد البشرية",    Branch="جدة",      JoinDate=new(2023,9,21),  Status="Inactive" },
//        new Employee{ EmployeeNumber=1519, Name="زياد باوزير",      JobTitle="مطور واجهات",         Department="تقنية المعلومات",    Branch="جيزان",    JoinDate=new(2022,3,11),  Status="Active" },
//        new Employee{ EmployeeNumber=1520, Name="وليد الشهراني",    JobTitle="محلل بيانات",         Department="البيانات والتحليلات", Branch="نجران",    JoinDate=new(2024,1,18),  Status="Active" },

//        new Employee{ EmployeeNumber=1521, Name="يوسف القحطاني",    JobTitle="مهندس نظم",           Department="تقنية المعلومات",    Branch="الرياض",   JoinDate=new(2021,5,30),  Status="Inactive" },
//        new Employee{ EmployeeNumber=1522, Name="رهف اليامي",       JobTitle="أخصائية تسويق",       Department="التسويق الرقمي",     Branch="أبها",     JoinDate=new(2023,6,7),   Status="Active" },
//        new Employee{ EmployeeNumber=1523, Name="نبيل المطيري",     JobTitle="مدير فرع",            Department="العمليات",           Branch="مكة",      JoinDate=new(2022,12,3),  Status="Active" },
//        new Employee{ EmployeeNumber=1524, Name="جاسم الغامدي",     JobTitle="أخصائي رواتب",        Department="المالية",           Branch="الرياض",   JoinDate=new(2024,2,20),  Status="Inactive" },
//        new Employee{ EmployeeNumber=1525, Name="أحمد عسيري",       JobTitle="أخصائي أمن معلومات",  Department="الأمن السيبراني",    Branch="جدة",      JoinDate=new(2022,5,4),   Status="Active" },
//        new Employee{ EmployeeNumber=1526, Name="فيصل الفيفي",      JobTitle="مسؤول مخزون",         Department="سلاسل الإمداد",      Branch="الدمام",   JoinDate=new(2023,11,11), Status="Active" },
//        new Employee{ EmployeeNumber=1527, Name="مروان السهلي",     JobTitle="مدير حسابات",         Department="المبيعات",           Branch="الرياض",   JoinDate=new(2021,9,9),   Status="Inactive" },
//        new Employee{ EmployeeNumber=1528, Name="فرح الراجحي",      JobTitle="كاتبة محتوى",         Department="التسويق الرقمي",     Branch="المدينة",  JoinDate=new(2024,3,5),   Status="Active" },
//        new Employee{ EmployeeNumber=1529, Name="إياد الحسون",      JobTitle="أخصائي جودة",         Department="الجودة",            Branch="ينبع",     JoinDate=new(2022,8,22),  Status="Active" },
//        new Employee{ EmployeeNumber=1530, Name="ولاء السعيد",      JobTitle="منسقة مشاريع",        Department="التطوير",           Branch="الرياض",   JoinDate=new(2023,1,13),  Status="Inactive" },

//        new Employee{ EmployeeNumber=1531, Name="طارق الزيدان",     JobTitle="مطور باك إند",        Department="تقنية المعلومات",    Branch="جدة",      JoinDate=new(2022,4,16),  Status="Active" },
//        new Employee{ EmployeeNumber=1532, Name="لمياء الهذلي",     JobTitle="أخصائية عقود",        Department="الشؤون القانونية",  Branch="الرياض",   JoinDate=new(2023,7,27),  Status="Active" },
//        new Employee{ EmployeeNumber=1533, Name="نادر القرني",      JobTitle="مهندس بنية تحتية",    Department="تقنية المعلومات",    Branch="الدمام",   JoinDate=new(2021,10,6),  Status="Inactive" },
//        new Employee{ EmployeeNumber=1534, Name="غادة الغامدي",     JobTitle="مديرة منتج",          Department="التطوير",           Branch="الرياض",   JoinDate=new(2024,1,30),  Status="Active" },
//        new Employee{ EmployeeNumber=1535, Name="حسام البقمي",      JobTitle="محلل مالي",           Department="المالية",           Branch="جدة",      JoinDate=new(2022,2,18),  Status="Active" },
//        new Employee{ EmployeeNumber=1536, Name="ملاك الدوسري",     JobTitle="أخصائية تدريب",       Department="الموارد البشرية",    Branch="الأحساء",  JoinDate=new(2023,9,9),   Status="Inactive" },
//        new Employee{ EmployeeNumber=1537, Name="إبراهيم العسيري",  JobTitle="مسؤول مبيعات",        Department="المبيعات",           Branch="الطائف",   JoinDate=new(2022,6,2),   Status="Active" },
//        new Employee{ EmployeeNumber=1538, Name="جمانة القحطاني",   JobTitle="محللة بيانات",        Department="البيانات والتحليلات", Branch="الرياض",   JoinDate=new(2023,3,28),  Status="Active" },
//        new Employee{ EmployeeNumber=1539, Name="رائد السلمي",      JobTitle="مبرمج تكاملات",       Department="تقنية المعلومات",    Branch="المدينة",  JoinDate=new(2021,4,14),  Status="Inactive" },
//        new Employee{ EmployeeNumber=1540, Name="هند اليوسف",       JobTitle="أخصائية تجربة عميل",  Department="خدمة العملاء",       Branch="جدة",      JoinDate=new(2024,4,2),   Status="Active" },

//        new Employee{ EmployeeNumber=1541, Name="عبدالمجيد الحربي", JobTitle="مشرف لوجستي",         Department="سلاسل الإمداد",      Branch="الرياض",   JoinDate=new(2021,6,12),  Status="Active" },
//        new Employee{ EmployeeNumber=1542, Name="نواف الشمري",      JobTitle="اختبار برمجيات",       Department="الجودة",            Branch="تبوك",     JoinDate=new(2023,8,23),  Status="Inactive" },
//        new Employee{ EmployeeNumber=1543, Name="ثامر العنزي",      JobTitle="محلل عمليات",         Department="العمليات",           Branch="الرياض",   JoinDate=new(2022,9,30),  Status="Active" },
//        new Employee{ EmployeeNumber=1544, Name="علي العامري",      JobTitle="مطور تطبيقات",        Department="تقنية المعلومات",    Branch="نجران",    JoinDate=new(2023,11,5),  Status="Active" },
//        new Employee{ EmployeeNumber=1545, Name="سلمان القحطاني",   JobTitle="مدير حسابات رئيسية",  Department="المبيعات",           Branch="الخبر",     JoinDate=new(2022,1,9),   Status="Inactive" },
//        new Employee{ EmployeeNumber=1546, Name="بدور السويلم",     JobTitle="مسؤولة علاقات عامة",  Department="التسويق",            Branch="الرياض",   JoinDate=new(2024,2,14),  Status="Active" },
//        new Employee{ EmployeeNumber=1547, Name="جهاد الحسين",      JobTitle="أخصائي مخاطر",        Department="المالية",           Branch="جدة",      JoinDate=new(2021,3,21),  Status="Active" },
//        new Employee{ EmployeeNumber=1548, Name="رهف الخالدي",      JobTitle="كاتبة تقارير",        Department="البيانات والتحليلات", Branch="الدمام",   JoinDate=new(2023,5,19),  Status="Inactive" },
//        new Employee{ EmployeeNumber=1549, Name="عبدالإله الغنام",  JobTitle="مهندس أمن",           Department="الأمن السيبراني",    Branch="الرياض",   JoinDate=new(2022,7,7),   Status="Active" },
//        new Employee{ EmployeeNumber=1550, Name="ليلى باحمدان",     JobTitle="منسقة فعاليات",       Department="التسويق",            Branch="جدة",      JoinDate=new(2024,3,22),  Status="Active" },
//    };
//}
//}//
