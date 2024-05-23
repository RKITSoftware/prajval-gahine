using Microsoft.AspNetCore.Mvc;

namespace WebAPIDemo.Controllers
{
    /// <summary>
    /// This api handles all the logic for weather forecast.
    /// </summary>
    [ApiController]
    [Route("weather")]
    public class WeatherForecastController2 : ControllerBase
    {
        /// <summary>
        /// Gets weather forecast information
        /// </summary>
        /// <param name="parameter1">This is parameter 1</param>
        /// <param name="parameter2">This is parameter 2</param>
        /// <returns>Returns list of <see cref="WeatherForecast"/>WeatherForecast</returns>
        [HttpGet]
        public string Get()
        {
            return "GOOD";
        }
    }
}