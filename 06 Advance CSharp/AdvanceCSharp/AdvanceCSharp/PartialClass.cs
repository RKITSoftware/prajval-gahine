using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceCSharp
{
    internal partial class PartialClass
    {
        public void Greet()
        {
            Console.WriteLine("Good Morning 1");
        }
    }

    internal partial class PartialClass
    {
        public void Greet2()
        {
            Console.WriteLine("Good Morning 2");
        }
    }

    internal class C3 : PartialClass
    {
        public C3()
        {
            this.Greet();
        }
    }

    public static class StaticClass
    {
        public static int x;
    }
    public static class MyClass : Object { }

}
