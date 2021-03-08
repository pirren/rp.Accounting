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

        public void BuildBillingBaseXML(TBillingBase baseObject)
        {
            var billingBasee = ExtractType(baseObject);

            var urlBuilder = new UrlBuilder(BASE_URL, billingBasee.Date, "Privat");
            (_url, _file) = urlBuilder.GetFullUrl();

            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add($"Privat underlag {billingBasee.Date.Year} - {billingBasee.Date.Month}");
            worksheet.Cell("A1").Value = "Hello World!";
            worksheet.Cell("A2").FormulaA1 = "=MID(A1, 7, 5)";
            workbook.SaveAs($"{_url}/{_file}");
        }

        private static TBillingBase ExtractType(TBillingBase billingBase)
            => billingBase switch
            {
                PrivateBillingBase => billingBase as PrivateBillingBase,
                _ => billingBase as PrivateBillingBase
            };
        

        public void Dispose()
        {
            _file = string.Empty;
            _url = string.Empty;
        }
    }

    class UrlBuilder
    {
        private readonly string _url;
        private readonly string _file;

        public UrlBuilder(string baseUrl, DateTime date, string type)
        {
            _url = $"{baseUrl}/{type}";
            _file = $"fakturaunderlag_{type.ToLower()}_{date.Year}-{date.Month}.xlsx";
        }

        public (string, string) GetFullUrl() => (_url, _file);
    }
}
