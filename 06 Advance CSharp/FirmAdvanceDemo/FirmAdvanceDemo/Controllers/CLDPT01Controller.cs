using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Enums;
using FirmAdvanceDemo.Models.DTO;
using FirmAdvanceDemo.Utility;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing department-related operations.
    /// </summary>
    [RoutePrefix("api/department")]
    public class CLDPT01Controller : ApiController
    {
        /// <summary>
        /// Instance of BLDPT01Handler for handling department-related business logic.
        /// </summary>
        private readonly BLDPT01Handler _objBLDPT01Handler;

        /// <summary>
        /// Default constructor for CLDepartmentController.
        /// </summary>
        public CLDPT01Controller()
        {
            _objBLDPT01Handler = new BLDPT01Handler();
        }

        /// <summary>
        /// Action method to retrieve all departments. Requires Admin role.
        /// </summary>
        [HttpGet]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult GetDepartment()
        {
            // Retrieve all departments
            Response response = _objBLDPT01Handler.RetrieveDepartment();
            return Ok(response);
        }

        /// <summary>
        /// Action method to retrieve a department by its ID. Requires Admin or Employee role.
        /// </summary>
        /// <param name="departmentId">Department ID</param>
        [HttpGet]
        [Route("{departmentId}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A,E")]
        public IHttpActionResult GetDepartment(int departmentId)
        {
            // Retrieve a department by its ID
            Response response = _objBLDPT01Handler.RetrieveDepartment(departmentId);
            return Ok(response);
        }

        /// <summary>
        /// Action method to create a new department. Requires Admin role.
        /// </summary>
        /// <param name="objDTODPT01">DTO object containing department data</param>
        [HttpPost]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult PostDepartment(DTODPT01 objDTODPT01)
        {
            // Create a new department
            Response response;

            _objBLDPT01Handler.Operation = EnmOperation.A;

            _objBLDPT01Handler.Presave(objDTODPT01);

            response = _objBLDPT01Handler.Valdiate();

            if (!response.IsError)
            {
                response = _objBLDPT01Handler.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to update an existing department. Requires Admin role.
        /// </summary>
        /// <param name="objDTODPT01">DTO object containing updated department data</param>
        [HttpPut]
        [Route("")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult PutDepartment(DTODPT01 objDTODPT01)
        {
            // Update an existing department
            Response response;

            _objBLDPT01Handler.Operation = EnmOperation.E;

            _objBLDPT01Handler.Presave(objDTODPT01);

            response = _objBLDPT01Handler.Valdiate();

            if (!response.IsError)
            {
                response = _objBLDPT01Handler.Save();
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to delete a department by its ID. Requires Admin role.
        /// </summary>
        /// <param name="departmentId">Department ID</param>
        [HttpDelete]
        [Route("{departmentId}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult DeleteDepartment(int departmentId)
        {
            // Delete a department by its ID
            Response response = _objBLDPT01Handler.ValidateDelete(departmentId);

            if (!response.IsError)
            {
                response = _objBLDPT01Handler.Delete(departmentId);
            }
            return Ok(response);
        }
    }
}
