using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace firma_mvc
{
    public class InvoiceItem : Item
    {
        public int InvoiceId { get; set; }
        [DisplayName("Ilość")]
        public int Quantity { get; set; }
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }

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
                 return Quantity * PriceBrutto;
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