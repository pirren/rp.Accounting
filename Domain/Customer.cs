using System;
using System.Collections.Generic;

namespace rp.Accounting.Domain
{
    public class Customer : IIdentifier
    {
        public int Id { get; private set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; } = true;
        public double? HourlyFee { get; private set; }
        public CustomerType Type { get; private set; }
        public DateTime Registered { get; } = DateTime.Now;
        public ICollection<PrivateBillingItem> Items { get; set; }

        public Customer(string firstName, CustomerType type)
        {
            FirstName = firstName;
            Type = type;
        }

        public Customer(int id, string firstName, CustomerType type)
        {
            Id = id;
            FirstName = firstName;
            Type = type;
        }

        /// <summary>
        /// Sets the lastname only for private customers
        /// </summary>
        /// <param name="lastName"></param>
        public void SetLastName(string lastName)
        {
            if (this.Type is CustomerType.Company)
                return;
            this.LastName = lastName;
        }

        /// <summary>
        /// Updates the hourly fee for private customer
        /// </summary>
        /// <param name="hourlyFee"></param>
        public void UpdateHourlyPrice(double hourlyFee)
        {
            if (this.Type is CustomerType.Company)
                return;
            this.HourlyFee = hourlyFee;
        }
    }

    public enum CustomerType
    {
        Private,
        Company
    }
}
