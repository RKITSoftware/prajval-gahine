using ORMLiteDemo.Model;
using ServiceStack.OrmLite;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ORMLiteDemo
{
    internal class Program2
    {
        public static void Main()
        {
            //string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
            //Console.WriteLine(connString);

            //// create a connection instance for ur db (here mysql)
            //OrmLiteConnectionFactory dbFactory = new OrmLiteConnectionFactory(connString, MySqlDialect.Provider);

            //// open a connection
            //using (IDbConnection db = dbFactory.OpenDbConnection())
            //{
            //    USR01 user = new USR01()
            //    {
            //        r01f01 = 101,
            //        r01f02 = null,
            //        r01f03 = "pngggg@123"
            //    };

            //    List<object> lstToUpdate = new List<object>();

            //    // get how many fields to update
            //    PropertyInfo[] props = typeof(USR01).GetProperties();
            //    foreach (PropertyInfo prop in props)
            //    {
            //        //object value = prop.GetValue(user);
            //        Type propType = prop.PropertyType;

            //        dynamic defaultValue;
            //        if (propType.IsValueType)
            //        {
            //            defaultValue = Activator.CreateInstance(propType);
            //        }
            //        else
            //        {
            //            defaultValue = null;
            //        }

            //        dynamic propValue = prop.GetValue(user, null);

            //        if(defaultValue != propValue)
            //        {
            //            lstToUpdate.Add(new );
            //        }
            //    }
            //}
            object o = new { Name = "Prajval", Id = 5, Age = 20.56 };
        }
    }
}
