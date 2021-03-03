using rp.Accounting.App.Models.InfoModels;
using System.Threading.Tasks;

namespace rp.Accounting.App.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<CustomerInfo[]> GetAllCustomersAsync();
        Task<CustomerInfo[]> GetPrivateCustomersAsync();
        Task<CustomerInfo[]> GetCompanyCustomersAsync();
        Task<CustomerInfo[]> GetCustomerByIdAsync(int id);
    }
}
