using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using rp.Accounting.Domain;

namespace rp.Accounting.DataAccess
{
    public class RpContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<PrivateBilling> PrivateBillings { get; set; }
        public virtual DbSet<PrivateBillingItem> PrivateBillingItems { get; set; }
        public virtual DbSet<CompanyBilling> CompanyBillings { get; set; }
        public virtual DbSet<CompanyBillingItem> CompanyBillingItems { get; set; }

        public RpContext()
        { }

        public RpContext(DbContextOptions options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var cust = modelBuilder.Entity<Customer>();
            cust.HasKey(p => p.Id);
            cust.Property(p => p.Type).IsRequired();
            cust.Property(p => p.Email).IsRequired();
            cust.Property(p => p.Registered).IsRequired();
            cust.Property(p => p.Type).HasConversion<string>();

            var pb = modelBuilder.Entity<PrivateBilling>();
            pb.HasKey(p => p.Id);
            pb.Property(p => p.Date).IsRequired();

            var pbi = modelBuilder.Entity<PrivateBillingItem>();
            pbi.HasKey(p => p.Id);
            pbi.HasOne(p => p.PrivateBilling)
                .WithMany(p => p.Items)
                .OnDelete(DeleteBehavior.Cascade);

            var cb = modelBuilder.Entity<CompanyBilling>();
            cb.HasKey(p => p.Id);
            cb.Property(p => p.Date).IsRequired();

            var cbi = modelBuilder.Entity<CompanyBillingItem>();
            cbi.HasKey(p => p.Id);
            cbi.HasOne(p => p.CompanyBilling)
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
