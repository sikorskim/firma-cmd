using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class TempSettings
    {
        public int SelectedMonth { get; set; }
        public int SelectedYear { get; set; }

        public TempSettings()
        {
            SelectedMonth=DateTime.Now.Month;
            SelectedYear=DateTime.Now.Year;
        }
    }
}
