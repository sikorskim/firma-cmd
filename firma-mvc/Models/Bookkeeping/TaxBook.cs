using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public DateTime Date { get; set; }
        [DisplayName("Nr dowodu księgowego")]
        public string InvoiceNumber { get; set; }
        #region Contractor
        [DisplayName("L.p.")]
        public string Name { get; set; }
        public string NIP { get; set; }
        [DisplayName("Adres")]
        public string Address { get; set; }
        #endregion
        [DisplayName("Opis zdarzenia gospodarczego")]
        public string Description { get; set; }
        #region Income
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
        public decimal Salary { get; set; }
        public decimal OtherCosts { get; set; }
        public decimal TotalCosts { get { return Salary + OtherCosts; } }
        public decimal Column15 { get; set; }
        #endregion
        #region Research costs
        public string CostDescription { get; set; }
        public decimal ResearchCostValue { get; set; }
        #endregion
        public string Comments { get; set; }
    }
}
