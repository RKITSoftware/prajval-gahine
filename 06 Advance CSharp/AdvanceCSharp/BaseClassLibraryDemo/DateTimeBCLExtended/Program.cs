namespace BaseClassLibraryDemo.DateTimeBCLExtended
{
    /// <summary>
    /// Program class for entry point for DateTime extended demo
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// Method for entry point for DateTime extended demo
        /// </summary>
        public static void Main()
        {
            DateTime dateTime = DateTime.Now;
            //dateTime.Hour = 12;

            DateTimeExtended dateTimeEx = new DateTimeExtended(dateTime);
            dateTimeEx.Hour = 12;
            dateTimeEx.Minute = 0;
            dateTimeEx.Second = 0;
            Console.WriteLine(dateTimeEx.dateTime.ToString());
        }
    }
}
