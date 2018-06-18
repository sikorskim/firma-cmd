﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class VATRegisterSell
    {
        public int Id{ get; set; }
        [DisplayName("L.p.")]
        public int Number { get; set; }
        [DisplayName("Data dostawy")]
        public DateTime DeliveryDate { get; set; }
        [DisplayName("Data wystawienia")]
        public DateTime DateOfIssue { get; set; }
        [DisplayName("Numer dokumentu")]
        public string DocumentNumber { get; set; }
        [DisplayName("Kontrahent")]
        public string Contractor { get; set; }

        public string TaxedSellHeader { get { return "Sprzedaż opodatkowana"; } }
        [DisplayName("Brutto")]
        public decimal? ValueBrutto { get; set; }

        public string VAT23Header { get { return "Stawka 23%"; } }
        [DisplayName("Netto")]
        public decimal? ValueNetto23 { get; set; }
        [DisplayName("VAT")]
        public decimal? VATValue23 { get; set; }

        public string VAT7_8Header { get { return "Stawka 7/8%"; } }
        [DisplayName("Netto")]
        public decimal? ValueNetto7_8 { get; set; }
        [DisplayName("VAT")]
        public decimal? VATValue7_8 { get; set; }

        public string VAT3_5Header { get { return "Stawka 3/5%"; } }
        [DisplayName("Netto")]
        public decimal? ValueNetto3_5 { get; set; }
        [DisplayName("VAT")]
        public decimal? VATValue3_5 { get; set; }

        public string VAT0Header { get { return "Stawka 0%"; } }
        [DisplayName("Netto")]
        public decimal? ValueNetto0 { get; set; }

        [DisplayName("Sprzedaż zwolniona z opodatkowania")]
        public decimal? ValueTaxFree { get; set; }

        [DisplayName("Sprzedaż niepodlegająca opodatkowaniu")]
        public decimal? ValueNoTax { get; set; }

        [DisplayName("Suma VAT")]
        public decimal VATSummary { get { return getVATSummary(); } }

        decimal getVATSummary()
        {
            //return VATValue23 + VATValue7_8 + VATValue3_5;
            return 0;
        }
    }
}
