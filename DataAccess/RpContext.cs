using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using rp.Accounting.Domain;

namespace rp.Accounting.DataAccess
{
    public class RpContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<PrivateBillingBase> PrivateBillingBases { get; set; }
        public virtual DbSet<PrivateBillingBaseItem> PrivateBillingBaseItems { get; set; }

        public RpContext()
        { }

        public RpContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var cust = modelBuilder.Entity<Customer>();
            cust.HasKey(p => p.Id);
            cust.Property(p => p.FirstName).IsRequired();
            cust.Property(p => p.Type).IsRequired();
            cust.Property(p => p.Email).IsRequired();
            cust.Property(p => p.Registered).IsRequired();
            cust.Property(p => p.Type).HasConversion<string>();

            var pbb = modelBuilder.Entity<PrivateBillingBase>();
            pbb.HasKey(p => p.Id);
            pbb.Property(p => p.Date).IsRequired();

            var pbbi = modelBuilder.Entity<PrivateBillingBaseItem>();
            pbbi.HasKey(p => p.Id);
            pbbi.HasOne(p => p.PrivateBillingBase)
                .WithMany(p => p.Items)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Seed();
        }

        public class RpContextFactory : IDesignTimeDbContextFactory<RpContext>
        {
            public RpContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<RpContext>();
                optionsBuilder.UseSqlite("Data Source=../App/rp.Accounting.db")
                    .EnableSensitiveDataLogging();

                return new RpContext(optionsBuilder.Options);
            }
        }
    }
}
