using rp.Accounting.Domain;

namespace rp.Accounting.XMLParsing.Interfaces
{
    public interface IXMLBuilder
    {
        void BuildBillingBaseXML(TBillingBase billingBase);
    }
}
