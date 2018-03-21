using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace firma_lib
{
    public class VAT
    {        
        public int Id { get; set; }
        [DisplayName("Wartość")]
        public decimal Value { get; set; }

        public VAT(decimal value)
        {
            Value = value;
        }

        public bool add()
        {
            bool success = true;
            Database db = new Database();
            db.VAT.Add(this);
            db.SaveChanges();

            return success;
        }

        public List<VAT> getAll()
        {
            Database db = new Database();
            List<VAT> vat = db.VAT.Select(p => p).ToList();
            return vat;
        }
    }
}