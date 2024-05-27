using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Http;

namespace WebApplication1
{
    public class CssFilesController : ApiController
    {
        [HttpGet]
        [Route("api/cssfiles")]
        public IHttpActionResult GetCssFiles()
        {
            var contentFolder = HttpContext.Current.Server.MapPath("~/Content");
            if (!Directory.Exists(contentFolder))
            {
                return NotFound();
            }

            var cssFiles = Directory.GetFiles(contentFolder, "*.css")
                                    .Select(Path.GetFileName)
                                    .Select((word) => $"'{word}'")
                                    .Aggregate((acc, curr) =>
                                    {
                                            return acc + ", " + curr;
                                    });

            cssFiles = "[" + cssFiles + "]";
            return Ok(JArray.Parse(cssFiles));
        }
    }
}
