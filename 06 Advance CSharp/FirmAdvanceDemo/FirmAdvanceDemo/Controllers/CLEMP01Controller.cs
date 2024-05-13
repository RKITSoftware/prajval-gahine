using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utility;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing employee operations.
    /// </summary>
    [RoutePrefix("api/employee")]
    public class CLEMP01Controller : ApiController
    {
        #region Private Fields
        /// <summary>
        /// Instance of BLEmployee.
        /// </summary>
        private readonly BLEMP01Handler _objBLEMP01Handler;
        #endregion

        #region Constructors
        /// <summary>
        /// Default constructor for CLEmployeeController.
        /// </summary>
        public CLEMP01Controller()
        {
            _objBLEMP01Handler = new BLEMP01Handler();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Action method to retrieve all employees. Requires Admin role.
        /// </summary>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpGet]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetEmployee()
        {
            // Retrieve all employees
            Response response = _objBLEMP01Handler.RetrieveEmployee();
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve an employee by ID. Requires Admin or Employee role.
        /// </summary>
        /// <param name="employeeID">Employee ID</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpGet]
        [Route("{employeeID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult GetEmployee(int employeeID)
        {
            // Retrieve an employee by ID
            Response response = GeneralUtility.ValidateAccess(employeeID);

            if (!response.IsError)
            {
                response = _objBLEMP01Handler.RetrieveEmployee(employeeID);
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to create a new employee. Requires Admin role.
        /// </summary>
        /// <param name="objDTOUMP">DTO object containing employee data</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPost]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult PostEmployee(DTOUMP01 objDTOUMP)
        {
            // Create a new employee
            Response response;

            _objBLEMP01Handler.Operation = EnmOperation.A;

            response = _objBLEMP01Handler.Prevalidate(objDTOUMP);

            if (!response.IsError)
            {
                _objBLEMP01Handler.Presave(objDTOUMP);
                if (!response.IsError)
                {
                    response = _objBLEMP01Handler.Save();
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to update an existing employee. Requires Admin or Employee role.
        /// </summary>
        /// <param name="objDTOUMP">DTO object containing updated employee data</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpPut]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult PutEmployee(DTOUMP01 objDTOUMP)
        {
            // Update an existing employee
            Response response = GeneralUtility.ValidateAccess(objDTOUMP.ObjDTOEMP01.P01F01);

            _objBLEMP01Handler.Operation = EnmOperation.E;

            if (!response.IsError)
            {
                response = _objBLEMP01Handler.Prevalidate(objDTOUMP);

                if (!response.IsError)
                {
                    _objBLEMP01Handler.Presave(objDTOUMP);
                    if (!response.IsError)
                    {
                        response = _objBLEMP01Handler.Save();
                    }
                }
            }
            return Ok(response);
        }
        #endregion
    }
}
