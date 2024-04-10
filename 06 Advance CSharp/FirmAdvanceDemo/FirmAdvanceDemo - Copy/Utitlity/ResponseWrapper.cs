namespace FirmAdvanceDemo.Utitlity
{
    /// <summary>
    /// Class used to wrap up response data in uniform format
    /// </summary>
    public static class ResponseWrapper
    {
        /// <summary>
        /// Method to wrap up the response content
        /// </summary>
        /// <param name="message">Breifing about the response</param>
        /// <param name="data">Result data of the response</param>
        /// <returns></returns>
        public static object Wrap(string message, object data)
        {
            return new { message = message, data = data };
        }
    }
}