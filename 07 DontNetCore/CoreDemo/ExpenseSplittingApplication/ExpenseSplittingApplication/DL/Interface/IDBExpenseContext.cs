using ExpenseSplittingApplication.BL.Domain;
using System.Data;

namespace ExpenseSplittingApplication.DL.Interface
{
    public interface IDBExpenseContext
    {
        public SettlementReport GetSettlementReport(int userID);
        public List<int> GetUserIdsNotInDatabase(List<int> lstUserID);
    }
}
