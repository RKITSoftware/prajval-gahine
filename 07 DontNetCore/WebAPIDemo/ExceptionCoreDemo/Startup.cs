using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace ExceptionCoreDemo
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
            services.AddControllers();
            services.AddSwaggerGen();
        }

        /// <summary>
        /// Configure the application
        /// </summary>
        /// <param name="app">An application</param>
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
                //app.UseDeveloperExceptionPage();
                app.UseExceptionHandler(
                    configure =>
                    {
                        configure.Run(
                            async context =>
                            {
                                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                                context.Response.ContentType = "text/html";
                                IExceptionHandlerFeature? ex = context.Features.Get<IExceptionHandlerFeature>();
                                if (ex != null)
                                {
                                    string err = $"<h1>Error: {ex.Error.Message}</h1>";
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
