using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Services.Communication;
using System.Threading.Tasks;

namespace rp.Accounting.App.Services.Interfaces
{
    public interface ICompanyBillingService
    {
        Task<TResponse<CompanyBillingInfo>> GetCurrentBillingAsync();
        Task<TResponse<CompanyBillingInfo>> GetEarlierBillingAsync(int year, int month);
        Task<TResponse<CompanyBillingInfo>> SyncBillingItemsAsync(int billingBaseId);
        Task<TResponse<CompanyBillingInfo>> UpdateBillingAsync(CompanyBillingInfo dto);
        Task<TResponse<CompanyBillingInfo>> RemoveItemAsync(int billingBaseId, int customerId);
        Task<TResponse<FileInfo>> GetExcelSheetAsync(int id);
    }
}
