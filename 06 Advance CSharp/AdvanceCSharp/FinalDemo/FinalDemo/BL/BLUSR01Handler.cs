using FinalDemo.Connections;
using FinalDemo.DL;
using FinalDemo.Models.DTO;
using FinalDemo.Models.POCO;
using FinalDemo.Utilities;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Linq;
using System.Net;

namespace FinalDemo.BL
{
    /// <summary>
    /// Handles business logic operations related to USR01 entity.
    /// </summary>
    public class BLUSR01Handler
    {
        #region Public Properties

        /// <summary>
        /// Specifies the operation to be performed.
        /// </summary>
        public EnmOperation Operation { get; set; }

        #endregion

        #region Private Fields

        /// <summary>
        /// The USR01 POCO object associated with the current operation.
        /// </summary>
        private USR01 _objUSR01;

        /// <summary>
        /// The data access context for the USR01 entity.
        /// </summary>
        private readonly DBUSR01Context _context;

        /// <summary>
        /// The ormlite factory for creating database connections.
        /// </summary>
        private IDbConnectionFactory _connectionFactory;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BLUSR01Handler class,
        /// setting up the data access context (_context)
        /// and database connection factory (_connectionFactory).
        /// </summary>
        public BLUSR01Handler()
        {
            _context = new DBUSR01Context();
            _connectionFactory = OrmliteDBConnector.DBConnectionFactory;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Prepare the USR01 object before saving.
        /// </summary>
        public void Presave(DTOUSR01 objDTOUSR01)
        {
            _objUSR01 = objDTOUSR01.CreatePOCO<USR01>();
            if (Operation == EnmOperation.A)
            {
                _objUSR01.R01F01 = 0;
                _objUSR01.R01F04 = DateTime.Now;
            }
            else
            {
                _objUSR01.R01F05 = DateTime.Now;
            }
        }

        /// <summary>
        /// Validate the USR01 object.
        /// </summary>
        public Response Validate()
        {
            Response response = new Response();
            long count;
            if (Operation == EnmOperation.A)
            {
                try
                {
                    using (IDbConnection db = _connectionFactory.OpenDbConnection())
                    {
                        count = db.Count<USR01>(user => user.R01F02 == _objUSR01.R01F02);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (count > 0)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.Conflict;
                    response.Message = "Username already exists";

                    return response;
                }
            }
            else
            {
                try
                {
                    using (IDbConnection db = _connectionFactory.OpenDbConnection())
                    {
                        count = db.Count<USR01>(usr01 => usr01.R01F01 == _objUSR01.R01F01);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                if (count == 0)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = "User not found";

                    return response;
                }
            }
            return response;
        }

        /// <summary>
        /// Save the USR01 object to the database.
        /// </summary>
        public Response Save()
        {
            Response response = new Response();

            if (Operation == EnmOperation.A)
            {
                long id;
                try
                {
                    using (IDbConnection db = _connectionFactory.OpenDbConnection())
                    {
                        id = db.Insert<USR01>(_objUSR01, selectIdentity: true);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"User created with user id: {id}";

                return response;
            }
            else
            {
                try
                {
                    using (IDbConnection db = _connectionFactory.OpenDbConnection())
                    {
                        db.Update<USR01>(_objUSR01);
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"User updated with user id: {_objUSR01.R01F01}";
            }
            return response;
        }

        /// <summary>
        /// Validate the deletion of a user.
        /// </summary>
        public Response ValidateDelete(int id)
        {
            Response response = new Response();
            long count;
            try
            {
                using (IDbConnection db = _connectionFactory.OpenDbConnection())
                {
                    count = db.Count<USR01>(usr01 => usr01.R01F01 == id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            if (count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"User donot exists with user id: {id}";
            }
            return response;
        }

        /// <summary>
        /// Delete a user from the database.
        /// </summary>
        public Response DeleteUSR01(int id)
        {
            Response response = new Response();

            try
            {
                using (IDbConnection db = _connectionFactory.OpenDbConnection())
                {
                    db.DeleteById<USR01>(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"user deleted with user id: {id}";
            return response;
        }

        /// <summary>
        /// Retrieve all users from the database.
        /// </summary>
        public Response GetUSR01()
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
        /// Retrieve a user from the database based on the provided id.
        /// </summary>
        public Response GetUSR01(int id)
        {
            DataTable dtUSR01;
            Response response = new Response();
            dtUSR01 = _context.SelectUSR01(id);
            if (dtUSR01.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"User not found with user id: {id}";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtUSR01;
            return response;
        }
        #endregion
    }
}