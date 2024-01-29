using FirmWebApiDemo.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace FirmWebApiDemo.BL
{
    public class BLUser_Employee
    {

        /// <summary>
        /// File location to UserEmployee.json file
        /// </summary>
        public static string UserEmployeeFilePath = HttpContext.Current.Server.MapPath(@"~/data/UserEmployee.json");

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