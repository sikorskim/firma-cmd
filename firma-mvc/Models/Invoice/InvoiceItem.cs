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
        //[NotMapped]
        //public int ItemId { get; set; }
        //[RegularExpression(@"^\d*(\.|,|(\.\d{1,2})|(,\d{1,2}))?$", ErrorMessage = "Nieprawidłowy format!")]
        [DataType(DataType.Currency)]
        [DisplayName("Cena")]
        public decimal Price { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        //[RegularExpression(@"^\d*(\.|,|(\.\d{1,2})|(,\d{1,2}))?$", ErrorMessage = "Nieprawidłowy format!")]
        [DataType(DataType.Currency)]
        //[DisplayFormat(DataFormatString = "{0:E}", ApplyFormatInEditMode = true)]
        [DisplayName("Stawka VAT")]
        public decimal VATValue { get; set; }
        [DisplayName("Jednostka")]
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
                return getTotalPrice()+getTotalVATValue();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        decimal getTotalVATValue()
        {
            return getTotalPrice() * VATValue / 100;
            return 0;
        }
    }
}