using System.Security.Cryptography.X509Certificates;

namespace LamdaExpressionDemo
{
    internal class Program
    {
        public delegate int Sum(int x, int y);
        public delegate void Gm();
        public void Greet()
        {
            Console.WriteLine("Good morning");
        }
        public void Hello()
        {

            Gm GoodMorning = this.Greet;
            GoodMorning();
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Sum add = (x, y) => x + y;
            //Console.WriteLine("sum of 4,5 is: " + add(4,5));
            Console.WriteLine(add.Invoke(7, 8));
            Sum add2 = (x, y) => x * y;

            Gm vgm = () =>
            {
                Console.WriteLine("Very Good Morning");
            };

            vgm();

        }
    }
}