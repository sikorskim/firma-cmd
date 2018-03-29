using System.ComponentModel;

namespace firma_mvc
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Termin płatności")]
        public int DueTerm { get; set; }
    }
}