using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DatabaseWithCrudWebApi
{
    public static class Connection
    {
        public static MySqlConnection connection;

        static Connection()
        {
            string connectionString = ConfigurationManager.AppSettings["connectionString"];
            connection = new MySqlConnection(connectionString);
        }
    }
}