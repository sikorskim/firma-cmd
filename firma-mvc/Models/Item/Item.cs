﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using firma_mvc.Data;

namespace firma_mvc
{
    public class Item
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Jednostka miary")]
        public int UnitOfMeasureId { get; set; }
        [DisplayName("VAT")]
        public int VATId { get; set; }
        [DisplayName("Wartość netto")]
        public decimal Price { get; set; }
        [DisplayName("Wartość brutto")]
        public virtual decimal PriceBrutto
        {
            get { return getBruttoPrice(); }
        }
        [ForeignKey("VATId")]
        [DisplayName("VAT")]
        public virtual VAT VAT { get; set; }
        [ForeignKey("UnitOfMeasureId")]
        [DisplayName("Jednostka miary")]
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }

        decimal getBruttoPrice()
        {
            try
            {
                //return Price + VAT.Value * Price / 100;
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}
