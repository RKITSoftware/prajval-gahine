﻿using System.Net;

namespace ControllerInitialization
{
    /// <summary>
    /// Response class
    /// </summary>
    public class Response
    {
        #region Public Properties

        /// <summary>
        /// Gets or sets a value indicating whether the response indicates an error.
        /// </summary>
        public bool IsError { get; set; } = false;

        /// <summary>
        /// Gets or sets the HTTP status code associated with the response.
        /// </summary>
        public HttpStatusCode HttpStatusCode { get; set; }

        /// <summary>
        /// Gets or sets a descriptive message associated with the response.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets additional data carried by the response.
        /// </summary>
        public object Data { get; set; }

        #endregion
    }
}