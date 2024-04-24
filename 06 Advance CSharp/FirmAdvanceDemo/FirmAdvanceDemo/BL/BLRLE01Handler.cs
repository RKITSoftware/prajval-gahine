using FirmAdvanceDemo.DB;
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
    /// Handles business logic related to role operations.
    /// </summary>
    public class BLRLE01Handler
    {
        /// <summary>
        /// OrmLite Connection Factory instance representing a connection with a particular database.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Context for Role handler.
        /// </summary>
        private readonly DBRLE01Context _objDBRLE01Context;

        /// <summary>
        /// Instance of RLE01 model.
        /// </summary>
        private RLE01 _objRLE01;

        /// <summary>
        /// Gets or sets the operation type for role handling.
        /// </summary>
        public EnmOperation Operation { get; internal set; }

        /// <summary>
        /// Default constructor for BLRLE01Handler.
        /// </summary>  
        public BLRLE01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _objRLE01 = new RLE01();
            _objDBRLE01Context = new DBRLE01Context();
        }

        /// <summary>
        /// Validates the provided DTORLE01 instance before processing.
        /// </summary>
        /// <param name="objDTORLE01">DTORLE01 instance containing role data.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response Prevalidate(DTORLE01 objDTORLE01)
        {
            Response response = new Response();

            // in case of E check primarykey E01F01
            if (Operation == EnmOperation.E)
            {
                int roleCount;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    roleCount = (int)db.Count<RLE01>(role => role.E01F01 == objDTORLE01.E01F01);
                }
                if (roleCount == 0)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Role {objDTORLE01.E01F01} not found.";

                    return response;
                }
            }
            return response;
        }

        /// <summary>
        /// Sets the data for the role before saving it.
        /// </summary>
        /// <param name="objDTORLE01">DTORLE01 instance containing role data.</param>
        public void Presave(DTORLE01 objDTORLE01)
        {
            _objRLE01 = objDTORLE01.ConvertModel<RLE01>();

            if (Operation == EnmOperation.A)
            {
                _objRLE01.E01F01 = 0;
                _objRLE01.E01F03 = DateTime.Now;
            }
            else
            {
                _objRLE01.E01F04 = DateTime.Now;
            }
        }

        /// <summary>
        /// Validates the role data before saving.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response Validate()
        {
            Response response = new Response();
            // nothing to validate as of now
            return response;
        }

        /// <summary>
        /// Saves the role data.
        /// </summary>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save()
        {
            Response response = new Response();

            if (Operation == EnmOperation.A)
            {
                int roleID;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    roleID = (int)db.Insert<RLE01>(_objRLE01, selectIdentity: true);
                }
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"Role {roleID} created.";

                return response;
            }
            else
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    db.Update<RLE01>(_objRLE01);
                }
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"Role {_objRLE01.E01F01} updated.";

                return response;
            }
        }

        /// <summary>
        /// Validates the deletion of a role by ID.
        /// </summary>
        /// <param name="roleID">The ID of the role to be deleted.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response ValidateDelete(int roleID)
        {
            Response response = new Response();
            int roleCount;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                roleCount = (int)db.Count<RLE01>(role => role.E01F01 == roleID);
            }

            if (roleCount == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Role {roleID} not found.";

                return response;
            }
            return response;
        }

        /// <summary>
        /// Deletes a role by ID.
        /// </summary>
        /// <param name="roleID">The ID of the role to be deleted.</param>
        /// <returns>A response indicating the outcome of the delete operation.</returns>
        public Response Delete(int roleID)
        {
            Response response = new Response();
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<RLE01>(roleID);
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"Role {roleID} deleted.";

            return response;
        }

        /// <summary>
        /// Retrieves all roles.
        /// </summary>
        /// <returns>A response containing the retrieved role data.</returns>
        public Response RetrieveRole()
        {
            Response response = new Response();
            DataTable dtRole = _objDBRLE01Context.FetchRole();

            if (dtRole.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No role exists.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtRole;
            return response;
        }

        /// <summary>
        /// Retrieves a specific role by ID.
        /// </summary>
        /// <param name="roleID">The ID of the role to retrieve.</param>
        /// <returns>A response containing the retrieved role data.</returns>
        public Response RetrieveRole(int roleID)
        {
            Response response = new Response();
            DataTable dtRole = _objDBRLE01Context.FetchRole(roleID);

            if (dtRole.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Role {roleID} not found.";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtRole;
            return response;
        }
    }
}