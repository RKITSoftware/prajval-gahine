
using ExpenseSplittingApplication.Common.Helper;
using ExpenseSplittingApplication.Common.Interface;
using ExpenseSplittingApplication.Extensions;
using ExpenseSplittingApplication.Models.DTO.Swagger;
using ServiceStack;
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
            });

            services.AddControllers()
                .AddNewtonsoftJson()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.WriteIndented = true;
                });

            string connectionString = _configuration.GetConnectionString("ConnectionString");
            services.AddApplicationConnections(connectionString);
            services.AddBLServices();
            services.AddDBContexts();
            services.AddSingleton<IUtility, Utility>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseDeveloperExceptionPage();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
