namespace ExtensionMethodDemo
{
    /// <summary>
    /// An extension class to extend functionalities of Mathematics class
    /// </summary>
    internal static class MathematicsExtension
    {
        /// <summary>
        /// Method to multiply two integer numbers
        /// </summary>
        /// <param name="x">number 1</param>
        /// <param name="y">number 2</param>
        /// <returns>multiplication of given number</returns>
        public static int mult(this Mathematics m, int x, int y)
        {
            return x * y;
        }

        /// <summary>
        /// Method to divide two integer numbers
        /// </summary>
        /// <param name="x">number 1</param>
        /// <param name="y">number 2</param>
        /// <returns>division of given number</returns>
        public static float div(this Mathematics m, int x, int y)
        {
            return x / y;
        }
    }
}
