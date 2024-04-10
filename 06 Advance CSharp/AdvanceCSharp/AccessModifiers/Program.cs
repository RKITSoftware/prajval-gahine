namespace AccessModifiers
{
    internal class B
    {
        public void Dangerous()
        {
            Console.WriteLine("Dangerous");
        }
    }

    internal interface I
    {
        public void Dangerous();
    }

    public class D : I
    {
        public void Dangerous()
        {
            Console.WriteLine("Dangerous");
        }
    }
}