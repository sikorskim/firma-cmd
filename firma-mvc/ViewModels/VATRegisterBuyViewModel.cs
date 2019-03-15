using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class VATRegisterBuyViewModel
    {
        public VATRegisterBuy VATRegisterBuy { get; set; }
        public bool CarCost { get; set; }
        public bool BuyForTrade { get; set; }
        public bool OtherCost { get; set; }
        public string DescriptionForTaxBook { get; set; }

        public VATRegisterBuyViewModel(VATRegisterBuy vATRegisterBuy)
        {
            VATRegisterBuy = vATRegisterBuy;
        }
    }
}
