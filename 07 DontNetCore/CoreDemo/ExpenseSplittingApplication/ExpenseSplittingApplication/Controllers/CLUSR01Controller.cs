using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;
using ExpenseSplittingApplication.Models.DTO.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExpenseSplittingApplication.Controllers
{
    /// <summary>
    /// Controller for handling user-related operations.
    /// </summary>
    [Route("api/user")]
    [Authorize]
    public class CLUSR01Controller : ControllerBase
    {
        /// <summary>
        /// The user service interface.
        /// </summary>
        private readonly IUSR01Service _usr01Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLUSR01Controller"/> class.
        /// </summary>
        /// <param name="usr01Service">The user service interface.</param>
        public CLUSR01Controller(IUSR01Service usr01Service)
        {
            _usr01Service = usr01Service;
        }

        /// <summary>
        /// Creates a new user.
        /// </summary>
        /// <param name="objDTOUSR01">The user data transfer object.</param>
        /// <returns>An <see cref="IActionResult"/> containing the response.</returns>
        [HttpPost("")]
        [ValidateModel]
        [AllowAnonymous]
        public IActionResult PostUser([FromBody] DTOUSR01 objDTOUSR01)
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

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="objDTOUSR01">The user data transfer object.</param>
        /// <returns>An <see cref="IActionResult"/> containing the response.</returns>
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

        /// <summary>
        /// Changes the password of the current user.
        /// </summary>
        /// <param name="oldPassword">The old password of the user.</param>
        /// <param name="newPassword">The new password of the user.</param>
        /// <returns>An <see cref="IActionResult"/> containing the response.</returns>
        [HttpPatch("change-password")]
        public IActionResult ChangePassword(string oldPassword, string newPassword)
        {
            int userID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userID")?.Value ?? "");

            Response response = _usr01Service.ValidatePassword(userID, oldPassword);
            if (!response.IsError)
            {
                response = _usr01Service.ChangePassword(userID, newPassword);
            }
            return Ok(response);
        }
    }
}
