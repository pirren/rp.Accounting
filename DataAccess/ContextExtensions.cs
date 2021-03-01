using Microsoft.EntityFrameworkCore;
using rp.Accounting.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rp.Accounting.DataAccess
{
    public static class ContextExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var cust = modelBuilder.Entity<Customer>();
            cust.HasData(new
            {
                Id = 1,
                FirstName = "Olof",
                LastName = "Bängman",
                Address = "Gördelmakaregatan 18",
                Email = "bängen@hotmail.com",
                HourlyPrice = 352.0,
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
                HourlyPrice = 352.0,
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
                HourlyPrice = 352.0,
                Type = CustomerType.Private,
                Registered = DateTime.Parse("2018-04-08")
            },
            new
            {
                Id = 4,
                FirstName = "Akademibokhandeln Lerum",
                Address = "Bagges Torg 19",
                Email = "lerum@akademibokhandeln.se",
                Type = CustomerType.Company,
                Registered = DateTime.Parse("2019-05-28")
            },
            new
            {
                Id = 5,
                FirstName = "Akademibokhandeln Kungälv",
                Address = "Drottninggatan 20",
                Email = "kungalv@akademibokhandeln.se",
                Type = CustomerType.Company,
                Registered = DateTime.Parse("2019-05-28")
            });
        }
    }
}
