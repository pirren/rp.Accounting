using rp.Accounting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rp.Accounting.App.Infrastructure.Interfaces
{
    public interface ICompanyBillingRepository : ICustomerRepository
    {
        Task<CompanyBilling> GetCurrentBillingAsync();
        Task<CompanyBilling> GetEarlierBillingAsync(DateTime date, Type type);
        Task<CompanyBilling> GetBillingByIdAsync(int id);
    }
}
