using firma_mvc.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class IncomeTax
    {
        public int Id { get; set; }
        [DisplayName("Rok")]
        public int Year { get; set; }
        [DisplayName("Miesiąc")]
        public int Month { get; set; }
        [DisplayName("Opłacone")]
        public bool Paid { get; set; }
        [DisplayName("Zaliczka na podatek")]
        public decimal Value { get; set; }
        [DisplayName("Przychód")]
        public decimal Income { get; set; }
        [DisplayName("Koszty")]
        public decimal Loss { get; set; }
        [DisplayName("Dochód narastająco")]
        public decimal IncomeIncr { get; set; }
        [DisplayName("Ubezpieczenie społeczne")]
        public decimal SocialSecContr { get; set; }
        [DisplayName("Ubezpieczenie społeczne narastająco")]
        public decimal SocialSecContrIncr { get; set; }
        [DisplayName("Ubezpieczenie zdrowotne")]
        public decimal HealthSec { get; set; }
        [DisplayName("Ubezpieczenie zdrowotne narastająco")]
        public decimal HealthSecIncr { get; set; }

        public IncomeTax()
        { }

        public IncomeTax(int year, int month)
        {
            Year = year;
            Month = month;
        }

        // tax scale
        // dane z księgi?
        public IncomeTax compute(ApplicationDbContext _context)
        {
            decimal taxRelief = getTaxRelief(_context);            
            SocialSecContr = getSocialSecContr(_context);
            SocialSecContrIncr = getSocialSecContrIncr(_context);
            HealthSec = getHealthSec(_context);
            HealthSecIncr = getHealthSecIncr(_context);
            decimal taxRate = 0.18M;
            decimal paidTax = getPaidTax(_context);

            Income = getIncome(_context);
            Loss = getCosts(_context);
            IncomeIncr = getIncomeIncr(_context);

            Value = (IncomeIncr - SocialSecContrIncr) * taxRate - taxRelief - HealthSecIncr-paidTax;
            Value = Math.Round(Value);
            return this;
        }

        decimal getPaidTax(ApplicationDbContext _context)
        {
            decimal paidTax = 0;
            try
            {
                return paidTax = _context.IncomeTax.Where(p => p.Year == Year).Sum(p => p.Value);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        decimal getCosts(ApplicationDbContext _context)
        {
            return _context.VATRegisterBuy.Where(p => p.Month == Month && p.Year == Year).Sum(p => p.ValueNetto);
        }

        decimal getIncome(ApplicationDbContext _context)
        {
            return (decimal)_context.VATRegisterSell.Where(p => p.Month == Month && p.Year == Year).Sum(p => p.ValueNetto23);
        }

        decimal getIncomeIncr(ApplicationDbContext _context)
        {
            decimal incomeIncr = 0;

            try
            {
                incomeIncr = _context.IncomeTax.Last(p => p.Year == Year).IncomeIncr+getIncome(_context)-getCosts(_context);

            }
            catch (Exception e)
            {
                Console.WriteLine("getIncomeIncr() " + e.Message);
                incomeIncr = Income-getCosts(_context);
            }

            return incomeIncr;
        }

        decimal getSocialSecContr(ApplicationDbContext _context)
        {
            return Decimal.Parse(_context.Parameter.Single(p => p.Name == "zus").Value);
        }

        decimal getSocialSecContrIncr(ApplicationDbContext _context)
        {
            decimal socialSecContrIncr = 0;
            try
            {
                socialSecContrIncr = _context.IncomeTax.Last(p => p.Year == Year).SocialSecContrIncr + getSocialSecContr(_context);
            }
            catch (Exception e)
            {
                Console.WriteLine("getSocialSecIncr() " + e.Message);
                socialSecContrIncr = getSocialSecContr(_context);
            }
            return socialSecContrIncr;
        }

        decimal getHealthSec(ApplicationDbContext _context)
        {
            return Decimal.Parse(_context.Parameter.Single(p => p.Name == "odl_ub_zdrow").Value);
        }

        decimal getHealthSecIncr(ApplicationDbContext _context)
        {
            decimal healthSecIncr = 0;

            try
            {
                healthSecIncr = _context.IncomeTax.Last(p => p.Year == Year).HealthSecIncr + getHealthSec(_context);
            }
            catch (Exception e)
            {
                Console.WriteLine("getHealthSecIncr() " + e.Message);
                healthSecIncr = getHealthSec(_context);
            }
            return healthSecIncr;
        }

        decimal getTaxRelief(ApplicationDbContext _context)
        {
            return Decimal.Parse(_context.Parameter.Single(p => p.Name == "ulga_podatkowa").Value);
        }
    }
}