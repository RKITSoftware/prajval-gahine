using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using ServiceStack;
using ServiceStack.OrmLite;
using System;
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
        #region Private Fields
        /// <summary>
        /// OrmLite Connection Factory instance representing a connection with a particular database.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;
        #endregion

        #region Public Properties
        /// <summary>
        /// Instance of USR01 model representing user data.
        /// </summary>
        public USR01 ObjUSR01 { get; set; }

        /// <summary>
        /// Gets or sets the operation type for user handling.
        /// </summary>
        public EnmOperation Operation { get; set; }
        #endregion

        #region Public Properties
        /// <summary>
        /// Default constructor for BLUSR01Handler.
        /// </summary>
        public BLUSR01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Prevalidates an instance of DTOUSR01 (user portion).
        /// </summary>
        /// <param name="objDTOUSR01">Instance of DTOUSR01 containing user data.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response PrevalidateUser(DTOUSR01 objDTOUSR01)
        {
            Response response = new Response();

            bool isUsernameExists;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                isUsernameExists = db.Exists<USR01>(new { r01f02 = objDTOUSR01.R01F02 });
            }

            // in case of ADD username must NOT exists in DB
            if (Operation == EnmOperation.A)
            {
                if (isUsernameExists)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.Conflict;
                    response.Message = $"Username {objDTOUSR01.R01F02} already exists.";

                    return response;
                }
            }

            if (Operation == EnmOperation.E)
            {
                // check if supplied userID already exists ?
                bool isUserExists;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    isUserExists = db.Exists<USR01>(new { r01f01 = objDTOUSR01.R01F01 });
                }

                if (!isUserExists)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = $"User {objDTOUSR01.R01F01} not found.";

                    return response;
                }

                // check if usernmae exists then , the username belongs to this user only ?
                if (isUsernameExists)
                {
                    int userID;
                    using (IDbConnection db = _dbFactory.OpenDbConnection())
                    {
                        userID = db.Scalar<USR01, int>(user => user.R01F01, user => user.R01F02 == objDTOUSR01.R01F02);
                    }

                    if (userID != objDTOUSR01.R01F01)
                    {
                        response.IsError = true;
                        response.HttpStatusCode = HttpStatusCode.Conflict;
                        response.Message = $"Username {objDTOUSR01.R01F02} already exists.";

                        return response;
                    }
                }
            }
            return response;
        }

        /// <summary>
        /// Converts DTOUSR01 instance to USR01 instance.
        /// </summary>
        /// <param name="objDTOUSR01">Instance of DTOUSR01 containing user data.</param>
        public void PresaveUser(DTOUSR01 objDTOUSR01)
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
        /// Validates the USR01 instance.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response ValidateUser()
        {
            Response response = new Response();
            // nothing to validate as of now
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
        #endregion
    }
}