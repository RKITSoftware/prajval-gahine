namespace ExpenseSplittingApplication.Models
{
    /// <summary>
    /// Represents a generic response structure for operations.
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Gets or sets a value indicating whether the operation encountered an error.
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Gets or sets the HTTP status code associated with the response.
        /// </summary>
        public int HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets a message associated with the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets additional data associated with the response.
        /// </summary>
        public object Data { get; set; }
    }
}
