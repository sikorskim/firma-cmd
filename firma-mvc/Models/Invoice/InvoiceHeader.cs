using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace firma_mvc
{
    public class InvoiceHeader
    {
        public int Id { get; set; }
        [DisplayName("Numer faktury")]
        public string Number { get; set; }
        [DisplayName("Data wystawienia")]
        public DateTime DateOfIssue { get; set; }
        public int ContractorId { get; set; }
        [DisplayName("Kontrahent")]
        [ForeignKey("ContractorId")]
        public virtual Contractor Contractor { get; set; }
        public int PaymentMethodId { get; set; }
        [DisplayName("Forma płatności")]
        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod PaymentMethod { get; set; }
        [DisplayName("Ilość pozycji")]
        public int ItemsCount { get; set; }
        [DisplayName("Wartość netto")]
        public decimal TotalValue { get; set; }
        [DisplayName("Wartość brutto")]
        public decimal TotalValueInclVat { get; set; }
    }
}