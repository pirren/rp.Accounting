using System;

namespace rp.Accounting.Domain
{
    public class Customer
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

        public Customer(int id, string firstName, CustomerType type)
        {
            Id = id;
            FirstName = firstName;
            Type = type;
        }

        /// <summary>
        /// Updates the hourly fee for private customer
        /// </summary>
        /// <param name="hourlyFee"></param>
        public void UpdateHourlyPrice(double hourlyFee)
        {
            if (this.Type == CustomerType.Company)
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
