using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class FixedAssets
    {
        public int Id { get; set; }
        [DisplayName("Data nabycia")]
        public DateTime DateOfBuy { get; set; }
        [DisplayName("Data przyjęcia do użytkowania")]
        public DateTime DateOfUseStart { get; set; }
        [DisplayName("Nazwa środka trwałego")]
        public string Name { get; set; }
        [DisplayName("Symbol KŚT")]
        public string Identfier { get; set; }
        [DisplayName("Wartość początkowa")]
        public decimal OriginalValue { get; set; }
        [DisplayName("Stawka amortyzacyjna")]
        public decimal DepreciationRate { get; set; }
        [DisplayName("Wartość ulepszenia")]
        public decimal UpgradeValue { get; set; }
        [DisplayName("Zaktualizowana wartość początkowa")]
        public decimal UpdatedOriginalValue { get; set; }
        [DisplayName("Data likwidacji")]
        public DateTime LiquidationDate { get; set; }
        [DisplayName("Przyczyna likwidacji")]
        public string LiquidationReason { get; set; }
    }
}