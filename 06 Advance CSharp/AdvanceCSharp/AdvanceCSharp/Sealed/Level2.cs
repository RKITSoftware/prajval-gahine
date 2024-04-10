namespace AdvanceCSharp.Sealed
{
    /// <summary>
    /// Level2 class
    /// </summary>
    internal class Level2 : Level1
    {
        /// <summary>
        /// Describe method to dewscribe class
        /// </summary>
        public sealed override void Discribe()
        {
            Console.WriteLine("from level2");
        }
    }
}
