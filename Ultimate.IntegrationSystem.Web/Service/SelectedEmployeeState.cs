using Ultimate.IntegrationSystem.Web.Dto;

namespace Ultimate.IntegrationSystem.Web.Service
{
    public class SelectedEmployeeState
    {
        private EmployeeDto? _employee;

        public bool HasValue => _employee != null;
        public EmployeeDto? Current => _employee;   // هذه التي كانت ناقصة

        public void Set(EmployeeDto employee) => _employee = employee;
        public void Clear() => _employee = null;
    }
    //public class SelectedEmployeeState
    //{
    //    public EmployeeDto? Selected { get; private set; }
    //    public void Set(EmployeeDto employee) => Selected = employee;
    //    public void Clear() => Selected = null;
    //}
}
