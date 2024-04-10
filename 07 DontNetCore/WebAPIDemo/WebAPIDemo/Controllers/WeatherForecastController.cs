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
        private readonly ILogger<WeatherForecastController> _logger;
        /// <summary>
        ///
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Gets weather forecast information
        /// </summary>
        /// <param name="parameter1">This is parameter 1</param>
        /// <param name="parameter2">This is parameter 2</param>
        /// <returns>Returns list of <see cref="WeatherForecast"/>WeatherForecast</returns>
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get(string parameter1, string parameter2)
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            }).ToArray();
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