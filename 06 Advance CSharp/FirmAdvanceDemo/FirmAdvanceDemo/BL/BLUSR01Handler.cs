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
    public class BLUSR01Handler
    {
        /// <summary>
        /// Instance of USR01 model
        /// </summary>
        public USR01 ObjUSR01 { get; set; }

        /// <summary>
        /// List of Roles
        /// </summary>
        public List<EnmRole> lstRole;

        public EnmOperation Operation { get; set; }
        protected readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Default constructor for BLUser
        /// </summary>
        public BLUSR01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
        }

        /// <summary>
        /// Method to prevalidate instance of DTOUMP ( user portion)
        /// </summary>
        /// <param name="objUSR01">instance of DTOUMP</param>
        /// <param name="role">role of user</param>
        /// <param name="operation">operation to perform</param>
        /// <returns></returns>
        public Response Prevalidate(DTOUSR01 objDTOUSR01)
        {
            Response response = new Response();
            if (Operation == EnmOperation.E)
            {
                int userCount;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    userCount = (int)db.Count<USR01>(usr01 => usr01.R01F01 == objDTOUSR01.R01F01);
                }

                if (userCount == 0)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = $"User not found with id: {objDTOUSR01.R01F01}";

                    return response;
                }
            }
            return response;
        }

        /// <summary>
        /// Method to convert DTOUSR01 instance to USR01 instance
        /// </summary>
        /// <param name="objDTOUSR01">Instance of DTOUSR01</param>
        public void Presave(DTOUSR01 objDTOUSR01)
        {
            ObjUSR01 = objDTOUSR01.ConvertModel<USR01>();

            // converting string password to hased password (bytes)
            string secretKey = (string)ConfigurationManager.AppSettings["PasswordHashSecretKey"];
            ObjUSR01.R01F03 = GetHMACBase64(objDTOUSR01.R01F03, secretKey);

            if (Operation == EnmOperation.A)
            {
                ObjUSR01.R01F01 = 0;
                ObjUSR01.R01F06 = DateTime.Now;
            }
            else
            {
                ObjUSR01.R01F07 = DateTime.Now;
            }
        }

        /// <summary>
        /// Method to validate the USR01 instance
        /// </summary>
        /// <returns>True if USR01 instance is valid else false</returns>
        public Response Validate()
        {
            Response response = new Response();

            int userCount;
            string username = ObjUSR01.R01F02;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                userCount = (int)db.Count<USR01>(user => user.R01F02 == username);
            }

            if (userCount > 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Conflict;
                response.Message = $"User already exists with username: {username}";

                return response;
            }

            return response;
        }

        /// <summary>
        /// Method to fetch user roles using userid
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>ResponseStatusInfo instance containing userId if successful or null if any exception</returns>
        public Response FetchUserRolesByUserId(int userId)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    // get user roles
                    var SqlExp = db.From<ULE02>()
                        .Where(ur => ur.E02F02 == userId)
                        .Join<RLE01>((ur, r) => (int)ur.E02F03 == r.E01F01)
                        .Select<RLE01>(r => r.E01F02);

                    string[] roles = db.Select<string>(SqlExp).ToArray<string>();
                    return new Response()
                    {
                        IsError = true,
                        Message = $"User roles with user id: {userId}",
                        Data = roles
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsError = false,
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
        public Response AddResource(JObject UserEmployeeJson)
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
                    string hashedPassword = GeneralUtility.GetHMACBase64((string)UserJson["R01F03"], secretKey);

                    // create user and get the user id
                    USR01 user = new USR01()
                    {
                        R01F01 = 0,
                        R01F02 = (string)UserJson["R01F02"],
                        R01F03 = hashedPassword,
                        R01F04 = (string)UserJson["R01F04"],
                        R01F05 = (string)UserJson["R01F05"],
                        R01F06 = DateTime.Now.Date
                    };
                    int userId = (int)db.Insert<USR01>(user, selectIdentity: true);

                    // populate the user-role table
                    List<int> roles = ((JArray)UserJson["roles"]).ToObject<List<int>>();

                    IEnumerable<ULE02> lstRoleUser = roles.Select<int, ULE02>(roleId => new ULE02()
                    {
                        E02F02 = userId,
                        E02F03 = (EnmRole)roleId
                    });

                    db.InsertAll(lstRoleUser);

                    int employeeId = 0;
                    // now check if the user is an employee ? (ie. role is 2)
                    if (roles.Contains<int>(2) && EmployeeJson != null)
                    {
                        // create employee object and get the employee id
                        EMP01 employee = new EMP01()
                        {
                            P01F01 = 0,
                            P01F02 = (string)EmployeeJson["P01F02"],
                            P01F03 = (string)EmployeeJson["P01F03"],
                            P01F04 = ((string)EmployeeJson["P01F04"])[0],
                            P01F05 = DateTime.Parse((string)EmployeeJson["P01F05"]).Date,
                            P01F06 = (int)EmployeeJson["P01F06"]
                        };
                        employeeId = (int)db.Insert<EMP01>(employee, selectIdentity: true);

                        // populate the user-employee table
                        UMP02 userEmployee = new UMP02()
                        {
                            P02F02 = userId,
                            P02F03 = employeeId
                        };

                        db.Insert(userEmployee);
                    }
                    return new Response()
                    {
                        IsError = true,
                        Message = $"User created with username: {(string)UserJson["R01F02"]}",
                        Data = null
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response()
                {
                    IsError = false,
                    Message = ex.Message,
                    Data = null
                };
            }
        }
    }
}