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
    public class CompanyBillingService : ICompanyBillingService
    {
        private readonly ICompanyBillingRepository repo;
        public CompanyBillingService(ICompanyBillingRepository repo) => this.repo = repo;

        public async Task<TResponse<DateTime[]>> GetAllBillingDatesAsync()
        {
            var dates = await repo.GetAllBillingDatesAsync();
            return new TResponse<DateTime[]>(dates);
        }

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

        public async Task<TResponse<CompanyBillingInfo>> GetEarlierBillingAsync(int year, int month)
        {
            var result = await repo.GetBillingByDateAsync(year, month);
            if (result is null) return new TResponse<CompanyBillingInfo>(ServiceCode.NotFound);
            return new TResponse<CompanyBillingInfo>(result.ToDto());
        }

        public async Task<TResponse<FileInfo>> GetExcelSheetAsync(int id)
        {
            var billingBase = await repo.GetBillingByIdAsync(id);
            if (billingBase is null) return new TResponse<FileInfo>(ServiceCode.NotFound);

            var parser = new XMLBuilder();
            if (!parser.BuildBillingXML(billingBase)) return new TResponse<FileInfo>(ServiceCode.InternalServerError);

            return new TResponse<FileInfo>(new FileInfo(parser.FileName, parser.URL));
        }

        public async Task<TResponse<CompanyBillingInfo>> RemoveItemAsync(int billingBaseId, int customerId)
        {
            var billingBase = await repo.GetBillingByIdAsync(billingBaseId);
            if (billingBase is null) return new TResponse<CompanyBillingInfo>(ServiceCode.NotFound);

            try
            {
                billingBase.RemoveCustomer(customerId);
                await repo.CompleteAsync();
                return new TResponse<CompanyBillingInfo>(billingBase.ToDto());
            }
            catch { return new TResponse<CompanyBillingInfo>(ServiceCode.InternalServerError); }
        }

        public async Task<TResponse<CompanyBillingInfo>> SyncBillingItemsAsync(int billingBaseId)
        {
            var billingBase = await repo.GetBillingByIdAsync(billingBaseId);
            if (billingBase is null) return new TResponse<CompanyBillingInfo>(ServiceCode.NotFound);

            var customers = await repo.GetCompanyCustomers();
            var inactiveCustomers = await repo.GetCompanyInactiveCustomers();
            try
            {
                billingBase.ClearInactiveCustomers(inactiveCustomers)
                    .EnterUnhousedCustomers(customers);
                await repo.CompleteAsync();
                return new TResponse<CompanyBillingInfo>(billingBase.ToDto());
            }
            catch { return new TResponse<CompanyBillingInfo>(ServiceCode.InternalServerError); }
        }

        public async Task<TResponse<CompanyBillingInfo>> UpdateBillingAsync(CompanyBillingInfo dto)
        {
            var billingBase = await repo.GetBillingByIdAsync(dto.Id);
            if (billingBase is null) return new TResponse<CompanyBillingInfo>(ServiceCode.NotFound);
            foreach (var item in billingBase.Items)
            {
                var dtoItem = dto.Items.Where(s => s.Id == item.Id).FirstOrDefault();
                if (dtoItem is null) continue;
                item.Notes = dtoItem.Notes;
                item.ExVAT = int.Parse(dtoItem.ExVAT);
                item.CalculatePrice();
            }

            try
            {
                repo.Update(billingBase);
                await repo.CompleteAsync();
                return new TResponse<CompanyBillingInfo>(billingBase.ToDto());
            }
            catch { return new TResponse<CompanyBillingInfo>(ServiceCode.InternalServerError); }
        }
    }
}
