using rp.Accounting.App.Models.InfoModels;
using System;
using System.Threading.Tasks;

namespace rp.Accounting.App.Infrastructure.Interfaces
{
    public interface IPrivateBillingBaseRepository : ICustomerRepository
    {
        Task<PrivateBillingBaseInfo> GetCurrent();
        Task<PrivateBillingBaseInfo> GetEarlier(DateTime date, Type type);
    }
}
