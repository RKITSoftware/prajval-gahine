using Microsoft.AspNetCore.Mvc;

namespace ControllerInitialization.Controllers
{
    public class DataController : Controller
    {
        public object Index()
        {
            return "Hello";
        }
    }
}
