using System.Collections.Generic;

namespace jpk_check
{
    public class Invoice
    {
        public int Id { get; set; }
        public int InvoiceHeaderId { get; set; }
        public virtual InvoiceHeader InvoiceHeader { get; set; }
        public int InvoiceStatusId { get; set; }
        public virtual InvoiceStatus InvoiceStatus{get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }                
    }
}