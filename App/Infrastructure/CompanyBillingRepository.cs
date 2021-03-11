using Microsoft.EntityFrameworkCore;
using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.DataAccess;
using rp.Accounting.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace rp.Accounting.App.Infrastructure
{
    public class CompanyBillingRepository : CustomerRepository, ICompanyBillingRepository
    {
        public CompanyBillingRepository(RpContext ctx) : base(ctx) { }

        public async Task<CompanyBilling> GetBillingByIdAsync(int id)
            => await ctx.CompanyBillings.Where(s => s.Id == id)
                .Include(i => i.Items)
                .ThenInclude(c => c.Customer)
                .FirstOrDefaultAsync();

        public async Task<CompanyBilling> GetCurrentBillingAsync()
        {
            var dtNow = DateTime.Now;
            return await ctx.CompanyBillings
                .Where(p => p.Date.Year == dtNow.Year && p.Date.Month == dtNow.Month)
                .Include(i => i.Items)
                .ThenInclude(i => i.Customer)
                .FirstOrDefaultAsync();
        }

        public Task<CompanyBilling> GetEarlierBillingAsync(DateTime date, Type type)
        {
            throw new NotImplementedException();
        }
    }
}
