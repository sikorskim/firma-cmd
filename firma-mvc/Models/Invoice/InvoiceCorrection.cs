using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class InvoiceCorrection : Invoice
    {
        public int InvoiceId { get; set; }
        [DisplayName("Data korekty")]
        public DateTime DateOfCorrection { get; set; }
        [DisplayName("Powód korekty")]
        public string CorrectionCause { get; set; }
    }
}