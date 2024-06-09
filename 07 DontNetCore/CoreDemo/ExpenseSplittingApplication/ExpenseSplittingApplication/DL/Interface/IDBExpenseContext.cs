using ExpenseSplittingApplication.BL.Domain;
using System.Collections.Generic;

namespace ExpenseSplittingApplication.DL.Interface
{
    public interface IDBExpenseContext
    {
        double GetDueAmount(int userID, int recievableUserID);
        public SettlementReport GetSettlementReport(int userID);
        public List<int> GetUserIdsNotInDatabase(List<int> lstUserID);
    }
}
