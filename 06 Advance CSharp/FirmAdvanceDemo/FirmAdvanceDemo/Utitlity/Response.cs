using System.Net;

namespace FirmAdvanceDemo.Utitlity
{
    /// <summary>
    /// Class whose instance respresent a response result
    /// </summary>
    public class Response
    {
        /// <summary>
        /// Flag to state that whether the request was successfully perform
        /// </summary>
        public bool IsError { get; set; }

        /// <summary>
        /// Status code of the response of request
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// Message breifing the response
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Response data
        /// </summary>
        public object Data { get; set; }
    }
}