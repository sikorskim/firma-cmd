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
        public int UnitOfMeasureId { get; set; }
        [DisplayName("Jednostka miary")]
        [ForeignKey("UnitOfMeasureId")]
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }
        public int VATId { get; set; }
        [ForeignKey("VATId")]
        public virtual VAT VAT { get; set; }
        public decimal Price { get; set; }
        //public virtual decimal PriceBrutto { get { return Price + VAT.Value; } }
    }
}
