using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomDIContainer.logger
{
    internal class FileLogger : ILogger
    {
        private readonly string _path;
        public FileLogger()
        {
            _path = @"F:\prajval-gahine\prajval-gahine\07 DontNetCore\CustomDIContainer\CustomDIContainer\log\general.txt";
        }

        public FileLogger(string path)
        {
            _path = path;
        }

        public void Write(string message)
        {
            using(StreamWriter sw = new StreamWriter(_path, true))
            {
                sw.WriteLine(message);
            }
        }
    }
}
