using StackExchange.Redis;

namespace RedisConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
            IDatabase db = redis.GetDatabase();

            db.StringSet("name", "prajval");
            Console.WriteLine(db.StringGet("name"));
        }
    }
}