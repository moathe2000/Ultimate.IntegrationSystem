using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Ultimate.IntegrationSystem.Api.Dto;
using Ultimate.IntegrationSystem.Api.Dto.Muqeem.Responses;
using Ultimate.IntegrationSystem.Api.Interface;
using Ultimate.IntegrationSystem.Api.Models;

namespace Ultimate.IntegrationSystem.Api.Controllers
{
    public class OnyxERPController : Controller
    {


        private readonly ILogger<OnyxERPController> _logger;
        private readonly IDataAccessService _dataAccessSerivce;
        private readonly IDbModelMappingService _dbModelMapping;
        private readonly IMapper _mapper;
        public OnyxERPController(ILogger<OnyxERPController> logger, IDataAccessService dataAccessSerivce, IDbModelMappingService dbModelMapping, IMapper mapper)
        {
            _logger = logger;
            _dataAccessSerivce = dataAccessSerivce;
            _dbModelMapping = dbModelMapping;
            _mapper = mapper;
        }




        /// <summary>
        /// 
        /// </summary>
        /// <param name="para"></param>
        /// <returns></returns>

        [HttpPost("GetEmployees")]
 
        public async Task<ApiResultModel> GetEmployees([FromBody] EmployeePara para)
        {
            try
            {
                var json = await _dataAccessSerivce.GetEmployeesAsJson(
                    P_EMP_NO: para.P_EMP_NO,
                    P_EMP_NO_FROM: para.P_EMP_NO_FROM,
                    P_EMP_NO_TO: para.P_EMP_NO_TO,
                    P_LNG_NO: para.P_LNG_NO
                );

                if (string.IsNullOrWhiteSpace(json))
                {
                    return new ApiResultModel
                    {
                        Code = 204,
                        Message = "لا توجد بيانات متاحة",
                        Content = null
                    };
                }

                var response = _dbModelMapping.GetDbResultModelFromJson(json);
                var result = _dbModelMapping.MapJson<EmployeeModel>(response);

                if (response.Result?.MsgNo != "004" || result == null)
                {
                    return new ApiResultModel
                    {
                        Code = 204,
                        Message = "لم يتم العثور على موظفين مطابقين",
                        Content = null
                    };
                }

                // ✅ التحويل إلى DTO باستخدام AutoMapper
                var employees = _mapper.Map<EmployeeDto[]>(result);

                return new ApiResultModel
                {
                    Code = 0,
                    Message = "تم جلب البيانات بنجاح",
                    Content = employees
                };
            }
            catch (Exception e)
            {
                _logger.LogError(e, "حدث خطأ أثناء جلب بيانات الموظفين");

                return new ApiResultModel
                {
                    Code = 500,
                    Message = $"حدث خطأ: {e.Message}",
                    Content = null
                };
            }
        }


        //public async Task<ApiResultModel> GetEmployees([FromBody] EmployeePara para)
        //{
        //    try
        //    {
        //        // استدعاء الدالة التي تُرجع JSON
        //        var json = await _dataAccessSerivce.GetEmployeesAsJson(
        //            P_EMP_NO: para.P_EMP_NO,
        //            P_EMP_NO_FROM: para.P_EMP_NO_FROM,
        //            P_EMP_NO_TO: para.P_EMP_NO_TO,
        //            P_LNG_NO: para.P_LNG_NO
        //        );

        //        if (string.IsNullOrWhiteSpace(json))
        //            throw new Exception("Empty JSON response from GET_EMPLOYEE_JSON.");

        //            if (string.IsNullOrWhiteSpace(json))
        //            {
        //                return new ApiResultModel()
        //                {
        //                    Code = 204, // No Content
        //                    Message = "لا توجد بيانات متاحة",
        //                    Content = null
        //                };
        //            }

        //            //  تحليل JSON إلى كائن C# (بدلاً من XML)
        //            //var response = JsonConvert.DeserializeObject<ApiResultModel>(json);

