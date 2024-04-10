
namespace LamdaExpressionDemo.Closure.LocalFunction
{
    internal class Program
    {
        public static void Main()
        {
            Closure.FuncDelegate func1 = Closure.OuterFunc();
            func1();
        }
    }
}
