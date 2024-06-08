using ExpenseSplittingApplication.BL.Domain;

namespace ExpenseSplittingApplication.DL.Interface
{
    public interface IDBExpenseContext
    {
        public SettlementReport GetSettlementReport(int userID);
        public List<int> GetUserIdsNotInDatabase(List<int> lstUserID);
    }
}
