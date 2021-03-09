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
        /// <param name="billingBase"></param>
        public bool BuildBillingBaseXML(TBillingBase billingBase)
        {
            URL = @$"{BASEURL}\{billingBase.Date:yyyy-MMM}";

            if (billingBase is PrivateBillingBase)
            {
                FileName = $"fakturaunderlag_privat_{billingBase.Date:yyyy-MMM}.xlsx";
                return BuildPrivateXML((PrivateBillingBase) billingBase);
            }
            return false;
        }

        private bool BuildPrivateXML(PrivateBillingBase billingBase)
        {
            using var workbook = new XLWorkbook();
            var ws = workbook.Worksheets.Add($"Privat underlag {billingBase.Date:yyyy-MMM}");
            var privateSheet = new PrivateXMLSheet { Worksheet = ws };
            
            int rowNumber = 0;
            var allItems = billingBase.Items.ToList();
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
