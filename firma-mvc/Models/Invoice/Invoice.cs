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
            get { return countTotalNettPrice(); }
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
        {
            //try
            //{
            //    InvoiceItems = _context.InvoiceItem.Where(p => p.InvoiceId == Id).ToList();
            //}
            //catch (Exception)
            //{
            //    InvoiceItems=new List<InvoiceItem>();
            //}            
        }
        
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
            try
            {
                return InvoiceItems.Sum(p=>p.Item.Price);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
    }
}