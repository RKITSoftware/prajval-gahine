﻿using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;
using ExpenseSplittingApplication.Models.DTO.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExpenseSplittingApplication.Controllers
{
    [Route("api/user")]
    [Authorize]
    public class CLUSR01Controller : ControllerBase
    {
        private readonly IUSR01Service _usr01Service;

        public CLUSR01Controller(IUSR01Service usr01Service)
        {
            _usr01Service = usr01Service;
        }

        //[HttpGet("")]
        [NonAction]
        public IActionResult GetUser()
        {
            Response response = _usr01Service.GetAll();
            return Ok(response);
        }

        [HttpPost("")]
        [ValidateModel]
        [AllowAnonymous]
        public IActionResult PostUser([FromBody]DTOUSR01 objDTOUSR01)
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
        [ValidateModel]
        [ValidateUserAccess]
        public IActionResult PutUser([FromBody] DTOUSR01 objDTOUSR01)
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

        [HttpPatch("change-password")]
        public IActionResult ChangePassword(string oldPassword, string newPassword)
        {
            int userID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userID")?.Value ?? "");

            Response response = _usr01Service.ValidatePassword(userID, oldPassword);
            if(!response.IsError)
            {
                response = _usr01Service.ChangePassword(userID, newPassword);
            }
            return Ok(response);
        }

        
        [HttpDelete("")]
        public IActionResult DeleteUser()
        {
            int userID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userID")?.Value ?? "");

            Response response = _usr01Service.DeleteValidation(userID);
            if (!response.IsError)
            {
                response = _usr01Service.Delete(userID);
            }
            return Ok(response);
        }
    }
}
