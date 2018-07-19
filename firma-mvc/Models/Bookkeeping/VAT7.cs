﻿using firma_mvc.Data;
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

        public decimal compute(ApplicationDbContext _context)
        {
            decimal owing = (decimal)_context.VATRegisterSell.Where(p => p.Month == Month && p.Year == Year).Sum(p => p.VATValue23 + p.VATValue7_8 + p.VATValue3_5);
            decimal charged = (decimal)_context.VATRegisterBuy.Where(p => p.Month == Month && p.Year == Year).Sum(p => p.TaxDeductibleValue);
            owing = Math.Round(owing);
            charged = Math.Round(charged);
            decimal toPay = owing - charged;
            return toPay;
        }
    }
}