using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using rp.Accounting.App.Infrastructure;
using rp.Accounting.DataAccess;
using rp.Accounting.Domain;
using rp.Accounting.Tests.TestHelpers;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace rp.Accounting.Tests.Infrastructure
{
    public class CustomerRepositoryTests
    {
        private readonly SeedHelper seedHelper;
        public CustomerRepositoryTests() => this.seedHelper = new SeedHelper();

        #region GetAllCustomers Tests
        [Fact]
        public async Task GetAllCustomers_PopulatedDbSet_ReturnsExpectedObjects()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            ctx.Setup(s => s.Customers).ReturnsDbSet(customers);
            var repo = new CustomerRepository(ctx.Object);

            // act
            var result = await repo.GetAllCustomers().ToArrayAsync();

            // assert
            Assert.IsType<Customer[]>(result);
            Assert.All(result, res => Assert.True(res.Active));
            Assert.True(result.Length == customers.Count);
        }

        [Fact]
        public async Task GetAllCustomers_EmptyDbSet_ReturnsNoResults()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            ctx.Setup(s => s.Customers).ReturnsDbSet(Array.Empty<Customer>());
            var repo = new CustomerRepository(ctx.Object);

            // act
            var result = await repo.GetAllCustomers().ToArrayAsync();

            // assert
            Assert.IsType<Customer[]>(result);
            Assert.All(result, res => Assert.True(res.Active));
            Assert.True(result.Length == 0);
        }

        [Fact]
        public async Task GetAllCustomers_CustomerIsNotActive_ReturnsOnlyActive()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            customers.First().Active = false;
            ctx.Setup(s => s.Customers).ReturnsDbSet(customers);
            var repo = new CustomerRepository(ctx.Object);

            // act
            var result = await repo.GetAllCustomers().ToArrayAsync();

            // assert
            Assert.IsType<Customer[]>(result);
            Assert.All(result, res => Assert.True(res.Active));
            Assert.True(result.Length == customers.Count - 1);
        }
        #endregion

        #region GetPrivateCustomers Tests
        [Fact]
        public async Task GetPrivateCustomers_PopulatedDbSet_ReturnsExpectedObjects()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            ctx.Setup(s => s.Customers).ReturnsDbSet(customers);
            var repo = new CustomerRepository(ctx.Object);

            // act
            var result = await repo.GetPrivateCustomers().ToArrayAsync();

            // assert
            Assert.IsType<Customer[]>(result);
            Assert.All(result, res => Assert.True(res.Active));
            Assert.All(result, res => Assert.True(res.Type == CustomerType.Private));
        }

        [Fact]
        public async Task GetPrivateCustomers_EmptyDbSet_ReturnsNoResults()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            ctx.Setup(s => s.Customers).ReturnsDbSet(Array.Empty<Customer>());
            var repo = new CustomerRepository(ctx.Object);

            // act
            var result = await repo.GetPrivateCustomers().ToArrayAsync();

            // assert
            Assert.IsType<Customer[]>(result);
            Assert.All(result, res => Assert.True(res.Active));
            Assert.True(result.Length == 0);
        }

        [Fact]
        public async Task GetPrivateCustomers_CustomerIsNotActive_ReturnsOnlyActive()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            customers.First(f => f.Type == CustomerType.Private).Active = false;
            ctx.Setup(s => s.Customers).ReturnsDbSet(customers);
            var repo = new CustomerRepository(ctx.Object);

            // act
            var result = await repo.GetPrivateCustomers().ToArrayAsync();
            var privateCount = customers.Where(e => e.Type == CustomerType.Private).Count();

            // assert
            Assert.IsType<Customer[]>(result);
            Assert.All(result, res => Assert.True(res.Active));
            Assert.True(result.Length == privateCount - 1);
        }
        #endregion

        #region GetCompanyCustomers Tests
        [Fact]
        public async Task GetCompanyCustomers_PopulatedDbSet_ReturnsExpectedObjects()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            ctx.Setup(s => s.Customers).ReturnsDbSet(customers);
            var repo = new CustomerRepository(ctx.Object);

            // act
            var result = await repo.GetCompanyCustomers().ToArrayAsync();

            // assert
            Assert.IsType<Customer[]>(result);
            Assert.All(result, res => Assert.True(res.Active));
            Assert.All(result, res => Assert.True(res.Type == CustomerType.Company));
        }

        [Fact]
        public async Task GetCompanyCustomers_EmptyDbSet_ReturnsNoResults()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            ctx.Setup(s => s.Customers).ReturnsDbSet(Array.Empty<Customer>());
            var repo = new CustomerRepository(ctx.Object);

            // act
            var result = await repo.GetCompanyCustomers().ToArrayAsync();

            // assert
            Assert.IsType<Customer[]>(result);
            Assert.All(result, res => Assert.True(res.Active));
            Assert.True(result.Length == 0);
        }

        [Fact]
        public async Task GetCompanyCustomers_CustomerIsNotActive_ReturnsOnlyActive()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            customers.First(f => f.Type == CustomerType.Company).Active = false;
            ctx.Setup(s => s.Customers).ReturnsDbSet(customers);
            var repo = new CustomerRepository(ctx.Object);

            // act
            var result = await repo.GetCompanyCustomers().ToArrayAsync();
            var companyCount = customers.Where(e => e.Type == CustomerType.Company).Count();

            // assert
            Assert.IsType<Customer[]>(result);
            Assert.All(result, res => Assert.True(res.Active));
            Assert.True(result.Length == companyCount - 1);
        }
        #endregion

        #region GetCustomerById Tests
        [Fact]
        public async Task GetCustomerById_ExistingId_ReturnsExpectedObject()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            ctx.Setup(s => s.Customers).ReturnsDbSet(customers);
            var repo = new CustomerRepository(ctx.Object);

            // act
            var paramId = 1;
            var result = await repo.GetCustomerById(paramId).FirstOrDefaultAsync();

            // assert
            Assert.IsType<Customer>(result);
            Assert.True(result.Id == paramId);
        }

        [Fact]
        public async Task GetCustomerById_NonExistingId_ReturnsNoResult()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            ctx.Setup(s => s.Customers).ReturnsDbSet(customers);
            var repo = new CustomerRepository(ctx.Object);

            // act
            var paramId = 99;
            var result = await repo.GetCustomerById(paramId).FirstOrDefaultAsync();

            // assert
            Assert.Null(result);
        }
        #endregion
    }
}
