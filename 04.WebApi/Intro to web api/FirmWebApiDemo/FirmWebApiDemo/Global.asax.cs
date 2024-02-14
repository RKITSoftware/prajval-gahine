using System.Web.Http;
using FirmWebApiDemo.App_Start;

namespace FirmWebApiDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            //GlobalConfiguration.Configure(PNGConifg.Register);
        }
    }
}
