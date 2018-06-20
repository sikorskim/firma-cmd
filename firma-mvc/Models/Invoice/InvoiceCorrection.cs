using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class InvoiceCorrection : Invoice
    {
        public int InvoiceId { get; set; }
        public DateTime DateOfCorrection { get; set; }
        public string CorrectionCause { get; set; }
    }
}