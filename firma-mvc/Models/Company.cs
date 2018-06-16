using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class Company : Contractor
    {
        public string InvoiceIssuerName { get; set; }
        public string InvoiceIssueCity { get; set; }
    }
}
