using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class IncomeTax
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public bool Paid { get; set; }
        public decimal Value { get; set; }

        // tax scale
        public decimal compute(decimal income, decimal costs)
        {
            decimal taxRelief = 556.02M;
            decimal socialSecurityContribution = 519.28M;
            decimal healthSecurityRelief = 275.51M;
            decimal taxRate = 0.18M;

            decimal tax = (income - costs - socialSecurityContribution) * taxRate - taxRelief-healthSecurityRelief;
            tax = Math.Round(tax);
            return tax;
        }
    }
}
