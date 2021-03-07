using Moq;
using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Services;
using rp.Accounting.App.Services.Communication;
using rp.Accounting.Domain;
using rp.Accounting.Tests.TestHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace rp.Accounting.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly SeedHelper seedHelper;
        public CustomerServiceTests() => this.seedHelper = new SeedHelper();

        #region GetAllCustomersAsync
        [Fact]
        public async Task GetAllCustomersAsync_NoCustomers_ReturnsNoContent()
        {
            // arrange
            var repo = new Mock<ICustomerRepository>();
            repo.Setup(s => s.GetAllCustomers()).ReturnsAsync(new List<Customer>());
            var service = new CustomerService(repo.Object);

            // act
            var result = await service.GetAllCustomersAsync();

            // assert
            Assert.IsType<TResponse<CustomerInfo[]>>(result);
            Assert.Null(result.Entity);
            Assert.False(result.Success);
            Assert.True(result.Code == ServiceCode.NoContent);
        }

        [Fact]
        public async Task GetAllCustomersAsync_ExistingCustomers_ReturnsObjects()
        {
            // arrange
            var repo = new Mock<ICustomerRepository>();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            customers.First().Active = false;
            repo.Setup(s => s.GetAllCustomers()).ReturnsAsync(customers.Where(c => c.Active == true).ToList());
            var service = new CustomerService(repo.Object);

            // act
            var result = await service.GetAllCustomersAsync();

            // assert
            Assert.IsType<TResponse<CustomerInfo[]>>(result);
            Assert.All(result.Entity, res => Assert.True(res.Active == true));
            Assert.True(result.Entity.Length == customers.Count - 1);
            Assert.True(result.Success);
            Assert.True(result.Code == ServiceCode.Ok);
        }
        #endregion

        #region GetPrivateCustomersAsync
        [Fact]
        public async Task GetPrivateCustomersAsync_NoCustomers_ReturnsNoContent()
        {
            // arrange
            var repo = new Mock<ICustomerRepository>();
            repo.Setup(s => s.GetPrivateCustomers()).ReturnsAsync(new List<Customer>());
            var service = new CustomerService(repo.Object);

            // act
            var result = await service.GetPrivateCustomersAsync();

            // assert
            Assert.IsType<TResponse<CustomerInfo[]>>(result);
            Assert.Null(result.Entity);
            Assert.False(result.Success);
            Assert.True(result.Code == ServiceCode.NoContent);
        }

        [Fact]
        public async Task GetPrivateCustomersAsync_ExistingCustomers_ReturnsObjects()
        {
            // arrange
            var repo = new Mock<ICustomerRepository>();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            customers.First(c => c.Type == CustomerType.Private).Active = false;
            repo.Setup(s => s.GetPrivateCustomers())
                .ReturnsAsync(customers.Where(c => c.Active == true && c.Type == CustomerType.Private).ToList());
            var service = new CustomerService(repo.Object);

            // act
            var result = await service.GetPrivateCustomersAsync();

            // assert
            Assert.IsType<TResponse<CustomerInfo[]>>(result);
            Assert.All(result.Entity, res => Assert.True(res.Active == true));
            Assert.True(result.Entity.Length == customers.Where(c => c.Type == CustomerType.Private).Count() - 1);
            Assert.True(result.Success);
            Assert.True(result.Code == ServiceCode.Ok);
        }
        #endregion

        #region GetCompanyCustomersAsync
        [Fact]
        public async Task GetCompanyCustomersAsync_NoCustomers_ReturnsNoContent()
        {
            // arrange
            var repo = new Mock<ICustomerRepository>();
            repo.Setup(s => s.GetCompanyCustomers()).ReturnsAsync(new List<Customer>());
            var service = new CustomerService(repo.Object);

            // act
            var result = await service.GetCompanyCustomersAsync();

            // assert
            Assert.IsType<TResponse<CustomerInfo[]>>(result);
            Assert.Null(result.Entity);
            Assert.False(result.Success);
            Assert.True(result.Code == ServiceCode.NoContent);
        }

        [Fact]
        public async Task GetCompanyCustomersAsync_ExistingCustomers_ReturnsObjects()
        {
            // arrange
            var repo = new Mock<ICustomerRepository>();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            customers.First(c => c.Type == CustomerType.Company).Active = false;
            repo.Setup(s => s.GetCompanyCustomers())
                .ReturnsAsync(customers.Where(c => c.Active == true && c.Type == CustomerType.Company).ToList());
            var service = new CustomerService(repo.Object);

            // act
            var result = await service.GetCompanyCustomersAsync();

            // assert
            Assert.IsType<TResponse<CustomerInfo[]>>(result);
            Assert.All(result.Entity, res => Assert.True(res.Active == true));
            Assert.True(result.Entity.Length == customers.Where(c => c.Type == CustomerType.Company).Count() - 1);
            Assert.True(result.Success);
            Assert.True(result.Code == ServiceCode.Ok);
        }
        #endregion

        #region GetCustomerByIdAsync
        [Fact]
        public async Task GetCustomerByIdAsync_CustomerExists_ReturnsObjects()
        {
            // arrange
            var repo = new Mock<ICustomerRepository>();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            int id = 1;
            repo.Setup(s => s.GetCustomerById(id))
                .ReturnsAsync(customers.FirstOrDefault(f => f.Id == id));
            var service = new CustomerService(repo.Object);

            // act
            var result = await service.GetCustomerByIdAsync(id);

            // assert
            Assert.IsType<TResponse<CustomerInfo>>(result);
            Assert.True(result.Success);
            Assert.True(result.Code == ServiceCode.Ok);
            Assert.True(result.Entity.Id == id.ToString());
        }

        [Fact]
        public async Task GetCustomerByIdAsync_IdDoesNotExists_ReturnsNoSuccess()
        {
            // arrange
            var repo = new Mock<ICustomerRepository>();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            int id = 99;
            repo.Setup(s => s.GetCustomerById(id))
                .ReturnsAsync(customers.FirstOrDefault(f => f.Id == id));
            var service = new CustomerService(repo.Object);

            // act
            var result = await service.GetCustomerByIdAsync(id);

            // assert
            Assert.IsType<TResponse<CustomerInfo>>(result);
            Assert.Null(result.Entity);
            Assert.False(result.Success);
            Assert.True(result.Code == ServiceCode.NotFound);
        }
        #endregion
    }
}
