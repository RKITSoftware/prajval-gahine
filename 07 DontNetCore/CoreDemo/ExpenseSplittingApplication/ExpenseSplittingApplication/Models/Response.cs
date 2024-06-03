namespace ExpenseSplittingApplication.Models
{
    public class Response
    {
        public bool IsError { get; set; }

        public int HttpStatusCode { get; set; }

        public string Message { get; set; }

        public object Data { get; set; }
    }
}
