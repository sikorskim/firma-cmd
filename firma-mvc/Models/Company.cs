using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class Company
    {
    public int Id { get; set; }
    public string NIP { get; set; }
    [DisplayName("Pełna nazwa")]
    public string FullName { get; set; }
    [DisplayName("Nazwa")]
    public string Name { get; set; }
    [DisplayName("Kod kraju")]
    public string CountryCode { get; set; }
    [DisplayName("Województwo")]
    public string Voivodeship { get; set; }
    [DisplayName("Powiat")]
    public string County { get; set; }
    [DisplayName("Gmina")]
    public string Community { get; set; }
    [DisplayName("Miejscowość")]
    public string City { get; set; }
    [DisplayName("Ulica")]
    public string Street { get; set; }
    [DisplayName("Nr budynku")]
    public string BuldingNo { get; set; }
    [DisplayName("Kod pocztowy")]
    public string PostalCode { get; set; }
    [DisplayName("Poczta")]
    public string PostOffice { get; set; }
    [DisplayName("E-mail")]
    public string Email { get; set; }
    [DisplayName("Telefon")]
    public string Phone { get; set; }
}
}
