using firma_lib;
using System;

namespace firma_cmd
{
    class Program
    {
        static void Main(string[] args)
        {
            Contractor contractor = new Contractor("777", "firma1","","", "", "", "", "", "", "","");
            contractor.add();
            contractor.getAll();
            Console.Read();
        }
    }
}
