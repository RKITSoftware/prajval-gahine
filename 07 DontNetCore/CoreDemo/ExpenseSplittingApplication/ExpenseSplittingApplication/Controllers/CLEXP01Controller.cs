using ExpenseSplittingApplication.BL.Master.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;
using ExpenseSplittingApplication.Models.DTO.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExpenseSplittingApplication.Controllers
{
    /// <summary>
    /// Controller for handling expense-related operations.
    /// </summary>
    [Route("api/expense")]
    [ApiController]
    public class CLEXP01Controller : ControllerBase
    {
        /// <summary>
        /// The expense service interface.
        /// </summary>
        private readonly IEXP01Service _exp01Service;

        /// <summary>
        /// Initializes a new instance of the <see cref="CLEXP01Controller"/> class.
        /// </summary>
        /// <param name="exp01Service">The expense service interface.</param>
        public CLEXP01Controller(IEXP01Service exp01Service)
        {
            _exp01Service = exp01Service;
        }

        /// <summary>
        /// Creates a new expense.
        /// </summary>
        /// <param name="dtoEXC">The expense data transfer object.</param>
        /// <returns>An <see cref="IActionResult"/> containing the response.</returns>
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

        /// <summary>
        /// Retrieves the settlement report for the current user.
        /// </summary>
        /// <returns>An <see cref="IActionResult"/> containing the response.</returns>
        [HttpGet("settlement-report")]
        public IActionResult GetSettlementReport()
        {
            int userID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userID")?.Value ?? "");
            Response response = _exp01Service.GetSettlementReport(userID);
            return Ok(response);
        }

        /// <summary>
        /// Settles dues with another user.
        /// </summary>
        /// <param name="payableUserId">The ID of the user to whom the amount is payable.</param>
        /// <param name="amount">The amount to settle.</param>
        /// <returns>An <see cref="IActionResult"/> containing the response.</returns>
        [HttpPost("settle-dues/{payableUserId}")]
        public IActionResult SettleDues(int payableUserId, double amount)
        {
            int userID = int.Parse(User.Claims.FirstOrDefault(c => c.Type == "userID")?.Value ?? "");
            Response response = _exp01Service.SettleDues(userID, payableUserId, amount);
            return Ok(response);
        }
    }
}
