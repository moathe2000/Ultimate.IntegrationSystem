
namespace Ultimate.IntegrationSystem.Api.Interface
{
    public interface IDataAccessService
    {
        Task<string> GetDocsAndRnwlAsJson(decimal? P_EMP_NO = null, decimal? P_CODE_NO = null, decimal? P_DCMNT_TYP_NO = null, string P_DOC_NO = null, decimal? P_SUB_CODE_NO = null, int? P_ONLY_ACTIVE = 1, decimal? P_RNWL_DOC_TYP = 810, decimal? P_RNWL_DOC_NO = null, decimal? P_RNWL_DOC_SRL = null);
        Task<string> GetEmpDocsAsJson(decimal? P_EMP_NO = null, decimal? P_CODE_NO = null, decimal? P_DCMNT_TYP_NO = null, decimal? P_DOC_OWNR_TYP = null);
        Task<string> GetEmployeesAsJson(int? P_EMP_NO = null, int? P_EMP_NO_FROM = null, int? P_EMP_NO_TO = null, int? P_LNG_NO = 1); 
    }
}
