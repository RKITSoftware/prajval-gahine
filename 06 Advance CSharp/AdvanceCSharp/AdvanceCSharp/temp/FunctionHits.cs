namespace AdvanceCSharp.temp
{
    public static class Constants
    {
        public static int hitCount = 0;
    }
    internal class FunctionHits
    {
        public static void Main()
        {
            new ChildClass4();
            new ChildClass5();
            Console.WriteLine(Constants.hitCount);
            //Console.WriteLine(Constants.hitCount);
        }
    }
    public class BaseClass
    {
        public void TimesCalled(ref int x)
        {
            x = x * 100;
            Constants.hitCount++;
        }
    }

    public class ChildClass4 : BaseClass
    {
        public ChildClass4()
        {
            int x = 4;
            TimesCalled(ref x);
            Console.WriteLine(x);
        }
    }

    public class ChildClass5 : BaseClass
    {
        public ChildClass5()
        {
            int x = 5;
            TimesCalled(ref x);
            Console.WriteLine(x);
        }
    }
}
