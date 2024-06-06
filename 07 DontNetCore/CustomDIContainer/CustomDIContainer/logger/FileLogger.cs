using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDIContainer.logger
{
    /// <summary>
    /// File Logger class to log application information to file
    /// </summary>
    internal class FileLogger : ILogger
    {
        /// <summary>
        /// file path
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public FileLogger()
        {
            _path = @"F:\prajval-gahine\prajval-gahine\07 DontNetCore\CustomDIContainer\CustomDIContainer\log\general.txt";
        }

        /// <summary>
        /// Parameterized constructor with path
        /// </summary>
        /// <param name="path">File path</param>
        public FileLogger(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Method to write a message to file
        /// </summary>
        /// <param name="message">Message</param>
        public void Write(string message)
        {
            using(StreamWriter sw = new StreamWriter(_path, true))
            {
                sw.WriteLine(message);
            }
        }
    }
}
