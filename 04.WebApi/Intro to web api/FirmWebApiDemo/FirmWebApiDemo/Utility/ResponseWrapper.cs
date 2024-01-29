namespace FirmWebApiDemo.Utility
{
    public class ResponseWrapper
    {
        public static object Wrap(string message, object data)
        {
            return new { message = message, data = data };
        }
    }
}