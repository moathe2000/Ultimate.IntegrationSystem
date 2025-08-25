
using Ultimate.IntegrationSystem.Api.Dto.Muqeem.Requests;
using Ultimate.IntegrationSystem.Web.Models;

namespace Ultimate.IntegrationSystem.Web.Map
{
    public static class MuqeemMapper
    {

        public static IssueIqamaRequestDto ToIssueIqama(EmployeeDto emp, int duration, string birthCountryCode, string maritalCode,
                                                      string firstName, string fatherName, string grandFatherName, string PssprtIssueCty)
        {
            return new IssueIqamaRequestDto
            {
                EmployeeNumber= emp.EmployeeNumber,
                BorderNumber = emp.BorderNumber,
                IqamaDuration = duration.ToString(),
                LkBirthCountry = emp.BirthCountry?.ToString(),
                MaritalStatus = emp.MaritalStatus?.ToString(),
                PassportIssueCity = PssprtIssueCty,
                TrFirstName = emp.GivenName,
                TrFatherName = emp.FatherName,
                TrGrandFatherName = emp.GrandFatherName,
                TrFamilyName = emp.FamilyName
            };
        }

        public static RenewIqamaRequestDto ToRenewIqama(EmployeeDto emp, int duration)
        {
            return new RenewIqamaRequestDto
            {
                EmployeeNumber = emp.EmployeeNumber,
                IqamaNumber = emp.IqamaNumber,
                BorderNumber = emp.BorderNumber,
                IqamaDuration = duration.ToString()
            };
        }
    }
}
