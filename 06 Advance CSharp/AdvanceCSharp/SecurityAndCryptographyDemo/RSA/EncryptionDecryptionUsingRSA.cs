using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SecurityAndCryptographyDemo.RSA
{
    /// <summary>
    /// Class to perform RSA encryption and decryption
    /// </summary>
    internal class EncryptionDecryptionUsingRSA
    {
        /// <summary>
        /// An unicode encoder
        /// </summary>
        private static UnicodeEncoding _encoder = new UnicodeEncoding();

        /// <summary>
        /// Method to demonstrate RSA encrytion and decryption
        /// </summary>
        public static void RSA()
        {
            var text = "Hello World";

            Console.WriteLine("Text to encrypt: " + text);

            byte[] encData = Encrypt(text);
            string decData = Decrypt(encData);

            Console.WriteLine("Decrypted Text: " + decData);
        }

        /// <summary>
        /// RSA decryption method
        /// </summary>
        /// <param name="data">Byte data to decrypt</param>
        /// <returns>decrypted plain text</returns>
        public static string Decrypt(byte[] data)
        {
            var rsa = new RSACryptoServiceProvider();

            // get private key
            string privateKey = string.Empty;
            using (StreamReader sr = new StreamReader("./RSA/keys/PrivateKey.xml"))
            {
                privateKey = sr.ReadToEnd();
            }

            rsa.FromXmlString(privateKey);

            var decryptedByte = rsa.Decrypt(data, false);

            return _encoder.GetString(decryptedByte);
        }

        /// <summary>
        /// RSA Encryption method
        /// </summary>
        /// <param name="data">Plain text</param>
        /// <returns>decrypted byte data</returns>
        public static byte[] Encrypt(string data)
        {
            var rsa = new RSACryptoServiceProvider();


            // get public key
            string publicKey = string.Empty;
            using (StreamReader sr = new StreamReader("./RSA/keys/PublicKey.xml"))
            {
                publicKey = sr.ReadToEnd();
            }

            rsa.FromXmlString(publicKey);

            var dataToEncrypt = _encoder.GetBytes(data);

            var encryptedByteArray = rsa.Encrypt(dataToEncrypt, false).ToArray();

            // print decrypted data
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < encryptedByteArray.Length; i++)
            {
                sb.Append(encryptedByteArray[i]);
                if (i != encryptedByteArray.Length - 1)
                {
                    sb.Append(" ");
                }
            }
            Console.WriteLine("Encrypted data: ");
            Console.WriteLine(sb.ToString());

            return encryptedByteArray;
        }
    }
}
