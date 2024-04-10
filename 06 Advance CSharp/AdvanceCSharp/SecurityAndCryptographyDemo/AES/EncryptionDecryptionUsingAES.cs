
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecurityAndCryptographyDemo.AES
{
    /// <summary>
    /// Class to demonstrate aes encryption and decryption
    /// </summary>
    internal class EncryptionDecryptionUsingAES
    {
        /// <summary>
        /// Method to demonstrate aes encryption and decryption
        /// </summary>
        public static void EncryptionDecryptionUsingAesManaged()
        {
            // create a key
            string keyStr = "YbC8MDAdgX6EA2hE";
            byte[] key = Encoding.UTF8.GetBytes(keyStr);

            string data = "hello, world!";

            using (AesManaged aes = new AesManaged())
            {
                aes.Key = key;

                byte[] encryptedData = Encrypt(data, aes);
                Console.WriteLine($"Your encrypted data is: {Encoding.UTF8.GetString(encryptedData)}");

                string plainText = Decrypt(encryptedData, aes);
                Console.WriteLine($"Your decrypted text is: {plainText}");
            }
        }

        /// <summary>
        /// Aes encryption method
        /// </summary>
        /// <param name="plainText">Plain text to be decrypted</param>
        /// <param name="aes">Aes instance</param>
        /// <returns>byte[] encrypted data</returns>
        public static byte[] Encrypt(string plainText, AesManaged aes)
        {
            byte[] encryptedData;

            // create encryptor
            ICryptoTransform aesEncryptor = aes.CreateEncryptor();

            // create memory stream
            MemoryStream memoryStream = new MemoryStream();

            // create crypto stream using the crypto class
            using (CryptoStream crpytoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write))
            {
                // create stream writer and write data to the cryptostream
                using (StreamWriter streamWriter = new StreamWriter(crpytoStream))
                {
                    streamWriter.Write(plainText);
                }
                encryptedData = memoryStream.ToArray();
            }
            return encryptedData;
        }

        /// <summary>
        /// Aes decryption method
        /// </summary>
        /// <param name="encryptedData">Encrypted data</param>
        /// <param name="aes">Aes instance</param>
        /// <returns>plain text string</returns>
        public static string Decrypt(byte[] encryptedData, AesManaged aes)
        {
            string plainText = null;

            // create aes decryptor
            ICryptoTransform aesDecryptor = aes.CreateDecryptor();

            // create a memory stream instance
            using (MemoryStream memoryStream = new MemoryStream(encryptedData))
            {
                // create a cryoto stream instance
                using (CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Read))
                {
                    // read crypto stream
                    using (StreamReader streamReader = new StreamReader(cryptoStream))
                    {
                        plainText = streamReader.ReadToEnd();
                    }
                }
            }
            return plainText;
        }
    }
}
