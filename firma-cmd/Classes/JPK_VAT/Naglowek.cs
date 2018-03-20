using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jpk_check
{
    class Naglowek
    {
        public KodFormularza KodFormularza { get; set; }
        public int WariantFormularza { get; set; }
        public int CelZlozenia { get; set; }
        public DateTime DataWytworzeniaJPK{ get; set; }
        public DateTime DataOd { get; set; }
        public DateTime DataDo { get; set; }
        public string NazwaSystemu { get; set; }
    }
}
