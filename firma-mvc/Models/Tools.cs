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
            DirectoryInfo dir = new DirectoryInfo("tmp");
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Name.Contains(filename))
                {
                    file.Delete();
                }
            }
        }
    }
}
