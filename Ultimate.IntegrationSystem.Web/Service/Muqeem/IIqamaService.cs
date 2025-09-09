// File: Services/Muqeem/IIqamaService.cs
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Ultimate.IntegrationSystem.Web.Dto;
using Ultimate.IntegrationSystem.Web.Dto.Muqeem;
using Ultimate.IntegrationSystem.Web.Models;

namespace Ultimate.IntegrationSystem.Web.Service.Muqeem
{
    public interface IIqamaService
    {
        // ===== Iqama =====
        Task<ApiResultModel> RenewAsync(RenewIqamaRequestDto dto, CancellationToken ct = default);
        Task<ApiResultModel> IssueAsync(IssueIqamaRequestDto dto, CancellationToken ct = default);
        Task<ApiResultModel> TransferAsync(TransferIqamaRequestDto dto, CancellationToken ct = default);

        // ===== Exit Re-Entry =====
        Task<ApiResultModel> ExitReentryIssueAsync(FEVisaIssuanceRequestDto dto, CancellationToken ct = default);
        Task<ApiResultModel> ExitReentryCancelAsync(ERVisaCancellationRequestDto dto, CancellationToken ct = default);
        Task<ApiResultModel> ExitReentryReprintAsync(ReprintERVisaRequestDto dto, CancellationToken ct = default);
        Task<ApiResultModel> ExitReentryExtendAsync(ERVisaExtendRequestDto dto, CancellationToken ct = default);

        // ===== Final Exit =====
        Task<ApiResultModel> FinalExitIssueAsync(FEVisaIssuanceRequestDto dto, CancellationToken ct = default);
        Task<ApiResultModel> FinalExitCancelAsync(FEVisaCancellationRequestDto dto, CancellationToken ct = default);
        Task<ApiResultModel> FinalExitIssueDuringProbationAsync(IssueFEDuringTheProbationaryPeriodRequestDto dto, CancellationToken ct = default);

        // ===== Update Passport Info =====
        Task<ApiResultModel> UpdateInfoExtendPassportAsync(UIExtendPassportValidityRequestDto dto, CancellationToken ct = default);
        Task<ApiResultModel> UpdateInfoRenewPassportAsync(UIRenewPassportRequestDto dto, CancellationToken ct = default);

        // ===== Visit Visa =====
        Task<ApiResultModel> VisitVisaExtendAsync(ExtendVisitVisaRequestDto dto, CancellationToken ct = default);

        // ===== Reports =====
        Task<ApiResultModel> ReportInteractiveAsync(InteractiveServicesReportRequestDto dto, CancellationToken ct = default);
        Task<ApiResultModel> ReportMuqeemPrintAsync(MuqeemReportRequestDto dto, CancellationToken ct = default);
        Task<ApiResultModel> ReportVisitorPrintAsync(VisitorReportRequestDto dto, CancellationToken ct = default);

        // ===== Occupation =====
        Task<ApiResultModel> OccupationCheckApprovalAsync(ChangeOccupationApprovalRequestDto dto, CancellationToken ct = default);
        Task<ApiResultModel> OccupationChangeAsync(ChangeOccupationRequestDto dto, CancellationToken ct = default);

        // ===== Lookups (GET) =====
        Task<ApiResultModel> GetCitiesAsync(CancellationToken ct = default);
        Task<ApiResultModel> GetCountriesAsync(CancellationToken ct = default);
        Task<ApiResultModel> GetMaritalStatusesAsync(CancellationToken ct = default);

        // ===== Docs (دمجها لصفحة التابات) =====
        Task<List<EmployeeDocumentDto>> GetEmpDocsAsync(EmpDocParaDto para, CancellationToken ct = default);
    }
}
