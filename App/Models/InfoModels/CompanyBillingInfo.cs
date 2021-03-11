using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace rp.Accounting.App.Models.InfoModels
{
    public class CompanyBillingInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<CompanyBillingItemInfo> Items { get; set; }
    }

    public class CompanyBillingItemInfo
    {
        public int Id { get; set; }
        public int CompanyBillingId { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; }
        public string ExVAT { get; set; }
        public double IncVAT { get; set; }
    }
}
