// static members of a parent class are not inherited by can be accessed (access modifier constraint applied)

namespace AdvanceCSharp.temp
{
    internal class Abstract1
    {
        public static int v1 = 5;
    }

    internal class C1 : Abstract1
    {
        public void ChangeV1()
        {
            v1 -= 1;
        }
    }
    internal class C2 : Abstract1
    {
        public void ShowV1()
        {
            Console.WriteLine(v1);
        }
    }
}
