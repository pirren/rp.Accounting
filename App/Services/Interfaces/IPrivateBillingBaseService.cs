using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Services.Communication;
using System.Threading.Tasks;

namespace rp.Accounting.App.Services.Interfaces
{
    public interface IPrivateBillingBaseService
    {
        Task<ServiceResponse<PrivateBillingBaseInfo>> GetCurrentBillingBaseAsync();
        Task<ServiceResponse<PrivateBillingBaseInfo>> GetEarlierBillingBaseAsync(int year, int month);
    }
}
