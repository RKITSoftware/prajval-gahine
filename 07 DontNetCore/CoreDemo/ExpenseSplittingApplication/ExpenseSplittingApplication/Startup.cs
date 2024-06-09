using ExpenseSplittingApplication.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using NLog;
using System.Text;

namespace ExpenseSplittingApplication
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

            // initialize NLog
            // LogManager.LoadConfiguration("nlog.config");
            LogManager.Setup().LoadConfigurationFromFile("nlog.config");
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(options =>
            {
                AuthorizationPolicy policy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();

                options.Filters.Add(new AuthorizeFilter(policy));
            })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                })
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            //services.AddLogging(options =>
            //{
            //    options.ClearProviders();
            //});

            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                /*
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                */

                c.EnableAnnotations();

                //c.OperationFilter<PostMethodRequiredParameterFilter>();

                // Configure JWT bearer authentication
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter your JWT token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer", // must be lower case
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securityScheme, new string[] { } }
                });

                /*
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
                */

            });

            services.AddSwaggerGenNewtonsoftSupport();

            byte[] jwtSigningKey = Encoding.UTF8.GetBytes(_configuration["Jwt:SigningKey"]);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "esa.com",
                    ValidAudience = "esa.com",
                    IssuerSigningKey = new SymmetricSecurityKey(jwtSigningKey),
                };
            });

            services.AddAuthorization();
            /*
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Swagger", policy =>
                {
                    policy.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme);
                    policy.RequireAuthenticatedUser();
                });
            });
            */

            string connectionString = _configuration.GetConnectionString("ConnectionString");
            services.AddApplicationServices(connectionString);
        }

        public void Configure(WebApplication app)
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ESA v1");
                c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
            });

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IHostApplicationLifetime>()
                .ApplicationStopped.Register(LogManager.Shutdown);
        }
    }
}
