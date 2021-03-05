using Microsoft.EntityFrameworkCore;
using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.App.Models;
using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Models.RequestModels;
using rp.Accounting.App.Services.Communication;
using rp.Accounting.App.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace rp.Accounting.App.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository repo;
        public CustomerService(ICustomerRepository repo)
        {
            this.repo = repo;
        }

        public async Task<ServiceResponse<CustomerInfo[]>> GetAllCustomersAsync()
        {
            var result = await repo.GetAllCustomers()
                .Select(c => c.ToDto())
                .ToArrayAsync();

            if (result.Length > 0)
                return new ServiceResponse<CustomerInfo[]>(result);
            else return new ServiceResponse<CustomerInfo[]>(ServiceCode.NoContent);
        }

        public async Task<ServiceResponse<CustomerInfo[]>> GetCompanyCustomersAsync()
        {
            var result = await repo.GetCompanyCustomers()
                .Select(c => c.ToDto())
                .ToArrayAsync();

            if (result.Length > 0)
                return new ServiceResponse<CustomerInfo[]>(result);
            else return new ServiceResponse<CustomerInfo[]>(ServiceCode.NoContent);
        }

        public async Task<ServiceResponse<CustomerInfo>> GetCustomerByIdAsync(int id)
        {
            var result = await repo.GetCustomerById(id)
                .Select(s => s.ToDto())
                .FirstOrDefaultAsync();

            if (result == null)
                return new ServiceResponse<CustomerInfo>(ServiceCode.NotFound);
            else return new ServiceResponse<CustomerInfo>(result);
        }

        public async Task<ServiceResponse<CustomerInfo[]>> GetPrivateCustomersAsync()
        {
            var result = await repo.GetPrivateCustomers()
                .Select(c => c.ToDto())
                .ToArrayAsync();

            if (result.Length > 0)
                return new ServiceResponse<CustomerInfo[]>(result);
            else return new ServiceResponse<CustomerInfo[]>(ServiceCode.NoContent);
        }

        public async Task<ServiceResponse<object>> UpdateCustomerAsync(int id, CustomerRequest request)
        {
            var existingCustomer = await repo.GetCustomerById(id).FirstOrDefaultAsync();
            if (existingCustomer == null)
                return new ServiceResponse<object>(false, ServiceCode.NotFound);

            existingCustomer.FirstName = request.FirstName;
            existingCustomer.Address = request.Address;
            existingCustomer.Email = request.Email;
            if(request.TypeIsPrivate)
            {
                if (double.TryParse(request.HourlyFee, out double hourly))
                    existingCustomer.UpdateHourlyPrice(hourly);
                existingCustomer.LastName = request.LastName;
            }

            try
            {
                repo.DetachLocal(existingCustomer);
                repo.Update(existingCustomer);
                await repo.CompleteAsync();
                return new ServiceResponse<object>(true, ServiceCode.Ok);
            } catch {return new ServiceResponse<object>(false, ServiceCode.InternalServerError); }
        }
    }
}
