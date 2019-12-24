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
    // based on: 
    // ROZPORZĄDZENIE MINISTRA FINANSÓW z dnia 31 marca 2016 r. zmieniające rozporządzenie w sprawie prowadzenia podatkowej księgi przychodów i rozchodów

    public class TaxBook
    {
        public int Id { get; set; }

        // [DisplayName ("L.p.")]
        // public int Number { get; set; }

        [DisplayName("Data zdarzenia gospodarczego")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DisplayName("Nr dowodu księgowego")]
        public string InvoiceNumber { get; set; }
        #region Contractor
        [DisplayName("Kontrahent")]
        public int ContractorId { get; set; }
        #endregion
        [DisplayName("Opis zdarzenia gospodarczego")]
        public string Description { get; set; }
        #region Income
        public string IncomeHeader { get { return "Przychód"; } }

        [DisplayName("Wartość sprzedanych towarów i usług")]
        public decimal? SellValue { get; set; } = 0;
        [DisplayName("Pozostałe przychody")]
        public decimal? OtherIncome { get; set; } = 0;
        [DisplayName("Razem przychód")]
        public decimal? TotalIncome { get { return SellValue + OtherIncome; } }
        #endregion
        [DisplayName("Zakup towarów handlowych i materiałów wg cen zakupu")]
        public decimal? GoodsBuys { get; set; } = 0;
        [DisplayName("Koszty uboczne zakupu")]
        public decimal? BuysSideEffects { get; set; } = 0;
        #region Costs
        public string CostsHeader { get { return "Wydatki"; } }

        [DisplayName("Wynagrodzenia w gotówce i w naturze")]
        public decimal? Salary { get; set; } = 0;
        [DisplayName("Pozostałe wydatki")]
        public decimal? OtherCosts { get; set; } = 0;
        [DisplayName("Razem wydatki")]
        public decimal? TotalCosts { get { return Salary + OtherCosts; } }

        [DisplayName("")]
        public decimal? Column15 { get; set; } = 0;
        #endregion
        #region Research costs
        public string ResearchCostsHeader { get { return "Koszty działalności badawczo-rozwojowej, o których mowa w art. 26c ustawy o podatku dochodowym"; } }

        [DisplayName("Opis kosztu")]
        public string CostDescription { get; set; }

        [DisplayName("Wartość")]
        public decimal? ResearchCostValue { get; set; } = 0;
        #endregion
        [DisplayName("Uwagi")]
        public string Comments { get; set; }

        [DisplayName("Kontrahent")]
        [ForeignKey("ContractorId")]
        public virtual Contractor Contractor { get; set; }

        // public int getOrderNumber (ApplicationDbContext _context)
        // {
        //     try
        //     {
        //         return _context.TaxBookItem.Where (p => p.Date.Month == DateTime.Now.Month && p.Date.Year == DateTime.Now.Year).Last ().Number + 1;
        //     }
        //     catch (Exception)
        //     {
        //         return 1;
        //     }
        // }

        public string generate(ApplicationDbContext _context, int year, int month)
        {
            string path = "templates/taxBook.xml";
            XDocument doc = XDocument.Load(path);
            XElement root = doc.Element("Template");

            string dateTimeFormat = "yyyy-MM-dd";

            var taxBookItems = _context.TaxBookItem.Include(p => p.Contractor).Where(p => p.Date.Year == year && p.Date.Month == month).OrderBy(p => p.Date).ToList();

            var monthDict = Tools.getMonthsDictionary();

            string header = root.Element("Header").Value;
            header = string.Format(header, monthDict[month], year);

            string tableHeader = root.Element("TableHeader").Value;
            string tableSummary = root.Element("TableSummary").Value;
            string tableRow = root.Element("TableRow").Value;

            decimal totalSellVall = 0;
            decimal totalOtherInc = 0;
            decimal totalTotalInc = 0;
            decimal totalGoodsBuy = 0;
            decimal totalBuysSideEff = 0;
            decimal totalSalary = 0;
            decimal totalOtherCos = 0;
            decimal totalTotalCos = 0;
            decimal totalCol15 = 0;
            decimal totalResearchCos = 0;

            int i = 1;
            foreach (TaxBook item in taxBookItems)
            {
                string documentNo = Tools.handleLatexSpecialChars(item.InvoiceNumber);

                decimal sellVal = (decimal)item.SellValue;
                decimal otherInc = (decimal)item.OtherIncome;
                decimal totalInc = (decimal)item.TotalIncome;
                decimal goodsBuy = (decimal)item.GoodsBuys;
                decimal buysSideEff = (decimal)item.BuysSideEffects;
                decimal salary = (decimal)item.Salary;
                decimal otherCos = (decimal)item.OtherCosts;
                decimal totalCos = (decimal)item.TotalCosts;
                decimal col15 = (decimal)item.Column15;
                decimal researchCos = (decimal)item.ResearchCostValue;

                string newItem = string.Format(tableRow, i, item.Date.ToString(dateTimeFormat), documentNo, Tools.handleLatexSpecialChars(item.Contractor.FullName), item.Contractor.FullAddress, item.Description, sellVal.ToString("0.00"), otherInc.ToString("0.00"), totalInc.ToString("0.00"), goodsBuy.ToString("0.00"), buysSideEff.ToString("0.00"), salary.ToString("0.00"), otherCos.ToString("0.00"), totalCos.ToString("0.00"), col15.ToString("0.00"), item.CostDescription, researchCos.ToString("0.00"), item.Comments);

                tableHeader += newItem;

                totalSellVall += sellVal;
                totalOtherInc += otherInc;
                totalTotalInc += totalInc;
                totalGoodsBuy += goodsBuy;
                totalBuysSideEff += buysSideEff;
                totalSalary += salary;
                totalOtherCos += otherCos;
                totalTotalCos += totalCos;
                totalCol15 += col15;
                totalResearchCos += researchCos;

                i++;
            }

            tableSummary = string.Format(tableSummary, totalSellVall.ToString("0.00"), totalOtherInc.ToString("0.00"), totalTotalInc.ToString("0.00"), totalGoodsBuy.ToString("0.00"), totalBuysSideEff.ToString("0.00"), totalSalary.ToString("0.00"), totalOtherCos.ToString("0.00"), totalTotalCos.ToString("0.00"), totalCol15.ToString("0.00"), totalResearchCos.ToString("0.00"));
            tableHeader += tableSummary;

            string output = header + tableHeader;

            output = output.Replace("~^~^~^", "{{{");
            output = output.Replace("^~^~^~", "}}}");
            output = output.Replace("~^~^", "{{");
            output = output.Replace("^~^~", "}}");
            output = output.Replace("~^", "{");
            output = output.Replace("^~", "}");

            string time = DateTime.Now.ToFileTime().ToString();

            string outputFile = Tools.getHash(header + time);
            File.WriteAllText("tmp/" + outputFile + ".tex", output);

            Process process = new Process();
            process.StartInfo.WorkingDirectory = "tmp";
            process.StartInfo.FileName = "pdflatex";
            process.StartInfo.Arguments = "-synctex=1 -interaction=nonstopmode " + outputFile + ".tex";
            process.Start();
            process.Dispose();

            return outputFile + ".pdf";
        }

        public string getDownloadFilename(int year, int month)
        {
            return "ksiega" + year + month;
        }

        public decimal[] getLast12mIncome(ApplicationDbContext context)
        {
            decimal[] last12mIncome = new decimal[12];
            int currMonth = DateTime.Now.Month;
            int currYear = DateTime.Now.Year;
            int i = 0;

            foreach (decimal d in last12mIncome)
            {
                decimal income = (decimal)context.TaxBookItem.Where(p => p.Date.Year == currYear && p.Date.Month == currMonth).Sum(p => p.TotalIncome);
                last12mIncome[i]=income;                

                currMonth--;
                if (currMonth < 1)
                {
                    currMonth = 12;
                    currYear--;
                }
                i++;
            }

            last12mIncome=last12mIncome.Reverse().ToArray();
            return last12mIncome;
        }

        public decimal[] getLast12mCosts(ApplicationDbContext context)
        {
            decimal[] last12mCosts = new decimal[12];
            int currMonth = DateTime.Now.Month;
            int currYear = DateTime.Now.Year;
            int i = 0;

            foreach (decimal d in last12mCosts)
            {
                decimal costs = (decimal)context.TaxBookItem.Where(p => p.Date.Year == currYear && p.Date.Month == currMonth).Sum(p => p.TotalCosts + p.GoodsBuys);
                last12mCosts[i]=costs;                

                currMonth--;
                if (currMonth < 1)
                {
                    currMonth = 12;
                    currYear--;
                }
                i++;
            }

            last12mCosts=last12mCosts.Reverse().ToArray();
            return last12mCosts;
        }

        public decimal[] getLast12mNettIncome(ApplicationDbContext context)
        {
            decimal[] last12mNettIncome = new decimal[12];
            int i = 0;

            foreach (decimal d in last12mNettIncome)
            {
                decimal nettIncome = getLast12mIncome(context)[i]-getLast12mCosts(context)[i];
                last12mNettIncome[i]=nettIncome;                

                i++;
            }

            return last12mNettIncome;
        }

    }
}