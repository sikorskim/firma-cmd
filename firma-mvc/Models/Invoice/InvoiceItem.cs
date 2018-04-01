using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace firma_mvc
{
    public class InvoiceItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public int ItemId { get; set; }                
        [DisplayName("Ilość")]
        public int Quantity { get; set; }
        
        [ForeignKey("InvoiceId")]
        public virtual Invoice Invoice { get; set; }
        [ForeignKey("ItemId")]
        public virtual Item Item { get; set; }
        
        public InvoiceItem(int invoiceId)
        {
            InvoiceId = invoiceId;
        }
    }
}