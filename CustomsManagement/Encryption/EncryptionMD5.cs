using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace CustomersManagement.Encryption
{
    public class EncryptionMD5
    {
        public static string EncryptionByMD5(string str)
        {
            MD5CryptoServiceProvider Md5 = new MD5CryptoServiceProvider();

            byte[] Bte = Encoding.UTF8.GetBytes(str);
    
            Bte=Md5.ComputeHash(Bte);

            StringBuilder Sb = new StringBuilder();

            foreach (byte ba in Bte) {
                Sb.Append(ba.ToString("x2").ToLower());
            }
            return Sb.ToString();
        }
    }
}
