namespace ExceptionCoreDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IWebHostBuilder builder = new WebHostBuilder();

            builder.UseKestrel()
                .UseStartup<Startup>();

            builder.Build().Run();
        }
    }
}