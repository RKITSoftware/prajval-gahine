using System;

namespace CSharpBasicApp
{
    class Point
    {
        public int X;
        public  int Y;

        // Every class in .NET is derived class of System.Object
        // ToString is a virtual function in System.Object class => so, we can override it in the Point class

        public override string ToString()
        {
            return X + " " + Y;
        }
        public override bool Equals(Object obj)
        {
            Point p = (Point)obj;
            return this.X == p.X && this.Y == p.Y;
        }

        // If you have overriden Equals function and has implemented a diffrent functionality => then it is recommended to override the GetHashCode() function too.
        public override int GetHashCode()
        {
            int hashX = this.X * 10;
            int hashY = this.Y * 10;
            return (hashX + hashY) * 7;   
        }
    }
    internal class ObjectClass
    {
        static void Main(string[] args)
        {
            Point p1 = new Point() { X = 10, Y = 20 };
            Point p2 = new Point() { X = 10, Y = 20 };

            Console.WriteLine(p1.ToString());
            Console.WriteLine(p1.Equals(p2));
            Console.WriteLine(p1.GetHashCode() + " " + p2.GetHashCode());

            // GetType() function of System.Object is not a virtual method => hence cannot override it in child class
            Type tp1 = p1.GetType();
            Type tp2 = p2.GetType();
            Console.WriteLine(tp1 == tp2);
        }
    }
}
