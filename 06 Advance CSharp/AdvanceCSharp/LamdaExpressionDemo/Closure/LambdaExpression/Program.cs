
namespace LamdaExpressionDemo.Closure.LambdaExpression
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
