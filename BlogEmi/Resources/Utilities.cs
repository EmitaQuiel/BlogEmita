using System.Text;
using System.Security.Cryptography;

namespace BlogEmi.Resources
{
    public class Utilities
    {
        public static string EncryptKey(string key)
        {
            StringBuilder sb = new StringBuilder();
            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;

                byte[] result = hash.ComputeHash(enc.GetBytes(key));

                foreach (byte b in result)
                {
                    sb.Append(b.ToString("x2"));
                }

                return sb.ToString();
            }
        }
    }
}
