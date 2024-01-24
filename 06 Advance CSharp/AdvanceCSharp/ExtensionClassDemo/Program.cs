using ExtensionMethodDemo;

namespace ExtensionClassDemo
{
    public static class ExtensionClass2
    {
        public static void GreetGoodNight(this Greet g1)
        {
            Console.WriteLine("Good Night from, " + g1.Name);
        }
    }
}