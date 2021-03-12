using Moq;
using rp.Accounting.App.Infrastructure.Interfaces;
using rp.Accounting.App.Models;
using rp.Accounting.App.Models.InfoModels;
using rp.Accounting.App.Services;
using rp.Accounting.App.Services.Communication;
using rp.Accounting.Domain;
using rp.Accounting.Tests.TestHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace rp.Accounting.Tests.Services
{
    public class PrivateBillingServiceTests
    {
        private readonly SeedHelper seedHelper;
        public PrivateBillingServiceTests() => this.seedHelper = new SeedHelper();

        #region GetCurrentBillingAsync Tests
        [Fact]
        public async Task GetCurrentBillingAsync_BillingExists_ReturnsBilling()
        {
            // arrange
            var repo = new Mock<IPrivateBillingRepository>();
            var billings = seedHelper.GetQueryablePrivateBillingMockSet();
            repo.Setup(s => s.GetCurrentBillingAsync()).ReturnsAsync(billings.FirstOrDefault());
            var service = new PrivateBillingService(repo.Object);

            // act
            var result = await service.GetCurrentBillingAsync();

            // assert
            Assert.IsType<TResponse<PrivateBillingInfo>>(result);
            Assert.NotNull(result.Entity);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task GetCurrentBillingAsync_NoExistingBilling_ReturnsNewBilling()
        {
            // arrange
            var repo = new Mock<IPrivateBillingRepository>();
            var billings = seedHelper.GetQueryablePrivateBillingMockSet();
            var customers = seedHelper.GetQueryableCustomerMockSet();
            repo.Setup(s => s.GetPrivateCustomers()).ReturnsAsync(customers);
            var service = new PrivateBillingService(repo.Object);

            // act
            var result = await service.GetCurrentBillingAsync();

            // assert
            Assert.IsType<TResponse<PrivateBillingInfo>>(result);
            Assert.NotNull(result.Entity);
            Assert.True(result.Success);
        }
        #endregion

        #region SyncBillingItemsAsync Tests
        [Fact]
        public async Task SyncBillingItemsAsync_BadId_ReturnsNoSuccess()
        {
            // arrange
            var repo = new Mock<IPrivateBillingRepository>();
            var billings = seedHelper.GetQueryablePrivateBillingMockSet();
            int id = 99;
            repo.Setup(s => s.GetBillingByIdAsync(id)).ReturnsAsync(billings.FirstOrDefault(f => f.Id == id));
            var service = new PrivateBillingService(repo.Object);

            // act
            var result = await service.SyncBillingItemsAsync(id);

            // assert
            Assert.IsType<TResponse<PrivateBillingInfo>>(result);
            Assert.Null(result.Entity);
            Assert.False(result.Success);
        }
        #endregion

        #region UpdateBillingAsync Tests
        [Fact]
        public async Task UpdateBillingAsync_SuccessfulUpdate_ReturnsSuccessResponse()
        {
            // arrange
            var repo = new Mock<IPrivateBillingRepository>();
            var billings = seedHelper.GetQueryablePrivateBillingMockSet();
            var request = billings[0].ToDto();
            repo.Setup(s => s.GetBillingByIdAsync(request.Id)).ReturnsAsync(billings.FirstOrDefault(f => f.Id == request.Id));
            var service = new PrivateBillingService(repo.Object);

            // act
            var result = await service.UpdateBillingAsync(request);

            // assert
            Assert.IsType<TResponse<PrivateBillingInfo>>(result);
            Assert.NotNull(result.Entity);
            Assert.True(result.Success);
        }

        [Fact]
        public async Task UpdateBillingAsync_BadId_ReturnsNotFoundResponse()
        {
            // arrange
            var repo = new Mock<IPrivateBillingRepository>();
            var billings = seedHelper.GetQueryablePrivateBillingMockSet();
            var request = billings[0].ToDto();
            repo.Setup(s => s.GetBillingByIdAsync(99)).ReturnsAsync(billings.Where(b => b.Id == 99).FirstOrDefault());
            var service = new PrivateBillingService(repo.Object);

            // act
            var result = await service.UpdateBillingAsync(request);

            // assert
            Assert.IsType<TResponse<PrivateBillingInfo>>(result);
            Assert.Null(result.Entity);
            Assert.False(result.Success);
            Assert.True(result.Code == ServiceCode.NotFound);
        }
        #endregion

        #region RemoveItemAsync Tests
        [Fact]
        public async Task RemoveItemAsync_BadBillingId_ReturnsNotFound()
        {
            // arrange
            var repo = new Mock<IPrivateBillingRepository>();
            var billings = seedHelper.GetQueryablePrivateBillingMockSet();
            var id = 99;
            repo.Setup(s => s.GetBillingByIdAsync(id)).ReturnsAsync(billings.FirstOrDefault(f => f.Id == id));
            var service = new PrivateBillingService(repo.Object);

            // act
            var result = await service.RemoveItemAsync(id, 1);

            // assert
            Assert.IsType<TResponse<PrivateBillingInfo>>(result);
            Assert.Null(result.Entity);
            Assert.False(result.Success);
            Assert.True(result.Code == ServiceCode.NotFound);
        }

        [Fact]
        public async Task RemoveItemAsync_SuccessfullRemove_ReturnsSuccessResponse()
        {
            // arrange
            var repo = new Mock<IPrivateBillingRepository>();
            var billings = seedHelper.GetQueryablePrivateBillingMockSet();
            var id = 1;
            repo.Setup(s => s.GetBillingByIdAsync(id)).ReturnsAsync(billings.FirstOrDefault(f => f.Id == id));
            var service = new PrivateBillingService(repo.Object);

            // act
            var result = await service.RemoveItemAsync(id, 1);

            // assert
            Assert.IsType<TResponse<PrivateBillingInfo>>(result);
            Assert.NotNull(result.Entity);
            Assert.True(result.Success);
            Assert.True(result.Code == ServiceCode.Ok);
        }
        #endregion

        #region GetAllBillingDatesAsync Tests
        [Fact]
        public async Task GetAllBillingDatesAsync_ExistingBillings_ReturnsSuccessResponse()
        {
            // arrange
            var repo = new Mock<IPrivateBillingRepository>();
            var billings = seedHelper.GetQueryablePrivateBillingMockSet();
            repo.Setup(s => s.GetAllBillingDatesAsync()).ReturnsAsync(billings.Select(s => s.Date).ToArray());
            var service = new PrivateBillingService(repo.Object);

            // act
            var result = await service.GetAllBillingDatesAsync();

            // assert
            Assert.IsType<TResponse<DateTime[]>>(result);
            Assert.NotNull(result.Entity);
            Assert.True(result.Success);
            Assert.True(result.Entity.Length == billings.Count);
        }

        [Fact]
        public async Task GetAllBillingDatesAsync_NoBillings_ReturnsEmptyArray()
        {
            // arrange
            var repo = new Mock<IPrivateBillingRepository>();
            repo.Setup(s => s.GetAllBillingDatesAsync()).ReturnsAsync(Array.Empty<DateTime>());
            var service = new PrivateBillingService(repo.Object);

            // act
            var result = await service.GetAllBillingDatesAsync();

            // assert
            Assert.IsType<TResponse<DateTime[]>>(result);
            Assert.NotNull(result.Entity);
            Assert.True(result.Success);
            Assert.True(result.Entity.Length == 0);
        }
        #endregion

        #region GetEarlierBillingAsync Tests
        [Fact]
        public async Task GetEarlierBillingAsync_ExistingBilling_ReturnsSuccessResponse()
        {
            // arrange
            var repo = new Mock<IPrivateBillingRepository>();
            var billings = seedHelper.GetQueryablePrivateBillingMockSet();
            var (year, month) = (billings.First().Date.Year, billings.First().Date.Month);
            repo.Setup(s => s.GetBillingByDateAsync(year, month)).ReturnsAsync(
                billings.Where(b => b.Date.Year == year && b.Date.Month == month).FirstOrDefault()
            );
            var service = new PrivateBillingService(repo.Object);

            // act
            var result = await service.GetEarlierBillingAsync(year, month);

            // assert
            Assert.IsType<TResponse<PrivateBillingInfo>>(result);
            Assert.NotNull(result.Entity);
            Assert.True(result.Success);
            Assert.True(result.Entity.Date.Year == year && result.Entity.Date.Month == month);
        }

        [Fact]
        public async Task GetEarlierBillingAsync_NoBilling_ReturnsNoSuccess()
        {
            // arrange
            var repo = new Mock<IPrivateBillingRepository>();
            var billings = new List<PrivateBilling>();
            var (year, month) = (1970, 1);
            repo.Setup(s => s.GetBillingByDateAsync(year, month)).ReturnsAsync(
                billings.Where(b => b.Date.Year == year && b.Date.Month == month).FirstOrDefault()
            );
            var service = new PrivateBillingService(repo.Object);

            // act
            var result = await service.GetEarlierBillingAsync(year, month);

            // assert
            Assert.IsType<TResponse<PrivateBillingInfo>>(result);
            Assert.Null(result.Entity);
            Assert.False(result.Success);
        }

        [Fact]
        public async Task GetEarlierBillingAsync_BadDate_ReturnsNoSuccess()
        {
            // arrange
            var repo = new Mock<IPrivateBillingRepository>();
            var billings = seedHelper.GetQueryablePrivateBillingMockSet();
            var (year, month) = (1970, 1);
            repo.Setup(s => s.GetBillingByDateAsync(year, month)).ReturnsAsync(
                billings.Where(b => b.Date.Year == year && b.Date.Month == month).FirstOrDefault()
            );
            var service = new PrivateBillingService(repo.Object);

            // act
            var result = await service.GetEarlierBillingAsync(year, month);

            // assert
            Assert.IsType<TResponse<PrivateBillingInfo>>(result);
            Assert.Null(result.Entity);
            Assert.False(result.Success);
        } 
        #endregion
    }
}
