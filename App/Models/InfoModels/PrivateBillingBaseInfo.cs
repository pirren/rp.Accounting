using System;
using System.Collections.Generic;

namespace rp.Accounting.App.Models.InfoModels
{
    public class PrivateBillingBaseInfo
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } 
        public ICollection<PrivateBillingBaseItemInfo> Items { get; set; }
    }
}
