using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using firma_mvc.Data;
using System.ComponentModel.DataAnnotations;

namespace firma_mvc
{
    public class Invoice
    {
        public int Id { get; set; }
        [DisplayName("Numer faktury")]
        public string Number { get; set; }
        [DisplayName("Data wystawienia")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfIssue { get; set; }
        [DisplayName("Kontrahent")]
        public int ContractorId { get; set; }
        [DisplayName("Forma płatności")]
        public int PaymentMethodId { get; set; }     
        [DisplayName("Ilość pozycji")]
        public virtual int ItemsCount
        {
            get { return countItems(); }
        }
        [DisplayName("Wartość netto")]
        public decimal TotalValue 
        {
            get { return countTotalNettPrice(); }
        }
        [DisplayName("Wartość brutto")]
        public decimal TotalValueInclVat
        {
            get { return countTotalPriceBrutto(); }
        }
        [DisplayName("Status")]
        public int InvoiceStatusId { get; set; }

        [DisplayName("Kontrahent")]
        [ForeignKey("ContractorId")]            
        public virtual Contractor Contractor { get; set; }    
        [ForeignKey("InvoiceStatusId")]
        public virtual InvoiceStatus InvoiceStatus {get; set; }
        [DisplayName("Forma płatności")]
        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        
        private readonly ApplicationDbContext _context;

        public Invoice()
        {}
        
        int countItems()
        {
            try
            {
                return InvoiceItems.Count();
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        decimal countTotalNettPrice()
        {
                return InvoiceItems.Sum(p=>p.TotalPrice);
        }

        decimal countTotalPriceBrutto()
        {
            return InvoiceItems.Sum(p => p.TotalPriceBrutto);
        }
    }
}