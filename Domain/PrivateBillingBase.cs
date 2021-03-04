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
        public double TotalHours { get; set; }
        public double PricePerHour { get; set; }
        public double ExVAT { get; private set; }
        public double IncVAT { get; private set; }
        public double AfterRUT { get; private set; }

        /// <summary>
        /// Calculates the price based on TotalHours and PricePerHour
        /// </summary>
        public bool CalculatePrice()
        {
            if (TotalHours <= 0 || PricePerHour <= 0)
                return false;
            ExVAT = TotalHours * PricePerHour;
            IncVAT = ExVAT * 1.25;
            AfterRUT = IncVAT / 2;
            return true;
        }

        public PrivateBillingBaseItem(int id, PrivateBillingBase billingBase, Customer customer)
        {
            Id = id;
            Customer = customer;
            PrivateBillingBase = billingBase;
        }
    }
}
