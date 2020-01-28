using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace firma_mvc
{
    public class Tools
    {
        public static decimal decimalRound(decimal number)
        {
            return Math.Round(number, 2);
        }

        public static string getHash(string input)
        {
            string hashAlgo = "SHA256";
            HashAlgorithm algo = HashAlgorithm.Create(hashAlgo);
            byte[] hashBytes = algo.ComputeHash(Encoding.UTF8.GetBytes(input));

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("X2"));
            }
            string computedHash = sb.ToString();

            return computedHash;
        }

        public static void deleteTempFiles(string filename)
        {
            // DirectoryInfo dir = new DirectoryInfo("tmp");
            // foreach (FileInfo file in dir.GetFiles())
            // {
            //     if (file.Name.Contains(filename))
            //     {
            //         file.Delete();
            //     }
            // }
        }

        public static Dictionary<int, string> getMonthsDictionary()
        {
            Dictionary<int, string> months = new Dictionary<int, string>()
            {
                { 1, "styczeń" },
                { 2, "luty" },
                { 3, "marzec"},
                {4, "kwiecień" },
                {5, "maj" },
                {6,"czerwiec" },
                {7,"lipiec" },
                {8,"sierpień" },
                {9,"wrzesień" },
                {10, "październik" },
                {11, "listopad" },
                {12, "grudzień" }
            };

            return months;
        }

        public static List<int> getYearsList()
        {
            List<int> years = new List<int>()
            {
                2018,
                2019,
                2020
            };
            return years;
        }

        public static Dictionary<int, string> getJPKtypes()
        {
            Dictionary<int, string> jpkTypes = new Dictionary<int, string>()
            {
                {1,"JPK_VAT" },
            {2, "JPK_Ksiega" },
                { 3, "JPK_Faktura"}
            };

            return jpkTypes;
        }

        public static string handleLatexSpecialChars(string input)
        {
            char[] specChars = new char[] {'#', '_', '$', '&'};
            input = input.Replace(@"\", @"\textbackslash");
            input = input.Replace('"', '\'');       
            
            foreach(char c in specChars)
            {
                string str = @"\"+c.ToString();
                input=input.Replace(c.ToString(), str);
            }
     
            return input;
        }
    }
}
