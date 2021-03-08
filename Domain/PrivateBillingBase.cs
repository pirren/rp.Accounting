using System;
using System.Collections.Generic;
using System.Linq;

namespace rp.Accounting.Domain
{
    public class PrivateBillingBase : IIdentifier, TBillingBase
    {
        public int Id { get; private set; }
        public DateTime Date { get; private set; } = DateTime.Now;
        public ICollection<PrivateBillingBaseItem> Items { get; private set; }

        /// <summary>
        /// Updates the HourlyPrices of all customers
        /// </summary>
        /// <returns>Updated BillingBase</returns>
        public PrivateBillingBase UpdateHourlyPrices()
        {
            if (Items is null) return this;
            foreach (var item in this.Items)
                if (item.Customer.HourlyFee.HasValue)
                    item.PricePerHour = (double)item.Customer.HourlyFee;
            return this;
        }

        /// <summary>
        /// Clears out inactive customers from the BillingBase
        /// </summary>
        /// <param name="inactiveCustomers">List of inactive customers</param>
        /// <returns>Cleared BillingBase</returns>
        public PrivateBillingBase ClearInactiveCustomers(List<Customer> inactiveCustomers)
        {
            if (inactiveCustomers is null) return this;
            foreach (var customer in inactiveCustomers.Where(c => this.Items.Select(p => p.Customer.Id).Contains(c.Id)))
            {
                var obj = Items.Where(e => e.Customer == customer).FirstOrDefault();
                Items.Remove(obj);
            }
            return this;
        }

        /// <summary>
        /// Adds any unhoused customers to the BillingBase as new fields
        /// </summary>
        /// <param name="customers">List of private customers</param>
        /// <returns>Housed BillingBase</returns>
        public PrivateBillingBase EnterUnhousedCustomers(List<Customer> customers)
        {
            if (this.Items is null) throw new InvalidOperationException("This is a non green-field operation. Only populated objects can have additional fields.");
            var unhoused = customers.Where(c => c.Active && !this.Items.Select(p => p.Customer.Id).Contains(c.Id));
            foreach (var customer in unhoused)
                Items.Add(new PrivateBillingBaseItem(this, customer) { PricePerHour = customer.HourlyFee ?? 0.0 });
            return this;
        }

        /// <summary>
        /// Populates the BillingBase with customers
        /// </summary>
        /// <param name="customers">List of private customers</param>
        /// <returns>Populated BillingBase</returns>
        public PrivateBillingBase PopulateNew(List<Customer> customers)
        {
            if (customers is null) throw new ArgumentNullException(nameof(customers));
            if (this.Items is not null) throw new InvalidOperationException("This is a green-field operation. Only new objects can be populated.");
            this.Items = new List<PrivateBillingBaseItem>();
            foreach (var customer in customers.Where(c => c.Active))
                Items.Add(new PrivateBillingBaseItem(this, customer) { PricePerHour = customer.HourlyFee ?? 0.0 });
            return this;
        }

        public PrivateBillingBase() { }
    }

    public class PrivateBillingBaseItem : IIdentifier
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
        /// <returns>Changes were made</returns>
        public bool CalculatePrice()
        {
            if (TotalHours <= 0 || PricePerHour <= 0)
                return false;
            ExVAT = TotalHours * PricePerHour;
            IncVAT = ExVAT * 1.25;
            AfterRUT = IncVAT / 2;
            return true;
        }

        public PrivateBillingBaseItem() { }

        public PrivateBillingBaseItem(PrivateBillingBase billingBase, Customer customer)
        {
            Customer = customer;
            PrivateBillingBase = billingBase;
        }

        public PrivateBillingBaseItem(int id, PrivateBillingBase privateBillingBase, Customer customer)
        {
            Id = id;
            Customer = customer;
            PrivateBillingBase = privateBillingBase;
        }
    }
}
