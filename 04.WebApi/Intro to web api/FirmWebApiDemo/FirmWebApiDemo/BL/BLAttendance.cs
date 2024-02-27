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
    public class BLAttendance
    {
        /// <summary>
        /// File location to Attendance.json file
        /// </summary>
        private static readonly string AttendacneFilePath = HttpContext.Current.Server.MapPath(@"~/App_Data/Attendance.json");

        /// <summary>
        /// Gets all Employee Attedance list
        /// </summary>
        /// <returns>list of attendances</returns>
        public List<ATD01> GetAttendances()
        {
            List<ATD01> lstAttendance = (List<ATD01>)CacheManager.AppCache.Get("lstAttendance");

            if (lstAttendance == null)
            {
                using (StreamReader sr = new StreamReader(AttendacneFilePath))
                {
                    string attendanceJson = sr.ReadToEnd();
                    lstAttendance = JsonConvert.DeserializeObject<List<ATD01>>(attendanceJson);
                }

                CacheDependency cacheDependency = new CacheDependency(
                    AttendacneFilePath,
                    DateTime.Now.AddSeconds(20)
                );

                CacheManager.AppCache.Insert(
                    "lstAttendance",
                    lstAttendance,
                    cacheDependency,
                    DateTime.MaxValue,
                    new TimeSpan(0, 0, 20)
                );
            }
            return lstAttendance;
        }

        /// <summary>
        /// Method to add attendance in Attendance.json file array
        /// </summary>
        /// <param name="attendance">An Attendance object of ATD01 type</param>
        public void SetAttendance(ATD01 attendance)
        {
            List<ATD01> lstAttendance = GetAttendances();

            // add the user to list
            lstAttendance.Add(attendance);

            using (StreamWriter sw = new StreamWriter(AttendacneFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(lstAttendance, Formatting.Indented));
            }
        }

        /// <summary>
        /// Method to add list of attendance in Attendance.json file array
        /// </summary>
        /// <param name="employee">An Attendance List of ATD01 type</param>
        public void SetAttendances(List<ATD01> lstAttendance)
        {
            using (StreamWriter sw = new StreamWriter(AttendacneFilePath))
            {
                sw.Write(JsonConvert.SerializeObject(lstAttendance, Formatting.Indented));
            }
        }
    }
}