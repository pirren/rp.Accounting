using Microsoft.EntityFrameworkCore;
using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.DataAccess;
using rp.Accounting.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rp.Accounting.App.Infrastructure
{
    public class CustomerRepository : BaseRepository, ICustomerRepository
    {
        public CustomerRepository(RpContext ctx) : base(ctx)
        { }

        public async Task<List<Customer>> GetAllCustomers()
            => await ctx.Customers.Where(c => c.Active).AsNoTracking().ToListAsync();

        public async Task<List<Customer>> GetPrivateCustomers()
            => await ctx.Customers.Where(c => c.Active && c.Type == CustomerType.Private).ToListAsync();

        public async Task<List<Customer>> GetCompanyCustomers()
            => await ctx.Customers.Where(c => c.Active && c.Type == CustomerType.Company).ToListAsync();

        public async Task<Customer> GetCustomerById(int id)
            => await ctx.Customers.FirstOrDefaultAsync(f => f.Id == id);

        public async Task<List<Customer>> GetPrivateInactiveCustomers()
            => await ctx.Customers.Where(c => !c.Active && c.Type == CustomerType.Private).ToListAsync();

        public async Task<List<Customer>> GetCompanyInactiveCustomers()
            => await ctx.Customers.Where(c => !c.Active && c.Type == CustomerType.Company).ToListAsync();

        public async Task<List<Customer>> GetInactiveCustomers()
            => await ctx.Customers.Where(c => !c.Active).ToListAsync();
    }
}
