using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace ExceptionCoreDemo
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            else
            {
                app.UseExceptionHandler(
                    options =>
                    {
                        options.Run(
                            async context =>
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                context.Response.ContentType = "text/html";
                                IExceptionHandlerFeature? ex = context.Features.Get<IExceptionHandlerFeature>();
                                if (ex != null)
                                {
                                    string err = $"<h1>Error: {ex.Error.Message}</h1>{ex.Error.StackTrace}";
                                    await context.Response.WriteAsync(err).ConfigureAwait(false);
                                }
                            });
                    }
                );
            }
            
            app.UseRouting();
            app.UseEndpoints(options =>
            {
                options.MapControllerRoute("default", "{controller}/{action}/{id?}");
            });
        }
    }
}
