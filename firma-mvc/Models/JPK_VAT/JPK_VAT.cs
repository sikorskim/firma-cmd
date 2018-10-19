using firma_mvc.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace firma_mvc
{
    class JPK_VAT
    {
        static XNamespace etd = XNamespace.Get("http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/");
        //static XNamespace kck = XNamespace.Get("http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2013/05/23/eD/KodyCECHKRAJOW/");
        static XNamespace tns = XNamespace.Get("http://jpk.mf.gov.pl/wzor/2017/11/13/1113/");
        static XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");

        public Naglowek Naglowek { get; set; }
        public Podmiot Podmiot { get; set; }
        List<SprzedazWiersz> SprzedazWierszList { get; set; }
        public SprzedazCtrl SprzedazCtrl { get; set; }
        List<ZakupWiersz> ZakupWierszList { get; set; }
        public ZakupCtrl ZakupCtrl { get; set; }

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
                    K_19 = Tools.decimalRound((decimal)s.ValueNetto23),
                    K_20 = Tools.decimalRound((decimal)s.VATValue23),
                };

                SprzedazWierszList.Add(sprzedazWiersz);
                SprzedazCtrl.PodatekNalezny += s.VATSummary;
                SprzedazCtrl.PodatekNalezny = Tools.decimalRound(SprzedazCtrl.PodatekNalezny);
                i++;
            }
            SprzedazCtrl.LiczbaWierszySprzedazy = sprzedaz.Count();

            i = 1;
            var zakupy = _context.VATRegisterBuy.Include(j => j.Contractor).Where(p => p.Month == month && p.Year == year);
            foreach (VATRegisterBuy z in zakupy)
            {
                ZakupWiersz zakupWiersz = new ZakupWiersz()
                {
                    LpZakupu = i,
                    NrDostawcy = z.Contractor.NIP,
                    NazwaDostawcy = z.Contractor.FullName,
                    AdresDostawcy = z.Contractor.FullAddress,
                    DowodZakupu = z.DocumentNumber,
                    DataZakupu = z.DateOfIssue,
                    DataWplywu = z.DateOfIssue,
                    K_45 = Tools.decimalRound(z.ValueNetto),
                    K_46 = Tools.decimalRound((decimal)z.TaxDeductibleValue)
                };

                ZakupWierszList.Add(zakupWiersz);
                ZakupCtrl.PodatekNaliczony += (decimal)z.TaxDeductibleValue;
                ZakupCtrl.PodatekNaliczony = Tools.decimalRound(ZakupCtrl.PodatekNaliczony);
                i++;
            }
            ZakupCtrl.LiczbaWierszyZakupow = zakupy.Count();
        }

        public string generate()
        {
            XDocument xml = new XDocument();
            XElement root = new XElement(tns + "JPK");
            root.Add(new XAttribute(XNamespace.Xmlns + "etd", etd));
            root.Add(new XAttribute(XNamespace.Xmlns + "tns", tns));
            root.Add(new XAttribute(XNamespace.Xmlns + "xsi", xsi));

            XElement naglowek = new XElement(tns + "Naglowek");

            XElement kodFormularza = new XElement(tns + "KodFormularza", Naglowek.KodFormularza.Wartosc);
            XAttribute kodSystemowy = new XAttribute("kodSystemowy", Naglowek.KodFormularza.KodSystemowy);
            XAttribute wersjaSchemy = new XAttribute("wersjaSchemy", Naglowek.KodFormularza.WersjaSchemy);
            kodFormularza.Add(kodSystemowy);
            kodFormularza.Add(wersjaSchemy);
            naglowek.Add(kodFormularza);

            XElement wariantFormularza = new XElement(tns + "WariantFormularza", Naglowek.WariantFormularza);
            XElement celZlozenia = new XElement(tns + "CelZlozenia", Naglowek.CelZlozenia);
            XElement dataWytworzeniaJPK = new XElement(tns + "DataWytworzeniaJPK", Naglowek.DataWytworzeniaJPK.ToString("yyyy-MM-ddTHH:mm:ss"));
            XElement dataOd = new XElement(tns + "DataOd", formatDate(Naglowek.DataOd));
            XElement dataDo = new XElement(tns + "DataDo", formatDate(Naglowek.DataDo));
            XElement nazwaSystemu = new XElement(tns + "NazwaSystemu", Naglowek.NazwaSystemu);
            naglowek.Add(wariantFormularza);
            naglowek.Add(celZlozenia);
            naglowek.Add(dataWytworzeniaJPK);
            naglowek.Add(dataOd);
            naglowek.Add(dataDo);
            naglowek.Add(nazwaSystemu);
            root.Add(naglowek);

            XElement podmiot = new XElement(tns + "Podmiot1");
            XElement nip = new XElement(tns + "NIP", Podmiot.NIP);
            XElement pelnaNazwa = new XElement(tns + "PelnaNazwa", Podmiot.PelnaNazwa);
            XElement email = new XElement(tns + "Email", Podmiot.Email);
            podmiot.Add(nip);
            podmiot.Add(pelnaNazwa);
            podmiot.Add(email);
            root.Add(podmiot);

            foreach (SprzedazWiersz s in SprzedazWierszList)
            {
                XElement sprzedazWiersz = new XElement(tns + "SprzedazWiersz");
                XElement lpSprzedazy = new XElement(tns + "LpSprzedazy", s.LpSprzedazy);
                XElement nrKontrahenta = new XElement(tns + "NrKontrahenta", s.NrKontrahenta);
                XElement nazwaKontrahenta = new XElement(tns + "NazwaKontrahenta", s.NazwaKontrahenta);
                XElement adresKontrahenta = new XElement(tns + "AdresKontrahenta", s.AdresKontrahenta);
                XElement dowodSprzedazy = new XElement(tns + "DowodSprzedazy", s.DowodSprzedazy);
                XElement dataWystawienia = new XElement(tns + "DataWystawienia", formatDate(s.DataWystawienia));
                XElement dataSprzedazy = new XElement(tns + "DataSprzedazy", formatDate(s.DataSprzedazy));
                XElement k_19 = new XElement(tns + "K_19", Tools.decimalRound(s.K_19));
                XElement k_20 = new XElement(tns + "K_20", Tools.decimalRound(s.K_20));

                sprzedazWiersz.Add(lpSprzedazy);
                sprzedazWiersz.Add(nrKontrahenta);
                sprzedazWiersz.Add(nazwaKontrahenta);
                sprzedazWiersz.Add(adresKontrahenta);
                sprzedazWiersz.Add(dowodSprzedazy);
                sprzedazWiersz.Add(dataWystawienia);
                sprzedazWiersz.Add(dataSprzedazy);
                sprzedazWiersz.Add(k_19);
                sprzedazWiersz.Add(k_20);
                root.Add(sprzedazWiersz);
            }
            XElement sprzedazCtrl = new XElement(tns + "SprzedazCtrl");
            XElement liczbaWierszySprzedazy = new XElement(tns + "LiczbaWierszySprzedazy", SprzedazCtrl.LiczbaWierszySprzedazy);
            XElement podatekNalezny = new XElement(tns + "PodatekNalezny", SprzedazCtrl.PodatekNalezny);
            sprzedazCtrl.Add(liczbaWierszySprzedazy);
            sprzedazCtrl.Add(podatekNalezny);
            root.Add(sprzedazCtrl);

            foreach (ZakupWiersz z in ZakupWierszList)
            {
                XElement zakupWiersz = new XElement(tns + "ZakupWiersz");
                XElement lpZakupu = new XElement(tns + "LpZakupu", z.LpZakupu);
                XElement nrDostawcy = new XElement(tns + "NrDostawcy", z.NrDostawcy);
                XElement nazwaDostawcy = new XElement(tns + "NazwaDostawcy", z.NazwaDostawcy);
                XElement adresDostawcy = new XElement(tns + "AdresDostawcy", z.AdresDostawcy);
                XElement dowodZakupu = new XElement(tns + "DowodZakupu", z.DowodZakupu);
                XElement dataZakupu = new XElement(tns + "DataZakupu", formatDate(z.DataZakupu));
                XElement dataWplywu = new XElement(tns + "DataWplywu", formatDate(z.DataWplywu));
                XElement k_45 = new XElement(tns + "K_45", z.K_45);
                XElement k_46 = new XElement(tns + "K_46", z.K_46);

                zakupWiersz.Add(lpZakupu);
                zakupWiersz.Add(nrDostawcy);
                zakupWiersz.Add(nazwaDostawcy);
                zakupWiersz.Add(adresDostawcy);
                zakupWiersz.Add(dowodZakupu);
                zakupWiersz.Add(dataZakupu);
                zakupWiersz.Add(dataWplywu);
                zakupWiersz.Add(k_45);
                zakupWiersz.Add(k_46);
                root.Add(zakupWiersz);
            }
            XElement zakupCtrl = new XElement(tns +"ZakupCtrl");
            XElement liczbaWierszyZakupu = new XElement(tns + "LiczbaWierszyZakupow", ZakupCtrl.LiczbaWierszyZakupow);
            XElement podatekNaliczony = new XElement(tns + "PodatekNaliczony", ZakupCtrl.PodatekNaliczony);
            zakupCtrl.Add(liczbaWierszyZakupu);
            zakupCtrl.Add(podatekNaliczony);
            root.Add(zakupCtrl);

            xml.Add(root);

            string time = DateTime.Now.ToFileTime().ToString();
            string filename = Tools.getHash(xml.ToString() + time)+".xml";
            xml.Save("tmp/" + filename);

            return filename;
        }

        string formatDate(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
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
