using ExtensionClassDemo;

namespace ExtensionMethodDemo
{
    public class Greet
    {
        public string Name;
        public void GreetOther()
        {
            Console.WriteLine("Good Morning from " + this.Name);
        }
    }

    public static class ExtensionClass
    {
        public static void Introduce(this Greet o1)
        {
            Console.WriteLine("My name is " + o1.Name);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Greet g1 = new Greet();
            g1.Name = "prajval";

            ExtensionClass.Introduce(g1);
            g1.Introduce();

            g1.GreetGoodNight();
        }
    }
}