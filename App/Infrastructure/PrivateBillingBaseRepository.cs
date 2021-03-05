using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.DataAccess;
using System;
using System.Threading.Tasks;

namespace rp.Accounting.App.Infrastructure
{
    public class PrivateBillingBaseRepository : CustomerRepository, IPrivateBillingBaseRepository
    {
        public PrivateBillingBaseRepository(RpContext ctx) : base(ctx)
        { }

        public Task<PrivateBillingBaseInfo> GetCurrent()
        {
            throw new NotImplementedException();
        }

        public Task<PrivateBillingBaseInfo> GetEarlier(DateTime date, Type type)
        {
            throw new NotImplementedException();
        }
    }
}
