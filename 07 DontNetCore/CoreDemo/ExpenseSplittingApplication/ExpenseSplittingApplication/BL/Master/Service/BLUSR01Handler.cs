using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.DL.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;
using ExpenseSplittingApplication.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace ExpenseSplittingApplication.BL.Master.Service
{
    public class BLUSR01Handler : IUSR01Service
    {
        private IDbConnectionFactory _dbFactory;

        private USR01 _user;
        private IDBUserContext _dbUserContext;

        public EnmOperation Operation { get; set; }

        public BLUSR01Handler(IDbConnectionFactory dbFactory, IDBUserContext context)
        {
            _dbFactory = dbFactory;
            _dbUserContext = context;
        }

        public Response Delete(int id)
        {
            Response response = new Response();
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<USR01>(id);
            }
            response.Message = $"User with id: {id} deleted successfully.";
            response.HttpStatusCode = StatusCodes.Status200OK;
            return response;
        }

        public Response DeleteValidation(int id)
        {
            Response response = new Response();
            if (!UserIDExists(id))
            {
                response.IsError = true;
                response.HttpStatusCode = StatusCodes.Status404NotFound;
                response.Message = $"User ID: {id} donot exists.";
            }
            return response;
        }

        public Response GetAll()
        {
            Response response = new Response();
            DataTable dtUser = _dbUserContext.GetAll();

            response.HttpStatusCode = StatusCodes.Status200OK;
            response.Data = dtUser;

            return response;
        }

        public void PreSave(DTOUSR01 objDto)
        {
            if(Operation == EnmOperation.A)
            {
                _user = new USR01()
                {
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

        public bool UsernameExists(string username)
        {
            bool usernameExists;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                usernameExists = db.Exists<USR01>(user => user.R01F02 == username);
            }
            return usernameExists;
        }

        public bool UserIDExists(int userID)
        {
            bool userIDExists;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                userIDExists = db.Exists<USR01>(user => user.R01F01 == userID);
            }
            return userIDExists;
        }

        public Response PreValidation(DTOUSR01 objDto)
        {
            Response response = new Response();

            if(Operation == EnmOperation.E)
            {
                if (!UserIDExists(objDto.R01F01))
                {
                    response.IsError = true;
                    response.HttpStatusCode = StatusCodes.Status404NotFound;
                    response.Message = "User not found";
                }
            }
            return response;
        }

        public Response Save()
        {
            Response response = new Response();
            
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    db.Insert<USR01>(_user);

                    response.Message = $"Welcome, {_user.R01F02}! Your account has been  created.";
                }
                else
                {
                    Action<IDbCommand> commandFilter = cmd =>
                    {
                        cmd.Parameters.RemoveAt("@R01F03");
                        cmd.CommandText = cmd.CommandText.Replace(", `R01F03`=@R01F03", "");
                    };

                    db.Update<USR01>(_user, commandFilter);
                    response.Message = $"User details has been successfully updated.";
                }
            }
            response.HttpStatusCode = StatusCodes.Status200OK;

            return response;
        }

        public Response Validation()
        {
            Response response = new Response();
            if (Operation == EnmOperation.A)
            {
                // add
                // check if username already taken
                if (UsernameExists(_user.R01F02))
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
        public Response ValidatePassword(int userID, string oldPassword)
        {
            Response response = new Response();
            string currentPassword;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                currentPassword = db.Scalar<USR01, string>(user => user.R01F03, user => user.R01F01 == userID);
            }
            if(currentPassword != oldPassword)
            {
                response.IsError = true;
                response.HttpStatusCode = StatusCodes.Status409Conflict;
                response.Message = "Old password does not matched.";
            }
            return response;
        }

        public Response ChangePassword(int userID, string newPassword)
        {
            Response response = new Response();
            using(IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.Update<USR01>(new {R01F03 = newPassword}, where: user => user.R01F01 == userID);
            }
            response.HttpStatusCode = StatusCodes.Status200OK;
            response.Message = "Password was changed successfully.";
            return response;
        }
    }
}
