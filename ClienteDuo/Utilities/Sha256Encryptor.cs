using System;
using System.Security.Cryptography;
using System.Text;

namespace ClienteDuo.Utilities
{
    internal class Sha256Encryptor
    {
        protected Sha256Encryptor()
        {
        }

        public static String SHA256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }
    }
}
