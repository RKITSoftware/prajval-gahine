using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBasicApp
{
    internal class DataType
    {
        static void Main()
        {
            decimal d = 10.1M;
            float f = 10.1F;

            short s = 10;
            int i = 10;

            s = (short)i;   // explicit type casting
            i = s;  // implicit type casting

            // exception in type casting
            //f = (float)d;
            //d = (decimal)f;

            short s1, s2, s3;
            s1 = 10; s2 = 20;
            s3 = (short)(s1 + s2);   // short, byte when performed operation are automatically type casted to int

            byte b = 100;
            int n = 256;
            checked
            {
                b = (byte)n;
            }

            //unchecked
            //{
            //    b = (byte)n;
            //}
        }
    }
}
