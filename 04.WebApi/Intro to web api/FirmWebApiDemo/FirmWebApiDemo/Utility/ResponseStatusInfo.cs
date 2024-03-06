using System.Net;

namespace FirmWebApiDemo.Utility
{
    /// <summary>
    /// A wrapper class to wrap up returned result from BL methodss
    /// </summary>
    public class ResponseStatusInfo
    {
        /// <summary>
        /// Flag to describe where intended logic was performed successfully or not
        /// </summary>
        public bool IsRequestSuccessful;

        /// <summary>
        /// Status code with which the operation was completed
        /// </summary>
        public HttpStatusCode StatusCode;

        /// <summary>
        /// Message from the BL method
        /// </summary>
        public string Message;

        /// <summary>
        /// Data emitted by the BL method
        /// </summary>
        public object Data;
    }
}