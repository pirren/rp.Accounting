using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.App.Models;
using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Services.Communication;
using rp.Accounting.App.Services.Interfaces;
using rp.Accounting.Domain;
using System;
using System.Threading.Tasks;

namespace rp.Accounting.App.Services
{
    public class CompanyBillingService : ICompanyBillingService
    {
        private readonly ICompanyBillingRepository repo;
        public CompanyBillingService(ICompanyBillingRepository repo) => this.repo = repo;  

        public async Task<TResponse<CompanyBillingInfo>> GetCurrentBillingAsync()
        {
            var billingBase = await repo.GetCurrentBillingAsync();
            if (billingBase is not null) return new TResponse<CompanyBillingInfo>(billingBase.ToDto());

            var privateCustomers = await repo.GetCompanyCustomers();
            var newBillingBase = new CompanyBilling()
                .PopulateNew(privateCustomers);

            try
            {
                await repo.AddAsync(newBillingBase);
                await repo.CompleteAsync();
                return new TResponse<CompanyBillingInfo>(newBillingBase.ToDto());
            }
            catch { return new TResponse<CompanyBillingInfo>(ServiceCode.InternalServerError); }
        }

        public Task<TResponse<CompanyBillingInfo>> GetEarlierBillingAsync(int year, int month)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse<FileInfo>> GetExcelSheetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse<CompanyBillingInfo>> RemoveItemAsync(int billingBaseId, int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse<CompanyBillingInfo>> SyncBillingItemsAsync(int billingBaseId)
        {
            throw new NotImplementedException();
        }

        public Task<TResponse<CompanyBillingInfo>> UpdateBillingAsync(CompanyBillingInfo dto)
        {
            throw new NotImplementedException();
        }
    }
}
