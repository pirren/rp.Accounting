using ClosedXML.Excel;
using rp.Accounting.Domain;
using rp.Accounting.XMLParsing.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace rp.Accounting.XMLParsing
{
    public class CompanyXMLSheet : IXMLSheet
    {
        public IXLWorksheet Worksheet { get; set; }

        public void BuildHeader()
        {
            Worksheet.Range("A1", "E1").Style.Fill.SetBackgroundColor(XLColor.GreenPigment);
            Worksheet.Row(1).Style.Font.SetBold(true);

            Worksheet.Cell("A1").SetValue("Företag");
            Worksheet.Cell("B1").SetValue("Email");
            Worksheet.Cell("C1").SetValue("Noteringar");
            Worksheet.Cell("D1").SetValue("Ex Moms");
            Worksheet.Cell("E1").SetValue("Ink Moms");
        }

        public void BuildItems<T>(IEnumerable<T> items, ref int rowNumber) where T : class
        {
            var allItems = items.Cast<CompanyBillingItem>().ToList();
            for (int i = 0; i < allItems.Count; i++)
            {
                var item = allItems[i];
                rowNumber = i + 2;
                Worksheet.Cell($"A{rowNumber}").SetValue($"{item.Customer.FirstName}");
                Worksheet.Cell($"B{rowNumber}").SetValue($"{item.Email}");
                Worksheet.Cell($"C{rowNumber}").SetValue($"{item.Notes}");
                Worksheet.Cell($"D{rowNumber}").SetValue($"{item.ExVAT}");
                Worksheet.Cell($"E{rowNumber}").SetValue($"{item.IncVAT}");
            }
        }

        public void BuildTotal<T>(IEnumerable<T> items, ref int rowNumber) where T : class
        {
            var allItems = items.Cast<CompanyBillingItem>().ToList();

            var exVatList = allItems.Where(i => i.ExVAT > 0).Select(s => s.ExVAT).ToList();
            var exVatSum = exVatList.Any() ? exVatList.Aggregate((x, y) => x + y) : 0.0;

            var incVatList = allItems.Where(i => i.IncVAT > 0).Select(s => s.IncVAT).ToList();
            var incVatSum = incVatList.Any() ? incVatList.Aggregate((x, y) => x + y) : 0.0;

            rowNumber++;
            Worksheet.Row(rowNumber).Style.Font.SetBold(true);
            Worksheet.Cell($"A{rowNumber}").SetValue("Totalt");
            Worksheet.Cell($"D{rowNumber}").SetValue(exVatSum);
            Worksheet.Cell($"E{rowNumber}").SetValue(incVatSum);
        }
    }
}
