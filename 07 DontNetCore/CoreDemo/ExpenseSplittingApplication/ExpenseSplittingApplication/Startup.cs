using ExpenseSplittingApplication.Extensions;
using ExpenseSplittingApplication.Middlewares;
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
using NLog.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ExpenseSplittingApplication
{
    /// <summary>
    /// Configures services and middleware for the ExpenseSplittingApplication.
    /// </summary>
    public class Startup
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor to initialize Startup with configuration settings.
        /// </summary>
        /// <param name="configuration">The configuration settings.</param>
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

            // Configure NLog logging
            LogManager.Setup().LoadConfigurationFromFile("nlog.config");
        }

        /// <summary>
        /// Configures the services used by the application.
        /// </summary>
        /// <param name="services">The service collection to configure.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            // Add HttpContextAccessor for accessing HttpContext in services
            services.AddHttpContextAccessor();

            // Configure logging with NLog
            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddNLog();
            });

            // Configure JWT authentication
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

            // Add authorization policies
            services.AddAuthorization();

            // Add controllers with authorization policy requiring authenticated users
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

            // Add Swagger generation and configuration
            services.AddSwaggerGen(c =>
            {
                // Include XML comments for Swagger documentation
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.EnableAnnotations();

                // Configure JWT bearer authentication in Swagger
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter your JWT token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
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
            });

            services.AddSwaggerGenNewtonsoftSupport();

            // Get connection string from configuration
            string connectionString = _configuration.GetConnectionString("ConnectionString");

            // Add application-specific services
            services.AddApplicationServices(connectionString);
        }

        /// <summary>
        /// Configures the middleware pipeline for the application.
        /// </summary>
        /// <param name="app">The application builder.</param>
        public void Configure(WebApplication app)
        {
            // Use developer exception page for development environment
            app.UseDeveloperExceptionPage();

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            // Serve Swagger UI and Swagger JSON
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "ESA v1");
                c.InjectStylesheet("/swagger-ui/SwaggerDark.css");
            });

            // Serve static files from wwwroot
            app.UseStaticFiles();

            // Enable routing
            app.UseRouting();

            // Enable authentication and authorization
            app.UseAuthentication();
            app.UseAuthorization();

            // Map controllers
            app.MapControllers();

            // Register NLog shutdown on application stop
            ((IApplicationBuilder)app).ApplicationServices.GetRequiredService<IHostApplicationLifetime>()
                .ApplicationStopped.Register(LogManager.Shutdown);
        }
    }
}
