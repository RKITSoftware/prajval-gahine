using FilterDemo.filters;

namespace FilterDemo
{
    /// <summary>
    /// Provide a class for intializing services and middlewares used by a web app
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Register servicesn into the application
        /// </summary>
        /// <param name="services">The ServicesCollection to add the services to.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddControllers(options =>
            {
                //options.Filters.Add(new GlobalHeaderFilter("name", "prajval"));
                options.Filters.Add<CustomAuthorizationFilter>();
                options.Filters.Add<CustomResourceFilter>();
                options.Filters.Add<CustomActionFilter>();
                options.Filters.Add<CustomResultFilter>();
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configure the application
        /// </summary>
        /// <param name="app">An application</param>
        public void Configure(IApplicationBuilder app)
        {
            // Configure the HTTP request pipeline.
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
