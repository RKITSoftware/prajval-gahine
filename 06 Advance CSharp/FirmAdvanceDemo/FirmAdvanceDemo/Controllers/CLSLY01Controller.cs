using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Models.DTO.DataAnnotations;
using FirmAdvanceDemo.Utility;
using System;
using System.Globalization;
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
        #region Private Fields
        /// <summary>
        /// Instance of the salary handler for managing salary operations.
        /// </summary>
        private readonly BLSLY01Handler _objBLSLY01Handler;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the CLSLY01Controller class.
        /// </summary>
        public CLSLY01Controller()
        {
            _objBLSLY01Handler = new BLSLY01Handler();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Credits salary to employees.
        /// </summary>
        /// <returns>HTTP response indicating the success or failure of the operation.</returns>
        [HttpPost]
        [Route("credit")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        [ValidateMonthYear]
        public IHttpActionResult CreditSalary(int year, int month)
        {
            Response response;
            _objBLSLY01Handler.PresaveUnSalariedAttendance(year, month);
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

            Response response = GeneralUtility.ValidateAccess(employeeID);
            if (!response.IsError)
            {
                response = _objBLSLY01Handler.DownloadSalarySlip(employeeID, startDate, endDate);

                if (!response.IsError)
                {
                    HttpResponse httpResponse = HttpContext.Current.Response;

                    httpResponse.Clear();
                    httpResponse.AppendHeader("Content-Type", "text/csv");
                    httpResponse.AppendHeader("Content-Disposition", $"attachment;filename=salary-slip-{employeeID}-{startDate:yyyyMMdd}To{endDate:yyyyMMdd}.csv;");

                    httpResponse.BinaryWrite((byte[])response.Data);

                    return Ok();
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Downloads the salary slips in CSV format for the specified employee and month range.
        /// </summary>
        /// <param name="employeeID">The ID of the employee.</param>
        /// <param name="startYear">The start year of the month range.</param>
        /// <param name="startMonth">The start month of the month range.</param>
        /// <param name="endYear">The end year of the month range.</param>
        /// <param name="endMonth">The end month of the month range.</param>
        /// <returns>An IHttpActionResult containing the downloaded CSV file.</returns>
        [HttpGet]
        [Route("download/month-range/{employeeID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
        [ValidateMonthYear]
        public IHttpActionResult GetSalarySlipCsvForMonthRange(int employeeID, int startYear, int startMonth, int endYear, int endMonth)
        {

            Response response = GeneralUtility.ValidateAccess(employeeID);
            if (!response.IsError)
            {
                DateTime startDate = new DateTime(startYear, startMonth, 1);
                DateTime endDate = new DateTime(endYear, endMonth, 1);

                response = _objBLSLY01Handler.DownloadSalarySlip(employeeID, startDate, endDate);

                if (!response.IsError)
                {
                    HttpResponse httpResponse = HttpContext.Current.Response;

                    httpResponse.Clear();
                    httpResponse.AppendHeader("Content-Type", "text/csv");
                    httpResponse.AppendHeader("Content-Disposition", $"attachment;filename=salary-slip-{employeeID}-{startYear}/{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(startMonth)}To{endYear}/{CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(endMonth)}.csv;");

                    httpResponse.BinaryWrite((byte[])response.Data);

                    return Ok();
                }
            }
            return Ok(response);
        }

        /// <summary>
        /// Downloads the salary slips in CSV format for the specified month.
        /// </summary>
        /// <param name="year">The year of month.</param>
        /// <param name="month">The month of salary slip.</param>
        /// <returns></returns>
        [HttpGet]
        [Route("download/salary-slip")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "A")]
        [ValidateMonthYear]
        public IHttpActionResult GetSalarySlipCsvForMonthRange(int year, int month)
        {
            Response response = _objBLSLY01Handler.DownloadSalarySlipForMonth(year, month);

            if (!response.IsError)
            {
                HttpResponse httpResponse = HttpContext.Current.Response;

                httpResponse.Clear();
                httpResponse.AppendHeader("Content-Type", "text/csv");
                httpResponse.AppendHeader("Content-Disposition", $"attachment;filename=salary-slip-{year}/{month}.csv;");

                httpResponse.BinaryWrite((byte[])response.Data);

                return Ok();
            }
            return Ok(response);
        }
        #endregion
    }
}
