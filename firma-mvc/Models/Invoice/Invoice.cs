using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using firma_mvc.Data;

namespace firma_mvc
{
    public class Invoice
    {
        public int Id { get; set; }
        [DisplayName("Numer faktury")]
        public string Number { get; set; }
        [DisplayName("Data wystawienia")]
        public DateTime DateOfIssue { get; set; }
        public int ContractorId { get; set; }
//        [DisplayName("Kontrahent")]        
        public int PaymentMethodId { get; set; }
//        [DisplayName("Forma płatności")]        
        [DisplayName("Ilość pozycji")]
        public int ItemsCount { get; set; }
        [DisplayName("Wartość netto")]
        public decimal TotalValue { get; set; }
        [DisplayName("Wartość brutto")]
        public decimal TotalValueInclVat { get; set; }
        public int InvoiceStatusId { get; set; }
//        [DisplayName("Status")]
        
        [ForeignKey("ContractorId")]            
        public virtual Contractor Contractor { get; set; }    
        [ForeignKey("InvoiceStatusId")]
        public virtual InvoiceStatus InvoiceStatus {get; set; }
        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod PaymentMethod { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
        
        
        public Invoice()
        {}
        
//        public string getNumber()
//        {
//            string number=string.Empty;            
//            
//            try
//            {
//                string lastNumber = _context.InvoiceHeader.Last(p => p.DateOfIssue.Year == DateTime.Now.Year).Number;                
//                int nextNumber = Int32.Parse(lastNumber.Substring(0, lastNumber.IndexOf('/')));
//                nextNumber++;
//                number = nextNumber.ToString()+"/" + DateTime.Now.Year;
//            }
//            catch (Exception ex)
//            {
//                number = "1/" + DateTime.Now.Year;
//            }
//            
//            return number;
//        }
    }
}