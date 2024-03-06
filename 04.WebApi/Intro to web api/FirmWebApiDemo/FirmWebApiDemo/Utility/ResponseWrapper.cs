namespace FirmWebApiDemo.Utility
{
    /// <summary>
    /// Wrapper class to wrap the return value from any controller action
    /// </summary>
    public class ResponseWrapper
    {
        /// <summary>
        /// Wrapper method to wrap the return value from any controller action
        /// </summary>
        /// <param name="message">Message</param>
        /// <param name="data">Data</param>
        /// <returns></returns>
        public static object Wrap(string message, object data)
        {
            return new { message = message, data = data };
        }
    }
}