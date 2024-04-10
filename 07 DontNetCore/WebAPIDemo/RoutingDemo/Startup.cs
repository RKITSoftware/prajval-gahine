using Microsoft.AspNetCore.Builder;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace RoutingDemo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // add swagger middlewares 
            app.UseSwagger();
            app.UseSwaggerUI();

            // add routing middleware
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller}/{action}");
                endpoints.MapControllerRoute("product", "product/{*action}");
            });
        }
    }
}
