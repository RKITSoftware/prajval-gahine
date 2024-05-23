using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Controllers
{
    /// <summary>
    /// This api handles all the logic for weather forecast.
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        /// <summary>
        /// List of weather type
        /// </summary>
        private static readonly string[] Summaries = new[] {
            "Freezing",
            "Bracing",
            "Chilly",
            "Cool",
            "Mild",
            "Warm",
            "Balmy",
            "Hot",
            "Sweltering",
            "Scorching"
        };

        /// <summary>
        /// Gets weather forecast information
        /// </summary>
        /// <returns>Returns list of <see cref="WeatherForecast"/>WeatherForecast</returns>
        [HttpGet]
        [Route("WeatherDetails")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now,
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();
        }

        /// <summary>
        /// A test get method
        /// </summary>
        /// <param name="parameter1">City</param>
        /// <param name="parameter2">State</param>
        /// <returns></returns>
        [HttpGet(Name = "CityWeather")]
        public string FetchCityWeather(string city, string state)
        {
            return $"{city}, {state} has {Summaries[3]} weather.";
        }

        /// <summary>
        /// Post weather forecast
        /// </summary>
        /// <param name="forecast">WeatherForeCast object</param>
        /// <returns>Returns string response</returns>
        [HttpPost]
        public IActionResult Post(WeatherForecast forecast)
        {
            //Post business logic here
            return Ok();
        }
    }
}