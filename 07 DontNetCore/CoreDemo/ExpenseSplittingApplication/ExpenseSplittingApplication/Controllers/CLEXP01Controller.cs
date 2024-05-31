using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ExpenseSplittingApplication.Models;

namespace ExpenseSplittingApplication.Controllers
{
    [Route("api/[controller]")]
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
                response = _exp01Service.Validation();
                if (!response.IsError)
                {
                    response = _exp01Service.Save();
                }
            }
            return Ok(response);
        }

        [HttpDelete("")]
        public IActionResult DeleteExpense(int id)
        {
            Response response = _exp01Service.DeleteValidation(id);
            if (!response.IsError)
            {
                response = _exp01Service.Delete(id);
            }
            return Ok(response);
        }
    }
}
9