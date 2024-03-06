using FirmWebApiDemo.Models;
using FirmWebApiDemo.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Caching;

namespace FirmWebApiDemo.BL
{
    /// <summary>
    /// Buisness Logic class to handle user employee mapping
    /// </summary>
    public class BLUserEmployee
    {
        /// <summary>
        /// File location to UserEmployee.json file
        /// </summary>
        public static string UserEmployeeFilePath = HttpContext.Current.Server.MapPath(@"~/App_Data/UserEmployee.json");

        /// <summary>
        /// Fetches List of USR01_EMP01 objects from UserEmployee.json file
        /// </summary>
        /// <returns></returns>
        public List<UIDEID01> GetUserEmployees()
        {
            List<UIDEID01> lstUidEid = (List<UIDEID01>)CacheManager.AppCache.Get("lstEidUid");
            if (lstUidEid == null)
            {
                using (StreamReader sr = new StreamReader(UserEmployeeFilePath))
                {
                    string userEmployeeJson = sr.ReadToEnd();
                    lstUidEid = JsonConvert.DeserializeObject<List<UIDEID01>>(userEmployeeJson);
                }

                CacheDependency cacheDependency = new CacheDependency(
                    UserEmployeeFilePath,
                    DateTime.Now.AddSeconds(20)
                );

                CacheManager.AppCache.Insert(
                    "lstUidEid",
                    lstUidEid,
                    cacheDependency,
                    DateTime.MaxValue,
                    new TimeSpan(0, 0, 20)
                );
            }

            return lstUidEid;
        }

        /// <summary>
        /// Sets a UserId, EmployeeId field into UserEmployee.json file
        /// </summary>
        /// <param name="UserId">User Id</param>
        /// <param name="EmployeeId">Employee Id</param>
        public void SetUserEmployee(UIDEID01 UidEid)
        {
            List<UIDEID01> lstUserEmployee = GetUserEmployees();
            lstUserEmployee.Add(UidEid);

            string UserEmployeejson = JsonConvert.SerializeObject(lstUserEmployee, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(UserEmployeeFilePath))
            {
                sw.Write(UserEmployeejson);
            }
        }

        /// <summary>
        /// Mehtod to set a list of UIDEID01 to file
        /// </summary>
        /// <param name="lstUidEid">List of UIDEID01</param>
        public void SetUserEmployees(List<UIDEID01> lstUidEid)
        {
            string UserEmployeejson = JsonConvert.SerializeObject(lstUidEid, Formatting.Indented);

            using (StreamWriter sw = new StreamWriter(UserEmployeeFilePath))
            {
                sw.Write(UserEmployeejson);
            }
        }
    }
}