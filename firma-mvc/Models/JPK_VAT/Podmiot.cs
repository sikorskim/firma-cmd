namespace firma_mvc
{
    public class Podmiot
    {
        public string NIP { get; set; }
        public string PelnaNazwa { get; set; }
        public string Email { get; set; }

        public Podmiot(Company company)
        {
            NIP = company.NIP;
            PelnaNazwa = company.FullName;
            Email = company.Email;
        }
    }
}