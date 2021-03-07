using rp.Accounting.Domain;
using System;
using System.Threading.Tasks;

namespace rp.Accounting.App.Infrastructure.Interfaces
{
    public interface IPrivateBillingBaseRepository : ICustomerRepository
    {
        Task<PrivateBillingBase> GetCurrentBillingBaseAsync();
        Task<PrivateBillingBase> GetEarlierBillingBaseAsync(DateTime date, Type type);
        Task<PrivateBillingBase> GetByIdAsync(int id);
    }
}
