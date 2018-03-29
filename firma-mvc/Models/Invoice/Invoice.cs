using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace firma_mvc
{
    public class Invoice
    {
        public int Id { get; set; }
        public int InvoiceHeaderId { get; set; }
        [ForeignKey("InvoiceHeaderId")]
        public virtual InvoiceHeader InvoiceHeader { get; set; }
        public int InvoiceStatusId { get; set; }
        [ForeignKey("InvoiceStatusId")]
        public virtual InvoiceStatus InvoiceStatus{get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}