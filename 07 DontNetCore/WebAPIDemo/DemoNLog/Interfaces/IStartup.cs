namespace DemoNLog.Interfaces
{
    /// <summary>
    /// Contract for Startup class
    /// </summary>
    public interface IStartup
    {
        /// <summary>
        /// Method to configure services in IServiceCollection instance
        /// </summary>
        /// <param name="services">IServiceCollection instance</param>
        /// <param name="configuration">IConfiguration instance</param>
        public void ConfigureServices(IServiceCollection services);

        /// <summary>
        /// Method to Configure Middleware pipeline
        /// </summary>
        /// <param name="app">IApplicationBuilder instance</param>
        /// <param name="env">IWebHostEnvironment instance</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env);
    }
}
