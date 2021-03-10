using rp.Accounting.Domain;
using System;
using System.Threading.Tasks;

namespace rp.Accounting.App.Infrastructure.Interfaces
{
    public interface IPrivateBillingRepository : ICustomerRepository
    {
        Task<PrivateBilling> GetCurrentBillingAsync();
        Task<PrivateBilling> GetEarlierBillingAsync(DateTime date, Type type);
        Task<PrivateBilling> GetBillingByIdAsync(int id);
    }
}
