using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using Ultimate.IntegrationSystem.Api.Common.Enum;
using Ultimate.IntegrationSystem.Api.Dto.Muqeem;
using Ultimate.IntegrationSystem.Api.Dto.Muqeem.Requests;
using Ultimate.IntegrationSystem.Api.Dto.Muqeem.Responses;
using Ultimate.IntegrationSystem.Api.Integrations.Muqeem;
using Ultimate.IntegrationSystem.Api.Models;

namespace Ultimate.IntegrationSystem.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MuqeemController : ControllerBase
    {
        private readonly ILogger<MuqeemController> _logger;
        private readonly ISyncToMuqeemService _syncToMuqeem;
        private readonly IMapper _mapper;
        public MuqeemController(ILogger<MuqeemController> logger, ISyncToMuqeemService syncToMuqeem, IMapper mapper)
        {
            _logger = logger;
            _syncToMuqeem = syncToMuqeem;
            _mapper = mapper;
        }

        // 🟢 Authentication
        //[HttpPost("Authenticate")]
        //public async Task<ApiResultModel> Authenticate([FromBody] LoginDto dto = null)
        //    => await CallMuqeem(MuqeemEndpoint.Authenticate, dto, "Muqeem Authenticate");

        // 🟢 Exit Re-Entry Visa
        [HttpPost("ExitReentry/Issue")]
        public async Task<ApiResultModel> IssueExitReentry([FromBody] FEVisaIssuanceRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.ExitReentry_Issue, dto, "Exit-Reentry Issue");

        [HttpPost("ExitReentry/Cancel")]
        public async Task<ApiResultModel> CancelExitReentry([FromBody] ERVisaCancellationRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.ExitReentry_Cancel, dto, "Exit-Reentry Cancel");

        [HttpPost("ExitReentry/Reprint")]
        public async Task<ApiResultModel> ReprintExitReentry([FromBody] ReprintERVisaRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.ExitReentry_Reprint, dto, "Exit-Reentry Reprint");

        [HttpPost("ExitReentry/Extend")]
        public async Task<ApiResultModel> ExtendExitReentry([FromBody] ERVisaExtendRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.ExitReentry_Extend, dto, "Exit-Reentry Extend");

        // 🟢 Final Exit
        [HttpPost("FinalExit/Issue")]
        public async Task<ApiResultModel> IssueFinalExit([FromBody] FEVisaIssuanceRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.FinalExit_Issue, dto, "Final Exit Issue");

        [HttpPost("FinalExit/Cancel")]
        public async Task<ApiResultModel> CancelFinalExit([FromBody] FEVisaCancellationRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.FinalExit_Cancel, dto, "Final Exit Cancel");

        [HttpPost("FinalExit/IssueDuringProbation")]
        public async Task<ApiResultModel> IssueFinalExitProbation([FromBody] IssueFEDuringTheProbationaryPeriodRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.FinalExit_Probation, dto, "Final Exit Probation Issue");

        // 🟢 Iqama
        [HttpPost("Iqama/Renew")]
        public async Task<ApiResultModel> RenewIqama([FromBody] RenewIqamaRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.Iqama_Renew, dto, "Iqama Renew");

        [HttpPost("Iqama/Issue")]
        public async Task<ApiResultModel> IssueIqama([FromBody] IssueIqamaRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.Iqama_Issue, dto, "Iqama Issue");
        //  [HttpPost("Iqama/Issue")]
        //public async Task<ApiResultModel> IssueIqama([FromBody] IssueIqamaRequestDto dto)
        //{
        //    var result = await _syncToMuqeem.SendAsync((int)MuqeemEndpoint.Iqama_Issue, dto);

        //    if (result.Code == 0 && result.Content != null)
        //    {
        //        var contentStr = result.Content.ToString()?.Trim();

        //        // لو مش JSON (ما يبدأش بـ { أو [)، رجعها نص عادي بدون Parse
        //        if (string.IsNullOrWhiteSpace(contentStr) || !(contentStr.StartsWith("{") || contentStr.StartsWith("[")))
        //        {
        //            return new ApiResultModel
        //            {
        //                Code = 500,
        //                Message = "Iqama Issue: استجابة ليست JSON. المحتوى: " + contentStr,
        //                Content = null
        //            };
        //        }

        //        // الآن نقدر نعمل Deserialize بأمان
        //        var raw = System.Text.Json.JsonSerializer.Deserialize<RawIssueIqamaResponse>(
        //            contentStr,
        //            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        //        var mapped = _mapper.Map<IssueIqamaResponseDto>(raw);

        //        return new ApiResultModel
        //        {
        //            Code = 0,
        //            Message = "Iqama Issue: تمت العملية بنجاح.",
        //            Content = mapped
        //        };
        //    }


        //    return new ApiResultModel
        //    {
        //        Code = result?.Code ?? 500,
        //        Message = $"Iqama Issue: {result?.Message ?? "فشل غير معروف"}",
        //        Content = null
        //    };
        //}
        [HttpPost("Iqama/Transfer")]
        public async Task<ApiResultModel> TransferIqama([FromBody] TransferIqamaRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.Iqama_Transfer, dto, "Iqama Transfer");

        // 🟢 Update Passport Info
        [HttpPost("UpdateInfo/ExtendPassport")]
        public async Task<ApiResultModel> ExtendPassport([FromBody] UIExtendPassportValidityRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.UpdateInfo_Extend, dto, "UpdateInfo Extend Passport");

        [HttpPost("UpdateInfo/RenewPassport")]
        public async Task<ApiResultModel> RenewPassport([FromBody] UIRenewPassportRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.UpdateInfo_Renew, dto, "UpdateInfo Renew Passport");

        // 🟢 Visit Visa
        [HttpPost("VisitVisa/Extend")]
        public async Task<ApiResultModel> ExtendVisitVisa([FromBody] ExtendVisitVisaRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.VisitVisa_Extend, dto, "Visit Visa Extend");

        // 🟢 Reports
        [HttpPost("Reports/Interactive")]
        public async Task<ApiResultModel> InteractiveServicesReport([FromBody] InteractiveServicesReportRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.Report_Interactive, dto, "Interactive Services Report");

        [HttpPost("Reports/Muqeem/Print")]
        public async Task<ApiResultModel> PrintMuqeemReport([FromBody] MuqeemReportRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.MuqeemReport_Print, dto, "Muqeem Report Print");

        [HttpPost("Reports/Visitor/Print")]
        public async Task<ApiResultModel> PrintVisitorReport([FromBody] VisitorReportRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.VisitorReport_Print, dto, "Visitor Report Print");

        // 🟢 Occupation
        [HttpPost("Occupation/CheckApproval")]
        public async Task<ApiResultModel> CheckOccupationApproval([FromBody] ChangeOccupationApprovalRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.Occupation_CheckApproval, dto, "Check Occupation Approval");

        [HttpPost("Occupation/Change")]
        public async Task<ApiResultModel> ChangeOccupation([FromBody] ChangeOccupationRequestDto dto = null)
            => await CallMuqeem(MuqeemEndpoint.Occupation_Change, dto, "Change Occupation");

        // 🟢 Lookups
        [HttpGet("Lookups/Cities")]
        public async Task<ApiResultModel> GetCities()
            => await CallMuqeem(MuqeemEndpoint.Lookup_Cities, null, "Lookup Cities");

        [HttpGet("Lookups/Countries")]
        public async Task<ApiResultModel> GetCountries()
            => await CallMuqeem(MuqeemEndpoint.Lookup_Countries, null, "Lookup Countries");

        [HttpGet("Lookups/MaritalStatuses")]
        public async Task<ApiResultModel> GetMaritalStatuses()
            => await CallMuqeem(MuqeemEndpoint.Lookup_Marital, null, "Lookup Marital Statuses");


        // 🟢 Helper
        private async Task<ApiResultModel> CallMuqeem(MuqeemEndpoint endpoint, object payload, string op)
        {
            try
            {
                if (payload == null && Request.Method == "POST")
                    return new ApiResultModel { Code = 400, Message = $"{op}: المدخلات غير صالحة (Body فارغ)." };

                var result = await _syncToMuqeem.SendAsync((int)endpoint, payload);

                if (result == null)
                    return new ApiResultModel { Code = 500, Message = $"{op}: استجابة غير معروفة." };

                if (result.Code == 0)
                    return new ApiResultModel { Code = 0, Content = result.Content, Message = $"{op}: تمت العملية بنجاح." };

                return new ApiResultModel { Code = result.Code, Content = result.Content, Message = $"{op}: {result.Message}" };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{op} - Unexpected error");
                return new ApiResultModel { Code = 500, Message = $"{op}: {ex.Message}" };
            }
        }
    }
}
