using System;

namespace CSharpBasicApp
{
    internal class Enum
    {
        static void Main()
        {
            Days d;
            d = Days.Friday;
            Console.WriteLine(d.ToString().GetType() + " " + d.GetType());
        }
    }
}


enum Days : int
{
    Sunday = 0,
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
}