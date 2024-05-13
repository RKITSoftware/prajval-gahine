using FirmAdvanceDemo.DL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Net;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Handles business logic related to administrative operations.
    /// </summary>
    public class BLADM00Handler
    {
        #region Private Fields
        /// <summary>
        /// The database context for administrative operations.
        /// </summary>
        private readonly DBADM00Context _objDBADM00Context;

        /// <summary>
        /// Instance of BLUSR01Handler
        /// </summary>
        private BLUSR01Handler _objBLUSR01Handler;

        /// <summary>
        /// OrmLite Connection Factory instance representing a connection with a particular database.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets or sets the operation type for user handling.
        /// </summary>
        public EnmOperation Operation { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the BLADM00Handler class.
        /// </summary>
        public BLADM00Handler()
        {
            _objDBADM00Context = new DBADM00Context();
            _dbFactory = OrmliteDbConnector.DbFactory;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Prevalidates an instance of DTOUSR01 (user portion).
        /// </summary>
        /// <param name="objDTOUSR01">Instance of DTOUSR01 containing user data.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response PrevalidateAdmin(DTOUSR01 objDTOUSR01)
        {
            _objBLUSR01Handler = new BLUSR01Handler();
            _objBLUSR01Handler.Operation = Operation;
            return _objBLUSR01Handler.PrevalidateUser(objDTOUSR01);
        }

        /// <summary>
        /// Converts DTOUSR01 instance to USR01 instance.
        /// </summary>
        /// <param name="objDTOUSR01">Instance of DTOUSR01 containing user data.</param>
        public void PresaveAdmin(DTOUSR01 objDTOUSR01)
        {
            _objBLUSR01Handler.PrevalidateUser(objDTOUSR01);
        }

        /// <summary>
        /// Saves the changes made to the administrative data.
        /// </summary>
        /// <returns>A response indicating the success of the operation.</returns>
        public Response Save()
        {
            Response response = new Response();

            if (Operation == EnmOperation.A)
            {
                int userID;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    using (IDbTransaction tnx = db.OpenTransaction())
                    {
                        try
                        {
                            userID = (int)db.Insert<USR01>(_objBLUSR01Handler.ObjUSR01, selectIdentity: true);

                            int roleID = db.Scalar<RLE01, int>(role => role.E01F01, role => role.E01F02 == EnmRole.A);

                            ULE02 objULE02 = new ULE02()
                            {
                                E02F02 = userID,
                                E02F03 = roleID,
                                E02F04 = DateTime.Now
                            };
                            db.Insert<ULE02>(objULE02);

                            tnx.Commit();
                        }
                        catch
                        {
                            tnx.Rollback();
                            throw;
                        }
                    }
                }

                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"Admin with userID {userID} created.";
            }
            else
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    db.Update<USR01>(_objBLUSR01Handler.ObjUSR01);
                }

                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"Admin with userID {_objBLUSR01Handler.ObjUSR01.R01F01} updated.";
            }
            return response;
        }

        /// <summary>
        /// Retrieves a admin with the specified user ID.
        /// </summary>
        /// <param name="userID">The ID of the user to retrieve.</param>
        /// <returns>A response containing the retrieved admin data.</returns>
        public Response RetrieveAdmin(int userID)
        {
            Response response = new Response();
            DataTable dtAdmin;
            dtAdmin = _objDBADM00Context.FetchAdmin(userID);
            if (dtAdmin.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"User {userID} not found.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtAdmin;
            return response;
        }

        /// <summary>
        /// Retrieves all admins.
        /// </summary>
        /// <returns>A response containing the retrieved admin data.</returns>
        public Response RetrieveAdmin()
        {
            Response response = new Response();
            DataTable dtAdmin;
            dtAdmin = _objDBADM00Context.FetchAdmin();
            if (dtAdmin.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No user found.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtAdmin;
            return response;
        }


        /// <summary>
        /// Validates the deletion of an admin.
        /// </summary>
        /// <param name="userID">The ID of the user to be deleted.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response ValidateDelete(int userID)
        {
            return _objBLUSR01Handler.ValidateDelete(userID);
        }

        /// <summary>
        /// Deletes an admin with the specified user ID.
        /// </summary>
        /// <param name="userID">The ID of the user to delete.</param>
        /// <returns>A response indicating the outcome of the deletion operation.</returns>
        public Response Delete(int userID)
        {
            return _objBLUSR01Handler.Delete(userID);
        }
        #endregion
    }
}