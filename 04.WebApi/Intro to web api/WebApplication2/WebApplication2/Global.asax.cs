using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Routing;

namespace WebApplication2
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest(object sender, EventArgs e) //Not triggered with PUT
        {
            //your code
            //Console.WriteLine("Application_BeginRequest");
            System.Diagnostics.Debug.WriteLine("Application_BeginRequest");
            //Console.ReadLine();
        }
        protected void Application_Start()
        {
            Console.WriteLine(HttpContext.Current);
            Console.WriteLine("Application_Start");
            System.Diagnostics.Debug.WriteLine("Application_Start");
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
