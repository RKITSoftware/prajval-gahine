using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace FirmAdvanceDemo.Utitlity
{
    public class GeneralUtility
    {
        public static Byte[] GetHMAC(string text, string key)
        {
            key = key ?? "FirmAdvanceDemoSecretKey";

            using (HMACSHA256 hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                Byte[] hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return hash;
            }
        }
    }
}