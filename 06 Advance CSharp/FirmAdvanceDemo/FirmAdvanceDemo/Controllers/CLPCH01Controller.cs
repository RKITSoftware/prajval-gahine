using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utitlity;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/punch")]
    [AccessTokenAuthentication]
    [BasicAuthorization(Roles = "E")]
    public class CLPCH01Controller : ApiController
    {

        /// <summary>
        /// Instance of BLPunch
        /// </summary>
        private readonly BLPCH01Handler _objBLPCH01Handler;

        /// <summary>
        /// Default constructor for CLPunchController
        /// </summary>
        public CLPCH01Controller()
        {
            _objBLPCH01Handler = new BLPCH01Handler();
        }

        [HttpPost]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
        public IHttpActionResult PostPunch(DTOPCH01 objDTOPCH01)
        {
            Response response = GeneralUtility.ValidateAccess(objDTOPCH01.H01F02);

            if (!response.IsError)
            {
                _objBLPCH01Handler.Operation = EnmOperation.A;

                response = _objBLPCH01Handler.Prevalidate(objDTOPCH01);

                if (!response.IsError)
                {
                    _objBLPCH01Handler.Presave(objDTOPCH01);
                    response = _objBLPCH01Handler.Validate();
                    if (!response.IsError)
                    {
                        response = _objBLPCH01Handler.Save();
                    }
                }
            }
            return Ok(response);
        }
    }
}
