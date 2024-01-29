
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SecurityAndCryptographyDemo
{
    internal class EncryptionUsingAES
    {
        public static void EncryptUsingAesManaged(){

            // create a key
            //byte[] key = new byte[]
            //{
            //    0x01, 0x12, 0x34, 0x56, 0x78, 0x90, 0x11, 0xAC,
            //    0x15, 0x8B, 0x7C, 0xD3, 0x9A, 0xAA, 0x13, 0x77
            //};

            string data = "hello, world";

            using (AesManaged aes = new AesManaged())
            {
                byte[] encryptedData = Encrypt(data, aes);
                Console.WriteLine($"Your encrypted data is: {Encoding.UTF8.GetString(encryptedData)}");
                
                string plainText = Decrypt(encryptedData, aes);
                Console.WriteLine($"Your decrypted text is: {plainText}");
            
            
            }
        }

        public static byte[] Encrypt(string plainText, AesManaged aes)
        {
            byte[] encryptedData;

            // create encryptor
            ICryptoTransform aesEncryptor = aes.CreateEncryptor();

            // create memory stream
            MemoryStream memoryStream = new MemoryStream();

            // create crypto stream using the crypto class
            using(CryptoStream crpytoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write))
            {
                // create stream writer and write data to the cryptostream
                using(StreamWriter streamWriter = new StreamWriter(crpytoStream))
                {
                    streamWriter.Write(plainText);
                }
                encryptedData = memoryStream.ToArray();
            }
            return encryptedData;
        }


        public static string Decrypt(byte[] encryptedData, AesManaged aes)
        {
            string plainText = null;

            // create aes decryptor
            ICryptoTransform aesDecryptor = aes.CreateDecryptor();

            // create a memory stream instance
            using(MemoryStream memoryStream = new MemoryStream(encryptedData))
            {
                // create a cryoto stream instance
                using(CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Read))
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
