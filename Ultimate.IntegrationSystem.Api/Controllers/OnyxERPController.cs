using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Ultimate.IntegrationSystem.Api.Dto;
using Ultimate.IntegrationSystem.Api.Interface;
using Ultimate.IntegrationSystem.Api.Models;

namespace Ultimate.IntegrationSystem.Api.Controllers
{
    public class OnyxERPController : Controller
    {


        private readonly ILogger<OnyxERPController> _logger;
        private readonly IDataAccessSerivce _dataAccessSerivce;
        private readonly IDbModelMappingService _dbModelMapping;
        private readonly IMapper _mapper;
        public OnyxERPController(ILogger<OnyxERPController> logger, IDataAccessSerivce dataAccessSerivce, IDbModelMappingService dbModelMapping)
        {
            _logger = logger;
            _dataAccessSerivce = dataAccessSerivce;
            _dbModelMapping = dbModelMapping;
        }

     

 
/// <summary>
/// <see langword="get"/>
/// </summary>
/// <param name="para"></param>
/// <returns></returns>

        [HttpPost("GetEmployees")]
    public async Task<ApiResultModel> GetEmployees([FromBody] EmployeePara para)
    {
        try
        {
            // استدعاء الدالة التي تُرجع JSON
            var json = await _dataAccessSerivce.GetEmployeesAsJson(
                P_EMP_NO: para.P_EMP_NO,
                P_EMP_NO_FROM: para.P_EMP_NO_FROM,
                P_EMP_NO_TO: para.P_EMP_NO_TO,
                P_LNG_NO: para.P_LNG_NO
            );

            if (string.IsNullOrWhiteSpace(json))
                throw new Exception("Empty JSON response from GET_EMPLOYEE_JSON.");

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new ApiResultModel()
                    {
                        Code = 204, // No Content
                        Message = "لا توجد بيانات متاحة",
                        Content = null
                    };
                }

                //  تحليل JSON إلى كائن C# (بدلاً من XML)
                //var response = JsonConvert.DeserializeObject<ApiResultModel>(json);

                //// التحقق مما إذا كانت هناك مشكلة في البيانات

                var response = _dbModelMapping.GetDbResultModelFromJson(json);
                var result = _dbModelMapping.MapJson<EmployeeRecordModel>(response);
                // If Content is not already a string, convert or serialize it properly
                if (response.Result?.MsgNo != "004" && (response.Data == null))
                {
                    return new ApiResultModel()
                    {
                        Code = 0, // No Content
                        Message = "لم يتم العثور على أصناف مطابقة",
                        Content = null
                    };
                }

                // ✅ إعادة النتيجة كما هي بدون تحويل غير ضروري
                return new ApiResultModel()
                {
                    Code = 0,
                    Message = "",
                    Content = result // Content is now a string
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "حدث خطأ أثناء جلب بيانات ");

                return new ApiResultModel()
                {
                    Code = 500,
                    Message = $"حدث خطأ: {e.Message}"
                };
            }

           
        }


        [HttpPost("GetEmpDocs")]
        public async Task<ApiResultModel> GetEmpDocs([FromBody] EmpDocPara para)
        {
            try
            {
                // 1) استدعاء الدالة التي تُرجع JSON من أوراكل
                var json = await _dataAccessSerivce.GetEmpDocsAsJson(
                    P_EMP_NO: para.P_EMP_NO,
                    P_CODE_NO: para.P_CODE_NO,
                    P_DCMNT_TYP_NO: para.P_DCMNT_TYP_NO,
                    P_DOC_OWNR_TYP: para.P_DOC_OWNR_TYP
                );

                // 2) التحقق من الاستجابة
                if (string.IsNullOrWhiteSpace(json))
                {
                    return new ApiResultModel
                    {
                        Code = 204, // No Content
                        Message = "لا توجد بيانات متاحة",
                        Content = null
                    };
                }

                // 3) تحليل JSON إلى نموذجك الوسيط ثم إلى الموديل النهائي
                var response = _dbModelMapping.GetDbResultModelFromJson(json);

                // إن كانت الدالة المخزنة أعادت خطأ (MsgNo ≠ 004)
                if (response?.Result?.MsgNo != "004")
                {
                    return new ApiResultModel
                    {
                        Code = 500,
                        Message = response?.Result?.MsgTxt ?? "حدث خطأ غير معروف من الدالة GET_EMP_DOC_LIST_CUR.",
                        Content = null
                    };
                }

                // حوّل البيانات إلى الموديل النهائي (عدّل النوع حسب موديلك الفعلي)
                var result = _dbModelMapping.MapJson<EmpDocRecordModel>(response) ?? new List<EmpDocRecordModel>();

                // 4) لا توجد بيانات بعد التحويل
                if (result.Count == 0)
                {
                    return new ApiResultModel
                    {
                        Code = 204,
                        Message = "لا توجد بيانات متاحة",
                        Content = null
                    };
                }

                // 5) نجاح
                return new ApiResultModel
                {
                    Code = 0,
                    Message = "Success",
                    Content = result
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetEmpDocs failed");
                return new ApiResultModel
                {
                    Code = 500,
                    Message = $"حدث خطأ: {ex.Message}",
                    Content = null
                };
            }
        }

    }




}

