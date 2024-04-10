using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using System;
using System.Text;
using System.Web;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/download")]
    public class CLDownloadController : BaseController
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
        [Route("salaryslip/{id}")]
        public IHttpActionResult GetSalarySlipCsv(int id, string start, string end)
        {
            ResponseStatusInfo rsi = _objBLDownload.DownloadSalarySlip(
                id,
                DateTime.ParseExact(start, "yyyy-MM-dd", null),
                DateTime.ParseExact(end, "yyyy-MM-dd", null)
                );

            HttpResponse response = HttpContext.Current.Response;

            response.Clear();
            response.AppendHeader("Content-Type", "text/csv");
            response.AppendHeader("Content-Disposition", $"attachment;filename=salary-slip-{id}-{start}TO{end}.csv;");

            string csvContent = rsi.Data.ToString();
            response.BinaryWrite(Encoding.UTF8.GetBytes(csvContent));
            return Ok();
        }
    }
}
