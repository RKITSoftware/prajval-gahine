using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2
{
    public class ValidateUser
    {
        /// <summary>
        /// Method to validate given credentials of user
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">user password</param>
        /// <returns></returns>
        public static bool Login(string username, string password)
        {
            // role based authentication
            if (USR01.GetUsers().Any<USR01>(user => username == user.r01f02 && password == user.r01f03))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Gets instance of USR01 if given credentials are valid
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">user password</param>
        /// <returns>returns instance of USR01 if credentials are valid else null</returns>
        public static USR01 GetUserDetails(string username, string password)
        {
            return USR01.GetUsers().FirstOrDefault<USR01>(user => username == user.r01f02 && password == user.r01f03);
        }
    }
}