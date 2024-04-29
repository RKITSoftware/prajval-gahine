using FirmAdvanceDemo.DL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Net;

namespace FirmAdvanceDemo.BL
{
    public class BLADM00Handler : BLUSR01Handler
    {
        private readonly DBADM00Context _objDBADM00Context;

        public BLADM00Handler()
        {
            _objDBADM00Context = new DBADM00Context();
        }

        public Response Save()
        {
            Response response = new Response();

            if(Operation == EnmOperation.A)
            {
                int userID;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    using(IDbTransaction tnx = db.OpenTransaction())
                    {
                        try
                        {
                            userID = (int)db.Insert<USR01>(_objUSR01, selectIdentity: true);

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
                    db.Update<USR01>(_objUSR01);
                }

                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"Admin with userID {_objUSR01.R01F01} updated.";
            }
            return response;
        }

        /// <summary>
        /// Retrieves a user with the specified user ID.
        /// </summary>
        /// <param name="userID">The ID of the user to retrieve.</param>
        /// <returns>A response containing the retrieved user data.</returns>
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
        /// Retrieves all users.
        /// </summary>
        /// <returns>A response containing the retrieved user data.</returns>
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
    }
}