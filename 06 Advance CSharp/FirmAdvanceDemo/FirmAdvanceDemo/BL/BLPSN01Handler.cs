using FirmAdvanceDemo.DB;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Models.POCO;
using FirmAdvanceDemo.Utitlity;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Net;

namespace FirmAdvanceDemo.BL
{
    public class BLPSN01Handler
    {
        /// <summary>
        /// Ormlite Connection Factory instance from Connection class - that represent a connection with a particular database
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        private readonly DBPSN01Context _objDBPSN01Context;

        public BLPSN01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _objPSN01 = new PSN01();
            _objDBPSN01Context = new DBPSN01Context();
        }

        /// <summary>
        /// Instance of PSN01 model
        /// </summary>
        private PSN01 _objPSN01;

        public EnmOperation Operation { get; internal set; }


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
        /// Method to convert DTOPSN01 instance to PSN01 instance
        /// </summary>
        /// <param name="objDTOPSN01">Instance of DTOPSN01</param>
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

        public Response Validate()
        {
            Response response = new Response();
            // nothing to validate as of now
            return response;
        }

        public Response Save()
        {
            Response response = new Response();

            if (Operation == EnmOperation.A)
            {
                int positionId;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    positionId = (int)db.Insert<PSN01>(_objPSN01, selectIdentity: true);
                }
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"Position {positionId} created.";

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

        public Response ValidateDelete(int positionId)
        {
            Response response = new Response();
            int positionCount;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                positionCount = (int)db.Count<PSN01>(position => position.N01F01 == positionId);
            }

            if (positionCount == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Position {positionId} not found.";

                return response;
            }
            return response;
        }

        public Response Delete(int positionId)
        {
            Response response = new Response();
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<PSN01>(positionId);
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"Position {positionId} deleted.";

            return response;
        }

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

        public Response RetrievePosition(int positionId)
        {
            Response response = new Response();
            DataTable dtPosition = _objDBPSN01Context.FetchPosition(positionId);

            if (dtPosition.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Position {positionId} not found.";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtPosition;
            return response;
        }
    }
}