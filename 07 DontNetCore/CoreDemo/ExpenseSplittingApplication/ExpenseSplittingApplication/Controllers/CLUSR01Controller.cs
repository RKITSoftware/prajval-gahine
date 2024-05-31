using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Models;
using Microsoft.AspNetCore.Mvc;
using ExpenseSplittingApplication.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace ExpenseSplittingApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CLUSR01Controller : ControllerBase
    {
        private readonly IUSR01Service _usr01Service;

        public CLUSR01Controller(IUSR01Service usr01Service)
        {
            _usr01Service = usr01Service;
        }

        [HttpGet("")]
        public IActionResult GetUser()
        {
            return Ok(_usr01Service.GetAll());
        }

        [HttpPost("")]
        public IActionResult PostUser(DTOUSR01 objDTOUSR01)
        {
            _usr01Service.Operation = EnmOperation.A;
            Response response = _usr01Service.PreValidation(objDTOUSR01);

            if (!response.IsError)
            {
                _usr01Service.PreSave(objDTOUSR01);
                response = _usr01Service.Validation();

                if (!response.IsError)
                {
                    response = _usr01Service.Save();
                }
            }
            return Ok(response);
        }

        [HttpPut("")]
        public IActionResult PutUser(DTOUSR01 objDTOUSR01)
        {
            _usr01Service.Operation = EnmOperation.E;
            Response response = _usr01Service.PreValidation(objDTOUSR01);

            if (!response.IsError)
            {
                _usr01Service.PreSave(objDTOUSR01);
                response = _usr01Service.Validation();

                if (!response.IsError)
                {
                    response = _usr01Service.Save();
                }
            }
            return Ok(response);
        }

        [HttpPatch("")]
        public IActionResult ChangePassword(string password)
        {
            Response response = new Response();
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            Response response = _usr01Service.DeleteValidation(id);
            if(!response.IsError)
            {
                response = _usr01Service.Delete(id);
            }
            return Ok(response);
        }
    }
}
