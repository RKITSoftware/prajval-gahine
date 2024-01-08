using System;

namespace CSharpBasicApp
{
    internal class ControlStatments
    {
        static void Main()
        {
            switch (1)
            {
                case 0:
                    Console.WriteLine("case 0");
                    break;
                case 1:
                case 2:
                    Console.WriteLine("case 2");
                    break;
                default:
                    Console.WriteLine("defualt case");
                    break;
            }

            int n = 10;
            while (true)
            {
                n--;
                if(n == 0)
                {
                    break;
                }
                Console.WriteLine(n);
            }
        }
    }
}
