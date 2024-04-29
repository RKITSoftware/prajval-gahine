using FirmAdvanceDemo.Test;
using System;
using System.Web.Http;

namespace FirmAdvanceDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //PostAMonthPunch.Run2();
            //ComputeAttendance.Run2();
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Request hit");
        }
    }
}