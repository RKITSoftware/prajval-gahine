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
    /// Handles business logic related to position operations.
    /// </summary>
    public class BLPSN01Handler
    {
        /// <summary>
        /// OrmLite Connection Factory instance representing a connection with a particular database.
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        /// <summary>
        /// Context for Position handler.
        /// </summary>
        private readonly DBPSN01Context _objDBPSN01Context;

        /// <summary>
        /// Default constructor for BLPSN01Handler.
        /// </summary>
        public BLPSN01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _objPSN01 = new PSN01();
            _objDBPSN01Context = new DBPSN01Context();
        }

        /// <summary>
        /// Instance of PSN01 model.
        /// </summary>
        private PSN01 _objPSN01;

        /// <summary>
        /// Gets or sets the operation type for position handling.
        /// </summary>  
        public EnmOperation Operation { get; internal set; }

        /// <summary>
        /// Validates the provided DTOPSN01 instance before processing.
        /// </summary>
        /// <param name="objDTOPSN01">DTOPSN01 instance containing position data.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response Prevalidate(DTOPSN01 objDTOPSN01)
        {
            Response response = new Response();

            // validate department id N01F06
            int departmentCount;

            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                departmentCount = (int)db.
                    Count<DPT01>(department => department.T01F01 == objDTOPSN01.N01F06);
            }
            if (departmentCount == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Department {objDTOPSN01.N01F06} not found.";

                return response;
            }

            // in case of E check primarykey N01F01
            if (Operation == EnmOperation.E)
            {
                int positionCount;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    positionCount = (int)db.Count<PSN01>(position => position.N01F01 == objDTOPSN01.N01F01);
                }
                if (positionCount == 0)
                {
                    response.IsError = true;
                    response.HttpStatusCode = HttpStatusCode.NotFound;
                    response.Message = $"Position {objDTOPSN01.N01F01} not found.";

                    return response;
                }
            }
            return response;
        }

        /// <summary>
        /// Sets the data for the position before saving it.
        /// </summary>
        /// <param name="objDTOPSN01">DTOPSN01 instance containing position data.</param>
        public void Presave(DTOPSN01 objDTOPSN01)
        {
            _objPSN01 = objDTOPSN01.ConvertModel<PSN01>();

            if (Operation == EnmOperation.A)
            {
                _objPSN01.N01F01 = 0;
                _objPSN01.N01F07 = DateTime.Now;
            }
            else
            {
                _objPSN01.N01F08 = DateTime.Now;
            }
        }

        /// <summary>
        /// Validates the position data before saving.
        /// </summary>
        /// <returns>A response indicating the validation result.</returns>
        public Response Validate()
        {
            Response response = new Response();
            // nothing to validate as of now
            return response;
        }

        /// <summary>
        /// Saves the position data.
        /// </summary>
        /// <returns>A response indicating the outcome of the save operation.</returns>
        public Response Save()
        {
            Response response = new Response();

            if (Operation == EnmOperation.A)
            {
                int positionID;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    positionID = (int)db.Insert<PSN01>(_objPSN01, selectIdentity: true);
                }
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"Position {positionID} created.";

                return response;
            }
            else
            {
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    db.Update<PSN01>(_objPSN01);
                }
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"Position {_objPSN01.N01F01} updated.";

                return response;
            }
        }

        /// <summary>
        /// Validates the deletion of a position by ID.
        /// </summary>
        /// <param name="positionID">The ID of the position to be deleted.</param>
        /// <returns>A response indicating the validation result.</returns>
        public Response ValidateDelete(int positionID)
        {
            Response response = new Response();
            int positionCount;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                positionCount = (int)db.Count<PSN01>(position => position.N01F01 == positionID);
            }

            if (positionCount == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Position {positionID} not found.";

                return response;
            }
            return response;
        }

        /// <summary>
        /// Deletes a position by ID.
        /// </summary>
        /// <param name="positionID">The ID of the position to be deleted.</param>
        /// <returns>A response indicating the outcome of the delete operation.</returns>
        public Response Delete(int positionID)
        {
            Response response = new Response();
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<PSN01>(positionID);
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"Position {positionID} deleted.";

            return response;
        }

        /// <summary>
        /// Retrieves all positions.
        /// </summary>
        /// <returns>A response containing the retrieved position data.</returns>
        public Response RetrievePosition()
        {
            Response response = new Response();
            DataTable dtPosition = _objDBPSN01Context.FetchPosition();

            if (dtPosition.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No position exists.";
                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtPosition;
            return response;
        }

        public Response RetrievePosition(int positionID)
        {
            Response response = new Response();
            DataTable dtPosition = _objDBPSN01Context.FetchPosition(positionID);

            if (dtPosition.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Position {positionID} not found.";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtPosition;
            return response;
        }
    }
}