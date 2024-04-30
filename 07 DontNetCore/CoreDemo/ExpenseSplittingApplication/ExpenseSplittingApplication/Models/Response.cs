using System.Net;

namespace ExpenseSplittingApplication.Models
{
    public class Response
    {
        public bool IsError { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
