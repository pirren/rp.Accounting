using rp.Accounting.Domain;
using System.Collections.Generic;
using System.Linq;

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

        public List<PrivateBilling> GetQueryablePrivateBillingMockSet()
        {
            var customers = GetQueryableCustomerMockSet();

            var billing1 = new PrivateBilling(1);
            var billing2 = new PrivateBilling(2);
            billing1.PopulateNew(customers.Where(c => c.Type == CustomerType.Private && c.Active).ToList());
            billing2.PopulateNew(customers.Where(c => c.Type == CustomerType.Private && c.Active).ToList());

            return new List<PrivateBilling> { billing1, billing2 };
        }

        public List<CompanyBilling> GetQueryableCompanyBillingMockSet()
        {
            var customers = GetQueryableCustomerMockSet();

            var billing1 = new CompanyBilling(1);
            var billing2 = new CompanyBilling(2);
            billing1.PopulateNew(customers.Where(c => c.Type == CustomerType.Company && c.Active).ToList());
            billing2.PopulateNew(customers.Where(c => c.Type == CustomerType.Company && c.Active).ToList());

            return new List<CompanyBilling> { billing1, billing2 };
        }
    }
}
