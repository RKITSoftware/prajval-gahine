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
using NLog.Extensions.Logging;
using System;
using System.IO;
using System.Reflection;
using System.Text;

namespace ExpenseSplittingApplication
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;

            LogManager.Setup().LoadConfigurationFromFile("nlog.config");
        }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddHttpContextAccessor();

            services.AddLogging(builder =>
            {
                builder.ClearProviders();
                builder.AddNLog();
            });


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

            services.AddHttpContextAccessor();
            services.AddSwaggerGen(c =>
            {
                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.EnableAnnotations();

                // Configure JWT bearer authentication
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
