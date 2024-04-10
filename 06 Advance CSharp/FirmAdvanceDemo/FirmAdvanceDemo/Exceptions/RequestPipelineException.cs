using FirmAdvanceDemo.Utitlity;
using System;

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