using ConsoleToWebApiApplication1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;

namespace ConsoleToWebApiApplication1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            CreateHostBuilder().Build().Run();
        }
        public static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder().ConfigureWebHostDefaults(webHost =>
            {
                webHost.UseStartup<Startup>();
            });
        }
    }
}