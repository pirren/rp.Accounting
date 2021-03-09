using ClosedXML.Excel;
using rp.Accounting.Domain;
using rp.Accounting.XMLParsing.Interfaces;
using System;
using System.Linq;

namespace rp.Accounting.XMLParsing
{
    /// <summary>
    /// Holds FileName and URL after successful xml build
    /// </summary>
    public class XMLBuilder : IXMLBuilder, IDisposable
    {
        public string URL { get; private set; }
        public string FileName { get; private set; }

        private const string BASEURL = @"BillingBase";
        const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        /// <summary>
        /// Builds an excel sheet from any BillingBase type
        /// </summary>
        /// <param name="billingBase"></param>
        public void BuildBillingBaseXML(TBillingBase billingBase)
        {
            var urlBuilder = new UrlBuilder(BASEURL, billingBase.Date, "Privat");
            (URL, FileName) = urlBuilder.GetFullUrl();
            if (billingBase is PrivateBillingBase) BuildPrivateXML((PrivateBillingBase)billingBase);
        }

        private void BuildPrivateXML(PrivateBillingBase billingBase)
        {
            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add($"Privat underlag {billingBase.Date.Year} - {billingBase.Date.Month}");
            worksheet.Cell("A1").Value = "Namn";
            worksheet.Cell("B1").Value = "Veckor";
            worksheet.Cell("C1").Value = "Antal gånger";
            worksheet.Cell("D1").Value = "Timmar per gång";
            worksheet.Cell("E1").Value = "Totala timmar";
            worksheet.Cell("F1").Value = "Timpris";
            worksheet.Cell("G1").Value = "Ex. Moms";
            worksheet.Cell("H1").Value = "Ink. Moms";
            worksheet.Cell("I1").Value = "Efter rut";

            var allItems = billingBase.Items.ToList();

            for (int i = 0; i < billingBase.Items.Count; i++)
            {
                var item = allItems[i];
                var rowNumber = i + 2;
                worksheet.Cell($"A{rowNumber}").Value = $"{item.Customer.FirstName} {item.Customer.LastName}";
                worksheet.Cell($"B{rowNumber}").Value = $"{item.WeeksAttended}";
                worksheet.Cell($"C{rowNumber}").Value = $"{item.AmountOccassions}";
                worksheet.Cell($"D{rowNumber}").Value = $"{item.HoursPerVisit}";
                worksheet.Cell($"E{rowNumber}").Value = $"{item.TotalHours}";
                worksheet.Cell($"F{rowNumber}").Value = $"{item.PricePerHour}";
                worksheet.Cell($"G{rowNumber}").Value = $"{item.ExVAT}";
                worksheet.Cell($"H{rowNumber}").Value = $"{item.IncVAT}";
                worksheet.Cell($"I{rowNumber}").Value = $"{item.AfterRUT}";
            }

            workbook.SaveAs($"{URL}/{FileName}");
        }

        public void Dispose()
        {
            FileName = string.Empty;
            URL = string.Empty;
        }

        private class UrlBuilder
        {
            private readonly string _url;
            private readonly string _file;

            public UrlBuilder(string baseUrl, DateTime date, string type)
            {
                _url = $"{baseUrl}/{type}";
                _file = $"fakturaunderlag_{type.ToLower()}_{date.Year}-{date.Month}.xlsx";
            }

            /// <summary>
            /// Returns full url of object
            /// </summary>
            /// <returns>Url, FileName</returns>
            public (string, string) GetFullUrl() => (_url, _file);
        }
    }
}
