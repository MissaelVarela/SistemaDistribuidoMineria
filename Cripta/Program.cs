using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;

namespace Cripta
{
    internal class Program
    {

        

        static void Main(string[] args)
        {
            Crypt ct = new Crypt();

            Console.WriteLine("-- Palabra para encriptar --");
            string a = Console.ReadLine();


            a = ct.Encrypt(a, "taco");

            Console.WriteLine("-- Palabra encriptada --");
            Console.WriteLine(a);
            a = ct.Decrypt(a, "taco");
            Console.WriteLine("-- Palabra desencriptada --");
            Console.WriteLine(a);
            Console.ReadKey();
        }
    }
}
