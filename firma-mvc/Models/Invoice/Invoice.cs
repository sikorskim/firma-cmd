using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using firma_mvc.Data;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class Invoice
    {
        public int Id { get; set; }
        [DisplayName("Numer faktury")]
        public string Number { get; set; }
        [DisplayName("Data wystawienia")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfIssue { get; set; }
        [DisplayName("Kontrahent")]
        public int ContractorId { get; set; }
        [DisplayName("Forma płatności")]
        public int PaymentMethodId { get; set; }     
        [DisplayName("Ilość pozycji")]
        public virtual int ItemsCount
        {
            get { return countItems(); }
        }
        [DisplayName("Wartość netto")]
        public decimal TotalValue 
        {
            get { return countTotalNettPrice(); }
        }
        [DisplayName("Wartość brutto")]
        public decimal TotalValueInclVat
        {
            get { return countTotalPriceBrutto(); }
        }
        [DisplayName("Status")]
        public int InvoiceStatusId { get; set; }

        [DisplayName("Kontrahent")]
        [ForeignKey("ContractorId")]            
        public virtual Contractor Contractor { get; set; }    
        [ForeignKey("InvoiceStatusId")]
        public virtual InvoiceStatus InvoiceStatus {get; set; }
        [DisplayName("Forma płatności")]
        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }            

        public Invoice()
        {}
        
        int countItems()
        {
            try
            {
                return InvoiceItems.Count();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        decimal countTotalNettPrice()
        {
            try
            {
                return InvoiceItems.Sum(p => p.TotalPrice);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        decimal countTotalPriceBrutto()
        {
            try
            {
                return InvoiceItems.Sum(p => p.TotalPriceBrutto);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public static string getTotalValueInWords()
        {
            string[] value;


            //   TotalValueBrutto.ToString().Split('.');
            //TotalValueBrutto.ToString().Split(',');


            //string beforePoint = ;

            string valueInWords = "";

            string[] ones = { "jeden", "dwa", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć" };
            string[] teens = { "jedenaście", "dwanaście", "trzynaście", "czternaście", "piętnaście", "szesnaście", "siedemnaście", "osiemanście", "dziewiętnaście" };
            string[] doubles = { "dziesięć", "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt" };
            string[] triplets = { "sto", "dwieście", "trzysta", "czterysta", "pięćset", "sześćset", "siedemset", "osiemset", "dziewięćset" };
            string[] thousands = { "tysiąc", "tysięcy" };
            string[] millions = { "milion", "milionów" };



            return valueInWords;
        }

        public void generate()
        {
            string path = "invoiceTemplate.xml";
            XDocument doc = XDocument.Load(path);
            XElement root = doc.Element("Template");

            string header = root.Element("Header").Value;
            //header = string.Format(header, Company.Name, Company.FullAddress, Company.NIP, Company.REGON, Company.Phone, Company.Email, Company.Website, Company.BankName, Company.BankAccount);

            string cityOfIssue = root.Element("DatePlace").Value;
            string dateOfIssue = DateOfIssue.ToShortDateString();
            //string dateOfDelivery = DateOfDelivery.ToShortDateString();
            //cityOfIssue = string.Format(cityOfIssue, dateOfIssue, IssuePlace, dateOfDelivery);

            string title = root.Element("Title").Value;
            title = string.Format(title, Number);

            string sellerBuyer = root.Element("SellerBuyer").Value;
            //sellerBuyer = string.Format(sellerBuyer, Company.FullName, Company.FullAddress, Company.NIP, Contractor.FullName, Contractor.FullAddress, Contractor.NIP);

            header += cityOfIssue += title += sellerBuyer;

            string invoiceItemsTable = root.Element("InvoiceItemsTableHeader").Value;
            string invoiceItemsSummary = root.Element("InvoiceItemTableSummary").Value;
            string invoiceItem = root.Element("InvoiceItem").Value;

            int i = 1;
            foreach (InvoiceItem item in InvoiceItems)
            {
              //  string newItem = string.Format(invoiceItem, i, item.Item.Name, item.Item.UnitOfMeasure, item.Quantity, item.Item.UnitPrice.ToString("0.00"), item.ValueNetto.ToString("0.00"), item.Item.VatRate.ToString("0.00"), item.VATValue.ToString("0.00"), item.ValueBrutto.ToString("0.00"));
              //  Console.WriteLine(newItem);
              //  invoiceItemsTable += newItem;
                i++;
            }
            //invoiceItemsSummary = string.Format(invoiceItemsSummary, TotalValueNetto.ToString("0.00"), TotalVATValue.ToString("0.00"), TotalValueBrutto.ToString("0.00"));
            invoiceItemsTable += invoiceItemsSummary;

            string taxTableHeader = root.Element("TaxTableHeader").Value;
            string tax = root.Element("Tax").Value;
            string taxTableSummary = root.Element("TaxTableSummary").Value;
            string taxTable = taxTableHeader + tax + taxTableSummary;

            string priceSummary = root.Element("PriceSummary").Value;
            //priceSummary = string.Format(priceSummary, TotalValueBrutto.ToString("0.00"), "słownie złotych", "groszy");
            string paymentMethod = root.Element("PaymentMethod").Value;
            //paymentMethod = string.Format(paymentMethod, PaymentMethod.Name, DateOfIssue.AddDays(PaymentMethod.DueDate).ToShortDateString());
            string issuer = root.Element("Issuer").Value;
            //issuer = string.Format(issuer, Company.IssuerName);
            string footer = paymentMethod + issuer;


            string output = header + invoiceItemsTable + taxTable + priceSummary + footer;

            output = output.Replace("~^~^", "{{");
            output = output.Replace("^~^~", "}}");
            output = output.Replace("~^", "{");
            output = output.Replace("^~", "}");
            File.WriteAllText("out.tex", output);
            
            // wait to avoid FileNotFoundException
            Task.Delay(1000);

            Process process = new Process();
            process.StartInfo.FileName = "pdflatex";
            process.StartInfo.Arguments = "out.tex";
            process.Start();
            process.Dispose();
        }

    }
}