        //            //// التحقق مما إذا كانت هناك مشكلة في البيانات

        //            var response = _dbModelMapping.GetDbResultModelFromJson(json);
        //            var result = _dbModelMapping.MapJson<EmployeeModel>(response);
        //            // If Content is not already a string, convert or serialize it properly
        //            if (response.Result?.MsgNo != "004" && (response.Data == null))
        //            {
        //                return new ApiResultModel()
        //                {
        //                    Code = 0, // No Content
        //                    Message = "لم يتم العثور على بيانات  مطابقة",
        //                    Content = null
        //                };
        //            }

        //            // ✅ إعادة النتيجة كما هي بدون تحويل غير ضروري
        //            return new ApiResultModel()
        //            {
        //                Code = 0,
        //                Message = "",
        //                Content = result // Content is now a string
        //            };
        //        }
        //        catch (Exception e)
        //        {
        //            _logger.LogError(e, "حدث خطأ أثناء جلب بيانات ");

        //            return new ApiResultModel()
        //            {
        //                Code = 500,
        //                Message = $"حدث خطأ: {e.Message}"
        //            };
        //        }


        //    }


        [HttpPost("GetEmpDocs2")]
        public async Task<ApiResultModel> GetEmpDocs1([FromBody] EmpDocPara para)
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



