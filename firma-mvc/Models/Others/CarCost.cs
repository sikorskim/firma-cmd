using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using firma_mvc.Data;

namespace firma_mvc
{
    public class CarCost
    {
        public int Id { get; set; }
        [DisplayName("Data")]
        public DateTime Date { get; set; }
        [DisplayName("Cena brutto")]
        public decimal Price { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }
        [DisplayName("Typ kosztu")]
        public int CarCostTypeId { get; set; }

        [ForeignKey("CarCostTypeId")]
        [DisplayName("Rodzaj kosztu")]
        public virtual CarCostType CarCostType { get; set; }
    }
}
