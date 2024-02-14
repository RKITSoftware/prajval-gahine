using FirmAdvanceDemo.Models;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Business logic class for User - defines all props and methods to suppor User controller
    /// </summary>
    public class BLUser : BLResource<USR01>
    {
        /// <summary>
        /// Method to fetch used id using username from database
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>ResponseStatusInfo instance containing userId if successful or null if any exception</returns>
        public static ResponseStatusInfo FetchUserIdByUsername(string username)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    SqlExpression<USR01> sqlExp = db.From<USR01>()
                        .Where(user => user.r10f02 == username)
                        .Select<USR01>(user => user.Id);

                    int userId = db.Single<int>(sqlExp);
                    if (userId == 0)
                    {
                        throw new Exception($"No user exists with user name: {username}");
                    }
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"UserId with username is: {userId}",
                        Data = userId
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseStatusInfo()
                {
                    IsRequestSuccessful = false,
                    Message = ex.Message,
                    Data = 0
                };
            }
        }

        /// <summary>
        /// Method to fetch username using used id
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>ResponseStatusInfo instance containing username if successful or null if any exception</returns>
        public static ResponseStatusInfo FetchUsernameByUserId(int userId)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    SqlExpression<USR01> sqlExp = db.From<USR01>()
                        .Where(user => user.Id == userId)
                        .Select<USR01>(user => user.r10f02);

                    string username = db.Single<string>(sqlExp);
                    if (username == null)
                    {
                        throw new Exception($"No user exists with user id: {userId}");
                    }
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"Usernmame of user with userId {username}",
                        Data = username
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

        /// <summary>
        /// Method to fetch user roles using userid
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>ResponseStatusInfo instance containing userId if successful or null if any exception</returns>
        public static ResponseStatusInfo FetchUserRolesByUserId(int userId)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    // get user roles
                    var SqlExp = db.From<USR01RLE01>()
                        .Where(ur => ur.r01f01 == userId)
                        .Join<RLE01>((ur, r) => ur.r01f02 == r.Id)
                        .Select<RLE01>(r => r.e01f02);

                    string[] roles = db.Select<string>(SqlExp).ToArray<string>();
                    return new ResponseStatusInfo()
                    {
                        IsRequestSuccessful = true,
                        Message = $"User roles with user id: {userId}",
                        Data = roles
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

        /// <summary>
        /// Method to add a User to database, which indeed adds dependent entity (employee or admin) and populates dependent junction tables accordingly
        /// </summary>
        /// <param name="UserEmployeeJson">JSON object containing user and/or employee information</param>
        /// <returns>ResponseStatusInfo instance containing data as null</returns>
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
                        r01f06 = DateTime.Now.Date
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
                            p01f05 = DateTime.Parse((string)EmployeeJson["p01f05"]).Date,
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