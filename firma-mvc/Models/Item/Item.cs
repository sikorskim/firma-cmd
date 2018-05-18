using System;
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
        [DisplayName("Wartość VAT")]
        public int VATId { get; set; }
        [DisplayName("Wartość netto")]
        public decimal Price { get; set; }
        [DisplayName("Wartość brutto")]
        public virtual decimal PriceBrutto
        {
            get { return getBruttoPrice(); }
        }

        [ForeignKey("VATId")]
        public virtual VAT VAT { get; set; }
        [ForeignKey("UnitOfMeasureId")]
        public virtual UnitOfMeasure UnitOfMeasure { get; set; }



        decimal getBruttoPrice()
        {
            //            try
            //            {
            return Price;// + _context.VAT.Single(p=>p.Id==VATId).Value / 100 * Price;
                         //            }
                         //            catch (Exception e)
                         //            {
                         //                return 0;
                         //            }
        }
    }
}
