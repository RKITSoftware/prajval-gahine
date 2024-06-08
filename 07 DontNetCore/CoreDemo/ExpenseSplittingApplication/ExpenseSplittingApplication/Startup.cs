
using ExpenseSplittingApplication.BL.Common.Service;
using ExpenseSplittingApplication.Extensions;
using ExpenseSplittingApplication.SwaggerRequirements;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace ExpenseSplittingApplication
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<PostMethodRequiredParameterFilter>();
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authentication"
                };

                c.AddSecurityDefinition("basic", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[] { } }
                };

                c.AddSecurityRequirement(securityRequirement);

                // Ensure that the [Authorize] attribute is detected
                c.OperationFilter<SecurityRequirementsOperationFilter>();

            });

            services.AddControllers()
                .AddNewtonsoftJson()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            services.AddAuthentication("Basic")
                .AddScheme<AuthenticationSchemeOptions, CustomAuthenticationHandler>("Basic", null);
            services.AddAuthorization();

            string connectionString = _configuration.GetConnectionString("ConnectionString");
            services.AddApplicationServices(connectionString);
        }

        public void Configure(WebApplication app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();

            //app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();
            /*
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            */
        }
    }
}
