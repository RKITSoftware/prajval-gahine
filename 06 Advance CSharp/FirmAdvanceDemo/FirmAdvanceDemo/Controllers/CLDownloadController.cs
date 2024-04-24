using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utility;
using System;
using System.Text;
using System.Web;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    /// <summary>
    /// Controller for managing download-related operations.
    /// </summary>
    [RoutePrefix("api/download")]
    public class CLDownloadController : ApiController
    {
        /// <summary>
        /// Instance of BLDownload for handling download-related business logic.
        /// </summary>
        private readonly BLDownload _objBLDownload;

        /// <summary>
        /// Default constructor for CLDownloadController.
        /// </summary>
        public CLDownloadController()
        {
            _objBLDownload = new BLDownload();
        }

        /// <summary>
        /// Action method to download salary slip as a CSV file for a specific employee within a date range. Requires Employee role.
        /// </summary>
        /// <param name="employeeID">Employee ID</param>
        /// <param name="start">Start date of the salary slip</param>
        /// <param name="end">End date of the salary slip</param>
        /// <returns>HTTP response indicating the result of the operation.</returns>
        [HttpGet]
        [Route("salaryslip/{employeeID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
        public IHttpActionResult GetSalarySlipCsv(int employeeID, DateTime start, DateTime end)
        {
            // Download salary slip as CSV for the specified employee within the specified date range
            Response statusInfo = _objBLDownload.DownloadSalarySlip(
                employeeID,
                start,
                end
            );

            HttpResponse response = HttpContext.Current.Response;

            response.Clear();
            response.AppendHeader("Content-Type", "text/csv");
            response.AppendHeader("Content-Disposition", $"attachment;filename=salary-slip-{employeeID}-{start:yyyyMMdd}TO{end:yyyyMMdd}.csv;");

            string csvContent = (string)statusInfo.Data;
            response.BinaryWrite(Encoding.UTF8.GetBytes(csvContent));
            return Ok(response);
        }
    }
}
