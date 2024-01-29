using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace FirmAdvanceDemo.Utitlity
{
    public class ResponseStatusInfo
    {
        public bool IsRequestSuccessful;

        public HttpStatusCode StatusCode;

        public string Message;

        public object Data;
    }
}