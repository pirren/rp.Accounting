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
        Task<CompanyBilling> GetBillingByDateAsync(int year, int month);
        Task<CompanyBilling> GetBillingByIdAsync(int id);
        Task<DateTime[]> GetAllBillingDatesAsync();
    }
}
