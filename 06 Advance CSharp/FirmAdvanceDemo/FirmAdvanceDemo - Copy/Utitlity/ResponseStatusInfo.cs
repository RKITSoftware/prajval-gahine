using System.Net;

namespace FirmAdvanceDemo.Utitlity
{
    /// <summary>
    /// Class whose instance respresent a response result
    /// </summary>
    public class ResponseStatusInfo
    {
        /// <summary>
        /// Flag to state that whether the request was successfully perform
        /// </summary>
        public bool IsRequestSuccessful;

        /// <summary>
        /// Status code of the response of request
        /// </summary>
        public HttpStatusCode StatusCode;

        /// <summary>
        /// Message breifing the response
        /// </summary>
        public string Message;

        /// <summary>
        /// Response data
        /// </summary>
        public object Data;
    }
}