using ClosedXML.Excel;
using rp.Accounting.Domain;
using rp.Accounting.XMLParsing.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace rp.Accounting.XMLParsing
{
    /// <summary>
    /// Holds FileName and URL after successful xml build
    /// </summary>
    public class XMLBuilder : IXMLBuilder
    {
        public string URL { get; private set; }
        public string FileName { get; private set; }

        private const string BASEURL = @"Fakturaunderlag";

        /// <summary>
        /// Builds an excel sheet from any BillingBase type
        /// </summary>
        /// <param name="billing"></param>
        public bool BuildBillingBaseXML(TBilling billing)
        {
            URL = @$"{BASEURL}\{billing.Date:yyyy-MMM}";

            if (billing is PrivateBilling privateBilling)
            {
                FileName = $"fakturaunderlag_privat_{billing.Date:yyyy-MMM}.xlsx";
                return BuildPrivateXML(privateBilling);
            }
            return false;
        }

        private bool BuildPrivateXML(PrivateBilling billing)
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add($"Privat underlag {billing.Date:yyyy-MMM}");
            var privateSheet = new PrivateXMLSheet { Worksheet = ws };
            
            int rowNumber = 0;
            var allItems = billing.Items.ToList();
            privateSheet.BuildHeader();
            privateSheet.BuildItems(allItems, ref rowNumber);
            privateSheet.BuildTotal(allItems, ref rowNumber);

            ws.Columns("A", "I").AdjustToContents();

            try
            {
                workbook.SaveAs($"{URL}/{FileName}");
                return true;
            } catch { return false;  }
        }
    }
}
