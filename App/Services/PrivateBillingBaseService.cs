using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.App.Models;
using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Services.Communication;
using rp.Accounting.App.Services.Interfaces;
using rp.Accounting.Domain;
using System;
using System.Linq;
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

        public async Task<TResponse<PrivateBillingBaseInfo>> SyncBillingBaseItemsAsync(int billingBaseId)
        {
            var billingBase = await repo.GetByIdAsync(billingBaseId);
            if (billingBase == null) return new TResponse<PrivateBillingBaseInfo>(ServiceCode.NotFound);

            var customers = await repo.GetPrivateCustomers();
            var inactiveCustomers = await repo.GetInactiveCustomers();
            try
            {
                billingBase.ClearInactiveCustomers(inactiveCustomers);
                billingBase.EnterUnhousedCustomers(customers);
                billingBase.UpdateHourlyPrices();
                await repo.CompleteAsync();
                return new TResponse<PrivateBillingBaseInfo>(billingBase.ToDto());
            }
            catch { return new TResponse<PrivateBillingBaseInfo>(ServiceCode.InternalServerError); }
        }

        public async Task<TResponse<PrivateBillingBaseInfo>> GetCurrentBillingBaseAsync()
        {
            var billingBase = await repo.GetCurrentBillingBaseAsync();
            if (billingBase != null) return new TResponse<PrivateBillingBaseInfo>(billingBase.ToDto());

            var privateCustomers = await repo.GetPrivateCustomers();
            var newBillingBase = new PrivateBillingBase()
                .PopulateNew(privateCustomers);

            try
            {
                await repo.AddAsync(newBillingBase);
                await repo.CompleteAsync();
                return new TResponse<PrivateBillingBaseInfo>(newBillingBase.ToDto());
            }
            catch { return new TResponse<PrivateBillingBaseInfo>(ServiceCode.InternalServerError); }
        }

        public Task<TResponse<PrivateBillingBaseInfo>> GetEarlierBillingBaseAsync(int year, int month)
        {
            throw new NotImplementedException();
        }

        public async Task<TResponse<PrivateBillingBaseInfo>> UpdateBillingBaseAsync(PrivateBillingBaseInfo dto)
        {
            var billingBase = await repo.GetByIdAsync(dto.Id);
            foreach (var item in billingBase.Items)
            {
                var dtoItem = dto.Items.Where(s => s.Id == item.Id).FirstOrDefault();
                if (dtoItem is null) continue;
                item.AmountOccassions = int.Parse(dtoItem.AmountOccassions);
                item.HoursPerVisit = dtoItem.HoursPerVisit;
                item.TotalHours = double.Parse(dtoItem.TotalHours);
                item.WeeksAttended = dtoItem.WeeksAttended;
                item.CalculatePrice();
            }

            try
            {
                repo.Update(billingBase);
                await repo.CompleteAsync();
                return new TResponse<PrivateBillingBaseInfo>(billingBase.ToDto());
            }
            catch { return new TResponse<PrivateBillingBaseInfo>(ServiceCode.InternalServerError); }
        }
    }
}
