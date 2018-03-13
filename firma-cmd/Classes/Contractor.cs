using System;
using System.ComponentModel;
using System.Xml.Linq;

namespace jpk_check
{
    public class Contractor : JPKpodmiot
    {
        public Contractor(string nip, string name, string countryCode, string voivodeship, string county, string community, string city, string street, string buldingNo, string postalCode, string postOffice)
        {
            this.nip = nip;
            pelnaNazwa = name;
            kodKraju = countryCode;
            wojewodztwo = voivodeship;
            powiat = county;
            gmina = community;
            miejscowosc = city;
            ulica = street;
            nrDomu = buldingNo;
            kodPocztowy = postalCode;
            poczta = postOffice;    
        }

        public bool add()
        {
            string path = "contractors.xml";
            
            XDocument doc = XDocument.Load(path);
            XElement root = doc.Element("Contractors");
            
            XElement contractor = new XElement("Contractor");
            contractor.Add(new XAttribute("NIP", nip));
            contractor.Add(new XAttribute("FullName", pelnaNazwa));
            contractor.Add(new XAttribute("CountryCode", kodKraju));
            contractor.Add(new XAttribute("Voivodeship", wojewodztwo));
            contractor.Add(new XAttribute("County", powiat));
            contractor.Add(new XAttribute("Community", gmina));
            contractor.Add(new XAttribute("City", miejscowosc));
            contractor.Add(new XAttribute("Street", ulica));
            contractor.Add(new XAttribute("BuldingNo", nrDomu));
            contractor.Add(new XAttribute("PostalCode", kodPocztowy));
            contractor.Add(new XAttribute("PostOffice", poczta));
            
            root.Add(contractor);
            doc.Add(root);
            doc.Save(path);

            return true;
        }
    }
}