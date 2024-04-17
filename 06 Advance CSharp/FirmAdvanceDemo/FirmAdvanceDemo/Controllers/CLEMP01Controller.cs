using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utitlity;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing employee operations.
    /// </summary>
    [RoutePrefix("api/employee")]
    public class CLEMP01Controller : ApiController
    {
        /// <summary>
        /// Instance of BLEmployee
        /// </summary>
        private readonly BLEMP01Handler _objBLEMP01Handler;

        /// <summary>
        /// Default constructor for CLEmployeeController
        /// </summary>
        public CLEMP01Controller()
        {
            _objBLEMP01Handler = new BLEMP01Handler();
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetEmployee()
        {
            Response response = _objBLEMP01Handler.RetrieveEmployee();
            return Ok(response);

        }

        [HttpGet]
        [Route("{employeeId}")]
        public IHttpActionResult GetEmployee(int employeeId)
        {
            Response response = _objBLEMP01Handler.RetrieveEmployee(employeeId);
            return Ok(response);
        }

        [HttpPost]
        [Route("")]
        public IHttpActionResult PostEmployee(DTOUMP01 objDTOUMP)
        {
            Response response;

            _objBLEMP01Handler.Operation = EnmOperation.A;

            response = _objBLEMP01Handler.Prevalidate(objDTOUMP);

            if (!response.IsError)
            {
                _objBLEMP01Handler.Presave(objDTOUMP);
                response = _objBLEMP01Handler.Validate();
                if (!response.IsError)
                {
                    response = _objBLEMP01Handler.Save();
                }
            }
            return Ok(response);
        }

        [HttpPut]
        [Route("")]
        public IHttpActionResult PutEmployee(DTOUMP01 objDTOUMP)
        {
            Response response;

            _objBLEMP01Handler.Operation = EnmOperation.E;

            response = _objBLEMP01Handler.Prevalidate(objDTOUMP);

            if (!response.IsError)
            {
                _objBLEMP01Handler.Presave(objDTOUMP);
                response = _objBLEMP01Handler.Validate();
                if (!response.IsError)
                {
                    response = _objBLEMP01Handler.Save();
                }
            }
            return Ok(response);
        }
    }
}
