using ServiceStack.OrmLite;
using System.Configuration;
using System.Data;
using FirmAdvanceDemo.Models.POCO;

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
            //string ConnString = ConfigurationManager.ConnectionStrings["MyDB"].ConnectionString;
            ////string ConnString = "server=127.0.0.1;uid=Admin;pwd=gs@123;";

            //OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(ConnString, MySqlDialect.Provider);

            //using (IDbConnection db = dbFactory.OpenDbConnection())
            //{
            //    db.ExecuteNonQuery($"CREATE DATABASE IF NOT EXISTS `firmadvance2`");
            //    db.ChangeDatabase("firmadvance2");
            //    ////db.CreateTableIfNotExists<STG01>();
            //    ////db.Insert<STG01>(new STG01() { g01f02 = null });
            //    ////db.CreateTableIfNotExists<RLE01>();
            //    ////db.CreateTableIfNotExists<DPT01>();
            //    ////db.CreateTableIfNotExists<PSN01>();
            //    ////db.CreateTableIfNotExists<USR01>();
            //    ////db.CreateTableIfNotExists<ULE02>();
            //    ////db.CreateTableIfNotExists<EMP01>();
            //    ////db.CreateTableIfNotExists<UMP02>();
            //    ////db.CreateTableIfNotExists<ATD01>();
            //    ////db.CreateTableIfNotExists<LVE02>();
            //    ////db.CreateTableIfNotExists<SLY01>();
            //    ////db.CreateTable<PCH01>();
            //}
        }
    }
}