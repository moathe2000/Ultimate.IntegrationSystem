
using Ultimate.IntegrationSystem.Web.Dto.Muqeem;
using Ultimate.IntegrationSystem.Web.Models;

namespace Ultimate.IntegrationSystem.Web.Service.Muqeem
{
    public interface IIqamaService
    {
        Task<ApiResultModel> RenewAsync(RenewIqamaRequestDto dto, CancellationToken ct = default);
        Task<ApiResultModel> IssueAsync(IssueIqamaRequestDto dto, CancellationToken ct = default);
      //  Task<ApiResultModel> TransferAsync(TransferIqamaRequestDto dto, CancellationToken ct = default);
    }
}
