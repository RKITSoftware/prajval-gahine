using ModelAndBLClassLib.BL;

namespace ServiceDemo
{
    public static class ServiceExtensions
    {
        public static void AddBLProduct(this IServiceCollection services)
        {
            services.AddScoped<BLProduct>();
        }
    }
}
