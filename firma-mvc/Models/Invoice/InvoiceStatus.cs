using System.ComponentModel;

namespace firma_mvc
{ 
    public class InvoiceStatus
    {
        public int Id { get; set; }
        [DisplayName("Status")]
        public string Name { get; set; }                
    }
}