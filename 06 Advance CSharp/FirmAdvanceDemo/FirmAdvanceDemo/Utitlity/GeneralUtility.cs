using Org.BouncyCastle.Asn1.Cms;
using Org.BouncyCastle.Asn1.Pkcs;
using System;
using System.IO;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;

namespace FirmAdvanceDemo.Utitlity
{
    /// <summary>
    /// Class to supply general utility operations
    /// </summary>
    public class GeneralUtility
    {
        /// <summary>
        /// Method to create and attach principal to current thread and http context
        /// </summary>
        /// <param name="userId">user id</param>
        /// <param name="username">username</param>
        /// <param name="roles">user roles</param>
        /// <returns></returns>
        public static bool AttachPrinicpal(string userId, string username, string[] roles)
        {
            GenericIdentity identity = new GenericIdentity(username);
            identity.AddClaim(new Claim("Id", userId));
            identity.AddClaim(new Claim("Username", username));

            GenericPrincipal principal = new GenericPrincipal(identity, roles);

            Thread.CurrentPrincipal = principal;
            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
                return true;
            }
            return false;
        }


        /// <summary>
        /// Get HMAC (Hash-based Message Authentication Code) hash of given string(text) using given string(key)
        /// </summary>
        /// <param name="text">String whose secure hash is to be calculated</param>
        /// <param name="key">Key using which the hash is to computed</param>
        /// <returns></returns>
        public static Byte[] GetHMAC(string text, string key)
        {
            key = key ?? "FirmAdvanceDemoSecretKey";

            using (HMACSHA256 hmacsha256 = new HMACSHA256(Encoding.UTF8.GetBytes(key)))
            {
                Byte[] hash = hmacsha256.ComputeHash(Encoding.UTF8.GetBytes(text));
                return hash;
            }
        }

        /// <summary>
        /// Method to perform AesEncryption on given text using given key
        /// </summary>
        /// <param name="text">Text which is to be encrypted</param>
        /// <param name="key">Key using which aes encryption is to be done</param>
        /// <returns></returns>
        public static string AesEncrypt(string text, string key)
        {
            string encyrptedText = null;

            key = key ?? "FirmAdvanceDemoR";
            string IV = "abcdefghijklmnop";

            byte[] encodedIV = Encoding.UTF8.GetBytes(IV);
            byte[] encodedKey = Encoding.UTF8.GetBytes(key);

            using (Aes aes = AesManaged.Create())
            {
                aes.Key = encodedKey;
                aes.IV = encodedIV;

                ICryptoTransform aesEncryptor = aes.CreateEncryptor();

                MemoryStream memoryStream = new MemoryStream();

                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter streamWriter = new StreamWriter(cryptoStream))
                    {
                        streamWriter.Write(text);
                    }
                    encyrptedText = Convert.ToBase64String(memoryStream.ToArray());
                }
            }
            return encyrptedText;
        }

        /// <summary>
        /// Method to perform AesEncryption on given ecrypted text using given key
        /// </summary>
        /// <param name="encryptedText">Encrypted Text which is to be decoded</param>
        /// <param name="key">Key using which aes decryption is to be done</param>
        /// <returns></returns>
        public static string AesDecrypt(string encryptedText, string key)
        {
            string decryptedText = null;

            key = key ?? "FirmAdvanceDemoR";
            string IV = "abcdefghijklmnop";

            byte[] encodedIV = Encoding.UTF8.GetBytes(IV);
            byte[] encodedKey = Encoding.UTF8.GetBytes(key);

            using (Aes aes = AesManaged.Create())
            {
                aes.Key = encodedKey;
                aes.IV = encodedIV;

                ICryptoTransform aesDecryptor = aes.CreateDecryptor();

                MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(encryptedText));

                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Read))
                {
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        decryptedText = streamReader.ReadToEnd();
                    }
                }
            }
            return decryptedText;
        }
    }
}