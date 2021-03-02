using System;
using System.Collections.Generic;

namespace rp.Accounting.Domain
{
    public class PrivateBillingBase
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; } = DateTime.Now;
        public ICollection<PrivateBillingBaseItem> Items { get; private set; }
    }

    public class PrivateBillingBaseItem
    {
        public int Id { get; private set; }
        public PrivateBillingBase PrivateBillingBase { get; private set; }
        public Customer Customer { get; private set; }
        public string WeeksAttended { get; set; }
        public int AmountOccassions { get; set; }
        public string HoursPerVisit { get; set; }
        public int TotalHours { get; set; }
        public double PricePerHour { get; set; }
        public double ExVAT { get; set; }
        public double IncVAT { get; set; }
        public double AfterRUT { get; set; }
    }
}
