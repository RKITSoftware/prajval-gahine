using System;

namespace FirmWebApiDemo.Exceptions.CustomException
{
    /// <summary>
    /// Exception class that is to be used when username is not found
    /// </summary>
    public class UsernameNotFoundException : Exception
    {
        /// <summary>
        /// UsernameNotFoundException default constructor
        /// </summary>
        public UsernameNotFoundException() : base("username not found") { }

        /// <summary>
        /// UsernameNotFoundException constructor with exception-message
        /// </summary>
        /// <param name="message">Exception message</param>
        public UsernameNotFoundException(string message) : base(message)
        {
        }

        /// <summary>
        /// UsernameNotFoundException constructor with exception-message and inner-exception
        /// </summary>
        /// <param name="message">Exception Message</param>
        /// <param name="innerException">Inner Exception</param>
        public UsernameNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}