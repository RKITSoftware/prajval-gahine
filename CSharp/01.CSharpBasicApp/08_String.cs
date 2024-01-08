using System;
namespace CSharpBasicApp
{
    internal class String
    {
        static void Main(string[] args)
        {

            string s = "";
            // StringBuilder class object
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("P");
            sb.Append("N");
            sb.Append("G");
            s = sb.ToString();
            Console.WriteLine(s);
            string a = "abc";
            //string b = "A𠈓C";
            int len = a.Length;
            //Console.WriteLine(a.Length);
            //Console.WriteLine(b.Length);
        }
    }
}
