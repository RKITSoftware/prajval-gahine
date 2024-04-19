
using FirmAdvanceDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Data;
using System.Net.PeerToPeer;
using System.Net;

namespace FirmAdvanceDemo.Utitlity
{
    public class GeneralHandler
    {
        private static readonly IDbConnectionFactory _dbFactory;
        static GeneralHandler()
        {
            _dbFactory = OrmliteDbConnector.DbFactory;
        }

        public static bool CheckUserIDExists(int userID)
        {

            int count = 0;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                count = (int)db.Count<USR01>(user => user.R01F01 == userID);
            }
            return count > 0;
        }

        public static bool CheckUsernameExists(string username)
        {
            int count = 0;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                count = (int)db.Count<USR01>(user => user.R01F02 == username);
            }
            return count > 0;
        }

        public static string RetrieveUsernameByUserID(int userID)
        {

            string username = string.Empty;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                username = db.Scalar<USR01, string>(user => user.R01F01 == userID);
            }

            return username;
        }

        public static int RetrieveUserIdByUsername(string username)
        {
            int userId = 0;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                userId = db.Scalar<USR01, int>(user => user.R01F02 == username);
            }
            return userId;
        }

        public static string RetrievePassword(string username)
        {
            string hashedPassword = string.Empty;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                hashedPassword = db.Scalar<USR01, string>(user => user.R01F03, user => user.R01F02 == username);
            }
            return hashedPassword;
        }

        public static int RetrieveEmployeeIDByUserID(int userID)
        {
            int employeeID = 0;
            using (IDbConnection db = _dbFactory.OpenDbConnection())
            {
                employeeID = db.Scalar<UMP02, int>(userEmployee => userEmployee.P02F03, userEmployee => userEmployee.P02F02 == userID);
            }

            return employeeID;
        }
    }
}