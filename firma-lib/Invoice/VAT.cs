using System.ComponentModel;

namespace firma_lib
{
    public class VAT
    {        
        public int Id { get; set; }
        [DisplayName("Wartość")]
        public decimal Value { get; set; }
    }
}