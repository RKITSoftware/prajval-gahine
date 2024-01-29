using System.Web.Http;

namespace ExceptionInWebAPIProject
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //throw new Exception();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
