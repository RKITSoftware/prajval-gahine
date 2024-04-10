using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models;
using FirmAdvanceDemo.Utitlity;
using ServiceStack.OrmLite;
using System.Data;
using System.Linq;

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
        /// <param name="userId">Out paramter, mehtod sets user id if user exists else set to 0</param>
        /// <param name="roles">Out parameter, method sets user-roles if user exists else set null</param>
        /// <returns></returns>
        public static bool Login(string username, string password, out int userId, out string[] roles)
        {
            userId = 0;
            roles = null;

            // first hash the password
            byte[] hashedPassword = GeneralUtility.GetHMAC(password, null);

            // get userId
            ResponseStatusInfo rsiGetUserId = BLUser.FetchUserIdByUsername(username);

            // get roles associated with that userId
            if (rsiGetUserId.IsRequestSuccessful)
            {
                userId = (int)rsiGetUserId.Data;
                ResponseStatusInfo rsiGetUserRoles = BLUser.FetchUserRolesByUserId(userId);
                if (rsiGetUserRoles.IsRequestSuccessful)
                {
                    roles = (string[])rsiGetUserRoles.Data;
                }
            }

            if(userId == 0 || roles == null)
            {
                return false;
            }
            return true;
        }
    }
}