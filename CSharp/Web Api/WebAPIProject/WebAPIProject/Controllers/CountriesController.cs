using ConsoleToWebAPIProject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConsoleToWebAPIProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        /*
        [BindProperty]
        public string? Name { get; set; }

        [BindProperty]
        public int Population { get; set; }

        [BindProperty]
        public int Area { get; set; }
        */

        //[BindProperty]
        //public CountryModel? country { get; set; }

        [BindProperty(SupportsGet = true)]
        public CountryModel country { get; set; }


        //[HttpPost("")]
        //public string AddCountry()
        //{
        //    return $"{this.Name}, {this.Population}, {this.Area}";
        //}

        //[HttpPost("")]
        //public string AddCountry()
        //{
        //    return $"{this?.country?.Name}, {this?.country?.Population}, {this?.country?.Area}";
        //}

        [HttpGet("")]
        public string AddCountry()
        {
            return $"{this?.country?.Name}, {this?.country?.Population}, {this?.country?.Area}";
        }

        // primitive data from Query string
        [HttpGet("test")]
        public string AddCountryTest(string Name, int Population, int Area)
        {
            return $"Country adding test";
        }

        // primitive data from URL route
        [HttpGet("{name}/{population}/{area}")]
        public string AddCountryTest2(string Name, int Population, int Area)
        {
            return $"Country adding test2";
        }

        // complex data by default from body
        [HttpPost("")]
        public string AddCountry(CountryModel country, string Name, int Population)
        {
            return "complex data is binded with post request body";
        }



    }
}
