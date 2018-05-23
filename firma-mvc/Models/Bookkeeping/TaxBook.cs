using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    // based on: 
    // ROZPORZĄDZENIE MINISTRA FINANSÓW z dnia 31 marca 2016 r. zmieniające rozporządzenie w sprawie prowadzenia podatkowej księgi przychodów i rozchodów

    public class TaxBook
    {
        public int Id { get; set; }
        [DisplayName("L.p.")]
        public int Number { get; set; }
        [DisplayName("Data zdarzenia gospodarczego")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [DisplayName("Nr dowodu księgowego")]
        public string InvoiceNumber { get; set; }
        #region Contractor
        public string ContractorHeader { get { return "Kontrahent"; } }
        [DisplayName("Nazwa kontrahenta")]
        public string Name { get; set; }
        public string NIP { get; set; }
        [DisplayName("Adres")]
        public string Address { get; set; }
        #endregion
        [DisplayName("Opis zdarzenia gospodarczego")]
        public string Description { get; set; }
        #region Income
        public string IncomeHeader { get { return "Przychód"; } }
        [DisplayName("Wartość sprzedanych towarów i usług")]
        public decimal SellValue { get; set; }
        [DisplayName("Pozostałe przychody")]
        public decimal OtherIncome { get; set; }
        [DisplayName("Razem przychód")]
        public decimal VATValue { get { return SellValue + OtherIncome; } }
        #endregion
        [DisplayName("Zakup towarów handlowych i materiałów wg cen zakupu")]
        public decimal GoodsBuys { get; set; }
        [DisplayName("Koszty uboczne zakupu")]
        public decimal BuysSideEffects { get; set; }
        #region Costs
        public string CostsHeader { get { return "Wydatki"; } }
        [DisplayName("Wynagrodzenia w gotówce i w naturze")]
        public decimal Salary { get; set; }
        [DisplayName("Pozostałe wydatki")]
        public decimal OtherCosts { get; set; }
        [DisplayName("Razem wydatki")]
        public decimal TotalCosts { get { return Salary + OtherCosts; } }
        [DisplayName("")]
        public decimal Column15 { get; set; }
        #endregion
        #region Research costs
        public string ResearchCostsHeader { get { return "Koszty działalności badawczo-rozwojowej, o których mowa w art. 26c ustawy o podatku dochodowym"; } }
        [DisplayName("Opis kosztu")]
        public string CostDescription { get; set; }
        [DisplayName("Wartość")]
        public decimal ResearchCostValue { get; set; }
        #endregion
        [DisplayName("Uwagi")]
        public string Comments { get; set; }
    }
}
