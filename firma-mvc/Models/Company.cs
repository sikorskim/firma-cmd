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
        public string InvoiceIssuerName { get; set; }
        public string InvoiceIssueCity { get; set; }
        public string BankName { get; set; }
        public string BankAccountNumber { get; set; }
        [Url]
        public string Website { get; set; }   
    }
}
