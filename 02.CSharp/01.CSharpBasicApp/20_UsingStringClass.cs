using System;

namespace CSharpBasicApp
{
    /// <summary>
    /// MyClassString class demonstrate the string in C#
    /// </summary>
    internal class MyClassString
    {
        static void Main()
        {
            int n1 = 10;
            int n2 = 20;
            // string concatenation
            Console.WriteLine("The sum of " + n1 + " and " + n2 + " is " + (n1+n2));

            // more convinent to read => placeholder and value
            Console.WriteLine("The sum of {1} and {0} is {2}", n1, n2, n1+n2);

            // string interpolation - much cleaner than above => directly putting values in string itslf
            // string is getting interpolated => hence most efficient way => no concatination and mapping of placholder and value.
            Console.WriteLine($"The sum of {n1} and {n2} is {n1+n2}");

            string s = null;
            s = "The sum of " + n1 + " and " + n2 + " is " + (n1 + n2);

            s = string.Format("The sum of {0} and {1} is {2}", n1, n2, n1 + n2);

            s = $"The sum of {n1} and {n2} is {n1 + n2}";

            s = "";
            if(s.Length == 0)
            {
                Console.WriteLine("String is empty");
            }

            s = null;
            if (string.IsNullOrEmpty(s))
            {
                Console.WriteLine("String is either Null or Empty");
            }

            // converting a string to lower case string
            s = "PRAJVAL";
            Console.WriteLine(s.ToLower());

            // converting string to character array => though internally string is array of charcters only but we want in character array variable
            char[] chars = s.ToCharArray();

            // NOTE: anyting to string is done via ToString() method.
            // while string to number => we must parse the string.

            // NOTE: casting(implicit and/or explicit) of "any datatype to string" and "string to any datatype" is not allowed.
            // RULE: strings are parsed and not casted.
            string s1 = "200";
            int n;
            // n = (int)s;  // not allowed
            // s = (string) n;  // not allowed
            n = int.Parse(s1);  // throws a runtime exception
            bool success = int.TryParse(s1, out n);
            Console.WriteLine(success + " " + n);

            char c;
            // c = char(s1); // not allowed
            c = s[0];


            // properties and functions of string
            s = "Prajval Gahine";
            Console.WriteLine("Length of string is: " + s.Length);
            Console.WriteLine("Index of first a is: " + s.IndexOf('a'));
            Console.WriteLine("Index of first a is: " + s.LastIndexOf('a'));
            Console.WriteLine("Lower case string: " + s.ToLower());
            Console.WriteLine("Lower case string: " + s.ToUpper());
            Console.WriteLine("Does the string starts with Praj? " + s.StartsWith("Praj"));
            Console.WriteLine("Does the string ends with ine? " + s.EndsWith("ine"));
            Console.WriteLine("Does the string contains val? " + s.Contains("val"));
            Console.WriteLine("Are both string equal? " + s.Equals("Prajval Gahine"));
            Console.WriteLine("Are both string equal? " + s.Equals("prajval gahine", StringComparison.OrdinalIgnoreCase));

            string s2 = s.Replace('a', 'A');
        }
    }
}
