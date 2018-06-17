using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace firma_mvc
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        [DisplayName("Produkt")]
        public int ItemId { get; set; }
        [DisplayName("Ilość")]
        public int Quantity { get; set; }
        public virtual decimal TotalPrice { get { return getTotalPrice(); } }
        public virtual decimal TotalPriceBrutto { get { return getTotalPriceBrutto(); } }
        public virtual decimal TotalVATValue { get { return getTotalVATValue(); } }
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }

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
                return Quantity * Item.Price;
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
                 return Quantity * Item.PriceBrutto;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        decimal getTotalVATValue()
        {
            return getTotalPrice() * Item.VAT.Value / 100;
        }
    }
}