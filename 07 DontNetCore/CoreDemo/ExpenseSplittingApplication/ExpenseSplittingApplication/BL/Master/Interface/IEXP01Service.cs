using ExpenseSplittingApplication.BL.Common.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;

namespace ExpenseSplittingApplication.BL.Master.Interface
{
    /// <summary>
    /// Interface for expense-related services extending common operations.
    /// </summary>
    public interface IEXP01Service : ICommonService<DTOEXC>
    {
        /// <summary>
        /// Retrieves a settlement report for the specified user ID.
        /// </summary>
        /// <param name="userID">The ID of the user for whom the settlement report is retrieved.</param>
        /// <returns>A response object containing the settlement report.</returns>
        Response GetSettlementReport(int userID);

        /// <summary>
        /// Settles dues between two users.
        /// </summary>
        /// <param name="userID">The ID of the user initiating the settlement.</param>
        /// <param name="receivableUserID">The ID of the user who will receive the settlement.</param>
        /// <param name="amount">The amount to be settled.</param>
        /// <returns>A response object indicating the result of the settlement operation.</returns>
        Response SettleDues(int userID, int receivableUserID, double amount);
    }
}
