using Microsoft.EntityFrameworkCore;
using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.DataAccess;
using rp.Accounting.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace rp.Accounting.App.Infrastructure
{
    public class PrivateBillingBaseRepository : CustomerRepository, IPrivateBillingBaseRepository
    {
        public PrivateBillingBaseRepository(RpContext ctx) : base(ctx)
        { }

        public async Task<PrivateBillingBase> GetCurrentBillingBase()
        {
            var dtNow = DateTime.Now;
            return await ctx.PrivateBillingBases
                .Where(p => p.Date.Year == dtNow.Year && p.Date.Month == dtNow.Month)
                .Include(i => i.Items)
                .ThenInclude(i => i.Customer)
                .FirstOrDefaultAsync();
        }

        public async Task<PrivateBillingBase> GetEarlierBillingBase(DateTime date, Type type)
        {
            throw new NotImplementedException();
        }
    }
}
