using firma_mvc.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class FixedAssets
    {
        public int Id { get; set; }
        [DisplayName("Data nabycia")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfBuy { get; set; }
        [DisplayName("Data przyjęcia do użytkowania")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateOfUseStart { get; set; }
        [DisplayName("Nazwa środka trwałego")]
        public string Name { get; set; }
        [DisplayName("Symbol KŚT")]
        public string Identfier { get; set; }
        [DisplayName("Wartość początkowa")]
        public decimal OriginalValue { get; set; }
        [DisplayName("Stawka amortyzacyjna")]
        public decimal? DepreciationRate { get; set; }
        [DisplayName("Wartość ulepszenia")]
        public decimal? UpgradeValue { get; set; }
        [DisplayName("Zaktualizowana wartość początkowa")]
        public decimal? UpdatedOriginalValue { get; set; }
        [DisplayName("Data likwidacji")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? LiquidationDate { get; set; }
        [DisplayName("Przyczyna likwidacji")]
        public string LiquidationReason { get; set; }

        public string getNumber(ApplicationDbContext _context)
        {
            string num;
            try
            {
                num = _context.FixedAssets.Last().Identfier;
                int i = Int32.Parse(num.Substring(num.IndexOf('-') + 1, num.Length - num.IndexOf('-') - 1));
                i++;
                num = num.Substring(0, num.IndexOf('-') + 1) + i.ToString();
                return num;
            }
            catch (Exception)
            {
                return "ST-1";
            }
        }
    }
}