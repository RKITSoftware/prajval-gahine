using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using System.Net;

namespace FirmAdvanceDemo.Auth
{
    /// <summary>
    /// Class containing authentication operations for an user
    /// </summary>
    public class ValidateUser
    {
        /// <summary>
        /// Method to validate user username and password
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <param name="userId">Out paramter, mehtod sets user ID if user exists else set to 0</param>
        /// <param name="roles">Out parameter, method sets user-roles if user exists else set null</param>
        /// <returns></returns>
        public static bool Login(string username, string password)
        {
            string hashedPassword = GeneralUtility.GetHMACBase64(password);

            string hashedPasswordFromDB = GeneralHandler.RetrievePassword(username);

            return hashedPasswordFromDB.Equals(hashedPassword);
        }
    }
}