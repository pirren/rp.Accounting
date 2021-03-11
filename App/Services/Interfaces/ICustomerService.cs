using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Models.RequestModels;
using rp.Accounting.App.Services.Communication;
using System.Threading.Tasks;

namespace rp.Accounting.App.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<TResponse<CustomerInfo[]>> GetAllCustomersAsync();
        Task<TResponse<CustomerInfo[]>> GetAllInactiveCustomersAsync();
        Task<TResponse<CustomerInfo[]>> GetPrivateCustomersAsync();
        Task<TResponse<CustomerInfo[]>> GetCompanyCustomersAsync();
        Task<TResponse<CustomerInfo>> GetCustomerByIdAsync(int id);
        Task<TResponse<object>> UpdateCustomerAsync(int id, CustomerRequest request);
        Task<TResponse<object>> AddCustomerAsync(CustomerRequest request);
        Task<TResponse<CustomerInfo>> InactivateCustomerAsync(int id);
        Task<TResponse<CustomerInfo>> ActivateCustomerAsync(int id);
    }
}
