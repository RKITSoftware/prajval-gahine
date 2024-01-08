using System;

namespace CSharpBasicApp
{
    class Parent2
    {
        public void Foo1()
        {
            Console.WriteLine("Parent");
        }
    }
    class Child2 : Parent2
    {
        
    }
    class MyClassBinding
    {
        static void Main()
        {
            Parent2 p;
            Child2 c;

            p = new Parent2();
            p.Foo1();    // Parent

            c = new Child2();
            c.Foo1();    // Child

            p = null;
            c = null;

            // binding
            p = new Child2();
            p.Foo1();

        }
    }
}
