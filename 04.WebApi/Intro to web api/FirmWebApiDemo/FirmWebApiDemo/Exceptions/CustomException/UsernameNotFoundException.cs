using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmWebApiDemo.Exceptions.CustomException
{
    public class UsernameNotFoundException : Exception
    {
        public UsernameNotFoundException() : base("username not found") { }

        public UsernameNotFoundException(string message) : base(message)
        {
        }

        public UsernameNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}