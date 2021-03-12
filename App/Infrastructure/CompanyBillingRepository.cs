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

        public async Task<CompanyBilling> GetBillingByDateAsync(int year, int month)
            => await ctx.CompanyBillings
                .Where(p => p.Date.Year == year && p.Date.Month == month)
                .Include(i => i.Items)
                .ThenInclude(i => i.Customer)
                .FirstOrDefaultAsync();

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

        public async Task<DateTime[]> GetAllBillingDatesAsync()
            => await ctx.CompanyBillings
                .AsNoTracking()
                .OrderByDescending(s => s.Date)
                .Select(s => s.Date)
                .Take(12)
                .ToArrayAsync();
    }
}
