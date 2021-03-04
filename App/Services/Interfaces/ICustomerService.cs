using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Services.Communication;
using System.Threading.Tasks;

namespace rp.Accounting.App.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<ServiceResponse<CustomerInfo[]>> GetAllCustomersAsync();
        Task<ServiceResponse<CustomerInfo[]>> GetPrivateCustomersAsync();
        Task<ServiceResponse<CustomerInfo[]>> GetCompanyCustomersAsync();
        Task<ServiceResponse<CustomerInfo>> GetCustomerByIdAsync(int id);
    }
}
