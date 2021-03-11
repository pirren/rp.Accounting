using Moq;
using Moq.EntityFrameworkCore;
using rp.Accounting.App.Infrastructure;
using rp.Accounting.DataAccess;
using rp.Accounting.Domain;
using rp.Accounting.Tests.TestHelpers;
using System;
using System.Threading.Tasks;
using Xunit;

namespace rp.Accounting.Tests.Infrastructure
{
    public class PrivateBillingRepositoryTests
    {
        private readonly SeedHelper seedHelper;
        public PrivateBillingRepositoryTests() => this.seedHelper = new SeedHelper();

        #region GetCurrentBillingAsync Tests
        [Fact]
        public async Task GetCurrentBillingBaseAsync_Ongoing_ReturnsExpectedObjects()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var billingBases = seedHelper.GetQueryablePrivateBillingMockSet();
            ctx.Setup(s => s.PrivateBillings).ReturnsDbSet(billingBases);
            var repo = new PrivateBillingRepository(ctx.Object);

            // act
            var result = await repo.GetCurrentBillingAsync();

            // assert
            Assert.IsType<PrivateBilling>(result);
            Assert.True(result.Id == 1);
        }
        #endregion

        #region GetBillingByIdAsync Tests
        [Fact]
        public async Task GetBillingByIdAsync_IdExists_ReturnsExpectedObject()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            int id = 1;
            var billingBases = seedHelper.GetQueryablePrivateBillingMockSet();
            ctx.Setup(s => s.PrivateBillings).ReturnsDbSet(billingBases);
            var repo = new PrivateBillingRepository(ctx.Object);

            // act
            var result = await repo.GetBillingByIdAsync(id);

            // assert
            Assert.IsType<PrivateBilling>(result);
            Assert.True(result.Id == id);
        }

        [Fact]
        public async Task GetBillingByIdAsync_IdDoesNotExists_ReturnsNull()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var billingBases = seedHelper.GetQueryablePrivateBillingMockSet();
            ctx.Setup(s => s.PrivateBillings).ReturnsDbSet(billingBases);
            var repo = new PrivateBillingRepository(ctx.Object);

            // act
            var result = await repo.GetBillingByIdAsync(99);

            // assert
            Assert.Null(result);
        }
        #endregion

        #region GetAllBillingDatesAsync Tests
        [Fact]
        public async Task GetAllBillingDatesAsync_ExistingBillings_ReturnsCorrectObjectCount()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var billingBases = seedHelper.GetQueryablePrivateBillingMockSet();
            ctx.Setup(s => s.PrivateBillings).ReturnsDbSet(billingBases);
            var repo = new PrivateBillingRepository(ctx.Object);

            // act
            var result = await repo.GetAllBillingDatesAsync();

            // assert
            Assert.IsType<DateTime[]>(result);
            Assert.True(result.Length == billingBases.Count);
        }

        [Fact]
        public async Task GetAllBillingDatesAsync_NoExistingBillings_ReturnsEmptyArray()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var billingBases = seedHelper.GetQueryablePrivateBillingMockSet();
            ctx.Setup(s => s.PrivateBillings).ReturnsDbSet(Array.Empty<PrivateBilling>());
            var repo = new PrivateBillingRepository(ctx.Object);

            // act
            var result = await repo.GetAllBillingDatesAsync();

            // assert
            Assert.IsType<DateTime[]>(result);
            Assert.True(result.Length == 0);
        } 
        #endregion
    }
}
