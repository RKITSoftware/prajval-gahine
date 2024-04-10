
using ORMLiteDemo.Model;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;
using System.Configuration;
using System.Data;

namespace ORMLiteDemo
{
    /// <summary>
    /// Entry class for LINQ demo
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Entry method for LINQ demo
        /// </summary>
        static void Main()
        {
            string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            Console.WriteLine(connString);

            // create a connection instance for ur db (here mysql)
            OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connString, MySqlDialect.Provider);

            // open a connection
            using (IDbConnection db = dbFactory.OpenDbConnection())
            {
                // create USR01 table in db if not exists
                db.CreateTableIfNotExists<USR01>();

                // create a user list
                USR01[] lstUser = new USR01[]
                {
                    new USR01{ r01f02 = "prajvalgahine", r01f03 = "prajval@123"},
                    new USR01{ r01f02 = "deeppatel", r01f03 = "deep@123"},
                    new USR01{ r01f02 = "yashlathiya", r01f03 = "yash@123"},
                    new USR01{ r01f02 = "krinsikayada", r01f03 = "krinsi@123"},
                };

                // -------------------- INSERT -----------------------

                // insert user list in USR01 table
                db.Insert<USR01>(lstUser);
                //db.InsertAll<USR01>(lstUser);

                // -------------------- UPDATE -----------------------

                // update password of prajval to gahine@123 (i.e., update inplace)
                db.Update<USR01>(new { r01f03 = "prajval@123" }, where: user => user.r01f02 == "prajvalgahine");


                //or use single mthd => which first gets the user info from mysql and return USR01 object
                USR01 user = db.Single<USR01>(user => user.r01f02 == "prajvalgahine");
                // change the portion of user that u want to update
                user.r01f03 = "png@123";
                //update the same to mysql server
                db.Update<USR01>(user);


                // sql expression represented as an object => it will be used in onlyFields
                SqlExpression<USR01> SqlExp = db.From<USR01>()
                    .Where(user => user.r01f02 == "prajvalgahine")
                    .Update(user => user.r01f03);


                db.UpdateOnlyFields<USR01>(new USR01 { r01f03 = "prag@123" }, onlyFields: SqlExp);

                // updateOnly using dictionary
                Dictionary<string, object> toUpdateDict = new Dictionary<string, object>()
                {
                    [nameof(USR01.r01f03)] = "prajval@gahine"
                };

                db.UpdateOnly<USR01>( () => new USR01 { r01f03 = "prajval@gahine" }, where: user => user.r01f02 == "prajvalgahine");
                //Dictionary<string, object> toUpdateDict = new Dictionary<string, object>()
                //{
                //    [nameof(USR01.r01f03)] = "prajval@gahine"
                //};

                db.UpdateOnly<USR01>(toUpdateDict, user => user.r01f02 == "prajvalgahine");



                // -------------------- SELECT -----------------------

                List<USR01> lstUserFromSelect = db.Select<USR01>();
                PrintUserList(lstUserFromSelect);
                Console.WriteLine();

                // select using where filter
                lstUserFromSelect = db.Select<USR01>(user => user.r01f01 > 2);
                PrintUserList(lstUserFromSelect);
                Console.WriteLine();

                // select using Sql.In() (similar to LINQ Contains method)
                lstUserFromSelect = db.Select<USR01>(user => Sql.In(user.r01f01, 2, 3));
                PrintUserList(lstUserFromSelect);
                Console.WriteLine();

                // select using string method
                lstUserFromSelect = db.Select<USR01>(user => user.r01f02.StartsWith("pra"));
                PrintUserList(lstUserFromSelect);
                Console.WriteLine();

                lstUserFromSelect = db.Select<USR01>(user => user.r01f02.EndsWith("da"));
                PrintUserList(lstUserFromSelect);
                Console.WriteLine();

                lstUserFromSelect = db.Select<USR01>(user => user.r01f02.Contains("pp"));
                PrintUserList(lstUserFromSelect);
                Console.WriteLine();

                // select using SingleById - uses the provided value to query against a PK (expecting 1 value)
                USR01 userSelect = db.SingleById<USR01>(2);
                Console.WriteLine($"User id: {userSelect.r01f01}, username: {userSelect.r01f02}");
                Console.WriteLine();

                // select using single - use to bring 1 element from the result set
                userSelect = db.Single<USR01>(user => user.r01f02.Contains("ya"));
                Console.WriteLine($"User id: {userSelect.r01f01}, username: {userSelect.r01f02}");
                Console.WriteLine();

                // select using sql typed expression
                SqlExpression<USR01> SqlExpForSelect = db.From<USR01>()
                    .Where(user => user.r01f02 == "prajvalgahine")
                    .Select(Sql.Count("*"));

                // selecting a scalar quantity
                int count = db.Scalar<int>(SqlExpForSelect);
                Console.WriteLine(count);
                Console.WriteLine();

                // selecting just a column
                SqlExpForSelect = db.From<USR01>()
                    .Select(user => user.r01f02);
                List<string> lstUsername = db.Column<string>(SqlExpForSelect);
                foreach (string username in lstUsername)
                {
                    Console.WriteLine(username);
                }
                Console.WriteLine();

                // select 2 cols
                SqlExpForSelect = db.From<USR01>()
                    .Select(user => new { user.r01f01, user.r01f02 });
                Dictionary<int, string> IdNameDict = db.Dictionary<int, string>(SqlExpForSelect);
                foreach (KeyValuePair<int, string> IdName in IdNameDict)
                {
                    Console.WriteLine($"User id: {IdName.Key}, username: {IdName.Value}");
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Method to print user list
        /// </summary>
        /// <param name="lstUser">list of user</param>
        public static void PrintUserList(List<USR01> lstUser)
        {
            foreach (USR01 user in lstUser)
            {
                Console.WriteLine($"User id: {user.r01f01}, username: {user.r01f02}");
            }
        }
    }
}