
namespace LamdaExpressionDemo.Closure.LambdaExpression
{
    /// <summary>
    /// Class to demostrate closure using lambda expression
    /// </summary>
    internal class Closure
    {
        /// <summary>
        /// A delegate declaration with no args and no return
        /// </summary>
        public delegate void FuncDelegate();

        /// <summary>
        /// Method that returns a delegate instance (formed using Lambda exp) that contains a closured variable
        /// </summary>
        /// <returns>FuncDelegate instance</returns>
        public static FuncDelegate OuterFunc()
        {
            int outVar1 = 10;
            int outVar2 = 20;

            // a lambda expression
            FuncDelegate func1 = () =>
            {
                Console.WriteLine(outVar1);
            };
            return func1;
        }
    }
}
