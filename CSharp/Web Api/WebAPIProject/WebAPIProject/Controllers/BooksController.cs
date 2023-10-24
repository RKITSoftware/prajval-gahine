using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleToWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [Route("{id:int:min(10)}")]
        public string GetById(int id)
        {
            return $"Book int id: {id}";
        }

        [Route("{id:regex(a(b|c))}")]
        public string GetById(string id)
        {
            return $"Book string id: {id}";
        }
    }
}
