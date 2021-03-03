using rp.Accounting.Domain;
using System.Linq;

namespace rp.Accounting.App.Infrastructure.Interfaces
{
    public interface ICustomerRepository
    {
        IQueryable<Customer> GetAllCustomers();
        IQueryable<Customer> GetPrivateCustomers();
        IQueryable<Customer> GetCompanyCustomers();
        IQueryable<Customer> GetCustomerById(int id);
    }
}
