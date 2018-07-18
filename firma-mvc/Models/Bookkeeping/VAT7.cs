using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class VAT7
    {
        public int Id { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public bool Paid { get; set; }
        public decimal Value { get; set; }

        public decimal compute(decimal owing, decimal charged)
        {
            owing = Math.Round(owing);
            charged = Math.Round(charged);
            
            return owing-charged;
        }
    }
}
