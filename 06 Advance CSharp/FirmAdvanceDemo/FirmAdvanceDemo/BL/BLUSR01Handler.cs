using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using static FirmAdvanceDemo.Utility.GeneralUtility;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Handles business logic related to user operations.
    /// </summary>
    public class BLUSR01Handler
    {
        /// <summary>
        /// Instance of USR01 model representing user data.
        /// </summary>
        public USR01 ObjUSR01 { get; set; }

        /// <summary>
        /// List of user roles.
        /// </summary>
        public List<EnmRole> LstRole { get; set; }

        /// <summary>
        /// Gets or sets the operation type for user handling.
        /// </summary>
        public EnmOperation Operation { get; set; }

        /// <summary>
        /// OrmLite Connection Factory instance representing a connection with a particular database.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Context for User handler.
        /// </summary>
        private readonly DBUSR01Context _dbUSR01Context;

        /// <summary>
        /// Default constructor for BLUSR01Handler.
        /// </summary>
        public BLUSR01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _dbUSR01Context = new DBUSR01Context();
        }

        /// <summary>
        /// Prevalidates an instance of DTOUSR01 (user portion).
        /// </summary>
        /// <param name="objDTOUSR01">Instance of DTOUSR01 containing user data.</param>
        /// <returns>A response indicating the validation result.</returns>
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
                    response.Message = $"User {objDTOUSR01.R01F01} not found.";

                    return response;
                }
            }
            return response;
        }

        /// <summary>
        /// Converts DTOUSR01 instance to USR01 instance.
        /// </summary>
        /// <param name="objDTOUSR01">Instance of DTOUSR01 containing user data.</param>
        public void Presave(DTOUSR01 objDTOUSR01)
        {
            LstRole = objDTOUSR01.R01X06;
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
        /// Validates the USR01 instance.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response Validate()
        {
            Response response = new Response();

            int userID;
            string username = ObjUSR01.R01F02;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                userID = (int)db.Scalar<USR01, int>(user => user.R01F01, user => user.R01F02 == username);
            }

            if (userID != 0 && userID != ObjUSR01.R01F01)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Conflict;
                response.Message = $"User already exists with username: {username}";

                return response;
            }

            return response;
        }

        /// <summary>
        /// Fetches user roles using userID.
        /// </summary>
        /// <param name="userID">The ID of the user.</param>
        /// <returns>A response containing the user's roles if successful.</returns>
        public Response FetchUserRolesByuserID(int userID)
        {
            try
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    // get user roles
                    var SqlExp = db.From<ULE02>()
                        .Where(ur => ur.E02F02 == userID)
                        .Join<RLE01>((ur, r) => (int)ur.E02F03 == r.E01F01)
                        .Select<RLE01>(r => r.E01F02);

                    string[] roles = db.Select<string>(SqlExp).ToArray<string>();
                    return new Response()
                    {
                        IsError = true,
                        Message = $"User roles with user id: {userID}",
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
        /// Retrieves a user with the specified user ID.
        /// </summary>
        /// <param name="userID">The ID of the user to retrieve.</param>
        /// <returns>A response containing the retrieved user data.</returns>
        public Response RetrieveUser(int userID)
        {
            Response response = new Response();
            DataTable dtUser;
            dtUser = _dbUSR01Context.FetchUser(userID);
            if (dtUser.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"User {userID} not found.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtUser;
            return response;
        }

        /// <summary>
        /// Retrieves all users.
        /// </summary>
        /// <returns>A response containing the retrieved user data.</returns>
        public Response RetrieveUser()
        {
            Response response = new Response();
            DataTable dtUser;
            dtUser = _dbUSR01Context.FetchUser();
            if (dtUser.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No user found.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtUser;
            return response;
        }

        /// <summary>
        /// Saves the user data.
        /// </summary>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save()
        {
            Response response = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    int userID;
                    using (IDbTransaction txn = db.OpenTransaction())
                    {
                        try
                        {
                            userID = (int)db.Insert<USR01>(ObjUSR01, selectIdentity: true);
                            // get role id
                            int roleID = db.Scalar<RLE01, int>(role => role.E01F01, role => role.E01F02 == EnmRole.A);

                            // save user role
                            ULE02 objULE02 = new ULE02()
                            {
                                E02F02 = userID,
                                E02F03 = roleID,
                                E02F04 = DateTime.Now
                            };
                            db.Insert<ULE02>(objULE02);

                            txn.Commit();
                        }
                        catch
                        {
                            txn.Rollback();
                            throw;
                        }
                    }

                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"User {userID} created.";
                }
                else
                {
                    db.Update<USR01>(ObjUSR01);

                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"User {ObjUSR01.R01F01} updated.";
                }
            }
            return response;
        }

        /// <summary>
        /// Validates the deletion of a user.
        /// </summary>
        /// <param name="userID">The ID of the user to be deleted.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response ValidateDelete(int userID)
        {
            Response response = new Response();
            int userCount;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                userCount = (int)db.Count<USR01>(user => user.R01F01 == userID);
            }

            if (userCount == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"User {userID} not found.";

                return response;
            }
            return response;
        }

        /// <summary>
        /// Deletes a user with the specified user ID.
        /// </summary>
        /// <param name="userID">The ID of the user to delete.</param>
        /// <returns>A response indicating the outcome of the deletion operation.</returns>
        public Response Delete(int userID)
        {
            Response response = new Response();
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<USR01>(userID);
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"User {userID} deleted.";

            return response;
        }
    }
}