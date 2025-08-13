using Pax360.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace Pax360.Helpers
{
    public class CryptographyHelper : ICryptographyHelper
    {
        public string CreateMD5(string str)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(str);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("x2"));
                }

                Console.WriteLine(sb.ToString());
                return sb.ToString();
            }
        }
    }
}
