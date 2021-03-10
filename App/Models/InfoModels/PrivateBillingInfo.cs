using System;
using System.Collections.Generic;

namespace rp.Accounting.App.Models.InfoModels
{
    public class PrivateBillingInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public ICollection<PrivateBillingItemInfo> Items { get; set; }
    }
}
