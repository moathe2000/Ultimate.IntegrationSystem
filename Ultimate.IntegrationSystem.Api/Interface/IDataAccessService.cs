
namespace Ultimate.IntegrationSystem.Api.Interface
{
    public interface IDataAccessService
    {
        Task<string> GetEmpDocsAsJson(decimal? P_EMP_NO = null, decimal? P_CODE_NO = null, decimal? P_DCMNT_TYP_NO = null, decimal? P_DOC_OWNR_TYP = null);
        Task<string> GetEmployeesAsJson(int? P_EMP_NO = null, int? P_EMP_NO_FROM = null, int? P_EMP_NO_TO = null, int? P_LNG_NO = 1); 
    }
}
