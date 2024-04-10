using System.Net;

namespace FirmAdvanceDemo.Utitlity
{
    /// <summary>
    /// Class whose instance respresent a response result
    /// </summary>
    public class ResponseStatusInfo
    {

        /// <summary>
        /// Specifies that this rsi instance is already populated by below layer,
        /// So, no need to populate further
        /// </summary>
        public bool IsAlreadySet { get; set; } = false;

        /// <summary>
        /// Flag to state that whether the request was successfully perform
        /// </summary>
        public bool IsRequestSuccessful { get; set; }

        /// <summary>
        /// Status code of the response of request
        /// </summary>
        public HttpStatusCode StatusCode { get; set; }

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