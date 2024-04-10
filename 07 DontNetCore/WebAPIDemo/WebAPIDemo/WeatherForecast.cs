namespace WebAPIDemo
{
    /// <summary>
    /// Weather forecast object
    /// </summary>
    public class WeatherForecast
    {
        /// <summary>
        /// Date of the weather recorded
        /// </summary>
        public DateOnly Date
        {
            get;
            set;
        }
        /// <summary>
        /// Temperature in celcius
        /// </summary>
        public int TemperatureC
        {
            get;
            set;
        }
        /// <summary>
        /// Temperature in farenheit
        /// </summary>
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        /// <summary example="parjaval">
        /// Summary of the weather forecast
        /// </summary>
        /// <example>Prajval</example>
        public string? Summary
        {
            get;
            set;
        }
    }
}