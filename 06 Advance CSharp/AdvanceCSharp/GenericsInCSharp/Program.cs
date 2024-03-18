namespace GenericsInCSharp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Class1<int> o1 = new Class1<int>();
            Class1<double> o2 = new Class1<double>();

            Struct1<int> s1 = new Struct1<int>();
            Struct1<double> s2 = new Struct1<double>();
        }

        // create a generic class
        public class Class1<T>
        {
            public T Value { get; set; }
        }

        public struct Struct1<T>
        {
            public T Value { get; set; }
        }
    }
}