using System.Net;

namespace FirmWebApiDemo.Utility
{
    public class ResponseStatusInfo
    {
        public bool IsRequestSuccessful;

        public HttpStatusCode StatusCode;

        public string Message;

        public object Data;
    }
}