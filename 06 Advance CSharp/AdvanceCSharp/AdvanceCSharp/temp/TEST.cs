namespace AdvanceCSharp.temp
{
    internal class TEST
    {
        public static void Main()
        {
            List<int> nums = new List<int> { 1, 2, 3, 4 };
            var num = from n in nums select 2;
        }
    }
}
