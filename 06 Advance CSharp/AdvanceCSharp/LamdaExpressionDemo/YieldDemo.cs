using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamdaExpressionDemo
{
    internal class YieldDemo
    {
        public static IEnumerable<int> GetOneTwoThree()
        {
            Console.WriteLine("hello");
            //yield return 1;
            //yield return 2;
            //yield return 3;
            return Enumerable.Empty<int>();
        }
        public static void Main()
        {
            var nums = GetOneTwoThree();
        }
    }
}
