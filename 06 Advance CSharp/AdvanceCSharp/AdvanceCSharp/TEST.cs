using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceCSharp
{
    internal class TEST
    {
        public static void Main()
        {
            List<int> nums = new List<int> { 1, 2, 3, 4 };
            var num = from n in nums select 2;
        }
    }
}
