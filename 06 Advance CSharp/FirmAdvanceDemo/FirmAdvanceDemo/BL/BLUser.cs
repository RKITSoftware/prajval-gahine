using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using Newtonsoft.Json.Linq;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using static FirmAdvanceDemo.Utitlity.GeneralUtility;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Business logic class for User - defines all props and methods to suppor User controller
    /// </summary>
    public class BLUser : BLResource<USR01>
    {
        /// <summary>
        /// Instance of USR01 model
        /// </summary>
        public USR01 _objUSR01;

        /// <summary>
        /// List of Roles
        /// </summary>
        public List<EnmRole> lstRole;

        /// <summary>
        /// Default constructor for BLUser
        /// </summary>
        public BLUser()
        {

        }

        /// <summary>
        /// Constructor for BLUser, intialize 
        /// <see cref="ResponseStatusInfo"/>
        /// </summary>
        public BLUser(ResponseStatusInfo statusInfo) : base(statusInfo)
        {
        }

        /// <summary>
        /// Constructor for BLUser, initializes USR01 instance
        /// </summary>
        public BLUser(USR01 objUSR01)
        {
            _objUSR01 = objUSR01;
        }

        /// <summary>
        /// Method to prevalidate instance of DTOUMP ( user portion)
        /// </summary>
        /// <param name="objUSR01">instance of DTOUMP</param>
        /// <param name="role">role of user</param>
        /// <param name="operation">operation to perform</param>
        /// <returns></returns>
        public bool Prevalidate(IDTOUSR01 objUSR01, EnmRole role, EnmDBOperation operation)
        {
            bool isValid;
            if (operation == EnmDBOperation.Create)
            {
                isValid = (!objUSR01.r01102.IsNullOrEmpty()
                    && (!objUSR01.r01106.IsNullOrEmpty() && ValidateRole(role, objUSR01.r01106))
                    && (!objUSR01.r01103.IsNullOrEmpty() && ValidatePassword(objUSR01.r01103))
                    && (!objUSR01.r01105.IsNullOrEmpty() && ValidatePhoneNo(objUSR01.r01105))
                    && (!objUSR01.r01104.IsNullOrEmpty() && ValidateEmail(objUSR01.r01104))
                );
            }
            else
            {
                isValid = (objUSR01.r01102.IsNullOrEmpty()
                    && (objUSR01.r01106.IsNullOrEmpty() || ValidateRole(EnmRole.Employee, objUSR01.r01106))
                    && (objUSR01.r01103.IsNullOrEmpty() || ValidatePassword(objUSR01.r01103))
                    && (objUSR01.r01105.IsNullOrEmpty() || ValidatePhoneNo(objUSR01.r01105))
                    && (objUSR01.r01104.IsNullOrEmpty() || ValidateEmail(objUSR01.r01104))
                );
            }

            if (!isValid)
            {
                PopulateRSI(
                    false,
                    HttpStatusCode.BadRequest,
                    "Enter user data in correct format",
                    null
                );
            }
            return isValid;
        }

        /// <summary>
        /// Method to convert DTOUSR01 instance to USR01 instance
        /// </summary>
        /// <param name="objDTOUSR01">Instance of DTOUSR01</param>
        public void Presave(IDTOUSR01 objDTOUSR01, EnmDBOperation operation)
        {
            _objUSR01 = objDTOUSR01.ConvertModel<USR01>();

            // converting string password to hased password (bytes)
            string secretKey = (string)ConfigurationManager.AppSettings["PasswordHashSecretKey"];
            byte[] hashedPassword = GetHMAC(objDTOUSR01.r01103, secretKey);
            _objUSR01.r01f03 = hashedPassword;

            DateTime currentDateTime = DateTime.Now;
            if (operation == EnmDBOperation.Create)
            {
                _objUSR01.r01f06 = currentDateTime;
            }
            _objUSR01.r01f07 = currentDateTime;

            // initialize list of roles
            lstRole = objDTOUSR01.r01106;
        }

        /// <summary>
        /// Method to validate the USR01 instance
        /// </summary>
        /// <returns>True if USR01 instance is valid else false</returns>
        public bool Validate()
        {
            bool isValid = !DoesUsernameExists();
            if (!isValid && !_statusInfo.IsAlreadySet)
            {
                PopulateRSI(
                            false,
                            HttpStatusCode.OK,
                            "Username already exists",
                            null
                        );
            }
            return isValid;
        }

        /// <summary>
        /// Method to Add (Create) a new record of usr01 table in DB
        /// </summary>
        public void Save(EnmDBOperation operation, out int newUserId)
        {
            newUserId = 0;
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    if (operation == EnmDBOperation.Create)
                    {
                        newUserId = (int)db.Insert<USR01>(_objUSR01, selectIdentity: true);
                        int tempNewUserId = newUserId;
                        // create role - user.
                        List<ULE02> lstULE02 = lstRole.Select(role => new ULE02()
                        {
                            e02f02 = tempNewUserId,
                            e02f03 = role,
                            e02f04 = _objUSR01.r01f06,
                            e02f05 = _objUSR01.r01f06
                        }).ToList();
                        db.InsertAll<ULE02>(lstULE02);

                        PopulateRSI(true,
                            HttpStatusCode.OK,
                            $"User created with user id: {newUserId}",
                            null);
                    }
                    else
                    {
                        Dictionary<string, object> toUpdateDict = GetDictionary(_objUSR01);
                        toUpdateDict.Remove("r01f02");
                        db.UpdateOnly<USR01>(toUpdateDict, user => user.Id ==  _objUSR01.Id);

                        PopulateRSI(true,
                            HttpStatusCode.OK,
                            "User updated successfully",
                            null);
                    }
                }
            }
            catch (Exception ex)
            {
                PopulateRSI(false,
                    HttpStatusCode.BadRequest,
                    ex.Message,
                    null);
            }
        }

        /// <summary>
        /// Method to check does username exists in db
        /// </summary>
        /// <returns>true if username already exists else false</returns>
        public bool DoesUsernameExists()
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    //throw new Exception("SQL ERROR");
                    int count = (int)db.Count<USR01>(user => user.r01f02 == _objUSR01.r01f02);
                    return count > 0;
                }
            }
            catch(Exception ex)
            {
                PopulateRSI(
                    false,
                    HttpStatusCode.OK,
                    ex.Message,
                    null
                );
                return true;
            }
        }

        /// <summary>
        /// Method to fetch used id using username from database
        /// </summary>
        /// <param name="username">Username</param>
        /// <returns>ResponseStatusInfo instance containing userId if successful or null if any exception</returns>
        public ResponseStatusInfo FetchUserIdByUsername(string username)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    SqlExpression<USR01> sqlExp = db.From<USR01>()
                        .Where(user => user.r01f02 == username)
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
        public ResponseStatusInfo FetchUsernameByUserId(int userId)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    SqlExpression<USR01> sqlExp = db.From<USR01>()
                        .Where(user => user.Id == userId)
                        .Select<USR01>(user => user.r01f02);

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
        public ResponseStatusInfo FetchUserRolesByUserId(int userId)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    // get user roles
                    var SqlExp = db.From<ULE02>()
                        .Where(ur => ur.e02f02 == userId)
                        .Join<RLE01>((ur, r) => (int)ur.e02f03 == r.Id)
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
        public ResponseStatusInfo AddResource(JObject UserEmployeeJson)
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
                    //string secretKey = "FirmAdvanceDemoSecretKey";
                    string secretKey = ConfigurationManager.AppSettings["PasswordHashSecretKey"] as string;
                    // hash the password 
                    Byte[] hashedPassword = GeneralUtility.GetHMAC((string)UserJson["r01f03"], secretKey);

                    // create user and get the user id
                    USR01 user = new USR01()
                    {
                        Id = -1,
                        r01f02 = (string)UserJson["r01f02"],
                        r01f03 = hashedPassword,
                        r01f04 = (string)UserJson["r01f04"],
                        r01f05 = (string)UserJson["r01f05"],
                        r01f06 = DateTime.Now.Date
                    };
                    int userId = (int)db.Insert<USR01>(user, selectIdentity: true);

                    // populate the user-role table
                    List<int> roles = ((JArray)UserJson["roles"]).ToObject<List<int>>();

                    IEnumerable<ULE02> lstRoleUser = roles.Select<int, ULE02>(roleId => new ULE02()
                    {
                        e02f02 = userId,
                        e02f03 = (EnmRole)roleId
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
                        UMP02 userEmployee = new UMP02()
                        {
                            p02f02 = userId,
                            p02f03 = employeeId
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