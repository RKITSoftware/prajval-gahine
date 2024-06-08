using ExpenseSplittingApplication.BL.Common.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;
using ExpenseSplittingApplication.Models.POCO;

namespace ExpenseSplittingApplication.BL.Master.Interface
{
    public interface IUSR01Service : ICommonService<DTOUSR01>
    {
        Response ChangePassword(int userID, string newPassword);
        Response GetAll();
        USR01 GetUser(string username, string password);
        Response ValidatePassword(int userID, string oldPassword);
    }
}
