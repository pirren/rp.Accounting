using System;
using System.Collections.Generic;

namespace rp.Accounting.Domain
{
    public interface TBilling
    {
        public int Id { get; }
        public DateTime Date { get; }
    }

    public interface TBillingItem
    { }
}
