using rp.Accounting.Domain;
using System.Collections.Generic;

namespace rp.Accounting.XMLParsing.Interfaces
{
    public interface IXMLSheet
    {
        void BuildHeader();
        void BuildItems<T>(IEnumerable<T> items, ref int rowNumber) where T : class;
        void BuildTotal<T>(IEnumerable<T> items, ref int rowNumber) where T : class;
    }
}
