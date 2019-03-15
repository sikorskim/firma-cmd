using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class VATRegisterBuyViewModel : VATRegisterBuy
    {
        public bool CarCost { get; set; }
        public bool BuyForTrade { get; set; }
        public bool OtherCost { get; set; }
        public string DescriptionForTaxBook { get; set; }

        public VATRegisterBuy getVATRegisterBuy()
        {
            VATRegisterBuy vATRegisterBuy = new VATRegisterBuy();
            vATRegisterBuy.Id=this.Id;
            vATRegisterBuy.Number=this.Number;
            vATRegisterBuy.DeliveryDate=this.DeliveryDate;
            vATRegisterBuy.DateOfIssue=this.DateOfIssue;
            vATRegisterBuy.DocumentNumber=this.DocumentNumber;
            vATRegisterBuy.ContractorId=this.ContractorId;
            vATRegisterBuy.ValueBrutto=this.ValueBrutto;
            vATRegisterBuy.ValueNetto=this.ValueNetto;
            vATRegisterBuy.TaxDeductibleValue=this.TaxDeductibleValue;
            vATRegisterBuy.TaxFreeBuysValue=this.TaxFreeBuysValue;
            vATRegisterBuy.NoTaxDeductibleBuysValue=this.NoTaxDeductibleBuysValue;
            vATRegisterBuy.Contractor=this.Contractor;
            vATRegisterBuy.Month=this.Month;
            vATRegisterBuy.Year=this.Year;

            return vATRegisterBuy;
        }
    }
}
