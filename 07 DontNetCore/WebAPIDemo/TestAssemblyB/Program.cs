using TestAssemblyA;

namespace TestAssemblyB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = Const_V_RO.Const_Val;
            Const_V_RO c_v_ro = new Const_V_RO();
            int b = c_v_ro.RO_Val;

            Console.WriteLine("Const: " + a);
            Console.WriteLine("ReadOnly: " + b);
        }
    }
}