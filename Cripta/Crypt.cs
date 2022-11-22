using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cripta
{
    internal class Crypt
    {
        public string Encrypt(string elemento, string key)
        {
            //Clave de incriptacion hash

            string hash = key;
            byte[] data = UTF8Encoding.UTF8.GetBytes(elemento);

            //Configuraciones para el transform
            MD5 md5 = MD5.Create();
            TripleDES tripledes = TripleDES.Create();

            tripledes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripledes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripledes.CreateEncryptor();

            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(result);
        }

        public string Decrypt(string elemento, string key)
        {
            string hash = key;
            byte[] data = Convert.FromBase64String(elemento);

            //Configuraciones para el transform
            MD5 md5 = MD5.Create();
            TripleDES tripledes = TripleDES.Create();

            tripledes.Key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            tripledes.Mode = CipherMode.ECB;

            ICryptoTransform transform = tripledes.CreateDecryptor();

            byte[] result = transform.TransformFinalBlock(data, 0, data.Length);

            return UTF8Encoding.UTF8.GetString(result);
        }
    }
}
