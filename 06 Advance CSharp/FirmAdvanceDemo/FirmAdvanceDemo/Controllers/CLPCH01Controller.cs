using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utility;
using System;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing punch operations, such as submitting punch details.
    /// </summary>
    [RoutePrefix("api/punch")]
    public class CLPCH01Controller : ApiController
    {
        /// <summary>
        /// Instance of the punch handler for managing punch operations.
        /// </summary>
        private readonly BLPCH01Handler _objBLPCH01Handler;

        /// <summary>
        /// Initializes a new instance of the CLPCH01Controller class.
        /// </summary>
        public CLPCH01Controller()
        {
            _objBLPCH01Handler = new BLPCH01Handler();
        }

        /// <summary>
        /// Action method to submit punch details.
        /// </summary>
        /// <param name="objDTOPCH01">DTO containing punch details.</param>
        /// <returns>HTTP response indicating the success or failure of the punch submission.</returns>
        [HttpPost]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
        public IHttpActionResult PostPunch(DTOPCH01 objDTOPCH01)
        {
            // Validate if the user has access to submit punch for the given employee
            Response response = GeneralUtility.ValidateAccess(objDTOPCH01.H01F02);

            if (!response.IsError)
            {
                // Set operation type as 'Add' for punch submission
                _objBLPCH01Handler.Operation = EnmOperation.A;

                // Perform pre-validation of punch data
                response = _objBLPCH01Handler.Prevalidate(objDTOPCH01);

                if (!response.IsError)
                {
                    // Save punch data
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

        [HttpPost]
        [Route("virtual")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult PostVirtualPunch(DTOPCH01 objDTOPCH01)
        {
            Response response;

            response = _objBLPCH01Handler.PrevalidateVirtualPunch(objDTOPCH01);

            if (!response.IsError)
            {
                _objBLPCH01Handler.PresaveVirtualPunch(objDTOPCH01);
                response = _objBLPCH01Handler.Validate();
                if (!response.IsError)
                {
                    response = _objBLPCH01Handler.SaveVirtualPunch();
                }
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("ambiguous")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetAmbiguousPunchForDate(DateTime date)
        {
            Response response = _objBLPCH01Handler.RetrieveAmbiguousPunch(date);
            return Ok(response);
        }

        [HttpGet]
        [Route("employee/{employeeID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E, A")]
        public IHttpActionResult GetPunchForEmployeeByMonth(int employeeID, int year, int month)
        {
            Response response;
            response = GeneralUtility.ValidateAccess(employeeID);
            if (!response.IsError)
            {
                response = _objBLPCH01Handler.RetrievePunchForEmployeeByMonth(employeeID, year, month);
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("process-unprocessed-punches")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult ProcessUprocessedPunchsForDate(DateTime date)
        {
            Response response;
            _objBLPCH01Handler.PresaveProcessUnprocessPunchesForDate(date);
            response = _objBLPCH01Handler.ValidateProcessedPunches();
            if (!response.IsError)
            {
                response = _objBLPCH01Handler.SaveProcessedPunches();
            }
            return Ok(response);
        }
    }
}
