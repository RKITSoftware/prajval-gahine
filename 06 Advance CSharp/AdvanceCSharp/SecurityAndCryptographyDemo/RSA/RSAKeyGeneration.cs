using System.IO;
using System.Security.Cryptography;

namespace SecurityAndCryptographyDemo.RSA
{
    /// <summary>
    /// Class to generate public and private keys for RSA algorithm
    /// </summary>
    internal class RSAKeyGeneration
    {
        /// <summary>
        /// Method to generate and store RSA public and private key
        /// </summary>
        public static void GenerateAndStoreRSAKey()
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(2048);

            string privateKey = rsa.ToXmlString(true);
            using (StreamWriter sw = new StreamWriter("./RSA/keys/PrivateKey.xml"))
            {
                sw.Write(privateKey);
            }

            string publicKey = rsa.ToXmlString(false);
            using (StreamWriter sw = new StreamWriter("./RSA/keys/PublicKey.xml"))
            {
                sw.Write(publicKey);
            }
        }
    }
}
