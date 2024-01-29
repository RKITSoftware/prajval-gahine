using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicTypeDemo
{
    internal interface MyInterface1
    {
        public void MyMethod2();
    }
    internal class MyClass1 : MyInterface1
    {
        public void MyMethod1()
        {
            Console.WriteLine("My method 1");
        }
        public void MyMethod2()
        {
            Console.WriteLine("My method 2");
        }
    }
}
