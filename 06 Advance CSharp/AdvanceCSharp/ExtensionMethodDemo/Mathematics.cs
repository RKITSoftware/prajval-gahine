namespace ExtensionMethodDemo
{
    /// <summary>
    /// A minimal class to perform basic mathematics operation
    /// </summary>
    public class Mathematics
    {
        /// <summary>
        /// Method to add two integer numbers
        /// </summary>
        /// <param name="x">number 1</param>
        /// <param name="y">number 2</param>
        /// <returns>addition of given number</returns>
        public int add(int x, int y)
        {
            return x + y;
        }

        /// <summary>
        /// Method to subtract two integer numbers
        /// </summary>
        /// <param name="x">number 1</param>
        /// <param name="y">number 2</param>
        /// <returns>subtraction of given number</returns>
        public int sub(int x, int y)
        {
            return x - y;
        }
    }
}
