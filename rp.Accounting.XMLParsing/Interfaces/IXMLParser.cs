using rp.Accounting.Domain;
using System.Threading.Tasks;

namespace rp.Accounting.XMLParsing.Interfaces
{
    public interface IXMLParser
    {
        void BuildPrivateBillingBaseXML(PrivateBillingBase billingBase);
    }
}
