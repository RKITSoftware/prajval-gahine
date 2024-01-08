using System;

namespace CSharpBasicApp
{
    internal class Genericity
    {
        static void Main()
        {
            int n = 10;
            //Console.WriteLine(n.GetType());

            var m = 10;
            //Console.WriteLine(m.GetType());

            var s = "PNG";
            //Console.WriteLine(s.GetType());

            var i = 10 / 2.0;
            Console.WriteLine(i);
            Console.WriteLine(i.GetType());

            // object - are used to temporarly store value in heap (any mathematical operation cannot be performed on object)
            object o1 = 1;
            // below line genrtaed a compile time error.
            //Console.WriteLine(o1 + 2);
            int n1 = (int)o1;
            Console.WriteLine(n1 + 2);

            // dynamic - represents an object whose opns will be resolved at runtime. Its type is not resolved at compile time
            dynamic d1, d2;
            d1 = 100;
            //d2 = 10.01;
            d1 = d1 + 1;
            //Console.WriteLine(d1.getType2()); // error at runtime
            //Console.WriteLine(n.GetType2());  // error at compile time
        }
    }
}
