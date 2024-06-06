using ModelAndBLClassLib.BL;

namespace ServiceDemo
{
    /// <summary>
    /// Extension class for adding services
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Method add BLProduct class to DI container (service collection)
        /// </summary>
        /// <param name="services"></param>
        public static void AddBLProduct(this IServiceCollection services)
        {
            services.AddScoped<BLProduct>();
        }
    }
}
