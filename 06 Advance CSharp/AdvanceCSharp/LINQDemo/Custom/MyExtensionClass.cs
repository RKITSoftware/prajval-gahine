using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LINQDemo.Custom
{
    internal static class MyExtensionClass
    {
        public static void MyMethod(this MyClass o1)
        {
            Console.WriteLine("This is my method definition");
        }
    }
}
