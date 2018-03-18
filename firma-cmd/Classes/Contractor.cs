using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;

namespace jpk_check
{
    public class Contractor : JPKpodmiot
    {
        public Contractor()
        {
        }

        public Contractor(string nip, string name, string countryCode, string voivodeship, string county,
            string community, string city, string street, string buldingNo, string postalCode, string postOffice)
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

            if (!File.Exists(path))
            {
                XDocument newDoc = new XDocument();
                XElement newRoot = new XElement("Contractors");
                newDoc.Add(newRoot);
                newDoc.Save(path);
            }

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


            XElement root = XElement.Load(path);
            root.Add(contractor);
            root.Save(path);

            return true;
        }

        public List<Contractor> getAll()
        {
            string path = "contractors.xml";
            List<Contractor> list = new List<Contractor>();

            XDocument doc = XDocument.Load(path);

            int i = 1;
            foreach (XElement elem in doc.Element("Contractors").Elements("Contractor"))
            {
                Contractor contractor = get(elem);
                list.Add(contractor);
                contractor.print(i);
                i++;
            }

            return list;
        }

        public Contractor get(XElement contractorElement)
        {
            Contractor contractor = new Contractor();
            contractor.pelnaNazwa = contractorElement.Attribute("FullName").Value;
            contractor.nip = contractorElement.Attribute("NIP").Value;
            contractor.kodKraju = contractorElement.Attribute("CountryCode").Value;
            contractor.wojewodztwo = contractorElement.Attribute("Voivodeship").Value;
            contractor.powiat = contractorElement.Attribute("County").Value;
            contractor.gmina = contractorElement.Attribute("Community").Value;
            contractor.miejscowosc = contractorElement.Attribute("City").Value;
            contractor.ulica = contractorElement.Attribute("Street").Value;
            contractor.nrDomu = contractorElement.Attribute("BuldingNo").Value;
            contractor.kodPocztowy = contractorElement.Attribute("PostalCode").Value;
            contractor.poczta = contractorElement.Attribute("PostOffice").Value;

            return contractor;
        }

        void print(int i)
        {
            Console.WriteLine(i + "\t" + pelnaNazwa + "\t" + nip + "\t" + kodKraju + "\t" + kodPocztowy + "\t" +
                              miejscowosc + "\t" + ulica + "\t" + nrDomu);
        }

    }
}