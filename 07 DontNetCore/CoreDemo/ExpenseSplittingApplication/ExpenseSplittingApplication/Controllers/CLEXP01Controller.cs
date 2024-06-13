using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;
using ExpenseSplittingApplication.Models.DTO.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExpenseSplittingApplication.Controllers
{
    [Route("api/expense")]
    [ApiController]
    public class CLEXP01Controller : ControllerBase
    {
        private readonly IEXP01Service _exp01Service;

        public CLEXP01Controller(IEXP01Service exp01Service)
        {
            _exp01Service = exp01Service;
        }

        [HttpPost("")]
        [ValidateUserAccess]
        public IActionResult PostExpense(DTOEXC dtoEXC)
        {
            _exp01Service.Operation = EnmOperation.A;
            Response response = _exp01Service.PreValidation(dtoEXC);
            if (!response.IsError)
            {
                _exp01Service.PreSave(dtoEXC);
                response = _exp01Service.Save();
            }
            return Ok(response);
        }

        [HttpGet("settlement-report")]
        public IActionResult GetSettlementReport()
        {
            int userID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userID")?.Value ?? "");
            Response response = _exp01Service.GetSettlementReport(userID);
            return Ok(response);
        }

        [HttpPost("settle-dues/{payableUserId}")]
        public IActionResult SettleDues(int payableUserId, double amount)
        {
            int userID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userID")?.Value ?? "");
            Response response = _exp01Service.SettleDues(userID, payableUserId, amount);
            return Ok(response);
        }
    }
}