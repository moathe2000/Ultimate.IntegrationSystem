
using Ultimate.IntegrationSystem.Web.Dto;
using Ultimate.IntegrationSystem.Web.Dto.Muqeem;

namespace Ultimate.IntegrationSystem.Web.Map
{
    public static class MuqeemMapper
    {

        public static IssueIqamaRequestDto ToIssueIqama(EmployeeDto emp, int duration, string birthCountryCode, string maritalCode,
                                                      string firstName, string fatherName, string grandFatherName, string PssprtIssueCty,string BorderId = null)
        {
            return new IssueIqamaRequestDto
            {
                EmployeeNumber= emp.Id.ToString(),
                BorderNumber = BorderId,
                IqamaDuration = duration.ToString(),
                LkBirthCountry = emp.BirthCountry?.ToString(),
                MaritalStatus = emp.MaritalStatus?.ToString(),
                PassportIssueCity = PssprtIssueCty,
                TrFirstName = emp.FirstNameEn??emp.FirstName,
                TrFatherName = emp.MiddleNameEn??emp.MiddleName,
                TrGrandFatherName = emp.ThirdNameEn??emp.ThirdName,
                TrFamilyName = emp.LastNameEn??emp.LastName
            };
        }

        public static RenewIqamaRequestDto ToRenewIqama(EmployeeDto emp, int duration,string IqamaNumber)
        {
            return new RenewIqamaRequestDto
            {
                EmployeeNumber = emp.Id.ToString(),
                IqamaNumber = IqamaNumber,
                BorderNumber = emp.BorderId,
                IqamaDuration = duration.ToString()
            };
        }
    }
}
