using ExpenseSplittingApplication.BL.Common.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;
using ExpenseSplittingApplication.Models.POCO;

namespace ExpenseSplittingApplication.BL.Master.Interface
{
    /// <summary>
    /// Interface for user-related services extending common operations.
    /// </summary>
    public interface IUSR01Service : ICommonService<DTOUSR01>
    {
        /// <summary>
        /// Changes the password for the specified user.
        /// </summary>
        /// <param name="userID">The ID of the user whose password is to be changed.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>A response object indicating the result of the password change operation.</returns>
        Response ChangePassword(int userID, string newPassword);

        /// <summary>
        /// Retrieves the user based on the provided username and password.
        /// </summary>
        /// <param name="username">The username of the user to retrieve.</param>
        /// <param name="password">The password of the user to retrieve.</param>
        /// <returns>The retrieved user entity (USR01).</returns>
        USR01 GetUser(string username, string password);

        /// <summary>
        /// Validates the old password for the specified user before changing it.
        /// </summary>
        /// <param name="userID">The ID of the user whose password is being validated.</param>
        /// <param name="oldPassword">The old password to validate.</param>
        /// <returns>A response object indicating the result of the password validation operation.</returns>
        Response ValidatePassword(int userID, string oldPassword);
    }
}
