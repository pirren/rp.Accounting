using Moq;
using Moq.EntityFrameworkCore;
using rp.Accounting.App.Infrastructure;
using rp.Accounting.DataAccess;
using rp.Accounting.Domain;
using rp.Accounting.Tests.TestHelpers;
using System.Threading.Tasks;
using Xunit;

namespace rp.Accounting.Tests.Infrastructure
{
    public class PrivateBillingBaseRepositoryTests
    {
        private readonly SeedHelper seedHelper;
        public PrivateBillingBaseRepositoryTests() => this.seedHelper = new SeedHelper();

        #region GetCurrentBillingBaseAsync Tests
        [Fact]
        public async Task GetCurrentBillingBaseAsync_Ongoing_ReturnsExpectedObjects()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var billingBases = seedHelper.GetQueryablePrivateBillingBaseMockSet();
            ctx.Setup(s => s.PrivateBillingBases).ReturnsDbSet(billingBases);
            var repo = new PrivateBillingBaseRepository(ctx.Object);

            // act
            var result = await repo.GetCurrentBillingBaseAsync();

            // assert
            Assert.IsType<PrivateBillingBase>(result);
            Assert.True(result.Id == 1);
        }
        #endregion

        #region GetByIdAsync Tests
        [Fact]
        public async Task GetByIdAsync_IdExists_ReturnsExpectedObject()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            int id = 1;
            var billingBases = seedHelper.GetQueryablePrivateBillingBaseMockSet();
            ctx.Setup(s => s.PrivateBillingBases).ReturnsDbSet(billingBases);
            var repo = new PrivateBillingBaseRepository(ctx.Object);

            // act
            var result = await repo.GetByIdAsync(id);

            // assert
            Assert.IsType<PrivateBillingBase>(result);
            Assert.True(result.Id == id);
        }

        [Fact]
        public async Task GetByIdAsync_IdDoesNotExists_ReturnsNull()
        {
            // arrange
            var ctx = new Mock<RpContext>();
            var billingBases = seedHelper.GetQueryablePrivateBillingBaseMockSet();
            ctx.Setup(s => s.PrivateBillingBases).ReturnsDbSet(billingBases);
            var repo = new PrivateBillingBaseRepository(ctx.Object);

            // act
            var result = await repo.GetByIdAsync(99);

            // assert
            Assert.Null(result);
        }
        #endregion
    }
}
