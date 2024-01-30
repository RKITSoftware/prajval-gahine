using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Data;
using System.Linq;
using System.Web;
using FirmAdvanceDemo.Models;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using ServiceStack.OrmLite;
using static System.Net.Mime.MediaTypeNames;
using System.Security.Cryptography;
using System.Text;

namespace FirmAdvanceDemo.BL
{
    public class BLUser : BLResource<USR01>
    {
        public static ResponseStatusInfo AddResource(JObject UserEmployeeJson)
        {

            JObject UserJson = (JObject)UserEmployeeJson["user"];
            JObject EmployeeJson = (JObject)UserEmployeeJson["employee"];
            JObject AdminJson = (JObject)UserEmployeeJson["admin"];

            try
            {
                if (UserJson == null)
                {
                    throw new Exception("No user details provided");
                }

                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    string secretKey = "FirmAdvanceDemoSecretKey";
                    // hash the password 
                    Byte[] hashedPassword = GeneralUtility.GetHMAC((string)UserJson["r01f03"], secretKey);

                    // create user and get the user id
                    USR01 user = new USR01()
                    {
                        Id = -1,
                        r10f02 = (string)UserJson["r01f02"],
                        r01f03 = hashedPassword,
                        r01f04 = (string)UserJson["r01f04"],
                        r01f05 = (string)UserJson["r01f05"],
                        r01f06 = DateTime.Now
                    };
                    int userId = (int)db.Insert<USR01>(user, selectIdentity: true);

                    // populate the user-role table
                    List<int> roles = ((JArray)UserJson["roles"]).ToObject<List<int>>();

                    IEnumerable<USR01RLE01> lstRoleUser = roles.Select<int, USR01RLE01>(roleId => new USR01RLE01()
                    {
                        r01f01 = userId,
                        r01f02 = roleId
                    });

                    db.InsertAll(lstRoleUser);

                    int employeeId = 0;
                    // now check if the user is an employee ? (ie. role is 2)
                    if (roles.Contains<int>(2) && EmployeeJson != null)
                    {
                        // create employee object and get the employee id
                        EMP01 employee = new EMP01()
                        {
                            Id = -1,
                            p01f02 = (string)EmployeeJson["p01f02"],
                            p01f03 = (string)EmployeeJson["p01f03"],
                            p01f04 = ((string)EmployeeJson["p01f04"])[0],
                            p01f05 = DateTime.Parse((string)EmployeeJson["p01f05"]),
                            p01f06 = (int)EmployeeJson["p01f06"]
                        };
                        employeeId = (int)db.Insert<EMP01>(employee, selectIdentity: true);

                        // populate the user-employee table
                        USR01EMP01 userEmployee = new USR01EMP01()
                        {
                            p01f01 = userId,
                            p01f02 = employeeId
                        };

                        db.Insert(userEmployee);
                    }
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"User created with username: {(string)UserJson["r01f02"]}",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}