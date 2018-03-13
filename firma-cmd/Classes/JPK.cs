using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace jpk_check
{
    class JPK
    {
        static XNamespace etd = XNamespace.Get("http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2016/01/25/eD/DefinicjeTypy/");
        static XNamespace kck = XNamespace.Get("http://crd.gov.pl/xml/schematy/dziedzinowe/mf/2013/05/23/eD/KodyCECHKRAJOW/");
        static XNamespace tns = XNamespace.Get("http://jpk.mf.gov.pl/wzor/2016/10/26/10261/");
        static XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");

        public static JPKheader readHeader(string path)
        {
            JPKheader jpkHeader = new JPKheader();

            XDocument doc = XDocument.Load(path);
            XElement elem = doc.Element(tns+"JPK").Element(tns+"Naglowek");
            jpkHeader.kodFormularza = elem.Element(tns+"KodFormularza").Value.ToString();
            jpkHeader.wariantFormularza = elem.Element(tns+"WariantFormularza").Value.ToString();
            jpkHeader.celZlozenia = elem.Element(tns+"CelZlozenia").Value.ToString();
            jpkHeader.dataUtworzenia = elem.Element(tns+"DataWytworzeniaJPK").Value.ToString();
            jpkHeader.dataOd = elem.Element(tns+"DataOd").Value.ToString();
            jpkHeader.dataDo = elem.Element(tns+"DataDo").Value.ToString();
            jpkHeader.kodWaluty = elem.Element(tns+"DomyslnyKodWaluty").Value.ToString();
            jpkHeader.kodUrzedu = elem.Element(tns+"KodUrzedu").Value.ToString();

            return jpkHeader;
        }

        public static JPKpodmiot readSubject(string path)
        {
            JPKpodmiot jpkPodmiot = new JPKpodmiot();

            XDocument doc = XDocument.Load(path);
            XElement elemIdent = doc.Element(tns + "JPK").Element(tns + "Podmiot1").Element(tns + "IdentyfikatorPodmiotu");
            XElement elemAdres = doc.Element(tns + "JPK").Element(tns + "Podmiot1").Element(tns + "AdresPodmiotu");

            jpkPodmiot.nip = elemIdent.Element(etd + "NIP").Value.ToString();
            jpkPodmiot.pelnaNazwa = elemIdent.Element(etd + "PelnaNazwa").Value.ToString();
            jpkPodmiot.kodKraju = elemAdres.Element(tns + "KodKraju").Value.ToString();
            jpkPodmiot.wojewodztwo = elemAdres.Element(tns + "Wojewodztwo").Value.ToString();
            jpkPodmiot.powiat = elemAdres.Element(tns + "Powiat").Value.ToString();
            jpkPodmiot.gmina = elemAdres.Element(tns + "Gmina").Value.ToString();
            jpkPodmiot.ulica = elemAdres.Element(tns + "Ulica").Value.ToString();
            jpkPodmiot.nrDomu = elemAdres.Element(tns + "NrDomu").Value.ToString();
            jpkPodmiot.miejscowosc = elemAdres.Element(tns + "Miejscowosc").Value.ToString();
            jpkPodmiot.kodPocztowy = elemAdres.Element(tns + "KodPocztowy").Value.ToString();
            jpkPodmiot.poczta = elemAdres.Element(tns + "Poczta").Value.ToString();

            return jpkPodmiot;
        }

        public static List<JPKsprzedazWiersz> readSell(string path)
        {
            List<JPKsprzedazWiersz> listaSprzedazy = new List<JPKsprzedazWiersz>();

            XDocument doc = XDocument.Load(path);
            var sprzedaz = doc.Element(tns + "JPK").Elements(tns + "SprzedazWiersz");

            foreach (XElement elem in sprzedaz)
            {
                JPKsprzedazWiersz wiersz = new JPKsprzedazWiersz();

                wiersz.typ = elem.Attribute("typ").Value.ToString();
                wiersz.lpSprzedazy = elem.Element(tns + "LpSprzedazy").Value.ToString();
                wiersz.nrKontrahenta = elem.Element(tns + "NrKontrahenta").Value.ToString();
                wiersz.nazwaKontrahenta = elem.Element(tns + "NazwaKontrahenta").Value.ToString();
                wiersz.adresKontrahenta = elem.Element(tns + "AdresKontrahenta").Value.ToString();
                wiersz.dowodSprzedazy = elem.Element(tns + "DowodSprzedazy").Value.ToString();
                wiersz.dataWystawienia = elem.Element(tns + "DataWystawienia").Value.ToString();

                //string temp = elem.Element(tns + "K_19").Value.ToString();
                //if (temp != null)

                //wiersz.k_19 = elem.Element(tns + "K_19").Value.ToString();
                //wiersz.k_20 = elem.Element(tns + "K_20").Value.ToString();

                listaSprzedazy.Add(wiersz);
            }

            return listaSprzedazy;
        }
    }
}
