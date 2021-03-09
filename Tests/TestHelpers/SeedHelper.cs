using rp.Accounting.Domain;
using System.Collections.Generic;

namespace rp.Accounting.Tests.TestHelpers
{
    public class SeedHelper
    {
        public List<Customer> GetQueryableCustomerMockSet()
            => new List<Customer>()
            {
                new Customer(1, "Pelle", CustomerType.Private),
                new Customer(2, "Martina", CustomerType.Private),
                new Customer(3, "Gunde", CustomerType.Private),
                new Customer(4, "Sverker", CustomerType.Private),
                new Customer(5, "Carina", CustomerType.Private),
                new Customer(6, "Homer", CustomerType.Private),
                new Customer(7, "Lisa", CustomerType.Private),
                new Customer(8, "Apple Göteborg", CustomerType.Company),
                new Customer(9, "Grönsaksboden i Solna", CustomerType.Company)
            };

        public List<PrivateBillingBase> GetQueryablePrivateBillingBaseMockSet()
        {
            var customers = GetQueryableCustomerMockSet();

            var billingBase1 = new PrivateBillingBase(1);
            var billingBase2 = new PrivateBillingBase(2);
            billingBase1.PopulateNew(customers);
            billingBase2.PopulateNew(customers);

            return new List<PrivateBillingBase> { billingBase1, billingBase2 };
        }
    }
}
