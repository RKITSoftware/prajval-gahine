namespace PocoDtoConversion
{
    /// <summary>
    /// Provide a class for intializing services and middlewares used by a web app
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Configure the application
        /// </summary>
        /// <param name="app">An application</param>
        public void Configure(IApplicationBuilder app)
        {
            // Configure the HTTP request pipeline.
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}");
            });
        }

        /// <summary>
        /// Register servicesn into the application
        /// </summary>
        /// <param name="services">The ServicesCollection to add the services to.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            services.AddMvc(options =>
            {
                options.EnableEndpointRouting = false;
            });
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
