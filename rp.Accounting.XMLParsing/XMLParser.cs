using ClosedXML.Excel;
using rp.Accounting.Domain;
using rp.Accounting.XMLParsing.Interfaces;
using System;
using System.Threading.Tasks;

namespace rp.Accounting.XMLParsing
{
    public class XMLParser : IXMLParser, IDisposable
    {
        private string _url;
        private string _file;

        public string URL => _url;
        public string File => _file;

        private const string BASE_URL = @"BillingBase";

        public void BuildPrivateBillingBaseXML(PrivateBillingBase billingBase)
        {
            var urlBuilder = new UrlBuilder(BASE_URL, billingBase.Date, "Privat");

            _file = urlBuilder.File;
            _url = urlBuilder.Url;

            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add($"Privat underlag {billingBase.Date.Year} - {billingBase.Date.Month}");
            worksheet.Cell("A1").Value = "Hello World!";
            worksheet.Cell("A2").FormulaA1 = "=MID(A1, 7, 5)";
            workbook.SaveAs($"{_url}/{_file}");
        }

        public void Dispose()
        {
            _file = string.Empty;
            _url = string.Empty;
        }
    }

    class UrlBuilder
    {
        public string Url { get; private set; }
        public string File { get; private set; }
        public UrlBuilder(string baseUrl, DateTime date, string type)
        {
            Url = $"{baseUrl}/{type}";
            File = $"fakturaunderlag_{type.ToLower()}_{date.Year}-{date.Month}.xlsx";
        }
    }
}
