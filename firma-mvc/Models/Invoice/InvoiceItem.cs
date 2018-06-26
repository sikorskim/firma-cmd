using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace firma_mvc
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        [DisplayName("Ilość")]
        public int Quantity { get; set; }
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
        [NotMapped]
        public int ItemId { get; set; }
        //[RegularExpression(@"^\d*(\.|,|(\.\d{1,2})|(,\d{1,2}))?$", ErrorMessage = "Nieprawidłowy format!")]
        public decimal Price { get; set; }
        public string Name { get; set; }
        //[RegularExpression(@"^\d*(\.|,|(\.\d{1,2})|(,\d{1,2}))?$", ErrorMessage = "Nieprawidłowy format!")]
        public decimal VATValue { get; set; }
        public string UnitOfMeasureShortName { get; set; }
        public virtual decimal TotalPrice { get { return getTotalPrice(); } }
        public virtual decimal TotalPriceBrutto { get { return getTotalPriceBrutto(); } }
        public virtual decimal TotalVATValue { get { return getTotalVATValue(); } }

        public InvoiceItem()
        { }

        public InvoiceItem(int invoiceId)
        {
            InvoiceId = invoiceId;
        }

        decimal getTotalPrice()
        {
            try
            {
                return Quantity * Price;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        decimal getTotalPriceBrutto()
        {
            try
            {
                //return Quantity * PriceBrutto;
                return 0;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        decimal getTotalVATValue()
        {
            //   return getTotalPrice() * Item.VAT.Value / 100;
            return 0;
        }
    }
}