using firma_mvc.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class VATRegisterBuy
    {
        public int Id { get; set; }
        [DisplayName("L.p.")]
        public int Number { get; set; }
        [DisplayName("Data dostawy")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DeliveryDate { get; set; }
        [DisplayName("Data wystawienia")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfIssue { get; set; }
        [DisplayName("Numer dokumentu")]
        public string DocumentNumber { get; set; }
        [DisplayName("Kontrahent")]
        public int ContractorId { get; set; }
        [DisplayName("Wartość brutto")]
        public decimal ValueBrutto { get; set; } = 0;
        [DisplayName("Wartość netto")]
        public decimal ValueNetto { get; set; } = 0;
        [DisplayName("Kwota podatku podlegającego odliczeniu")]
        public decimal? TaxDeductibleValue { get; set; } = 0;
        [DisplayName("Wartość zakupów nieopodatkowanych")]
        public decimal? TaxFreeBuysValue { get; set; } = 0;
        [DisplayName("Wartość zakupów, od których podatek VAT nie podlega odliczeniu")]
        public decimal? NoTaxDeductibleBuysValue { get; set; } = 0;

        [DisplayName("Kontrahent")]
        [ForeignKey("ContractorId")]
        public virtual Contractor Contractor { get; set; }

        [DisplayName("Miesiąc")]
        public int Month { get; set; }
        [DisplayName("Rok")]
        public int Year { get; set; }

        public int getOrderNumber(ApplicationDbContext _context)
        {
            try
            {
                return _context.VATRegisterBuy.Where(p => p.Month == Month && p.Year == Year).Last().Number+1;
            }
            catch (Exception)
            {
                return 1;
            }
        }
    }
}
