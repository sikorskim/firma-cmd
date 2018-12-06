using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class Company : Contractor
    {
        public string REGON { get; set; }
        [DisplayName("Wystawca faktur")]
        public string InvoiceIssuerName { get; set; }
        [DisplayName("Miejsce wystawienia faktur")]
        public string InvoiceIssueCity { get; set; }
        [DisplayName("Nazwa banku")]
        public string BankName { get; set; }
        [DisplayName("Numer konta bankowego")]
        public string BankAccountNumber { get; set; }
        [Url]
        [DisplayName("Strona internetowa")]
        public string Website { get; set; }   
    }
}
