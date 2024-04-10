namespace LamdaExpressionDemo
{
    /// <summary>
    /// Class of Yield Demo
    /// </summary>
    internal class YieldDemo
    {
        /// <summary>
        /// Method to perform iteration
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<int> GetOneTwoThree()
        {
            Console.WriteLine("hello");
            int x = 100;
            yield return 1;
            yield return 2;
            Console.WriteLine(x);
            yield return 3;
        }
        public static void Main()
        {
            foreach (var num in GetOneTwoThree())
            {
                Console.WriteLine(num);
            }
        }
    }
}
