using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpBasicApp
{
    internal class OperatorsInCshrap
    {
        static void Main()
        {
            int x = 10, y = 20;
            int result;

            // arithmetic expresssion +, -, *, /, %
            Console.WriteLine("addtion: " + (x + y));
            Console.WriteLine("subtraction: " + (x - y));
            Console.WriteLine("multiplication: " + (x - y));
            Console.WriteLine("division: " + (x / y));
            Console.WriteLine("modulo: " + (x % y));

            // increment and decrement ++, --
            Console.WriteLine("increment: " + x++);
            Console.WriteLine("increment: " + x--);

            // comparision operator ==, >, <, >=, <=, !=

            // logical opertaor &&, ||, !

            // bitwise operator &, |, ^, <<, >>, ~

            // assignment operator +=, -=, *=, =, /=, %=, <<=, >>=, &=, ^=, |=

            // ternary operator (condition) ? first_expression : second_expression
        }
    }
}
