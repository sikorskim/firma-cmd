using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class CalcViewModel
    {
        public int CalcTypeId { get; set; }
        public decimal NettoValue { get; set; }    
        public decimal BruttoValue { get; set; }        
    }
}