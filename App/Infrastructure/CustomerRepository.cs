using Microsoft.EntityFrameworkCore;
using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.DataAccess;
using rp.Accounting.Domain;
using System.Linq;

namespace rp.Accounting.App.Infrastructure
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RpContext ctx;
        public CustomerRepository(RpContext ctx)
        {
            this.ctx = ctx;
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return ctx.Customers.Where(c => c.Active).AsNoTracking();
        }

        public IQueryable<Customer> GetPrivateCustomers()
        {
            return ctx.Customers.Where(c => c.Active && c.Type == CustomerType.Private).AsNoTracking();
        }

        public IQueryable<Customer> GetCompanyCustomers()
        {
            return ctx.Customers.Where(c => c.Active && c.Type == CustomerType.Company).AsNoTracking();
        }

        public IQueryable<Customer> GetCustomerById(int id)
        {
            return ctx.Customers.Where(c => c.Id == id).AsNoTracking();
        }
    }
}
