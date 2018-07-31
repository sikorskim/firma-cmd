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
using System.Security.Cryptography;
using System.Text;

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
        [DisplayName("Data dostawy")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfDelivery { get; set; }
        [DisplayName("Kontrahent")]
        public int ContractorId { get; set; }
        public int CompanyId { get; set; }
        [DisplayName("Opłacona")]
        public bool Paid { get; set; }
        [DisplayName("Forma płatności")]
        public int PaymentMethodId { get; set; }
        [DisplayName("Termin płatności")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime PaymentDueTerm { get { return getPaymentDueTerm(); } }
        [DisplayName("Ilość pozycji")]
        public virtual int ItemsCount
        {
            get { return countItems(); }
        }
        [DisplayName("Wartość netto")]
        public decimal TotalValue
        {
            get { return getTotalNettPrice(); }
        }
        [DisplayName("Wartość brutto")]
        public decimal TotalValueInclVat
        {
            get { return getTotalPriceBrutto(); }
        }
        public decimal TotalVATValue
        {
            get { return getTotalVATValue(); }
        }
        [DisplayName("Status")]
        public int InvoiceStatusId { get; set; }

        [DisplayName("Kontrahent")]
        [ForeignKey("ContractorId")]
        public virtual Contractor Contractor { get; set; }
        [ForeignKey("InvoiceStatusId")]
        public virtual InvoiceStatus InvoiceStatus { get; set; }
        [DisplayName("Forma płatności")]
        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        // to do: change this to connect with login
        [DisplayName("Firma")]
        [ForeignKey("CompanyId")]
        public virtual Company Company { get; set; }

        public Invoice()
        { }

        int countItems()
        {
            try
            {
                return InvoiceItems.Count();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        decimal getTotalNettPrice()
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

        decimal getTotalPriceBrutto()
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

        decimal getTotalVATValue()
        {
            try
            {
                return InvoiceItems.Sum(p => p.TotalVATValue);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        DateTime getPaymentDueTerm()
        {
            try
            {
                return DateOfIssue.AddDays(PaymentMethod.DueTerm);
            }
            catch (Exception)
            {
                return DateOfIssue;
            }
        }

        public string generate()
        {
            string path = "templates/invoice.xml";
            XDocument doc = XDocument.Load(path);
            XElement root = doc.Element("Template");

            string header = root.Element("Header").Value;
            header = string.Format(header, Company.Name, Company.FullAddress, Company.Phone, Company.Email, Company.Website, Company.BankName, Company.BankAccountNumber);

            string cityOfIssue = root.Element("DatePlace").Value;
            string dateOfIssue = DateOfIssue.ToShortDateString();
            string dateOfDelivery = DateOfDelivery.ToShortDateString();
            cityOfIssue = string.Format(cityOfIssue, dateOfIssue, Company.InvoiceIssueCity, dateOfDelivery);

            string title = root.Element("Title").Value;
            title = string.Format(title, Number);

            string sellerBuyer = root.Element("SellerBuyer").Value;
            sellerBuyer = string.Format(sellerBuyer, Company.FullName, Company.FullAddress, Company.NIP, Contractor.FullName, Contractor.FullAddress, Contractor.NIP);

            header += cityOfIssue += title += sellerBuyer;

            string invoiceItemsTable = root.Element("InvoiceItemsTableHeader").Value;
            string invoiceItemsSummary = root.Element("InvoiceItemTableSummary").Value;
            string invoiceItem = root.Element("InvoiceItem").Value;

            int i = 1;
            foreach (InvoiceItem item in InvoiceItems)
            {
                string newItem = string.Format(invoiceItem, i, item.Name, item.UnitOfMeasureShortName, item.Quantity, item.Price.ToString("0.00"), item.TotalPrice.ToString("0.00"), item.VATValue.ToString("0.00"), item.TotalVATValue.ToString("0.00"), item.TotalPriceBrutto.ToString("0.00"));
                invoiceItemsTable += newItem;
                i++;
            }
            invoiceItemsSummary = string.Format(invoiceItemsSummary, TotalValue.ToString("0.00"), TotalVATValue.ToString("0.00"), TotalValueInclVat.ToString("0.00"));
            invoiceItemsTable += invoiceItemsSummary;

            string taxTableHeader = root.Element("TaxTableHeader").Value;
            string tax = root.Element("Tax").Value;
            tax = string.Format(tax, "23", TotalValue.ToString("0.00"), TotalVATValue.ToString("0.00"), TotalValueInclVat.ToString("0.00"));
            string taxTableSummary = root.Element("TaxTableSummary").Value;
            taxTableSummary = string.Format(taxTableSummary, TotalValue.ToString("0.00"), TotalVATValue.ToString("0.00"), TotalValueInclVat.ToString("0.00"));
            string taxTable = taxTableHeader + tax + taxTableSummary;

            string priceSummary = root.Element("PriceSummary").Value;
            priceSummary = string.Format(priceSummary, TotalValueInclVat.ToString("0.00"), getValueInWords(TotalValueInclVat));
            string paymentMethod = root.Element("PaymentMethod").Value;
            paymentMethod = string.Format(paymentMethod, PaymentMethod.Name, DateOfIssue.AddDays(PaymentMethod.DueTerm).ToShortDateString());
            string issuer = root.Element("Issuer").Value;
            issuer = string.Format(issuer, Company.InvoiceIssuerName);
            string footer = paymentMethod + issuer;


            //string output = header + invoiceItemsTable + priceSummary + footer;
            string output = header + invoiceItemsTable + taxTable + priceSummary + footer;

            output = output.Replace("~^~^", "{{");
            output = output.Replace("^~^~", "}}");
            output = output.Replace("~^", "{");
            output = output.Replace("^~", "}");


            string time = DateTime.Now.ToFileTime().ToString();

            string outputFile = Tools.getHash(sellerBuyer + time);
            File.WriteAllText("tmp/" + outputFile + ".tex", output);

            Process process = new Process();
            process.StartInfo.WorkingDirectory = "tmp";
            process.StartInfo.FileName = "pdflatex";
            process.StartInfo.Arguments = outputFile + ".tex";
            process.Start();

            // wait to avoid FileNotFoundException
            //Task.Delay(5000);
            process.Dispose();
            return outputFile + ".pdf";
        }

        string getValueInWords(decimal d)
        {
            string inputString = d.ToString("0.00");
            string[] parts;
            int i1;
            int i2;

            try
            {
                parts = inputString.Split(',');
                i1 = int.Parse(parts[0]);
                i2 = int.Parse(parts[1]);
            }
            catch (FormatException)
            {
                parts = inputString.Split('.');
                i1 = int.Parse(parts[0]);
                i2 = int.Parse(parts[1]);
            }

            string valueInWords = "";

            if (i1 >= 1 && i1 <= 9)
            {
                valueInWords = getOnes(i1);
            }
            else if (i1 >= 10 && i1 <= 19)
            {
                valueInWords = getTeens(i1);
            }
            else if (i1 >= 20 && i1 <= 99)
            {
                valueInWords = getDoubles(i1);
            }
            else if (i1 >= 100 && i1 <= 999)
            {
                valueInWords = getTriplets(i1);
            }
            else if (i1 >= 1000 && i1 <= 99999)
            {
                valueInWords = getThousands(i1);
            }

            valueInWords += " " + "PLN " + i2.ToString() + "/100";

            return valueInWords;
        }

        string getOnes(int i)
        {
            string[] ones = { "jeden", "dwa", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć" };
            return ones[i - 1];
        }

        string getTeens(int i)
        {
            string[] teens = { "dziesięć", "jedenaście", "dwanaście", "trzynaście", "czternaście", "piętnaście", "szesnaście", "siedemnaście", "osiemanście", "dziewiętnaście" };
            return teens[i - 10];
        }

        string getDoubles(int i)
        {
            string[] doubles = { "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt" };
            string output = doubles[i / 10 - 2];
            int i1 = i % 10;
            if (i1 != 0)
            {
                output += " " + getOnes(i1);
            }

            return output;
        }

        string getTriplets(int i)
        {
            string[] triplets = { "sto", "dwieście", "trzysta", "czterysta", "pięćset", "sześćset", "siedemset", "osiemset", "dziewięćset" };
            string output = triplets[i / 100 - 1];
            int i1 = i % 100;
            if (i1 != 0)
            {
                if (i1 >= 10 && i1 <= 19)
                {
                    output += " " + getTeens(i1);
                }
                if (i1 >= 20 && i1 <= 99)
                {
                    output += " " + getDoubles(i1);
                }
            }
            return output;
        }

        string getThousands(int i)
        {
            string[] thousands = { "tysiąc", "tysiące", "tysięcy" };
            string output = "";
            int i1 = i / 1000;
            if (i1 == 1)
            {
                output = thousands[0];
            }
            else if (i1 >= 2 && i1 <= 4)
            {
                output = getOnes(i1) + " " + thousands[1];
            }
            else if (i1 >= 5 && i1 <= 9)
            {
                output = getOnes(i1) + " " + thousands[2];
            }
            else if (i1 >= 10 && i1 <= 19)
            {
                output = getTeens(i1) + " " + thousands[2];
            }
            else if (i1 >= 20 && i1 <= 99)
            {
                output = getDoubles(i1);
                int i2 = Int32.Parse(i1.ToString()[1].ToString());
                if (i2 >= 2 && i2 <= 4)
                {
                    output += " " + thousands[1];
                }
                else
                {
                    output += " " + thousands[2];
                }
            }

            int i3 = i % 1000;
            if (i3 != 0)
            {
                output += " " + getTriplets(i3);
            }

            return output;
        }
    }
}