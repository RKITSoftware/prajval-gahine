namespace GenericsInCSharp
{
    class Base1
    {

    }
    class Derived1 : Base1
    {

    }
    internal class CovarianceAndContravariance
    {
        public static void SetObject(object o) { }
        public static void Main()
        {
            // assignment compatibility - is the idea that u can store a value that has a particular type into
            // storage location (variable) of a different type without loosing any data.
            // An object of a more derived type is assigned to an object of a less derived type
            string str = "s";
            object obj = str;

            unsafe
            {
                if (str == obj)
                {
                    Console.WriteLine("true");
                }
            }

            // covariance - defined that what and to what extend change is b/w two entity
            // An object which is instantiated with a more derived type argument is assigned to an object instantiated
            // with a less derived type argument
            // Assigment compatibility is preserved
            IEnumerable<Derived1> enD1 = new List<Derived1>();
            IEnumerable<Base1> enB1 = enD1;


            // contravariance
            // An object that is instantiated with a less derived type argumnet
            // is assigned to an object instantiated with a less derived type argument
            // assigment compatibility is reversed
            Action<object> actObject = SetObject;
            Action<string> actString = actObject;



            // structs
            stu1 s1 = new stu1() { x = 1, y = 2 };
            stu1 s2 = new stu1() { x = 1, y = 2 };

            // .net object
            object o = new { };
            unsafe
            {

                //if(&(*s1) == &(*s2))
                //{
                //    Console.WriteLine("true hello");
                //}
            }

        }

        public struct stu1
        {
            public int x;
            public int y;
        }
    }
}
