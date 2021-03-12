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
    public class CompanyBillingRepositoryTests
    {
        private readonly SeedHelper seedHelper;
        public CompanyBillingRepositoryTests() => this.seedHelper = new SeedHelper();

        #region GetCurrentBillingAsync Tests
        [Fact]
        public async Task GetCurrentBillingBaseAsync_Ongoing_ReturnsExpectedObjects()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var billingBases = seedHelper.GetQueryableCompanyBillingMockSet();
            ctx.Setup(s => s.CompanyBillings).ReturnsDbSet(billingBases);
            var repo = new CompanyBillingRepository(ctx.Object);

            // act
            var result = await repo.GetCurrentBillingAsync();

            // assert
            Assert.IsType<CompanyBilling>(result);
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
            var billingBases = seedHelper.GetQueryableCompanyBillingMockSet();
            ctx.Setup(s => s.CompanyBillings).ReturnsDbSet(billingBases);
            var repo = new CompanyBillingRepository(ctx.Object);

            // act
            var result = await repo.GetBillingByIdAsync(id);

            // assert
            Assert.IsType<CompanyBilling>(result);
            Assert.True(result.Id == id);
        }

        [Fact]
        public async Task GetBillingByIdAsync_IdDoesNotExists_ReturnsNull()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var billingBases = seedHelper.GetQueryableCompanyBillingMockSet();
            ctx.Setup(s => s.CompanyBillings).ReturnsDbSet(billingBases);
            var repo = new CompanyBillingRepository(ctx.Object);

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
            var billingBases = seedHelper.GetQueryableCompanyBillingMockSet();
            ctx.Setup(s => s.CompanyBillings).ReturnsDbSet(billingBases);
            var repo = new CompanyBillingRepository(ctx.Object);

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
            var billingBases = seedHelper.GetQueryableCompanyBillingMockSet();
            ctx.Setup(s => s.CompanyBillings).ReturnsDbSet(Array.Empty<CompanyBilling>());
            var repo = new CompanyBillingRepository(ctx.Object);

            // act
            var result = await repo.GetAllBillingDatesAsync();

            // assert
            Assert.IsType<DateTime[]>(result);
            Assert.True(result.Length == 0);
        }
        #endregion

        #region GetBillingByDateAsync Tests
        [Fact]
        public async Task GetBillingByDateAsync_ExistingBillings_ReturnsCorrectObject()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var billingBases = seedHelper.GetQueryableCompanyBillingMockSet();
            var (year, month) = (billingBases.First().Date.Year, billingBases.First().Date.Month);
            ctx.Setup(s => s.CompanyBillings).ReturnsDbSet(billingBases);
            var repo = new CompanyBillingRepository(ctx.Object);

            // act
            var result = await repo.GetBillingByDateAsync(year, month);

            // assert
            Assert.IsType<CompanyBilling>(result);
            Assert.True(result.Date.Month == month && result.Date.Year == year);
        }

        [Fact]
        public async Task GetBillingByDateAsync_NoBillings_ReturnsNull()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            ctx.Setup(s => s.CompanyBillings).ReturnsDbSet(Array.Empty<CompanyBilling>());
            var repo = new CompanyBillingRepository(ctx.Object);

            // act
            var result = await repo.GetBillingByDateAsync(1700, 1);

            // assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetBillingByDateAsync_BadDate_ReturnsNull()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var billingBases = seedHelper.GetQueryableCompanyBillingMockSet();
            ctx.Setup(s => s.CompanyBillings).ReturnsDbSet(billingBases);
            var repo = new CompanyBillingRepository(ctx.Object);

            // act
            var result = await repo.GetBillingByDateAsync(1700, 1);

            // assert
            Assert.Null(result);
        }
        #endregion
    }
}
