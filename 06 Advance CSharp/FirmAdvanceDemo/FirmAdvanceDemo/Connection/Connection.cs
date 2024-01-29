using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo
{
    public class Connection
    {
        public static string ConnString;

        public static OrmLiteConnectionFactory dbFactory;

        static Connection()
        {
            ConnString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            dbFactory = new OrmLiteConnectionFactory(ConnString, MySqlDialect.Provider);
        }
    }
}