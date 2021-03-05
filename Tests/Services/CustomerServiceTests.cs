using Microsoft.EntityFrameworkCore.Query;
using Moq;
using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Services;
using rp.Accounting.App.Services.Communication;
using rp.Accounting.DataAccess;
using rp.Accounting.Domain;
using rp.Accounting.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace rp.Accounting.Tests.Services
{
    public class CustomerServiceTests
    {
        private readonly SeedHelper seedHelper;
        public CustomerServiceTests() => this.seedHelper = new SeedHelper();

        //[Fact]
        //public async Task GetAllCustomersAsync_NoCustomers_ReturnNoContent()
        //{
        //    // arrange
        //    var repo = new Mock<ICustomerRepository>();
        //    var customers = seedHelper.GetQueryableCustomerMockSet();
        //    repo.Setup(s => s.GetAllCustomers()).Returns(Enumerable.Empty<Customer>().AsQueryable());
        //    var service = new CustomerService(repo.Object);

        //    // act
        //    var result = await service.GetAllCustomersAsync();

        //    // assert
        //    Assert.IsType<ServiceResponse<CustomerInfo[]>>(result);
        //    //Assert.All(result.Entity, res => Assert.True(res.Active));
        //    //Assert.True(result.Entity.Length == 0);
        //}
    }
}
