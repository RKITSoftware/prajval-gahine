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

    public class BLRLE01Handler
    {
        /// <summary>
        /// Ormlite Connection Factory instance from Connection class - that represent a connection with a particular database
        /// </summary>
        private readonly OrmLiteConnectionFactory _dbFactory;

        private readonly DBRLE01Context _objDBRLE01Context;

        public BLRLE01Handler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
            _objRLE01 = new RLE01();
            _objDBRLE01Context = new DBRLE01Context();
        }

        /// <summary>
        /// Instance of RLE01 model
        /// </summary>
        private RLE01 _objRLE01;

        public EnmOperation Operation { get; internal set; }


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
        /// Method to convert DTORLE01 instance to RLE01 instance
        /// </summary>
        /// <param name="objDTORLE01">Instance of DTORLE01</param>
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
                int roleId;
                using (IDbConnection db = _dbFactory.OpenDbConnection())
                {
                    roleId = (int)db.Insert<RLE01>(_objRLE01, selectIdentity: true);
                }
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = $"Role {roleId} created.";

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

        public Response ValidateDelete(int roleId)
        {
            Response response = new Response();
            int roleCount;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                roleCount = (int)db.Count<RLE01>(role => role.E01F01 == roleId);
            }

            if (roleCount == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Role {roleId} not found.";

                return response;
            }
            return response;
        }

        public Response Delete(int roleId)
        {
            Response response = new Response();
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                db.DeleteById<RLE01>(roleId);
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Message = $"Role {roleId} deleted.";

            return response;
        }

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

        public Response RetrieveRole(int roleId)
        {
            Response response = new Response();
            DataTable dtRole = _objDBRLE01Context.FetchRole(roleId);

            if (dtRole.Rows.Count == 0)
            {
                response.IsError = true;
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"Role {roleId} not found.";

                return response;
            }
            response.HttpStatusCode = HttpStatusCode.OK;
            response.Data = dtRole;
            return response;
        }
    }
}