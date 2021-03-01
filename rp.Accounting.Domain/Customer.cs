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
        public double HourlyPrice { get; private set; }
        public CustomerType Type { get; private set; }
        public DateTime Registered { get; } = DateTime.Now;

        public Customer(int id, string firstName, CustomerType type)
        {
            Id = id;
            FirstName = firstName;
            Type = type;
        }

        /// <summary>
        /// Updates the HourlyPrice of private customers
        /// </summary>
        /// <param name="hourlyPrice"></param>
        public void UpdateHourlyPrice(double hourlyPrice)
        {
            if(this.Type == CustomerType.Private)
            this.HourlyPrice = hourlyPrice;
        }
    }

    public enum CustomerType
    {
        Private,
        Company
    }
}
