using Microsoft.EntityFrameworkCore;
using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.DataAccess;
using rp.Accounting.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace rp.Accounting.App.Infrastructure
{
    public class PrivateBillingRepository : CustomerRepository, IPrivateBillingRepository
    {
        public PrivateBillingRepository(RpContext ctx) : base(ctx)
        { }

        public async Task<PrivateBilling> GetBillingByIdAsync(int id)
        {
            return await ctx.PrivateBillings.Where(s => s.Id == id)
                .Include(i => i.Items)
                .ThenInclude(c => c.Customer)
                .FirstOrDefaultAsync();
        }

        public async Task<PrivateBilling> GetCurrentBillingAsync()
        {
            var dtNow = DateTime.Now;
            return await ctx.PrivateBillings
                .Where(p => p.Date.Year == dtNow.Year && p.Date.Month == dtNow.Month)
                .Include(i => i.Items)
                .ThenInclude(i => i.Customer)
                .FirstOrDefaultAsync();
        }

        public async Task<PrivateBilling> GetBillingByDateAsync(DateTime date, Type type)
        {
            throw new NotImplementedException();
        }

        public async Task<DateTime[]> GetAllBillingDatesAsync()
            => await ctx.PrivateBillings.Select(s => s.Date).ToArrayAsync();
    }
}
