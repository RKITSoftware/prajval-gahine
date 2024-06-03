using ExpenseSplittingApplication.BL.Common.Interface;
using ExpenseSplittingApplication.Models;
using ExpenseSplittingApplication.Models.DTO;

namespace ExpenseSplittingApplication.BL.Master.Interface
{
    public interface IUSR01Service : ICommonService<DTOUSR01>
    {
        Response ChangePassword(int userID, string newPassword);
        Response GetAll();
        Response ValidatePassword(int userID, string oldPassword);
    }
}
