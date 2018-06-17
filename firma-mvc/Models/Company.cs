using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string Website { get; set; }   
    }
}
