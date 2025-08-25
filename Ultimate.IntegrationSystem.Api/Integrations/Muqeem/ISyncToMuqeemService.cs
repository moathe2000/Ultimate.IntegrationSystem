using System;
using System.Threading;
using System.Threading.Tasks;
using Ultimate.IntegrationSystem.Api.Models;

namespace Ultimate.IntegrationSystem.Api.Integrations.Muqeem
{

    public interface ISyncToMuqeemService
    {
        Task<ApiResultModel> SendAsync(
            int endpointId,
            object payload = null,
            string templateVariable = null,
            TimeSpan? perAttemptTimeout = null,
            CancellationToken ct = default);

        // عمليات مسماة (اختياري)
        Task<ApiResultModel> IssueExitReentryAsync(int endpointId, object dto, CancellationToken ct = default);
        Task<ApiResultModel> CancelExitReentryAsync(int endpointId, object dto, CancellationToken ct = default);
        Task<ApiResultModel> ExtendExitReentryAsync(int endpointId, object dto, CancellationToken ct = default);
        Task<ApiResultModel> IssueFinalExitAsync(int endpointId, object dto, CancellationToken ct = default);
        Task<ApiResultModel> CancelFinalExitAsync(int endpointId, object dto, CancellationToken ct = default);
        Task<ApiResultModel> RenewIqamaAsync(int endpointId, object dto, CancellationToken ct = default);
        Task<ApiResultModel> UpdateInfoExtendPassportAsync(int endpointId, object dto, CancellationToken ct = default);
        Task<ApiResultModel> UpdateInfoRenewPassportAsync(int endpointId, object dto, CancellationToken ct = default);

    }

}
