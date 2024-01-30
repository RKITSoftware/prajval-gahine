using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using FirmAdvanceDemo.Models;

namespace FirmAdvanceDemo
{
    public class CreateTables
    {
        private static OrmLiteConnectionFactory _dbFactory;
        static CreateTables()
        {
            _dbFactory = Connection.dbFactory;
        }
        public static void CreateTablesMethod()
        {
            string ConnString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;

            OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConnString, MySqlDialect.Provider); ;
        
            using(IDbConnection db = dbFactory.OpenDbConnection())
            {
                db.CreateTableIfNotExists<RLE01>();
                db.CreateTableIfNotExists<DPT01>();
                db.CreateTableIfNotExists<PSN01>();
                db.CreateTableIfNotExists<USR01>();
                db.CreateTableIfNotExists<USR01RLE01>();
                db.CreateTableIfNotExists<EMP01>();
                db.CreateTableIfNotExists<USR01EMP01>();
                db.CreateTableIfNotExists<ATD01>();
                db.CreateTableIfNotExists<LVE01>();
                db.CreateTableIfNotExists<SLY01>();
            }
        
        }
    }
}