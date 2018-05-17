using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class Item
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Jednostka miary")]
        public int UnitOfMeasureId { get; set; }
        [DisplayName("Wartość VAT")]
        public int VATId { get; set; }
        [DisplayName("Wartość")]
        public decimal Price { get; set; }
        //public virtual decimal PriceBrutto { get { return Price + VAT.Value; } }
        
        [ForeignKey("VATId")]
        public virtual VAT VAT { get; set; }
        [ForeignKey("UnitOfMeasureId")]
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }
    }
}
