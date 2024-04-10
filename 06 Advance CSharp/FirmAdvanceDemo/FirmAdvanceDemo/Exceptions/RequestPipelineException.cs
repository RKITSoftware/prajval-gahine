using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FirmAdvanceDemo.Utitlity;

namespace FirmAdvanceDemo.Exceptions
{
    public class RequestPipelineException : Exception
    {
        public ResponseStatusInfo RSI { get; set; }

        public RequestPipelineException(ResponseStatusInfo rsi)
        {
            RSI = rsi;
        }
    }
}