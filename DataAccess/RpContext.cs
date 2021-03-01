using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using rp.Accounting.Domain;
using System;

namespace rp.Accounting.DataAccess
{
    public class RpContext : DbContext
    {
        public virtual DbSet<Customer> Customers { get; set; }

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
            cust.Property(p => p.Type).HasConversion<string>();

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
