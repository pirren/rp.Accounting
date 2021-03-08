using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Services.Communication;
using System.Threading.Tasks;

namespace rp.Accounting.App.Services.Interfaces
{
    public interface IPrivateBillingBaseService
    {
        Task<TResponse<PrivateBillingBaseInfo>> GetCurrentBillingBaseAsync();
        Task<TResponse<PrivateBillingBaseInfo>> GetEarlierBillingBaseAsync(int year, int month);
        Task<TResponse<PrivateBillingBaseInfo>> SyncBillingBaseItemsAsync(int billingBaseId);
        Task<TResponse<PrivateBillingBaseInfo>> UpdateBillingBaseAsync(PrivateBillingBaseInfo dto);
        Task<TResponse<FileInfo>> GetExcelSheetAsync(int id);
    }
}
