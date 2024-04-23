using FirmAdvanceDemo.Auth;
using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using System;
using System.Text;
using System.Web;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/download")]
    public class CLDownloadController : ApiController
    {
        /// <summary>
        /// Instance of BLDownload
        /// </summary>
        private readonly BLDownload _objBLDownload;

        /// <summary>
        /// Default constructor for CLDownloadController
        /// </summary>
        public CLDownloadController()
        {
            _objBLDownload = new BLDownload();
        }

        [HttpGet]
        [Route("salaryslip/{employeeID}")]
        [AccessTokenAuthentication]
        [BasicAuthorization(Roles = "E")]
        public IHttpActionResult GetSalarySlipCsv(int employeeID, DateTime start, DateTime end)
        {
            Response statusInfo = _objBLDownload.DownloadSalarySlip(
                employeeID,
                start,
                end
                );

            HttpResponse response = HttpContext.Current.Response;

            response.Clear();
            response.AppendHeader("Content-Type", "text/csv");
            response.AppendHeader("Content-Disposition", $"attachment;filename=salary-slip-{employeeID}-{start}TO{end}.csv;");

            string csvContent = (string)statusInfo.Data;
            response.BinaryWrite(Encoding.UTF8.GetBytes(csvContent));
            return Ok(response);
        }
    }
}
