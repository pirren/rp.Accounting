using rp.Accounting.Domain;

namespace rp.Accounting.XMLParsing.Interfaces
{
    public interface IXMLParser
    {
        void BuildBillingBaseXML(TBillingBase billingBase);
    }
}
