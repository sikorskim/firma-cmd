using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firma_mvc
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

        public Naglowek(int month, int year)
        {
            KodFormularza = new KodFormularza();
            WariantFormularza = 3;
            CelZlozenia = 0;
            DataWytworzeniaJPK = DateTime.Now;
            string yearMonthStr = year + "-" + month + "-";
            DataOd = DateTime.Parse(yearMonthStr+"01");
            DataDo = DateTime.Parse(yearMonthStr + DateTime.DaysInMonth(year, month).ToString());
            NazwaSystemu = "Computerman Firma";
        }
    }
}
