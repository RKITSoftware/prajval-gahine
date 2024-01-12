using System.Collections.Generic;

namespace WebApplication2.Models
{
    public class User
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

        public static List<User> GetUsers()
        {
            List<User> users = new List<User>()
            {
                new User() { r01f01 = 1, r01f02 = "prajvalgahine", r01f03 = "prajval@123", r01f04 = "employee"},
                new User() { r01f01 = 2, r01f02 = "yashlathiya", r01f03 = "yash@123", r01f04 = "employee"},
                new User() { r01f01 = 3, r01f02 = "deeppatel", r01f03 = "deep@123", r01f04 = "employee"},
                new User() { r01f01 = 4, r01f02 = "priteshgahine", r01f03 = "pritesh@123", r01f04 = "admin"}
            };
            return users;
        }
    }
}