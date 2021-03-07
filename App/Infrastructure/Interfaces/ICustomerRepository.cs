using rp.Accounting.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace rp.Accounting.App.Infrastructure.Interfaces
{
    public interface ICustomerRepository : IBaseRepository
    {
        Task<List<Customer>> GetAllCustomers();
        Task<List<Customer>> GetPrivateCustomers();
        Task<List<Customer>> GetCompanyCustomers();
        Task<Customer> GetCustomerById(int id);
        Task<List<Customer>> GetPrivateInactiveCustomers();
        Task<List<Customer>> GetCompanyInactiveCustomers();
        Task<List<Customer>> GetInactiveCustomers();
    }
}
