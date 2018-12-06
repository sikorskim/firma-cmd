using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace firma_mvc
{
    public class CarCostType
    {
        public int Id { get; set; }
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [DisplayName("Opis")]
        public string Description { get; set; }

        public CarCostType()
        {}
    }
}
