
using System.ComponentModel;
using System.Linq;
using LINQDemo.Custom;
using LINQDemo.Model;
using System.Configuration;
using ServiceStack.OrmLite;
using System.Data;

namespace LINQDemo
{
    internal partial class Program
    {

        static void Main(string[] args)
        {
            //LinqMethods();

            //LinqWithDataTable();

            string connString = ConfigurationManager.ConnectionStrings["ConnName"]?.ConnectionString;

            Console.WriteLine(connString);

            // create a connection to ur db
            OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connString, MySqlDialect.Provider);

            using(IDbConnection db = dbFactory.OpenDbConnection())
            {
                // create an instance of attendance (ATD01) class
                ATD01 attendance = new ATD01()
                {
                    d01f02 = 2,
                    d01f03 = new DateOnly(2024, 1, 25),
                    d01f04 = 7.5
                };
                //db.CreateTable<ATD01>();
                db.Insert<ATD01>(attendance);
            }
        }
    }
}