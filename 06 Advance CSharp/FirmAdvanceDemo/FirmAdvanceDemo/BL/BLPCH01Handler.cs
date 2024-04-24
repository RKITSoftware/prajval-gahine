using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utility;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;

namespace FirmAdvanceDemo.BL
{
    /// <summary>
    /// Handles business logic related to punch operations.
    /// </summary>
    public class BLPCH01Handler
    {
        /// <summary>
        /// Instance of PCH01 model.
        /// </summary>
        private PCH01 _objPCH01;

        /// <summary>
        /// Context for Punch handler.
        /// </summary>
        private readonly DBPCH01Context _objDBPCH01Context;

        /// <summary>
        /// Factory for creating OrmLite database connections.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Gets or sets the operation type for punch handling.
        /// </summary>
        public EnmOperation Operation { get; set; }

        /// <summary>
        /// Default constructor for BLPCH01Handler.
        /// </summary>
        public BLPCH01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _objDBPCH01Context = new DBPCH01Context();
        }

        /// <summary>
        /// Validates the provided DTOPCH01 instance before processing.
        /// </summary>
        /// <param name="objDTOPCH01">DTOPCH01 instance containing punch data.</param>
        /// <returns>A response indicating the validation result.</returns> 
        public Response Prevalidate(DTOPCH01 objDTOPCH01)
        {
            Response response = new Response();

            // validate employee id
            int employeeId = objDTOPCH01.H01F02;
            int employeeCount = objDTOPCH01.H01F02;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                employeeCount = (int)db.Count<EMP01>(employee => employee.P01F01 == employeeId);
            }

            if (employeeCount == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Employee not found with id: {employeeId}";

                return response;
            }

            if (Operation == EnmOperation.E)
            {
                int punchId = objDTOPCH01.H01F01;
                int punchCount;

                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    punchCount = (int)db.Count<PCH01>(punch => punch.H01F01 == punchId);
                }

                if (punchCount == 0)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Punch not found with id: {punchId}";

                    return response;
                }
            }
            return response;
        }

        /// <summary>
        /// Sets the data for the punch before saving it.
        /// </summary>
        /// <param name="objDTOPCH01">DTOPCH01 instance containing punch data.</param>
        public void Presave(DTOPCH01 objDTOPCH01)
        {
            _objPCH01 = objDTOPCH01.ConvertModel<PCH01>();
            if (Operation == EnmOperation.A)
            {
                _objPCH01.H01F01 = 0;
                _objPCH01.H01F02 = (int)HttpContext.Current.Items["employeeID"];
                _objPCH01.H01F03 = EnmPunchType.U;
                _objPCH01.H01F04 = DateTime.Now;
            }
            else
            {
                _objPCH01.H01F05 = DateTime.Now;
            }
        }

        /// <summary>
        /// Validates the punch data before saving.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response Validate()
        {
            Response response = new Response();
            // nothing to validate for PCH01 as of now
            return response;
        }

        /// <summary>
        /// Saves the punch data.
        /// </summary>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save()
        {
            Response response = new Response();

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                if (Operation == EnmOperation.A)
                {
                    int punchID = (int)db.Insert<PCH01>(_objPCH01, selectIdentity: true);

                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"Punch {punchID} created for employee {_objPCH01.H01F02}";

                    return response;
                }
                else
                {
                    db.Update<PCH01>(_objPCH01);

                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.Message = $"Punch {_objPCH01.H01F01} updated. ";

                    return response;
                }
            }
        }

        /// <summary>
        /// Retrieves punch records.
        /// </summary>
        /// <returns>A response containing the retrieved punch data.</returns>
        public Response RetrievePunch()
        {
            Response response = new Response();
            DataTable dtPunch = _objDBPCH01Context.FetchPunch();

            if (dtPunch.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No user found.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtPunch;
            return response;
        }

        /// <summary>
        /// Retrieves a specific punch record by punch ID.
        /// </summary>
        /// <param name="punchId">The ID of the punch record to retrieve.</param>
        /// <returns>A response containing the retrieved punch data.</returns>
        public Response RetrievePunch(int punchId)
        {
            Response response = new Response();
            DataTable dtPunch = _objDBPCH01Context.FetchPunch(punchId);

            if (dtPunch.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Punch {punchId} not found.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtPunch;
            return response;
        }
    }
}