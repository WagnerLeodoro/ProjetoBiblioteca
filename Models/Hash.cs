using System;
using System.Security.Cryptography;
using System.Text;

namespace Biblioteca.Models
{
    public class Hash
    {
        public static string Crypto(string txt)
        {
            MD5 MD5Hasher = MD5.Create();
            byte[] bytes = Encoding.Default.GetBytes(txt);
            byte[] hash = MD5Hasher.ComputeHash(bytes);

            StringBuilder sb = new StringBuilder();
            foreach (byte b in hash)
            {
                string Debug = b.ToString("x2");
                sb.Append(Debug);
            }
            return sb.ToString();
        }
    }
}