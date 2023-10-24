namespace ConsoleToWebAPIProject
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            // configuring service of controllers in our web api project
            // i.e injecting controller service
            services.AddControllers();

            // since adding a middleware file is like adding a service => therefore injecting that service here first before it's use
            services.AddTransient<CustomeMiddleware>();

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {


            // adding middleware to show error page when an exception occurs (in development mode)
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            // demonstrating use method
            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello from use-1 1 \n");
            //    await next();
            //    await context.Response.WriteAsync("Hello from use-1 2 \n");
            //});

            // converting above code into a seperate Middleware File (or say service)
            //app.UseMiddleware<CustomeMiddleware>();

            // demonstrating map method - it is used to run specific code for matched url
            //app.Map("/message", CustomCode);

            //app.Use(async (context, next) =>
            //{
            //    await context.Response.WriteAsync("Hello from use-2 1 \n");
            //    await next();
            //    await context.Response.WriteAsync("Hello from use-2 2 \n");
            //});

            // demonstrating run method
            //app.Run(async context =>
            //{
            //    await context.Response.WriteAsync("Hello from run method of middleware \n");
            //});

            // enabling routing functionality to the web application
            app.UseRouting();

            // mapping url to resources
            app.UseEndpoints(endpoints =>
            {
                /*
                // mapping url to resources using endpoints object
                endpoints.MapGet("/message", async context =>
                {
                    await context.Response.WriteAsync("Hello for get");
                });

                endpoints.MapPost("/message", async context =>
                {
                    await context.Response.WriteAsync("Hello for post");
                });
                */

                // mapping routes for the controllers
                endpoints.MapControllers();
            });
        }
        public void CustomCode(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Hello from map /message \n");
            });
        }
    }
}
