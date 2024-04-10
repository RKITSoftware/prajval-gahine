
namespace LamdaExpressionDemo.Closure.LocalFunction
{
    /// <summary>
    /// Class to demostrate closure using local function
    /// </summary>
    internal class Closure
    {
        /// <summary>
        /// A delegate declaration with no args and no return
        /// </summary>
        public delegate void FuncDelegate();

        /// <summary>
        /// Method that returns a delegate instance (formed using local function) that contains a closured variable
        /// </summary>
        /// <returns>FuncDelegate instance</returns>
        public static FuncDelegate OuterFunc()
        {
            int outVar1 = 10;
            int outVar2 = 20;

            // a local function
            void LocalFunc()
            {
                Console.WriteLine(outVar1);
            }
            FuncDelegate func1 = LocalFunc;
            return func1;
        }
    }
}
