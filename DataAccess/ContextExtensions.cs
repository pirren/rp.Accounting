using Microsoft.EntityFrameworkCore;
using rp.Accounting.Domain;
using System;

namespace rp.Accounting.DataAccess
{
    public static class ContextExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            #region Customer
            var cust = modelBuilder.Entity<Customer>();
            cust.HasData(new
            {
                Id = 1,
                FirstName = "Olof",
                LastName = "Bängman",
                Address = "Gördelmakaregatan 18",
                Email = "bängen@hotmail.com",
                Active = true,
                HourlyFee = 352.0,
                Type = CustomerType.Private,
                Registered = DateTime.Parse("2020-02-01")
            },
            new
            {
                Id = 2,
                FirstName = "Karin",
                LastName = "Bertram",
                Address = "Karduansvägen 23",
                Email = "karbert@yahoo.com",
                Active = true,
                HourlyFee = 352.0,
                Type = CustomerType.Private,
                Registered = DateTime.Parse("2018-11-21")
            },
            new
            {
                Id = 3,
                FirstName = "Martina",
                LastName = "Kroon",
                Address = "Apelsingatan 42",
                Email = "einstein@apelsin.nu",
                Active = true,
                HourlyFee = 352.0,
                Type = CustomerType.Private,
                Registered = DateTime.Parse("2018-04-08")
            },
            new
            {
                Id = 4,
                FirstName = "Akademibokhandeln Lerum",
                Address = "Bagges Torg 19",
                Email = "lerum@akademibokhandeln.se",
                Active = true,
                Type = CustomerType.Company,
                Registered = DateTime.Parse("2019-05-28")
            },
            new
            {
                Id = 5,
                FirstName = "Akademibokhandeln Kungälv",
                Address = "Drottninggatan 20",
                Email = "kungalv@akademibokhandeln.se",
                Active = true,
                Type = CustomerType.Company,
                Registered = DateTime.Parse("2019-05-28")
            });
            #endregion

            #region PrivateBilling
            var pb = modelBuilder.Entity<PrivateBilling>();
            pb.HasData(new
            {
                Id = 1,
                Date = DateTime.Parse("2021-01-04")
            });
            #endregion

            #region PrivateBillingItem
            var pbi = modelBuilder.Entity<PrivateBillingItem>();
            pbi.HasData(new
            {
                Id = 1,
                PrivateBillingId = 1,
                CustomerId = 1,
                WeeksAttended = "",
                AmountOccassions = 0,
                HoursPerVisit = "",
                TotalHours = 0.0,
                PricePerHour = 352.0,
                ExVAT = 0.0,
                IncVAT = 0.0,
                AfterRUT = 0.0,
            }, new
            {
                Id = 2,
                PrivateBillingId = 1,
                CustomerId = 2,
                WeeksAttended = "",
                AmountOccassions = 0,
                HoursPerVisit = "",
                TotalHours = 0.0,
                PricePerHour = 352.0,
                ExVAT = 0.0,
                IncVAT = 0.0,
                AfterRUT = 0.0,
            }, new
            {
                Id = 3,
                PrivateBillingId = 1,
                CustomerId = 3,
                WeeksAttended = "",
                AmountOccassions = 0,
                HoursPerVisit = "",
                TotalHours = 0.0,
                PricePerHour = 352.0,
                ExVAT = 0.0,
                IncVAT = 0.0,
                AfterRUT = 0.0,
            });
            #endregion

            #region CompanyBilling
            var cb = modelBuilder.Entity<CompanyBilling>();
            cb.HasData(new
            {
                Id = 1,
                Date = DateTime.Parse("2021-01-04")
            });
            #endregion

            #region CompanyBillingItem
            var cbi = modelBuilder.Entity<CompanyBillingItem>();
            cbi.HasData(new
            {
                Id = 1,
                CompanyBillingId = 1,
                CustomerId = 4,
                Name = "Akademibokhandeln Lerum",
                Email = "lerum@akademibokhandeln.se",
                Notes = "",
                ExVAT = 0.0,
                IncVAT = 0.0
            }, new
            {
                Id = 2,
                CompanyBillingId = 1,
                CustomerId = 5,
                Name = "Akademibokhandeln Kungälv",
                Email = "kungalv@akademibokhandeln.se",
                Notes = "",
                ExVAT = 0.0,
                IncVAT = 0.0
            }); 
            #endregion
        }
    }
}
