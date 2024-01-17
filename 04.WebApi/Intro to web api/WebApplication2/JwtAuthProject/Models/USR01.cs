using System.Collections.Generic;
using System.Linq;

namespace JwtAuthProject.Models
{
    public class USR01
    {
        /// <summary>
        /// User id
        /// </summary>
        public int r01f01 { get; set; }

        /// <summary>
        /// Username of the user
        /// </summary>
        public string r01f02 { get; set; }

        /// <summary>
        /// password of user for authentication purpose
        /// </summary>
        public string r01f03 { get; set; }

        /// <summary>
        /// user role for authorization purpose
        /// </summary>
        public string r01f04 { get; set; }

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

        public static USR01 GetUser(int id)
        {
            return USR01.GetUsers().Where(user => user.r01f01 == id).FirstOrDefault();
        }
    }
}