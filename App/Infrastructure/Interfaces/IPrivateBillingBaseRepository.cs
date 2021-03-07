using rp.Accounting.Domain;
using System;
using System.Threading.Tasks;

namespace rp.Accounting.App.Infrastructure.Interfaces
{
    public interface IPrivateBillingBaseRepository : ICustomerRepository
    {
        Task<PrivateBillingBase> GetCurrentBillingBase();
        Task<PrivateBillingBase> GetEarlierBillingBase(DateTime date, Type type);
    }
}
