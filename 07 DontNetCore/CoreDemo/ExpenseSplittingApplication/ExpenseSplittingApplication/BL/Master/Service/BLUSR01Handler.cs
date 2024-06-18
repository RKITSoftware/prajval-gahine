using ExpenseSplittingApplication.BL.Common.Interface;
using ExpenseSplittingApplication.BL.Common.Service;
using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Common.Helper;
using ExpenseSplittingApplication.DL.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;
using ExpenseSplittingApplication.Models.POCO;
using Microsoft.AspNetCore.Http;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Linq;

namespace ExpenseSplittingApplication.BL.Master.Service
{
    /// <summary>
    /// Handles user-related operations including creation, update, and password management.
    /// </summary>
    public class BLUSR01Handler : IUSR01Service
    {
        /// <summary>
        /// The database connection factory.
        /// </summary>
        private readonly IDbConnectionFactory _dbFactory;

        /// <summary>
        /// The user entity for internal operations.
        /// </summary>
        private USR01 _user;

        /// <summary>
        /// The user context for database operations.
        /// </summary>
        private readonly IDBUserContext _dbUserContext;

        /// <summary>
        /// The logging service for logging operations.
        /// </summary>
        private readonly ILoggerService _loggingService;

        /// <summary>
        /// Gets or sets the current operation to be performed.
        /// </summary>
        public EnmOperation Operation { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BLUSR01Handler"/> class.
        /// </summary>
        /// <param name="dbFactory">The database connection factory.</param>
        /// <param name="context">The user context.</param>
        /// <param name="loggingService">The logging service.</param>
        public BLUSR01Handler(IDBUserContext context, UserLoggerService loggingService)
        {
            _dbFactory = EsaOrmliteConnectionFactory.ConnectionFactory;
            _dbUserContext = context;
            _loggingService = loggingService;
        }

        /// <summary>
        /// Prepares the user entity for saving based on the provided DTO.
        /// </summary>
        /// <param name="objDto">The DTO containing user data.</param>
        public void PreSave(DTOUSR01 objDto)
        {
            if (Operation == EnmOperation.A)
            {
                _user = new USR01()
                {
                    R01F01 = 0,
                    R01F02 = objDto.R01F02,
                    R01F03 = objDto.R01F03,
                    R01F98 = DateTime.Now,
                };
            }
            else
            {
                _user = new USR01()
                {
                    R01F01 = objDto.R01F01,
                    R01F02 = objDto.R01F02,
                    R01F99 = DateTime.Now,
                };
            }
        }

        /// <summary>
        /// Performs pre-validation checks before saving user data.
        /// </summary>
        /// <param name="objDto">The DTO containing user data.</param>
        /// <returns>A response indicating the result of the pre-validation.</returns>
        public Response PreValidation(DTOUSR01 objDto)
        {
            Response response = new Response();

            if (Operation == EnmOperation.E)
            {
                if (!Utility.UserIDExists(objDto.R01F01))
                {
                    response.IsError = true;
                    response.HttpStatusCode = StatusCodes.Status404NotFound;
                    response.Message = "User not found";
                }
            }
            return response;
        }

        /// <summary>
        /// Saves the user data to the database.
        /// </summary>
        /// <returns>A response indicating the result of the save operation.</returns>
        public Response Save()
        {
            Response response = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    db.Insert<USR01>(_user);
                }
                else
                {
                    _loggingService.Information("User update initiated...");

                    db.UpdateNonDefaults<USR01>(_user, user => user.R01F01 == _user.R01F01);

                    _loggingService.Information("User update completed...");
                }
            }

            if (Operation == EnmOperation.A)
            {
                response.Message = $"Welcome, {_user.R01F02}! Your account has been created.";
            }
            else
            {

                response.Message = $"User details have been successfully updated.";
            }

            response.HttpStatusCode = StatusCodes.Status200OK;

            return response;
        }

        /// <summary>
        /// Performs validation checks before saving user data.
        /// </summary>
        /// <returns>A response indicating the result of the validation.</returns>
        public Response Validation()
        {
            Response response = new Response();
            if (Operation == EnmOperation.A)
            {
                // add
                // check if username already taken
                if (Utility.UsernameExists(_user.R01F02))
                {
                    response.IsError = true;
                    response.Message = $"Username: {_user.R01F02} already taken.";
                    response.HttpStatusCode = StatusCodes.Status409Conflict;

                    return response;
                }
            }
            else
            {
                // edit
                // check if username already taken by another user
                bool usernameTakenByAnotherUser;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    usernameTakenByAnotherUser = db.Exists<USR01>(user => user.R01F02 == _user.R01F02 && user.R01F01 != _user.R01F01);
                }
                if (usernameTakenByAnotherUser)
                {
                    response.IsError = true;
                    response.Message = $"Username: {_user.R01F02} already in use.";
                    response.HttpStatusCode = StatusCodes.Status409Conflict;

                    return response;
                }
            }

            return response;
        }

        /// <summary>
        /// Validates the old password for the specified user before changing it.
        /// </summary>
        /// <param name="userID">The ID of the user whose password is being validated.</param>
        /// <param name="oldPassword">The old password to validate.</param>
        /// <returns>A response indicating the result of the password validation operation.</returns>
        public Response ValidatePassword(int userID, string oldPassword)
        {
            Response response = new Response();
            string currentPassword;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                currentPassword = db.Scalar<USR01, string>(user => user.R01F03, user => user.R01F01 == userID);
            }
            if (currentPassword != oldPassword)
            {
                response.IsError = true;
                response.HttpStatusCode = StatusCodes.Status409Conflict;
                response.Message = "Old password does not match.";
            }
            return response;
        }

        /// <summary>
        /// Changes the password for the specified user.
        /// </summary>
        /// <param name="userID">The ID of the user whose password is to be changed.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>A response indicating the result of the password change operation.</returns>
        public Response ChangePassword(int userID, string newPassword)
        {
            _loggingService.Information("User password change initiated...");

            Response response = new Response();
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.Update<USR01>(new { R01F03 = newPassword, R01F99 = DateTime.Now }, where: user => user.R01F01 == userID);
            }
            response.HttpStatusCode = StatusCodes.Status200OK;
            response.Message = "Password was changed successfully.";

            _loggingService.Information("User password change completed...");
            return response;
        }

        /// <summary>
        /// Retrieves the user based on the provided username and password.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <param name="password">The password of the user to retrieve.</param>
        /// <returns>The retrieved user entity (USR01).</returns>
        public int GetUserId(string username, string password)
        {
            int userId;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                //objUSR01 = db.Single<USR01>(new { R01F02 = username, R01F03 = password });
                userId = db.Scalar<USR01, int>(user => user.R01F01, user => user.R01F02 == username && user.R01F03 == password);
            }
            return userId;
        }
    }
}
