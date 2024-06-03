using ExpenseSplittingApplication.Common.Interface;
using ExpenseSplittingApplication.DL.Interface;
using System.Data;

namespace ExpenseSplittingApplication.DL.Context
{
    public class DBUSR01Context : IDBUserContext
    {
        private IDbConnection _connection;
        private IUtility _utility;
        public DBUSR01Context(IDbConnection connection, IUtility utility)
        {
            _connection = connection;
            _utility = utility;
        }

        public DataTable GetAll()
        {
            string query = string.Format(@"
                    SELECT
                        R01F01,
                        R01F02
                    FROM
                        USR01");

            return _utility.ExecuteQuery(query);
        }
    }
}
