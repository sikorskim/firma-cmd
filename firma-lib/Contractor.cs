using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Linq;

namespace firma_lib
{
    public class Contractor
    {
        public string NIP { get; set; }
        public string FullName { get; set; }
        public string CountryCode { get; set; }
        public string Voivodeship { get; set; }
        public string County { get; set; }
        public string Community { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string BuldingNo { get; set; }
        public string PostalCode { get; set; }
        public string PostOffice { get; set; }  
        
        public Contractor()
        {
        }

        public Contractor(string nip, string name, string countryCode, string voivodeship, string county,
            string community, string city, string street, string buldingNo, string postalCode, string postOffice)
        {
            this.NIP = nip;
            FullName = name;
            CountryCode = countryCode;
            Voivodeship = voivodeship;
            County = county;
            Community = community;
            City = city;
            Street = street;
            BuldingNo = buldingNo;
            PostalCode = postalCode;
            PostOffice = postOffice;
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
            contractor.Add(new XAttribute("NIP", NIP));
            contractor.Add(new XAttribute("FullName", FullName));
            contractor.Add(new XAttribute("CountryCode", CountryCode));
            contractor.Add(new XAttribute("Voivodeship", Voivodeship));
            contractor.Add(new XAttribute("County", County));
            contractor.Add(new XAttribute("Community", Community));
            contractor.Add(new XAttribute("City", City));
            contractor.Add(new XAttribute("Street", Street));
            contractor.Add(new XAttribute("BuldingNo", BuldingNo));
            contractor.Add(new XAttribute("PostalCode", PostalCode));
            contractor.Add(new XAttribute("PostOffice", PostOffice));


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
            contractor.FullName = contractorElement.Attribute("FullName").Value;
            contractor.NIP = contractorElement.Attribute("NIP").Value;
            contractor.CountryCode = contractorElement.Attribute("CountryCode").Value;
            contractor.Voivodeship = contractorElement.Attribute("Voivodeship").Value;
            contractor.County = contractorElement.Attribute("County").Value;
            contractor.Community = contractorElement.Attribute("Community").Value;
            contractor.City = contractorElement.Attribute("City").Value;
            contractor.Street = contractorElement.Attribute("Street").Value;
            contractor.BuldingNo = contractorElement.Attribute("BuldingNo").Value;
            contractor.PostalCode = contractorElement.Attribute("PostalCode").Value;
            contractor.PostOffice = contractorElement.Attribute("PostOffice").Value;

            return contractor;
        }

        void print(int i)
        {
            Console.WriteLine(i + "\t" + FullName + "\t" + NIP + "\t" + CountryCode + "\t" + PostalCode + "\t" +
                              City + "\t" + Street + "\t" + BuldingNo);
        }

    }
}