namespace SecurityAndCryptographyDemo.RSA
{
    /// <summary>
    /// Entry class to demonstrate RSA encryption and decryption
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry method to demonstrate RSA encryption and decryption
        /// </summary>
        /// <param name="args">console arguments</param>
        static void Main(string[] args)
        {
            // Generate public and priavte key pair for RSA algorithm
            RSAKeyGeneration.GenerateAndStoreRSAKey();

            // demonstarting RSA encryption and decryption
            EncryptionDecryptionUsingRSA.RSA();
        }
    }
}