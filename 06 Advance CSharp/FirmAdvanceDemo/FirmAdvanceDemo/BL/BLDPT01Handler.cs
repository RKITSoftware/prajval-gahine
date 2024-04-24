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
    /// Handles business logic related to department operations.
    /// </summary>
    public class BLDPT01Handler
    {
        /// <summary>
        /// Instance of DPT01 POCO model.
        /// </summary>
        private DPT01 _objDPT01;

        /// <summary>
        /// Context for Department handler.
        /// </summary>
        private readonly DBDPT01Context _context;

        /// <summary>
        /// OrmLite Connection Factory representing a connection with a particular database.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Gets or sets the operation type for department handling.
        /// </summary>
        public EnmOperation Operation { get; set; }

        /// <summary>
        /// Initializes a new instance of the BLDPT01Handler class.
        /// </summary>
        public BLDPT01Handler()
        {
            _objDPT01 = new DPT01();
            _context = new DBDPT01Context();
            _dbFactory = OrmliteDbConnector.DbFactory;
        }

        /// <summary>
        /// Retrieves all departments.
        /// </summary>
        /// <returns>A response containing the department data.</returns>
        public Response RetrieveDepartment()
        {
            Response response = new Response();
            DataTable dtDPT01 = _context.FetchDepartment();

            if (dtDPT01.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No user exists";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtDPT01;
            return response;
        }

        /// <summary>
        /// Retrieves a specific department by its ID.
        /// </summary>
        /// <param name="departmentId">The ID of the department to retrieve.</param>
        /// <returns>A response containing the department data.</returns>
        public Response RetrieveDepartment(int departmentId)
        {
            Response response = new Response();
            DataTable dtDPT01 = _context.FetchDepartment(departmentId);

            if (dtDPT01.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Department not found with department id: {departmentId}";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtDPT01;
            return response;
        }

        /// <summary>
        /// Sets the data for the department before saving it.
        /// </summary>
        /// <param name="objDTPDPT01">The DTO containing department data.</param>
        public void Presave(DTODPT01 objDTPDPT01)
        {
            _objDPT01 = objDTPDPT01.ConvertModel<DPT01>();

            if (Operation == EnmOperation.A)
            {
                _objDPT01.T01F01 = 0;
                _objDPT01.T01F03 = DateTime.Now;
            }
            else
            {
                _objDPT01.T01F04 = DateTime.Now;
            }
        }

        /// <summary>
        /// Validates the department data before saving.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response Valdiate()
        {
            Response response = new Response();

            int count;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                count = (int)db.Count<DPT01>(dpt01 => dpt01.T01F02 == _objDPT01.T01F02);
            }

            if (count > 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.Conflict;
                response.Message = "Provided department name already exists";

                return response;
            }
            return response;
        }

        /// <summary>
        /// Saves the department data.
        /// </summary>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save()
        {
            Response response = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    int departmentID = (int)db.Insert<DPT01>(_objDPT01, selectIdentity: true);

                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"Department created with id: {departmentID}";
                    return response;
                }
                else
                {
                    db.Update<DPT01>(_objDPT01);

                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"Department updated with id: {_objDPT01.T01F01}";
                    return response;
                }
            }
        }

        /// <summary>
        /// Validates the deletion of a department.
        /// </summary>
        /// <param name="departmentId">The ID of the department to delete.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response ValidateDelete(int departmentId)
        {
            Response response = new Response();
            int count;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                count = (int)db.Count<DPT01>(dpt01 => dpt01.T01F01 == departmentId);
            }

            if (count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Department not found wiht id: {departmentId}";

                return response;
            }
            return response;
        }

        /// <summary>
        /// Deletes a department.
        /// </summary>
        /// <param name="departmentId">The ID of the department to delete.</param>
        /// <returns>A response indicating the outcome of the delete operation.</returns>
        public Response Delete(int departmentId)
        {
            Response response = new Response();
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<DPT01>(departmentId);
            }
            response.Message = $"Department deleted with id: {departmentId}";
            response.HttpStatusCode = HttpStatusCode.OK;

            return response;
        }
    }
}