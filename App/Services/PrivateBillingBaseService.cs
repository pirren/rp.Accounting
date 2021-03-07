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
    public class PrivateBillingBaseService : IPrivateBillingBaseService
    {
        private readonly IPrivateBillingBaseRepository repo;
        public PrivateBillingBaseService(IPrivateBillingBaseRepository repo)
        {
            this.repo = repo;
        }

        public async Task<ServiceResponse<PrivateBillingBaseInfo>> GetCurrentBillingBaseAsync()
        {
            var billingBase = await repo.GetCurrentBillingBase();
            if (billingBase != null) return new ServiceResponse<PrivateBillingBaseInfo>(billingBase.ToDto());

            var privateCustomers = await repo.GetPrivateCustomers();
            var newBillingBase = new PrivateBillingBase()
                .PopulateNew(privateCustomers);

            try
            {
                await repo.AddAsync(newBillingBase);
                await repo.CompleteAsync();
                return new ServiceResponse<PrivateBillingBaseInfo>(newBillingBase.ToDto());
            } catch { return new ServiceResponse<PrivateBillingBaseInfo>(ServiceCode.InternalServerError); }
        }

        public Task<ServiceResponse<PrivateBillingBaseInfo>> GetEarlierBillingBaseAsync(int year, int month)
        {
            throw new NotImplementedException();
        }
    }
}
