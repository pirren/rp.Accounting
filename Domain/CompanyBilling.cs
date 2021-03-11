using System;
using System.Collections.Generic;
using System.Linq;

namespace rp.Accounting.Domain
{
    public class CompanyBilling : IBilling
    {
        public int Id { get; private set; }
        public DateTime Date { get; } = DateTime.Now;
        public ICollection<CompanyBillingItem> Items { get; private set; }

        public CompanyBilling()
        { }

        public CompanyBilling(int id) { Id = id; }

        /// <summary>
        /// Removes a Customer from the BillingBase
        /// </summary>
        /// <param name="id">Id of Customer</param>
        /// <returns>Customer was found and successfully removed</returns>
        public bool RemoveCustomer(int id)
        {
            var item = Items.FirstOrDefault(i => i.Id == id);
            if (item is null) return false;
            Items.Remove(item);
            return true;
        }

        public CompanyBilling ClearInactiveCustomers(List<Customer> inactiveCustomers)
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
        /// Enters all Company customers that aren't in the CompanyBilling
        /// </summary>
        /// <param name="customers">All company customers</param>
        /// <returns>CompanyBilling</returns>
        public CompanyBilling EnterUnhousedCustomers(List<Customer> customers)
        {
            if (this.Items is null) throw new InvalidOperationException("This is a non green-field operation. Only populated objects can have additional fields.");
            var unhoused = customers.Where(c => c.Active && !this.Items.Select(p => p.Customer.Id).Contains(c.Id));
            foreach (var customer in unhoused)
                Items.Add(new CompanyBillingItem(this, customer));
            return this;
        }

        /// <summary>
        /// Populates a new CompanyBilling
        /// </summary>
        /// <param name="customers">All company customers</param>
        /// <returns>CompanyBilling</returns>
        public CompanyBilling PopulateNew(List<Customer> customers)
        {
            if (customers is null) throw new ArgumentNullException(nameof(customers));
            if (this.Items is not null) throw new InvalidOperationException("This is a green-field operation. Only new objects can be populated.");
            this.Items = new List<CompanyBillingItem>();
            foreach (var customer in customers.Where(c => c.Active))
                Items.Add(new CompanyBillingItem(this, customer));
            return this;
        }
    }

    public class CompanyBillingItem : IBillingItem, IIdentifier
    {
        public int Id { get; private set; }
        public CompanyBilling CompanyBilling { get; private set; }
        public Customer Customer { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Notes { get; set; } = "";
        public double ExVAT { get; set; }
        public double IncVAT { get; private set; }

        /// <summary>
        /// Sets IncVAT price based on ExVAT
        /// </summary>
        /// <returns>Changes were made</returns>
        public bool CalculatePrice()
        {
            if (ExVAT <= 0) return false;
            IncVAT = ExVAT * 1.25;
            return true;
        }

        public CompanyBillingItem() { }

        public CompanyBillingItem(CompanyBilling billingBase, Customer customer)
        {
            Customer = customer;
            CompanyBilling = billingBase;
        }

        public CompanyBillingItem(int id, CompanyBilling billingBase, Customer customer)
        {
            Id = id;
            Customer = customer;
            CompanyBilling = billingBase;
        }
    }
}
