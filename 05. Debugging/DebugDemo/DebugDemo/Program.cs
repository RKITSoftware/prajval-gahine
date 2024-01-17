namespace DebugDemo
{
    internal class Program
    {
        public static void Fun()
        {
            int x = 5;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, World!");
            Console.WriteLine("Hello, World!");
            int y;
            //y = 5;
            //y = 10;
            //if (true)
            //{
            //    int x = 5;
            //}
            Fun();
            //throw new Exception();
            Console.WriteLine(Thread.CurrentThread.Name);
            // conditional breakpoint isTrue and when changes
            for(int i=0; i<4; i++)
            {
                Console.WriteLine("iNSIDE LOOP, World!");
            }
        }
    }
}