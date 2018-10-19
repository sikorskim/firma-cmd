using firma_mvc.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace firma_mvc
{
    public class VATRegisterSell
    {
        public int Id{ get; set; }
        [DisplayName("L.p.")]
        public int Number { get; set; }
        [DisplayName("Data dostawy")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }
        [DisplayName("Data wystawienia")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfIssue { get; set; }
        [DisplayName("Numer dokumentu")]
        public string DocumentNumber { get; set; }
        [DisplayName("Kontrahent")]
        public int ContractorId { get; set; }

        public string TaxedSellHeader { get { return "Sprzedaż opodatkowana"; } }
        [DisplayName("Brutto")]
        public decimal? ValueBrutto { get; set; } = 0;

        public string VAT23Header { get { return "Stawka 23%"; } }
        [DisplayName("Netto")]
        public decimal? ValueNetto23 { get; set; } = 0;
        [DisplayName("VAT")]
        public decimal? VATValue23 { get; set; } = 0;

        public string VAT7_8Header { get { return "Stawka 7/8%"; } }
        [DisplayName("Netto")]
        public decimal? ValueNetto7_8 { get; set; } = 0;
        [DisplayName("VAT")]
        public decimal? VATValue7_8 { get; set; } = 0;

        public string VAT3_5Header { get { return "Stawka 3/5%"; } }
        [DisplayName("Netto")]
        public decimal? ValueNetto3_5 { get; set; } = 0;
        [DisplayName("VAT")]
        public decimal? VATValue3_5 { get; set; } = 0;

        public string VAT0Header { get { return "Stawka 0%"; } }
        [DisplayName("Netto")]
        public decimal? ValueNetto0 { get; set; } = 0;

        [DisplayName("Sprzedaż zwolniona z opodatkowania")]
        public decimal? ValueTaxFree { get; set; } = 0;

        [DisplayName("Sprzedaż niepodlegająca opodatkowaniu")]
        public decimal? ValueNoTax { get; set; } = 0;

        [DisplayName("Suma VAT")]
        public decimal VATSummary { get { return getVATSummary(); } }
        [DisplayName("Miesiąc")]
        public int Month { get; set; }
        [DisplayName("Rok")]
        public int Year { get; set; }

        [DisplayName("Kontrahent")]
        [ForeignKey("ContractorId")]
        public virtual Contractor Contractor { get; set; }

        public VATRegisterSell()
        { }

        public VATRegisterSell(DateTime dt)
        {
            DateOfIssue = dt.Date;
            DeliveryDate = dt.Date;
        }

        decimal getVATSummary()
        {
            return (decimal)VATValue23 + (decimal)VATValue7_8 + (decimal)VATValue3_5;            
        }

        public int getOrderNumber(ApplicationDbContext _context)
        {
            try
            {
               return _context.VATRegisterSell.Where(p => p.Month == Month && p.Year == Year).Last().Number + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public string generate (ApplicationDbContext _context, int year, int month)
        {
            string path = "templates/vatRegisterSell.xml";
            XDocument doc = XDocument.Load (path);
            XElement root = doc.Element ("Template");

            Company company = _context.Company.FirstOrDefault ();
            var vatRegisterSellItems = _context.VATRegisterSell.Include (i => i.Contractor).Where (p => p.Year == year && p.Month == month).ToList ();

            var monthDict = Tools.getMonthsDictionary ();

            string header = root.Element ("Header").Value;
            header = string.Format (header, monthDict[month], year, company.FullName, company.FullAddress, company.NIP);

            string tableHeader = root.Element ("TableHeader").Value;
            string tableSummary = root.Element ("TableSummary").Value;
            string tableRow = root.Element ("TableRow").Value;

            decimal totalBrutto = 0;
            decimal totalNetto23 = 0;
            decimal totalVat23 = 0;
            decimal totalNetto7_8 = 0;
            decimal totalVat7_8 = 0;
            decimal totalNetto3_5 = 0;
            decimal totalVat3_5 = 0;
            decimal totalNetto0 = 0;
            decimal totalTaxFree= 0;
            decimal totalNoTax = 0;
            decimal totalVat =0;

            foreach (VATRegisterSell item in vatRegisterSellItems)
            {
                string documentNo = Tools.handleLatexSpecialChars (item.DocumentNumber);

                decimal bruttoVal = (decimal) item.ValueBrutto;
                decimal netto23Val = (decimal) item.ValueNetto23;
                decimal vat23Val = (decimal) item.VATValue23;
                decimal netto7_8Val = (decimal) item.VATValue7_8;
                decimal vat7_8Val = (decimal) item.VATValue7_8;
                decimal netto3_5Val = (decimal) item.ValueNetto3_5;
                decimal vat3_5Val = (decimal) item.VATValue3_5;
                decimal netto0Val = (decimal) item.ValueNetto0;
                decimal taxFreeVal = (decimal) item.ValueTaxFree;
                decimal noTaxVal = (decimal) item.ValueNoTax;

                string newItem = string.Format (tableRow, item.Number, item.DeliveryDate.ToShortDateString (), item.DateOfIssue.ToShortDateString(), documentNo, item.Contractor.FullName, bruttoVal.ToString ("0.00"), netto23Val.ToString ("0.00"), vat23Val.ToString ("0.00"), netto7_8Val.ToString ("0.00"), vat7_8Val.ToString ("0.00"), netto3_5Val.ToString ("0.00"), vat3_5Val.ToString ("0.00"), netto0Val.ToString ("0.00"), taxFreeVal.ToString ("0.00"), noTaxVal.ToString ("0.00"), item.VATSummary.ToString ("0.00"));

                tableHeader += newItem;

                totalBrutto+=bruttoVal;
                totalNetto23+=netto23Val;
                totalVat23+=vat23Val;
                totalNetto7_8+=netto7_8Val;
                totalVat7_8+=vat7_8Val;
                totalNetto3_5+=netto3_5Val;
                totalVat3_5+=vat3_5Val;
                totalNetto0+=netto0Val;
                totalTaxFree+=taxFreeVal;
                totalNoTax+=noTaxVal;
                totalVat+=item.VATSummary;
            }

            tableSummary = string.Format (tableSummary, totalBrutto.ToString ("0.00"), totalNetto23.ToString ("0.00"), totalVat23.ToString ("0.00"), totalNetto7_8.ToString ("0.00"), totalVat7_8.ToString ("0.00"), totalNetto3_5.ToString ("0.00"), totalVat3_5.ToString ("0.00"), totalNetto0.ToString ("0.00"), totalTaxFree.ToString ("0.00"), totalNoTax.ToString ("0.00"), totalVat.ToString ("0.00"));
            tableHeader += tableSummary;

            string footer = root.Element ("Footer").Value;
            string output = header + tableHeader + footer;

            output = output.Replace ("~^~^~^", "{{{");
            output = output.Replace ("^~^~^~", "}}}");
            output = output.Replace ("~^~^", "{{");
            output = output.Replace ("^~^~", "}}");
            output = output.Replace ("~^", "{");
            output = output.Replace ("^~", "}");

            string time = DateTime.Now.ToFileTime ().ToString ();

            string outputFile = Tools.getHash (header + time);
            File.WriteAllText ("tmp/" + outputFile + ".tex", output);

            Process process = new Process ();
            process.StartInfo.WorkingDirectory = "tmp";
            process.StartInfo.FileName = "pdflatex";
            process.StartInfo.Arguments = outputFile + ".tex";
            process.Start ();

            process.Dispose ();
            return outputFile + ".pdf";
        }

        public string getDownloadFilename (int year, int month)
        {
            return "rejestrVAT" + year + month;
        }       
    }
}
