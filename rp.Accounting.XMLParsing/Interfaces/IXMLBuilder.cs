using rp.Accounting.Domain;

namespace rp.Accounting.XMLParsing.Interfaces
{
    public interface IXMLBuilder
    {
        bool BuildBillingBaseXML(TBillingBase billingBase);
    }
}
