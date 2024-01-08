using System;

namespace CSharpBasicApp
{
    class MyClassConstructorDemo
    {
        private int _a;
        public static int staticB = 102;
        static MyClassConstructorDemo()
        {
            MyClassConstructorDemo o = new MyClassConstructorDemo();
            Console.WriteLine(staticB);
            staticB = 202;
        }

        public static int staticC = 400;

        public int a
        {
            get
            {
                return _a;
            }
            set
            {
                // some validation on a here
                _a = value;
            }
        }

        // default constructor / parameterless constructor
        public MyClassConstructorDemo()
        {
            // common to all constructor
        }

        // parameterized constructor
        public MyClassConstructorDemo(int a) : this()
        {
            this.a = a;
        }

        // copy constructor
        public MyClassConstructorDemo(MyClassConstructorDemo o)
        {
             this._a = o._a;
        }
        // not recommended to define destructor in .NET unless you have unmanaged code/resources/objects in your .NET application
        //~MyClassConstructorDemo()
        //{

        //}
    }
    internal class ConstructorAndDestructor
    {
        static void Main()
        {
            //MyClassConstructorDemo o = new MyClassConstructorDemo(10);
            //MyClassConstructorDemo o2 = new MyClassConstructorDemo(o);
            //o2.a = 100;

            MyClassConstructorDemo.staticB = 302;
            Console.WriteLine(MyClassConstructorDemo.staticB);
        }
    }
}
