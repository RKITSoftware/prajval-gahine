using ExpenseSplittingApplication.Common.Interface;
using ExpenseSplittingApplication.Models.POCO;
using MySql.Data.MySqlClient;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System.Data;

namespace ExpenseSplittingApplication.Common.Helper
{
    public class Utility : IUtility
    {
        private IDbConnectionFactory _dbFactory;
        private IDbConnection _connection;
        public Utility(IDbConnectionFactory dbFactory, IDbConnection connection)
        {
            _dbFactory = dbFactory;
            _connection = connection;
        }
        public bool UserIDExists(int userID)
        {
            bool userIDExists;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                userIDExists = db.Exists<USR01>(user => user.R01F01 == userID);
            }
            return userIDExists;
        }

        public bool UsernameExists(string username)
        {
            bool usernameExists;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                usernameExists = db.Exists<USR01>(user => user.R01F02 == username);
            }
            return usernameExists;
        }

        public DataTable ExecuteQuery(string query)
        {
            IDbConnection connection = new MySqlConnection();
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, (MySqlConnection)_connection);

            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);

            return dataTable;
        }
    }
}
