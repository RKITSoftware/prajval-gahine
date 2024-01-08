using System;

namespace CSharpBasicApp
{
    interface IInterface1
    {
        void Foo1();
        void Foo2();
    }
    interface IInterface2
    {
        void Foo2();
        void Foo3();
    }
    class ClassImplements : IInterface1, IInterface2
    {
        public void Foo1()
        {
            Console.WriteLine("Foo1 method in class");
        }

        // explicitly implementing Foo2() for Interface1
        void IInterface1.Foo2()
        {
            Console.WriteLine("Foo2 method in class for interface-1");
        }
        // explicitly implementing Foo2() for Interface2
        void IInterface2.Foo2()
        {
            Console.WriteLine("Foo2 method in class for interface-2");
        }

        // if all implemntations of every interfaces are satisfied then u may avoid giving implicit implementation
        // implicityly implementing Foo2() for the class
        public void Foo2()
        {
            Console.WriteLine("Foo2 method in class");
        }
        public void Foo3()
        {
            Console.WriteLine("Foo2 method in class");
        }
    }
    internal class InterfaceImplementation
    {
        static void Main(string[] args)
        {
            IInterface1 i1;
            IInterface2 i2;
            ClassImplements c;

            i1 = new ClassImplements();
            i2 = new ClassImplements();
            c = new ClassImplements();

            i1.Foo2();  // "Foo2 method in class for interface-1"
            i2.Foo2();  // "Foo2 method in class for interface-2"
            c.Foo2();   // "Foo2 method in class"

            // explicit casting
            i1 = null;
            i2 = null;

            i1 = c; // implicit casting
            // c = i1; // invalid casting
            c = (ClassImplements)i1;    // explicit casting

            // i2 = i1;    // invlaid casting
            // invalid b/c i1 may be referreing any object who's class may have not implemented IInterface2
            
            // i1 must refer to "object of class ClassImplements which has also implemented IInterface2" o/w it will throw InvalidCastException at RunTime
            i2 = (IInterface2)i1;

            // invoking Foo2() of IInterface1 implementation using class object
            ((IInterface1)c).Foo2();            
        }
    }
}
