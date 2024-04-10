namespace DemoGenericService.BL
{
    public class BLResource<R>
    {
        public BLResource()
        {
            Console.WriteLine("Instance created for model: " + typeof(R).Name);
        }
    }
}
