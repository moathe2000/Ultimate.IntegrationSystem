using Newtonsoft.Json;
using System;
using Ultimate.IntegrationSystem.Api.Common.Enum;
using Ultimate.IntegrationSystem.Api.Dto.Muqeem.Responses;
using Ultimate.IntegrationSystem.Api.Models;

namespace Ultimate.IntegrationSystem.Api.Integrations.Muqeem
{
    public sealed class MuqeemResponseNormalizer : IResponseNormalizer
    {
        public ApiResultModel Normalize(MuqeemEndpoint endpoint, string rawResponse)
        {
            if (string.IsNullOrWhiteSpace(rawResponse))
                return new ApiResultModel(204, "لا توجد بيانات (Empty Response)", null);

            try
            {
                // 🟢 في حالة وجود خطأ Business Exception
                if (rawResponse.Contains("Business Exception", StringComparison.OrdinalIgnoreCase))
                {
                    var error = JsonConvert.DeserializeObject<BusinessError>(rawResponse);
                    return new ApiResultModel(400,
                        $"Business Exception: {error?.Details ?? error?.Error ?? "Invalid Payload"}",
                        null);
                }

                object content = endpoint switch
                {
                    // ✅ تجديد إقامة
                    MuqeemEndpoint.Iqama_Renew =>
                        JsonConvert.DeserializeObject<IqamaRenewalDto>(rawResponse),

                    // ✅ خروج نهائي (إصدار / إلغاء / فترة تجربة)
                    MuqeemEndpoint.FinalExit_Issue or MuqeemEndpoint.FinalExit_Cancel or MuqeemEndpoint.FinalExit_Probation =>
                        JsonConvert.DeserializeObject<FinalExitDto>(rawResponse),

                    // ✅ إصدار إقامة
                    MuqeemEndpoint.Iqama_Issue =>
                        JsonConvert.DeserializeObject<IqamaIssueResponseDto>(rawResponse),

                    // ✅ نقل كفالة
                    MuqeemEndpoint.Iqama_Transfer =>
                        JsonConvert.DeserializeObject<TransferIqamaDto>(rawResponse),

                    // ✅ موافقة تغيير مهنة
                    MuqeemEndpoint.Occupation_CheckApproval =>
                        JsonConvert.DeserializeObject<OccupationApprovalDto>(rawResponse),

                    // ✅ تغيير مهنة
                    MuqeemEndpoint.Occupation_Change =>
                        JsonConvert.DeserializeObject<OccupationChangedDto>(rawResponse),

                    // ✅ تمديد زيارة
                    MuqeemEndpoint.VisitVisa_Extend =>
                        JsonConvert.DeserializeObject<ExtendVisitVisaDto>(rawResponse),

                    // ✅ تقرير تفاعلي (قائمة)
                    MuqeemEndpoint.Report_Interactive =>
                        JsonConvert.DeserializeObject<InteractiveServicesReportDto[]>(rawResponse),

                    // ✅ Lookups (مدن / دول / الحالة الاجتماعية)
                    MuqeemEndpoint.Lookup_Cities or MuqeemEndpoint.Lookup_Countries or MuqeemEndpoint.Lookup_Marital =>
                        JsonConvert.DeserializeObject<LookupItem[]>(rawResponse),

                    // 🟢 أي استجابة غير معروفة ترجع نص خام
                    _ => rawResponse
                };

                return new ApiResultModel(0, "Success", content);
            }
            catch (Exception ex)
            {
                return new ApiResultModel(500, $"فشل في تحويل الاستجابة: {ex.Message}", rawResponse);
            }
        }

        // ✅ DTO خاص بأخطاء Business Exception
        private sealed class BusinessError
        {
            public string Error { get; set; }
            public string Details { get; set; }
        }
    }
}
