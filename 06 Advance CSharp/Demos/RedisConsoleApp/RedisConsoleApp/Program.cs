using StackExchange.Redis;

namespace RedisConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase(0);


            // setting a string in a set
            //db.StringSet("name", "prajval");
            //Console.WriteLine(db.StringGet("name"));

            // setting a hash entries
            //HashEntry[] userHashValue1 = new HashEntry[]
            //{
            //    new HashEntry("uid", "108"),
            //    new HashEntry("name", "prajval"),
            //    new HashEntry("surname", "gahine"),
            //    new HashEntry("eid", "105")
            //};
            //db.HashSet("user-sid-23165", userHashValue1);

            // getting a specific hash entry
            string v = db.HashGet("user-sid-23165", "surname").ToString();

            // getting all hash entry
            HashEntry[] hashRead = db.HashGetAll("user-sid-23165");
            foreach (HashEntry he in hashRead)
            {
                Console.WriteLine(he.Name + " " + he.Value);
            }
            Console.WriteLine();

            RedisValue[] lstEmployees = db.SetMembers("employees");
            foreach(RedisValue employee in lstEmployees)
            {
                Console.WriteLine(employee);
            }

            Console.WriteLine();
        }
    }
}