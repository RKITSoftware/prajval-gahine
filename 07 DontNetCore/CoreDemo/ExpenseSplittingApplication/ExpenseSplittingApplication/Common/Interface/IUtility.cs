using System.Data;

namespace ExpenseSplittingApplication.Common.Interface
{
    public interface IUtility
    {
        public bool UserIDExists(int userID);

        public bool UsernameExists(string username);

        public DataTable ExecuteQuery(string query);
    }
}
