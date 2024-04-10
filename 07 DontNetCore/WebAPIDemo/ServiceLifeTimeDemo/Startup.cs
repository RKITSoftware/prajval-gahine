using Microsoft.AspNetCore;
using Microsoft.Extensions.Configuration;
using ModelAndBLClassLib.BL;
using ServiceLifeTimeDemo.BL.Interface;
using ServiceLifeTimeDemo.BL.Service;
using System.Reflection.Metadata;
using System.Text;

namespace ServiceLifeTimeDemo
{
    public class Startup
    {
        private IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddTransient<ITransientService, BLTransientService>();
            services.AddScoped<IScopedService, BLScopedService>();
            services.AddSingleton<ISingletonService, BLSingletonService>();
            string x = _configuration.GetValue<string>("xmlCommentFile");
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();
            //app.Use(async (context, function) =>
            //{
            //    try
            //    {
            //        await function();
            //    }
            //    catch(Exception ex)
            //    {
            //        context.Response.Clear();
            //        if (ex is BadHttpRequestException badHttpRequestException)
            //        {
            //            context.Response.StatusCode = badHttpRequestException.StatusCode;
            //            byte[] data = Encoding.UTF8.GetBytes(string.Join(Environment.NewLine, badHttpRequestException.Data));
            //            context.Response.Body.Write(data, 0, data.Length);
            //        }
            //        else
            //        {
            //            context.Response.StatusCode = 500;
            //        }
            //    }
            //});
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
