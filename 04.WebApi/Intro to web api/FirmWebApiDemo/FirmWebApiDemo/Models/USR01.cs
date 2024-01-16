using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FirmWebApiDemo.Models
{
    public class USR01
    {
        /// <summary>
        /// File location to User.json file
        /// </summary>
        private static string UserFilePath = HttpContext.Current.Server.MapPath(@"~/data/User.json");

        /// <summary>
        /// User Id
        /// </summary>
        [Key]
        public int r01f01 { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public string r01f02 { get; set; }

        /// <summary>
        /// User Password
        /// </summary>
        public string r01f03 { get; set; }

        /// <summary>
        /// User Role
        /// </summary>
        public string r01f04 { get; set; }

        /// <summary>
        /// Method to fetch all users list from USR01.json file
        /// </summary>
        /// <returns>List of user of USR01 class</returns>
        public static List<USR01> GetUsers()
        {
            List<USR01> users = null;
            // get employee array from data.User.json file
            using (StreamReader sr = new StreamReader(UserFilePath))
            {
                string usersJson = sr.ReadToEnd();
                users = JsonConvert.DeserializeObject<List<USR01>>(usersJson);
            }
            return users;
        }

        /// <summary>
        /// Method to add user in User.json file array
        /// </summary>
        /// <param name="user">An user object of USR01 type</param>
        public static void SetUser(USR01 user)
        {
            List<USR01> users = users = GetUsers();

            // add the user to list
            users.Add(user);

            using (StreamWriter sw  = new StreamWriter(UserFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(users, Formatting.Indented));
            }
        }

        /// <summary>
        /// Method to add users in User.json file array
        /// </summary>
        /// <param name="user">An user List of USR01 type</param>
        public static void SetUsers(List<USR01> lstUser)
        {
            using (StreamWriter sw = new StreamWriter(UserFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(lstUser, Formatting.Indented));
            }
        }
    }
}