using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using ServerSideCachingDemo.Models;

namespace ServerSideCachingDemo.BL
{
    public class BLUser
    {
        /// <summary>
        /// Method to get all users
        /// </summary>
        /// <returns>List of users</returns>
        public static List<USR01> GetUsers()
        {
            Thread.Sleep(5000);
            List<USR01> lstUser = new List<USR01>()
            {
                new USR01() { r01f01 = 1, r01f02 = "prajvalgahine", r01f03 = "prajval@123", r01f04 = "employee"},
                new USR01() { r01f01 = 2, r01f02 = "yashlathiya", r01f03 = "yash@123", r01f04 = "employee"},
                new USR01() { r01f01 = 3, r01f02 = "deeppatel", r01f03 = "deep@123", r01f04 = "employee"},
                new USR01() { r01f01 = 4, r01f02 = "priteshgahine", r01f03 = "pritesh@123", r01f04 = "admin"}
            };
            return lstUser;
        }

        /// <summary>
        /// Method to get user with specific user id
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>A user instance of USR01 class</returns>
        public static USR01 GetUser(int id)
        {
            Thread.Sleep(5000);
            return BLUser.GetUsers().Where(user => user.r01f01 == id).FirstOrDefault();
        }
    }
}