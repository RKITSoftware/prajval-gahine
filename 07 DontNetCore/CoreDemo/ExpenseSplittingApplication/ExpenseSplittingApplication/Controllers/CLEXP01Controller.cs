using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize]
        [HttpGet("settlement-report")]
        public IActionResult GetSettlementReport(int userID)
        {
            Response response = _exp01Service.GetSettlementReport(userID);
            return Ok(response);
        }
    }
}