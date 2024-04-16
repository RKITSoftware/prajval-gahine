using DatabaseWithCrudWebApi.Contexts;
using DatabaseWithCrudWebApi.Models;
using Swashbuckle.Swagger;
using System;
using System.Data;
using System.Net;
using static DatabaseWithCrudWebApi.Utility;

namespace DatabaseWithCrudWebApi.BL
{
    /// <summary>
    /// Business logic for handling operations related to users.
    /// </summary>
    public class BLUSR01Handler
    {
        #region Private Members

        /// <summary>
        /// The user object used for operations within the class.
        /// </summary>
        private USR01 _objUSR01;

        /// <summary>
        /// Context object for USR01
        /// </summary>
        private readonly DBUSR01Context _context;

        #endregion

        #region Public Members

        /// <summary>
        /// Represents the type of operation
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion


        #region Constructor

        /// <summary>
        /// Initializes a new instance of the BLUSR01Handler class.
        /// </summary>
        public BLUSR01Handler()
        {
            _context = new DBUSR01Context();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prepares the user object for saving, setting creation or modification timestamps.
        /// </summary>
        /// <param name="objDTOUSR01">The data transfer object representing the user.</param>
        /// <param name="operation">The operation being performed (Create or Update).</param>
        public void Presave(DTOUSR01 objDTOUSR01)
        {
            _objUSR01 = Utility.ConvertModel<USR01>(objDTOUSR01);

            DateTime now = DateTime.Now;
            if (Operation == EnmOperation.Create)
            {
                _objUSR01.R01F01 = 0;
                _objUSR01.R01F04 = now;
            }
            else
            {
                _objUSR01.R01F05 = now;
            }
        }

        /// <summary>
        /// Validates the user object before saving.
        /// </summary>
        /// <param name="operation">The operation being performed (Create or Update).</param>
        /// <returns>True if the user object is valid, otherwise false.</returns>
        public Response Validate()
        {
            if (Operation == EnmOperation.Create)
            {
                // user creation
                if (string.IsNullOrEmpty(_objUSR01.R01F02))
                {
                    return ErrorResponse("username field cannot be empty", HttpStatusCode.PreconditionFailed);
                }

                if (string.IsNullOrEmpty(_objUSR01.R01F03))
                {
                    return ErrorResponse("password field cannot be empty", HttpStatusCode.PreconditionFailed);
                }

                bool? existsUser = _context.ExistsUsername(_objUSR01.R01F02);
                if (existsUser == null)
                {
                    return ErrorResponse("an intenal server occurred", HttpStatusCode.InternalServerError);
                }
                else if (existsUser == true)
                {
                    return ErrorResponse("user already exists", HttpStatusCode.Conflict);
                }
                return null;
            }
            else
            {
                // user updation
                bool? existsUser = _context.ExistsUserId(_objUSR01.R01F01);
                if (existsUser == null)
                {
                    return ErrorResponse("an intenal server occurred", HttpStatusCode.InternalServerError);
                }
                else if (existsUser == false)
                {
                    return ErrorResponse("username donot exists", HttpStatusCode.NotFound);
                }
                return null;
            }
        }

        /// <summary>
        /// Saves the user object to the database.
        /// </summary>
        /// <param name="operation">The operation being performed (Create or Update).</param>
        public Response Save()
        {
            if (Operation == EnmOperation.Create)
            {
                // user creation
                return _context.InsertUSR01(_objUSR01);
            }
            else
            {
                // user updation
                return _context.UpdateUSR01(_objUSR01);
            }
        }

        /// <summary>
        /// Retrieves a list of all users.
        /// </summary>
        /// <returns>A list of user objects.</returns>
        public Response GetAllUSR01()
        {
            Response response = new Response();
            DataTable dtUSR01 = _context.SelectUSR01();
            if (dtUSR01.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No user exists";
                return response;
            }

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtUSR01;
            return response;
        }

        /// <summary>
        /// Retrieves a user by their user ID.
        /// </summary>
        /// <param name="userId">The user ID to search for.</param>
        /// <returns>The user object if found, otherwise a new instance of USR01.</returns>
        public Response GetUSR01(int userId)
        {
            return _context.SelectUSR01(userId);
        }

        public Response ValidateDelete(int userId)
        {
            bool? isExists = _context.ExistsUserId(userId);
            if (isExists == null)
            {
                return ErrorResponse("an internal server occurred", HttpStatusCode.InternalServerError);
            }
            else if (isExists == false)
            {
                return ErrorResponse("user not found", HttpStatusCode.Conflict);
            }
            return null;
        }

        /// <summary>
        /// Deletes a user with the specified user ID from the database.
        /// </summary>
        /// <param name="userId">The ID of the user to delete.</param>
        public Response Delete(int userId)
        {
            return _context.DeleteUSR01(userId);
        }
        #endregion
    }
}