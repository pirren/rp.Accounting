using Microsoft.EntityFrameworkCore;
using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.App.Models;
using rp.Accounting.App.Models.InfoModels;
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

        public async Task<CustomerInfo[]> GetAllCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerInfo[]> GetCompanyCustomersAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerInfo[]> GetCustomerByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerInfo[]> GetPrivateCustomersAsync()
            => await repo.GetPrivateCustomers().Select(c => new CustomerInfo
            {
                Active = c.Active,
                Address = c.Address,
                Email = c.Email,
                FirstName = c.FirstName,
                LastName = c.LastName,
                HourlyFee = c.HourlyFee,
                Type = c.Type.EnumToString()
            }).ToArrayAsync();
    }
}
