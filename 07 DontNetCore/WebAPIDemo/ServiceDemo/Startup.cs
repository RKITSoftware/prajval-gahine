using ModelAndBLClassLib.BL;
using Swashbuckle.AspNetCore.Swagger;

namespace ServiceDemo
{
    public class Startup
    {
        private IConfiguration _configuration;
        //public Startup(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
            //services.AddScoped<BLProduct>();
            ServiceProvider sp = services.BuildServiceProvider();
            services.AddBLProduct();
            //BLProduct bl = (BLProduct)sp.GetRequiredService(typeof(BLProduct));
            //services.AddSingleton<IServiceProvider, ServiceProvider>();
        }

        public void Configure(IApplicationBuilder app, IServiceProvider sp)
        {
            BLProduct bl = (BLProduct)sp.GetRequiredService(typeof(BLProduct));

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
