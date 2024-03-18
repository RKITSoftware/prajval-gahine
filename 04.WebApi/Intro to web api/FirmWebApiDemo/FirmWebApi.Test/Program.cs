using FirmWebApiDemo;
using FirmWebApiDemo.Controllers;
using System.Reflection;
using System.Web.Http;

namespace FirmWebApi.Test
{
    internal class Program
    {
        static void Main(string[] args)
        {
            WebApiConfig.Register(new HttpConfiguration());

            CLUserController userController = new CLUserController();
            IHttpActionResult r1 = userController.GetUsersV1();

        }
    }
}