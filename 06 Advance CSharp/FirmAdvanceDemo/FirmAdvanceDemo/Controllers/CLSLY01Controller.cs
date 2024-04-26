using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utility;
using System;
using System.Web;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing salary operations, such as crediting salaries.
    /// </summary>
    [RoutePrefix("api/salary")]
    public class CLSLY01Controller : ApiController
    {
        /// <summary>
        /// Instance of the salary handler for managing salary operations.
        /// </summary>
        private readonly BLSLY01Handler _objBLSLY01Handler;

        /// <summary>
        /// Initializes a new instance of the CLSLY01Controller class.
        /// </summary>
        public CLSLY01Controller()
        {
            _objBLSLY01Handler = new BLSLY01Handler();
        }

        /// <summary>
        /// Credits salary to employees.
        /// </summary>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
        [HttpPost]
        [Route("credit")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        public IHttpActionResult CreditSalary()
        {
            Response response;
            _objBLSLY01Handler.PresaveUnSalariedAttendance();
            response = _objBLSLY01Handler.ValidateUnSalariedAttendance();
            if (!response.IsError)
            {
                response = _objBLSLY01Handler.SaveSalaries();
            }
            return Ok(response);
        }

        /// <summary>
        /// Action method to download salary slip as a CSV file for a specific employee within a date range. Requires Employee role.
        /// </summary>
        /// <param name="employeeID">Employee ID</param>
        /// <param name="startDate">Start date of the salary slip</param>
        /// <param name="endDate">End date of the salary slip</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpGet]
        [Route("download/{employeeID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
        public IHttpActionResult GetSalarySlipCsv(int employeeID, DateTime startDate, DateTime endDate)
        {

            Response response = _objBLSLY01Handler.DownloadSalarySlip(employeeID, startDate, endDate);

            if (!response.IsError)
            {
                HttpResponse httpResponse = HttpContext.Current.Response;

                httpResponse.Clear();
                httpResponse.AppendHeader("Content-Type", "text/csv");
                httpResponse.AppendHeader("Content-Disposition", $"attachment;filename=salary-slip-{employeeID}-{startDate:yyyyMMdd}To{endDate:yyyyMMdd}.csv;");

                httpResponse.BinaryWrite((byte[])response.Data);

                return Ok();
            }
            return Ok(response);
        }
    }
}
