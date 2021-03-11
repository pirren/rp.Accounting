using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.App.Models;
using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Models.RequestModels;
using rp.Accounting.App.Services.Communication;
using rp.Accounting.App.Services.Interfaces;
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

        public async Task<TResponse<CustomerInfo>> ActivateCustomerAsync(int id)
        {
            var customer = await repo.GetCustomerById(id);
            if (customer is null) return new TResponse<CustomerInfo>(ServiceCode.NotFound);

            try
            {
                customer.Active = true;
                await repo.CompleteAsync();
                return new TResponse<CustomerInfo>(customer.ToDto());
            }
            catch { return new TResponse<CustomerInfo>(ServiceCode.InternalServerError); }
        }

        public async Task<TResponse<object>> AddCustomerAsync(CustomerRequest request)
        {
            try
            {
                var newCustomer = request.ToDomain();
                await repo.AddAsync(newCustomer);
                await repo.CompleteAsync();
                return new TResponse<object>(true, ServiceCode.Ok);
            }
            catch
            {
                return new TResponse<object>(ServiceCode.InternalServerError);
            }
        }

        public async Task<TResponse<CustomerInfo[]>> GetAllCustomersAsync()
        {
            var result = await repo.GetAllCustomers();
            var mapped = result.Select(c => c.ToDto()).ToArray();

            if (mapped.Length == 0)
                return new TResponse<CustomerInfo[]>(ServiceCode.NoContent);
            return new TResponse<CustomerInfo[]>(mapped);
        }

        public async Task<TResponse<CustomerInfo[]>> GetCompanyCustomersAsync()
        {
            var result = await repo.GetCompanyCustomers();
            var mapped = result.Select(c => c.ToDto()).ToArray();

            if (mapped.Length == 0)
                return new TResponse<CustomerInfo[]>(ServiceCode.NoContent);
            return new TResponse<CustomerInfo[]>(mapped);
        }

        public async Task<TResponse<CustomerInfo>> GetCustomerByIdAsync(int id)
        {
            var result = await repo.GetCustomerById(id);
            if (result == null)
                return new TResponse<CustomerInfo>(ServiceCode.NotFound);

            var mapped = result.ToDto();
            return new TResponse<CustomerInfo>(mapped);
        }

        public async Task<TResponse<CustomerInfo[]>> GetPrivateCustomersAsync()
        {
            var result = await repo.GetPrivateCustomers();
            var mapped = result.Select(c => c.ToDto()).ToArray();

            if (mapped.Length == 0)
                return new TResponse<CustomerInfo[]>(ServiceCode.NoContent);
            return new TResponse<CustomerInfo[]>(mapped);
        }

        public async Task<TResponse<CustomerInfo>> InactivateCustomerAsync(int id)
        {
            var customer = await repo.GetCustomerById(id);
            if (customer is null) return new TResponse<CustomerInfo>(ServiceCode.NotFound);

            try
            {
                customer.Active = false;
                await repo.CompleteAsync();
                return new TResponse<CustomerInfo>(customer.ToDto());
            } catch { return new TResponse<CustomerInfo>(ServiceCode.InternalServerError); }
        }

        public async Task<TResponse<object>> UpdateCustomerAsync(int id, CustomerRequest request)
        {
            var existingCustomer = await repo.GetCustomerById(id);
            if (existingCustomer == null)
                return new TResponse<object>(false, ServiceCode.NotFound);

            existingCustomer.FirstName = request.FirstName;
            existingCustomer.Address = request.Address;
            existingCustomer.Email = request.Email;
            if (request.TypeIsPrivate)
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
                return new TResponse<object>(true, ServiceCode.Ok);
            }
            catch { return new TResponse<object>(false, ServiceCode.InternalServerError); }
        }
    }
}
