using FirmAdvanceDemo.BL;
using FirmAdvanceDemo.Utitlity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;

namespace FirmAdvanceDemo.Controllers
{
    [RoutePrefix("api/download")]
    public class CLDownloadController : ApiController
    {
        [NonAction]
        public IHttpActionResult Returner(ResponseStatusInfo responseStatusInfo)
        {
            if (responseStatusInfo.IsRequestSuccessful)
            {
                
                return Ok(ResponseWrapper.Wrap(responseStatusInfo.Message, responseStatusInfo.Data));
            }
            return ResponseMessage(Request.CreateErrorResponse(responseStatusInfo.StatusCode, responseStatusInfo.Message));
        }


        [HttpGet]
        [Route("salaryslip/{id}")]
        public IHttpActionResult GetSalarySlipCsv(int id, string start, string end)
        {
            ResponseStatusInfo rsi = BLDownload.DownloadSalarySlip(
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
