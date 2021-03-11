using System;
using System.Collections.Generic;

namespace rp.Accounting.Domain
{
    public interface IBilling
    {
        public int Id { get; }
        public DateTime Date { get; }

        bool RemoveCustomer(int id);
    }

    public interface IBillingItem
    {
        public Customer Customer { get; }

        bool CalculatePrice();
    }
}