        [HttpPost("GetEmpDocs")]
        public async Task<ActionResult<ApiResultModel>> GetEmpDocs(
    [FromBody] EmpDocPara para,
    CancellationToken ct)
        {
            try
            {
                // 0) قيم افتراضية آمنة
                para.P_ONLY_ACTIVE ??= 1;       // 1 = نشط فقط
                para.P_RNWL_DOC_TYP ??= 810;    // حسب منطقك

                // 1) استدعاء خدمة أوراكل
                var json = await _dataAccessSerivce.GetDocsAndRnwlAsJson(
                    P_EMP_NO: para.P_EMP_NO,
                    P_CODE_NO: para.P_CODE_NO,
                    P_DCMNT_TYP_NO: para.P_DCMNT_TYP_NO,
                    P_DOC_NO: para.P_DOC_NO,
                    P_SUB_CODE_NO: para.P_SUB_CODE_NO,
                    P_ONLY_ACTIVE: para.P_ONLY_ACTIVE,
                    P_RNWL_DOC_TYP: para.P_RNWL_DOC_TYP,
                    P_RNWL_DOC_NO: para.P_RNWL_DOC_NO,
                    P_RNWL_DOC_SRL: para.P_RNWL_DOC_SRL
                );

                // 2) لا توجد بيانات خام
                if (string.IsNullOrWhiteSpace(json))
                    return NoContent(); // ✅ يعالج CS8625

                // 3) تحليل الاستجابة العامة
                var response = _dbModelMapping.GetDbResultModelFromJson(json);
                if (response?.Result?.MsgNo != "004")
                {
                    return StatusCode(500, new ApiResultModel
                    {
                        Code = 500,
                        Message = response?.Result?.MsgTxt ?? "حدث خطأ غير معروف من الدالة GET_DOCS_AND_RNWL_JSON.",
                        Content = null
                    });
                }

                // 4) تحويل القسم DTL إلى الموديل الداخلي
                var documents = _dbModelMapping.MapJson<EmployeeDocument>(response)
                                 ?? new List<EmployeeDocument>();

                if (documents.Count == 0)
                    return NoContent(); // ✅

                // (اختياري) ترتيب
                documents = documents
                    .OrderByDescending(d => d.ExpiryDate ?? DateTime.MinValue)
                    .ToList();

                // 5) مابّينغ إلى DTO للـ Front-End
                var dtoList = _mapper.Map<List<EmployeeDocumentDto>>(documents);

                // 6) نجاح
                return Ok(new ApiResultModel
                {
                    Code = 0,
                    Message = "Success",
                    Content = dtoList
                });
            }
            catch (OperationCanceledException)
            {
                return StatusCode(499, new ApiResultModel
                {
                    Code = 499,
                    Message = "تم إلغاء الطلب",
                    Content = null
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetEmpDocs failed. EmpNo={EmpNo}", para?.P_EMP_NO);
                return StatusCode(500, new ApiResultModel
                {
                    Code = 500,
                    Message = $"حدث خطأ: {ex.Message}",
                    Content = null
                });
            }
        }

      //  [HttpPost("GetEmpDocs")]
        //public async Task<ApiResultModel> GetEmpDocs([FromBody] EmpDocPara para)
        //{
        //    try
        //    {
        //        // 1) استدعاء الدالة التي تُرجع JSON من أوراكل (docs + rnwl)
        //        var json = await _dataAccessSerivce.GetDocsAndRnwlAsJson(
        //            // فلاتر DTL
        //            P_EMP_NO: para.P_EMP_NO,
        //            P_CODE_NO: para.P_CODE_NO,
        //            P_DCMNT_TYP_NO: para.P_DCMNT_TYP_NO,
        //            P_DOC_NO: para.P_DOC_NO,        // اختياري (VARCHAR2)
        //            P_SUB_CODE_NO: para.P_SUB_CODE_NO,   // اختياري
        //            P_ONLY_ACTIVE: para.P_ONLY_ACTIVE,   // 1=نشط فقط (افتراضي), 0=الكل
        //                                                 // فلاتر RNWL
        //            P_RNWL_DOC_TYP: para.P_RNWL_DOC_TYP,  // افتراضيًا 810
        //            P_RNWL_DOC_NO: para.P_RNWL_DOC_NO,
        //            P_RNWL_DOC_SRL: para.P_RNWL_DOC_SRL
        //        );

        //        // 2) التحقق من الاستجابة
        //        if (string.IsNullOrWhiteSpace(json))
        //        {
        //            return new ApiResultModel
        //            {
        //                Code = 204, // No Content
        //                Message = "لا توجد بيانات متاحة",
        //                Content = null
        //            };
        //        }

        //        // 3) تحليل JSON إلى نموذج وسيط عام
        //        var response = _dbModelMapping.GetDbResultModelFromJson(json);
        //        if (response?.Result?.MsgNo != "004")
        //        {
        //            return new ApiResultModel
        //            {
        //                Code = 500,
        //                Message = response?.Result?.MsgTxt ?? "حدث خطأ غير معروف من الدالة GET_DOCS_AND_RNWL_JSON.",
        //                Content = null
        //            };
        //        }

        //        // 4) تحويل القسمين إلى موديلاتك النهائية
        //        //    يفترض أن الـ mapper يعرف أن Data.dtl تُحوّل إلى EmpDocRecordModel
        //        //    و Data.rnwl تُحوّل إلى EmpDocRnwlRecordModel

        //      //  ت إلى الموديل النهائي(عدّل النوع حسب موديلك الفعلي)
        //        var payload = _dbModelMapping.MapJson<EmployeeDocument>(response) ?? new List<EmployeeDocument>();

        //        //var dtl2 = _dbModelMapping.MapJson<EmployeeDocument[]>(response)
        //               //   ?? new List<EmpDocRecordModel>();
        //      //  var rnwl = _dbModelMapping.MapJsonSection<EmpDocRnwlRecordModel>(response, "rnwl")
        //                  // ?? new List<EmpDocRnwlRecordModel>();

               

        //        // 5) نجاح — نرجع كلا القائمتين + الميتاداتا إن أردت
              

        //        return new ApiResultModel
        //        {
        //            Code = 0,
        //            Message = "Success",
        //            Content = payload
        //        };
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "GetEmpDocs failed");
        //        return new ApiResultModel
        //        {
        //            Code = 500,
        //            Message = $"حدث خطأ: {ex.Message}",
        //            Content = null
        //        };
        //    }
        //}

    }




}

