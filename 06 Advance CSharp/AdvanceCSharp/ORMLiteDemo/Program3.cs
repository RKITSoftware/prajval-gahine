using ORMLiteDemo.Models.POCO;
using ServiceStack.Data;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORMLiteDemo
{
    internal class Program3
    {
        public static void Main()
        {
            InitializeDB.Init();

            //IDbConnectionFactory dbFactory = new OrmLiteConnectionFactory("server=127.0.0.1;uid=root;pwd=Mrpng@7121;database=firmadvance378", MySqlDialect.Provider);
            //using (IDbConnection db = dbFactory.OpenDbConnection())
            //{
            //    db.CreateTableIfNotExists<ATD01>();
            //    db.CreateTableIfNotExists<LVE02>();
            //}
        }
    }
}
