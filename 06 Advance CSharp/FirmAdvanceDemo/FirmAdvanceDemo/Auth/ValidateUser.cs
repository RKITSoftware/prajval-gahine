using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;

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
        public static bool Login(string username, string password, out int userId, out string[] roles)
        {
            userId = 0;
            roles = null;

            // first hash the password
            byte[] hashedPassword = GeneralUtility.GetHMACBase64(password, null);

            // get userId
            BLUSR01Handler objBLUser = new BLUSR01Handler();
            Response rsiGetUserId = objBLUser.FetchUserIdByUsername(username);

            // get roles associated with that userId
            if (rsiGetUserId.IsError)
            {
                userId = (int)rsiGetUserId.Data;
                Response rsiGetUserRoles = objBLUser.FetchUserRolesByUserId(userId);
                if (rsiGetUserRoles.IsError)
                {
                    roles = (string[])rsiGetUserRoles.Data;
                }
            }

            if (userId == 0 || roles == null)
            {
                return false;
            }
            return true;
        }
    }
}