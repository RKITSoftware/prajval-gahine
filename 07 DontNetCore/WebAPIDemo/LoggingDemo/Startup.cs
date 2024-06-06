namespace LoggingDemo
{
    /// <summary>
    /// Startup class for web app startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Method to configure app services
        /// </summary>
        /// <param name="services">IServiceCollection instance</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
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
