using ClosedXML.Excel;
using rp.Accounting.Domain;
using rp.Accounting.XMLParsing.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace rp.Accounting.XMLParsing
{
    public class PrivateXMLSheet : IXMLSheet
    {
        public IXLWorksheet Worksheet { get; set; }

        public void BuildHeader()
        {
            Worksheet.Range("A1", "I1").Style.Fill.SetBackgroundColor(XLColor.GreenPigment);
            Worksheet.Row(1).Style.Font.SetBold(true);

            Worksheet.Cell("A1").SetValue("Namn");
            Worksheet.Cell("B1").SetValue("Veckor");
            Worksheet.Cell("C1").SetValue("Antal gånger");
            Worksheet.Cell("D1").SetValue("Timmar per gång");
            Worksheet.Cell("E1").SetValue("Totala timmar");
            Worksheet.Cell("F1").SetValue("Timpris");
            Worksheet.Cell("G1").SetValue("Ex. Moms");
            Worksheet.Cell("H1").SetValue("Ink. Moms");
            Worksheet.Cell("I1").SetValue("Efter rut");
        }

        public void BuildItems<T>(IEnumerable<T> items, ref int rowNumber) where T : class
        {
            var allItems = items.Cast<PrivateBillingItem>().ToList();
            for (int i = 0; i < allItems.Count; i++)
            {
                var item = allItems[i];
                rowNumber = i + 2;
                Worksheet.Cell($"A{rowNumber}").SetValue($"{item.Customer.FirstName} {item.Customer.LastName}");
                Worksheet.Cell($"B{rowNumber}").SetValue($"{item.WeeksAttended}");
                Worksheet.Cell($"C{rowNumber}").SetValue($"{item.AmountOccassions}");
                Worksheet.Cell($"D{rowNumber}").SetValue($"{item.HoursPerVisit}");
                Worksheet.Cell($"E{rowNumber}").SetValue($"{item.TotalHours}");
                Worksheet.Cell($"F{rowNumber}").SetValue($"{item.PricePerHour}");
                Worksheet.Cell($"G{rowNumber}").SetValue($"{item.ExVAT}");
                Worksheet.Cell($"H{rowNumber}").SetValue($"{item.IncVAT}");
                Worksheet.Cell($"I{rowNumber}").SetValue($"{item.AfterRUT}");
            }
        }

        public void BuildTotal<T>(IEnumerable<T> items, ref int rowNumber) where T : class
        {
            var allItems = items.Cast<PrivateBillingItem>().ToList();

            var exVatList = allItems.Where(i => i.ExVAT > 0).Select(s => s.ExVAT).ToList();
            var exVatSum = exVatList.Any() ? exVatList.Aggregate((x, y) => x + y) : 0.0;

            var incVatList = allItems.Where(i => i.IncVAT > 0).Select(s => s.IncVAT).ToList();
            var incVatSum = incVatList.Any() ? incVatList.Aggregate((x, y) => x + y) : 0.0;

            var afterRutList = allItems.Where(i => i.AfterRUT > 0).Select(s => s.AfterRUT).ToList();
            var afterRutSum = afterRutList.Any() ? afterRutList.Aggregate((x, y) => x + y) : 0.0;

            rowNumber++;
            Worksheet.Row(rowNumber).Style.Font.SetBold(true);
            Worksheet.Cell($"A{rowNumber}").SetValue("Totalt");
            Worksheet.Cell($"G{rowNumber}").SetValue(exVatSum);
            Worksheet.Cell($"H{rowNumber}").SetValue(incVatSum);
            Worksheet.Cell($"I{rowNumber}").SetValue(afterRutSum);
        }
    }
}
