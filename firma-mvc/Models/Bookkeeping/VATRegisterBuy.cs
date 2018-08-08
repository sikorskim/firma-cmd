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
using firma_mvc.Data;
using Microsoft.EntityFrameworkCore;

namespace firma_mvc
{
    public class VATRegisterBuy
    {
        public int Id { get; set; }

        [DisplayName ("L.p.")]
        public int Number { get; set; }

        [DisplayName ("Data dostawy")]
        [DisplayFormat (DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }

        [DisplayName ("Data wystawienia")]
        [DisplayFormat (DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfIssue { get; set; }

        [DisplayName ("Numer dokumentu")]
        public string DocumentNumber { get; set; }

        [DisplayName ("Kontrahent")]
        public int ContractorId { get; set; }

        [DisplayName ("Wartość brutto")]
        public decimal ValueBrutto { get; set; } = 0;
        [DisplayName ("Wartość netto")]
        public decimal ValueNetto { get; set; } = 0;
        [DisplayName ("Kwota podatku podlegającego odliczeniu")]
        public decimal? TaxDeductibleValue { get; set; } = 0;
        [DisplayName ("Wartość zakupów nieopodatkowanych")]
        public decimal? TaxFreeBuysValue { get; set; } = 0;
        [DisplayName ("Wartość zakupów, od których podatek VAT nie podlega odliczeniu")]
        public decimal? NoTaxDeductibleBuysValue { get; set; } = 0;

        [DisplayName ("Kontrahent")]
        [ForeignKey ("ContractorId")]
        public virtual Contractor Contractor { get; set; }

        [DisplayName ("Miesiąc")]
        public int Month { get; set; }

        [DisplayName ("Rok")]
        public int Year { get; set; }

        public int getOrderNumber (ApplicationDbContext _context)
        {
            try
            {
                return _context.VATRegisterBuy.Where (p => p.Month == Month && p.Year == Year).Last ().Number + 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        // TODO:
        // fix slash in tex file rendering 
        public string generate (ApplicationDbContext _context, int year, int month)
        {
            string path = "templates/vatRegisterBuy.xml";
            XDocument doc = XDocument.Load (path);
            XElement root = doc.Element ("Template");

            Company company = _context.Company.FirstOrDefault ();
            var vatRegisterBuyItems = _context.VATRegisterBuy.Include (i => i.Contractor).Where (p => p.Year == year && p.Month == month).ToList ();

            var monthDict = Tools.getMonthsDictionary();

            string header = root.Element ("Header").Value;
            header = string.Format (header, company.FullName, monthDict[month], year, company.FullAddress, company.NIP);

            string tableHeader = root.Element ("TableHeader").Value;
            string tableSummary = root.Element ("TableSummary").Value;
            string tableRow = root.Element ("TableRow").Value;

            decimal totalValueBrutto = 0;
            decimal totalValueNetto = 0;
            decimal totalTaxDeductibleValue = 0;
            decimal totalTaxFreeBuysValue = 0;
            decimal totalNoTaxDeductibleValue = 0;

            foreach (VATRegisterBuy item in vatRegisterBuyItems)
            {
                decimal taxDeductibleVal = (decimal) item.TaxDeductibleValue;
                decimal taxFreeBuysVal = (decimal) item.TaxFreeBuysValue;
                decimal noTaxDeductibleVal = (decimal) item.NoTaxDeductibleBuysValue;

                string newItem = string.Format (tableRow, item.Number, item.DeliveryDate, item.DateOfIssue, item.DocumentNumber, item.Contractor.FullName, item.ValueBrutto.ToString ("0.00"), item.ValueNetto.ToString ("0.00"), taxDeductibleVal.ToString ("0.00"), taxFreeBuysVal.ToString ("0.00"), noTaxDeductibleVal.ToString ("0.00"));

                tableHeader += newItem;
                totalValueBrutto += item.ValueBrutto;
                totalValueNetto += item.ValueNetto;
                totalTaxDeductibleValue += taxDeductibleVal;
                totalTaxFreeBuysValue += taxFreeBuysVal;
                totalNoTaxDeductibleValue += noTaxDeductibleVal;
            }

            tableSummary = string.Format (tableSummary, totalValueBrutto.ToString ("0.00"), totalValueNetto.ToString ("0.00"), totalTaxDeductibleValue.ToString ("0.00"), totalTaxFreeBuysValue.ToString("0.00"), totalNoTaxDeductibleValue.ToString("0.00"));
            tableHeader+=tableSummary;
           
            string footer = root.Element ("Footer").Value;                                  
            string output = header + tableHeader + footer;

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

        public string getDownloadFilename(int year, int month)
        {
            return "vatRejZakupu" + year + month;
        }
    }
}