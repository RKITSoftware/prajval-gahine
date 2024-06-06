using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDIContainer.logger
{
    /// <summary>
    /// Logger inteface
    /// </summary>
    internal interface ILogger
    {
        /// <summary>
        /// Method to write to target
        /// </summary>
        /// <param name="message">Message</param>
        public void Write(string message);
    }
}
