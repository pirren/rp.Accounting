using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.App.Models;
using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Services.Communication;
using rp.Accounting.App.Services.Interfaces;
using rp.Accounting.Domain;
using rp.Accounting.XMLParsing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace rp.Accounting.App.Services
{
    public class PrivateBillingService : IPrivateBillingService
    {
        private readonly IPrivateBillingRepository repo;
        public PrivateBillingService(IPrivateBillingRepository repo)
        {
            this.repo = repo;
        }

        public async Task<TResponse<PrivateBillingInfo>> SyncBillingItemsAsync(int billingBaseId)
        {
            var billingBase = await repo.GetBillingByIdAsync(billingBaseId);
            if (billingBase is null) return new TResponse<PrivateBillingInfo>(ServiceCode.NotFound);

            var customers = await repo.GetPrivateCustomers();
            var inactiveCustomers = await repo.GetPrivateInactiveCustomers();
            try
            {
                billingBase.ClearInactiveCustomers(inactiveCustomers)
                    .EnterUnhousedCustomers(customers)
                    .UpdateHourlyPrices();
                await repo.CompleteAsync();
                return new TResponse<PrivateBillingInfo>(billingBase.ToDto());
            }
            catch { return new TResponse<PrivateBillingInfo>(ServiceCode.InternalServerError); }
        }

        /// <summary>
        /// Gets the monthly active BillingBase, or creates one if it does not exist.
        /// </summary>
        /// <returns>Active BillingBase</returns>
        public async Task<TResponse<PrivateBillingInfo>> GetCurrentBillingAsync()
        {
            var billingBase = await repo.GetCurrentBillingAsync();
            if (billingBase is not null) return new TResponse<PrivateBillingInfo>(billingBase.ToDto());

            var privateCustomers = await repo.GetPrivateCustomers();
            var newBillingBase = new PrivateBilling()
                .PopulateNew(privateCustomers);

            try
            {
                await repo.AddAsync(newBillingBase);
                await repo.CompleteAsync();
                return new TResponse<PrivateBillingInfo>(newBillingBase.ToDto());
            }
            catch { return new TResponse<PrivateBillingInfo>(ServiceCode.InternalServerError); }
        }

        public async Task<TResponse<PrivateBillingInfo>> GetEarlierBillingAsync(int year, int month)
        {
            var result = await repo.GetBillingByDateAsync(year, month);
            if (result is null) return new TResponse<PrivateBillingInfo>(ServiceCode.NotFound);
            return new TResponse<PrivateBillingInfo>(result.ToDto());
        }

        public async Task<TResponse<PrivateBillingInfo>> UpdateBillingAsync(PrivateBillingInfo dto)
        {
            var billingBase = await repo.GetBillingByIdAsync(dto.Id);
            if (billingBase is null) return new TResponse<PrivateBillingInfo>(ServiceCode.NotFound);
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
                return new TResponse<PrivateBillingInfo>(billingBase.ToDto());
            }
            catch { return new TResponse<PrivateBillingInfo>(ServiceCode.InternalServerError); }
        }

        public async Task<TResponse<FileInfo>> GetExcelSheetAsync(int id)
        {
            var billingBase = await repo.GetBillingByIdAsync(id);
            if (billingBase is null) return new TResponse<FileInfo>(ServiceCode.NotFound);

            var parser = new XMLBuilder();
            if (!parser.BuildBillingXML(billingBase)) return new TResponse<FileInfo>(ServiceCode.InternalServerError);

            return new TResponse<FileInfo>(new FileInfo(parser.FileName, parser.URL));
        }

        public async Task<TResponse<PrivateBillingInfo>> RemoveItemAsync(int billingBaseId, int customerId)
        {
            var billingBase = await repo.GetBillingByIdAsync(billingBaseId);
            if (billingBase is null) return new TResponse<PrivateBillingInfo>(ServiceCode.NotFound);

            try
            {
                billingBase.RemoveCustomer(customerId);
                await repo.CompleteAsync();
                return new TResponse<PrivateBillingInfo>(billingBase.ToDto());
            } catch { return new TResponse<PrivateBillingInfo>(ServiceCode.InternalServerError); }
        }

        public async Task<TResponse<DateTime[]>> GetAllBillingDatesAsync()
        {
            var dates = await repo.GetAllBillingDatesAsync();
            return new TResponse<DateTime[]>(dates);
        }
    }
}
