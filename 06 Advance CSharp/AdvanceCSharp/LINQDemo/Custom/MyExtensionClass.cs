namespace LINQDemo.Custom
{
    internal static class MyExtensionClass
    {
        public static void MyMethod(this MyClass o1)
        {
            Console.WriteLine("This is my method definition");
        }
    }
}
