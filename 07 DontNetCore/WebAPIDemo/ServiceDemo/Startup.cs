using ModelAndBLClassLib.BL;
using Swashbuckle.AspNetCore.Swagger;

namespace ServiceDemo
{
    /// <summary>
    /// Startup class for web app startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configuration
        /// </summary>
        private IConfiguration? _configuration;

        /// <summary>
        /// Default startup constructor
        /// </summary>
        public Startup()
        {
            _configuration = null;
        }

        /// <summary>
        /// Startup constructor for configuration initailization
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Method to configure app services
        /// </summary>
        /// <param name="services">IServiceCollection instance</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddBLProduct();
        }

        /// <summary>
        /// Method to configure request pipeline
        /// </summary>
        /// <param name="app">WebApplication instance</param>
        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}");
            });
        }
    }
}
