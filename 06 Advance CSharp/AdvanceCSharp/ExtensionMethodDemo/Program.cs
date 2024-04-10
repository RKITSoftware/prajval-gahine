namespace ExtensionMethodDemo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Mathematics m1 = new Mathematics();

            Console.WriteLine("addition: " + m1.add(20, 10));
            Console.WriteLine("subtraction: " + m1.sub(20, 10));
            Console.WriteLine("multiplication: " + m1.mult(20, 10));
            Console.WriteLine("division: " + m1.div(20, 10));
        }
    }
}