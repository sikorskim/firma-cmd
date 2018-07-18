using firma_mvc.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace firma_mvc
{
    class JPK_VAT
    {
        static XNamespace etd = XNamespace.Get("http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/");
        static XNamespace kck = XNamespace.Get("http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2013/05/23/eD/KodyCECHKRAJOW/");
        static XNamespace tns = XNamespace.Get("http://jpk.mf.gov.pl/wzor/2016/10/26/10261/");
        static XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");

        public Naglowek Naglowek { get; set; }
        public Podmiot Podmiot { get; set; }
        List<SprzedazWiersz> SprzedazWierszList { get; set; }
        SprzedazCtrl SprzedazCtrl { get; set; }
        List<ZakupWiersz> ZakupWierszList { get; set; }
        ZakupCtrl ZakupCtrl { get; set; }

        public JPK_VAT(int month, int year, ApplicationDbContext _context)
        {
            Naglowek = new Naglowek(month, year);
            Podmiot = new Podmiot(_context.Company.FirstOrDefault());
            SprzedazCtrl = new SprzedazCtrl();
            SprzedazWierszList = new List<SprzedazWiersz>();
            ZakupCtrl = new ZakupCtrl();
            ZakupWierszList = new List<ZakupWiersz>();

            int i = 1;
            var sprzedaz = _context.VATRegisterSell.Include(j => j.Contractor).Where(p => p.Month == month && p.Year == year);
            foreach (VATRegisterSell s in sprzedaz)
            {
                SprzedazWiersz sprzedazWiersz = new SprzedazWiersz()
                {
                    LpSprzedazy = i,
                    NrKontrahenta = s.Contractor.NIP,
                    NazwaKontrahenta = s.Contractor.FullName,
                    AdresKontrahenta = s.Contractor.FullAddress,
                    DowodSprzedazy = s.DocumentNumber,
                    DataWystawienia = s.DateOfIssue,
                    DataSprzedazy = s.DeliveryDate,
                    K_19 = (decimal)s.ValueNetto23,
                    K_20 = (decimal)s.VATValue23,
                };

                SprzedazWierszList.Add(sprzedazWiersz);
                SprzedazCtrl.PodatekNalezny += s.VATSummary;
                i++;
            }
            SprzedazCtrl.LiczbaWierszySprzedazy = i;

            i = 1;
            var zakupy = _context.VATRegisterBuy.Include(j => j.Contractor).Where(p => p.Month == month && p.Year == year);
            foreach (VATRegisterBuy z in zakupy)
            {
                ZakupWiersz zakupWiersz = new ZakupWiersz()
                {
                    LpZakupu=i,
                    NrDostawcy=z.Contractor.NIP,
                    NazwaDostawcy=z.Contractor.FullName,
                    AdresDostawcy=z.Contractor.FullAddress,
                    DowodZakupu =  z.DocumentNumber,
                    DataZakupu = z.DateOfIssue,
                    DataWplywu = z.DateOfIssue,
                    K_45=z.ValueNetto,
                    K_46= (decimal)z.TaxDeductibleValue
                };

                ZakupWierszList.Add(zakupWiersz);
                ZakupCtrl.PodatekNalezny += (decimal)z.TaxDeductibleValue;
                i++;
            }
            ZakupCtrl.LiczbaWierszySprzedazy = i;
        }

        public XDocument generate()
        {
            XDocument xml = new XDocument();
            XElement root = new XElement(tns + "JPK");
            XElement naglowek = new XElement(tns + "Naglowek");

            XElement kodFormularza = new XElement(tns + "KodFormularza");
            XAttribute kodSystemowy = new XAttribute("kodSystemowy", Naglowek.KodFormularza.KodSystemowy);
            XAttribute wersjaSchemy = new XAttribute("wersjaSchemy", Naglowek.KodFormularza.WersjaSchemy);


            return xml;
        }



        //        public static Naglowek readHeader(string path)
        //        {
        //            Naglowek jpkHeader = new Naglowek();

        //            XDocument doc = XDocument.Load(path);
        //            XElement elem = doc.Element(tns+"JPK").Element(tns+"Naglowek");
        ////            jpkHeader.KodFormularza = (int)elem.Element(tns+"KodFormularza").Value;
        ////            jpkHeader.WariantFormularza = elem.Element(tns+"WariantFormularza").Value.ToString();
        ////            jpkHeader.CelZlozenia = elem.Element(tns+"CelZlozenia").Value.ToString();
        ////            jpkHeader.DataWytworzeniaJPK = elem.Element(tns+"DataWytworzeniaJPK").Value.ToString();
        ////            jpkHeader.DataOd = elem.Element(tns+"DataOd").Value.ToString();
        ////            jpkHeader.DataDo = elem.Element(tns+"DataDo").Value.ToString();
        ////            jpkHeader.NazwaSystemu = elem.Element(tns+"NazwaSystemu").Value.ToString();

        //            return jpkHeader;
        //        }

        //        public static Podmiot readSubject(string path)
        //        {
        //            Podmiot jpkPodmiot = new Podmiot();

        //            XDocument doc = XDocument.Load(path);
        //            XElement elemIdent = doc.Element(tns + "JPK").Element(tns + "Podmiot1").Element(tns + "IdentyfikatorPodmiotu");
        //            XElement elemAdres = doc.Element(tns + "JPK").Element(tns + "Podmiot1").Element(tns + "AdresPodmiotu");

        ////            jpkPodmiot.nip = elemIdent.Element(etd + "NIP").Value.ToString();
        ////            jpkPodmiot.pelnaNazwa = elemIdent.Element(etd + "PelnaNazwa").Value.ToString();
        ////            jpkPodmiot.kodKraju = elemAdres.Element(tns + "KodKraju").Value.ToString();
        ////            jpkPodmiot.wojewodztwo = elemAdres.Element(tns + "Wojewodztwo").Value.ToString();
        ////            jpkPodmiot.powiat = elemAdres.Element(tns + "Powiat").Value.ToString();
        ////            jpkPodmiot.gmina = elemAdres.Element(tns + "Gmina").Value.ToString();
        ////            jpkPodmiot.ulica = elemAdres.Element(tns + "Ulica").Value.ToString();
        ////            jpkPodmiot.nrDomu = elemAdres.Element(tns + "NrDomu").Value.ToString();
        ////            jpkPodmiot.miejscowosc = elemAdres.Element(tns + "Miejscowosc").Value.ToString();
        ////            jpkPodmiot.kodPocztowy = elemAdres.Element(tns + "KodPocztowy").Value.ToString();
        ////            jpkPodmiot.poczta = elemAdres.Element(tns + "Poczta").Value.ToString();

        //            return jpkPodmiot;
        //        }

        //        public static List<SprzedazWiersz> readSell(string path)
        //        {
        //            List<SprzedazWiersz> listaSprzedazy = new List<SprzedazWiersz>();

        //            XDocument doc = XDocument.Load(path);
        //            var sprzedaz = doc.Element(tns + "JPK").Elements(tns + "SprzedazWiersz");

        //            foreach (XElement elem in sprzedaz)
        //            {
        //                SprzedazWiersz wiersz = new SprzedazWiersz();

        ////                wiersz.typ = elem.Attribute("typ").Value.ToString();
        ////                wiersz.lpSprzedazy = elem.Element(tns + "LpSprzedazy").Value.ToString();
        ////                wiersz.nrKontrahenta = elem.Element(tns + "NrKontrahenta").Value.ToString();
        ////                wiersz.nazwaKontrahenta = elem.Element(tns + "NazwaKontrahenta").Value.ToString();
        ////                wiersz.adresKontrahenta = elem.Element(tns + "AdresKontrahenta").Value.ToString();
        ////                wiersz.dowodSprzedazy = elem.Element(tns + "DowodSprzedazy").Value.ToString();
        ////                wiersz.dataWystawienia = elem.Element(tns + "DataWystawienia").Value.ToString();

        //                //string temp = elem.Element(tns + "K_19").Value.ToString();
        //                //if (temp != null)

        //                //wiersz.k_19 = elem.Element(tns + "K_19").Value.ToString();
        //                //wiersz.k_20 = elem.Element(tns + "K_20").Value.ToString();

        //                listaSprzedazy.Add(wiersz);
        //            }

        //            return listaSprzedazy;
        //        }
    }
}
