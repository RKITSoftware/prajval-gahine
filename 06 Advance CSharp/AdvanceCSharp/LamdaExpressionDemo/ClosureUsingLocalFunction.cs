using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LamdaExpressionDemo
{
    internal class ClosureUsingLocalFunction
    {
        delegate void FUNC2();

        public static void ClosureDemo<T2>()
        {
            Console.WriteLine("INSIDE CLOSURE Demo function");
            int y = 20;
            int z = 30;

            FUNC2 f2 = Func1<int>;
            f2 += () =>
            {
                T2 x;
                Console.WriteLine("Inside LEpx");
            };
            //f2 += Func1;

            void Func1<T>()
            {
                Console.WriteLine("INSIDE Func1 function");
                Console.WriteLine("y: " + ++y);
            }

            Func1<int>();
            f2();
            Console.WriteLine("y: " + ++y);
        }


        public static void Main()
        {
            ClosureDemo<int>();
        }
    }
}
