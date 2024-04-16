using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Connection
{
    public class MysqlDbConnector
    {
        public static MySqlConnection Connection { get; set; }

        static MysqlDbConnector()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["firmAdvance378"].ConnectionString;
            Connection = new MySqlConnection(connectionString);
        }
    }
}