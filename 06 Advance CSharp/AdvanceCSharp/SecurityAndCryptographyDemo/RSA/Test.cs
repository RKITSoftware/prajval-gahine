
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecurityAndCryptographyDemo.RSA
{
    /// <summary>
    /// Class to demonstrate aes encryption and decryption
    /// </summary>
    internal class Test
    {
        private static UnicodeEncoding _encoder = new UnicodeEncoding();


        /// <summary>
        /// Method to demonstrate aes encryption and decryption
        /// </summary>
        public static void EncryptionDecryptionRSA()
        {

            // get public key
            byte[] encryptedData = Encrypt("Hello World=====");

            string data = Decrypt(encryptedData);

            Console.WriteLine("\nDecrypted data:");
            Console.WriteLine(data);
        }

        /// <summary>
        /// Aes encryption method
        /// </summary>
        /// <param name="plainText">Plain text to be decrypted</param>
        /// <param name="aes">Aes instance</param>
        /// <returns>byte[] encrypted data</returns>
        public static byte[] Encrypt(string data)
        {

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);

            string publicKey = string.Empty;
            using (StreamReader sr = new StreamReader("./RSA/keys/PublicKey.xml"))
            {
                publicKey = sr.ReadToEnd();
            }
            rsa.FromXmlString(publicKey);

            byte[] dataToEncrypt = _encoder.GetBytes(data);

            byte[] encyrptedData = rsa.Encrypt(dataToEncrypt, true);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < encyrptedData.Length; i++)
            {
                sb.Append(encyrptedData[i]);
                if (i != encyrptedData.Length - 1)
                {
                    sb.Append(" ");
                }
            }
            Console.WriteLine("Encrypted data: ");
            Console.WriteLine(sb.ToString());
            return encyrptedData;
        }

        /// <summary>
        /// Aes decryption method
        /// </summary>
        /// <param name="encryptedData">Encrypted data</param>
        /// <param name="aes">Aes instance</param>
        /// <returns>plain text string</returns>
        public static string Decrypt(byte[] encryptedData)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);

            string privateKey = string.Empty;
            using (StreamReader sr = new StreamReader("./RSA/keys/PrivateKey.xml"))
            {
                privateKey = sr.ReadToEnd();
            }
            byte[] decryptedData = rsa.Decrypt(encryptedData, true);
            return _encoder.GetString(decryptedData);
        }
    }
}
