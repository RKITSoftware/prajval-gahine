using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.BL
{
    /// <summary>
    /// User Business Logic class to manage employee attendance
    /// </summary>
    public class BLUser
    {
        /// <summary>
        /// Method to get all users
        /// </summary>
        /// <returns>List of users</returns>
        public static List<USR01> GetUsers()
        {
            List<USR01> users = new List<USR01>()
            {
                new USR01() { r01f01 = 1, r01f02 = "prajvalgahine", r01f03 = "prajval@123", r01f04 = "employee"},
                new USR01() { r01f01 = 2, r01f02 = "yashlathiya", r01f03 = "yash@123", r01f04 = "employee"},
                new USR01() { r01f01 = 3, r01f02 = "deeppatel", r01f03 = "deep@123", r01f04 = "employee"},
                new USR01() { r01f01 = 4, r01f02 = "priteshgahine", r01f03 = "pritesh@123", r01f04 = "admin"}
            };
            return users;
        }
    }
}