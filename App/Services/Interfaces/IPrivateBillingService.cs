using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Services.Communication;
using System.Threading.Tasks;

namespace rp.Accounting.App.Services.Interfaces
{
    public interface IPrivateBillingService
    {
        Task<TResponse<PrivateBillingInfo>> GetCurrentBillingAsync();
        Task<TResponse<PrivateBillingInfo>> GetEarlierBillingAsync(int year, int month);
        Task<TResponse<PrivateBillingInfo>> SyncBillingItemsAsync(int billingBaseId);
        Task<TResponse<PrivateBillingInfo>> UpdateBillingAsync(PrivateBillingInfo dto);
        Task<TResponse<PrivateBillingInfo>> RemoveItemAsync(int billingBaseId, int customerId);
        Task<TResponse<FileInfo>> GetExcelSheetAsync(int id);
    }
}
