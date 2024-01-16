using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FirmWebApiDemo.Models
{
    public class USR01_EMP01
    {

        /// <summary>
        /// File location to UserEmployee.json file
        /// </summary>
        public static string UserEmployeeFilePath = HttpContext.Current.Server.MapPath(@"~/data/UserEmployee.json");


        /// <summary>
        /// User id
        /// </summary>
        public int p01f01;

        /// <summary>
        /// Employee id
        /// </summary>
        public int p01f02;

        public USR01_EMP01(int usedId, int EmployeeId)
        {
            this.p01f01 = usedId;
            this.p01f02 = EmployeeId;
        }


        /// <summary>
        /// Fetches List of USR01_EMP01 objects from UserEmployee.json file
        /// </summary>
        /// <returns></returns>
        public static List<USR01_EMP01> GetUserEmployees()
        {
            List<USR01_EMP01> UserEmployees = null;
            using (StreamReader sr = new StreamReader(UserEmployeeFilePath))
            {
                string userEmployeeJson = sr.ReadToEnd();
                UserEmployees = JsonConvert.DeserializeObject<List<USR01_EMP01>>(userEmployeeJson);
            }
            return UserEmployees;
        }

        /// <summary>
        /// Sets a UserId, EmployeeId field into UserEmployee.json file
        /// </summary>
        /// <param name="UserId">User Id</param>
        /// <param name="EmployeeId">Employee Id</param>
        public static void SetUserEmployee(int UserId, int EmployeeId)
        {
            List<USR01_EMP01> UserEmployees = GetUserEmployees();

            UserEmployees.Add(new USR01_EMP01(UserId, EmployeeId));

            string UserEmployeejson = JsonConvert.SerializeObject(UserEmployees, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(UserEmployeeFilePath))
            {
                sw.Write(UserEmployeejson);
            }
        }
    }
}