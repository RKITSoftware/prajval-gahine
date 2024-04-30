
namespace DemoNLog
{
    public class Startup : DemoNLog.Interfaces.IStartup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Contract for Startup class
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {   
            services.AddControllers();
            services.AddSwaggerGen();
            services.AddLogging(logging =>
                {
                    logging.AddConfiguration(_configuration.GetSection("Logging"));
                    logging.AddConsole();
                }
            );
        }

        /// <summary>
        /// Method to Configure Middleware pipeline
        /// </summary>
        /// <param name="app">IApplicationBuilder instance</param>
        /// <param name="env">IWebHostEnvironment instance</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if(env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action}/{id?}");
            });
        }
    }
}
