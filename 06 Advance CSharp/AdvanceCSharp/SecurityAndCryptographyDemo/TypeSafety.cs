using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityAndCryptographyDemo
{
    public struct MyClass1
    {
        public int x1;
    }
    public class MyClass2
    {
        public int x2;
        public string y2;
    }
    internal class TypeSafety
    {

            // try to type cast object referred by MyClass1 to MyClass2
            //((MyClass2)oc1).y2 = "Hello"; // compile time error
            // this is type safety offered by c# and .net
            //List<MyClass1> lstMyClass1 = new List<MyClass1>();        public static void TypeSafeDemo(){


    }
}
