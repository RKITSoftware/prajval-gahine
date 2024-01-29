
using System.Collections;

namespace DynamicTypeDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            object intNum1 = 2;
            //Console.WriteLine(intNum1 + 5);

            dynamic intNum2 = 2;
            Console.WriteLine(intNum2 + 5);

            int[] arr = new int[20];
            Console.WriteLine("Array count using ICollection: {0}", ((ICollection)arr).Count);
            //Console.WriteLine("Array count using ICollection: {0}", arr.Count);
            Console.WriteLine("Array count using IEnumerble: {0}", arr.Count());
            //PrintCount(arr);

            // Test class
            dynamic c1 = new MyClass1();
            c1.MyMethod2();
        }

        static void PrintCount(ICollection collection)
        {
            dynamic d = collection;
            Console.WriteLine("Static typing: {0}", collection.Count);
            Console.WriteLine("Dynamic typing: {0}", d.Count());
        }
    }
}