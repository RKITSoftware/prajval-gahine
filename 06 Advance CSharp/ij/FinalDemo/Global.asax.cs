using FinalDemo.Connection;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Web.Http;

namespace FinalDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            try
            {
                string jsonFilePath = Server.MapPath("~/App_Data/databaseconfig.json");
                string jsonContent = File.ReadAllText(jsonFilePath);

                DatabaseConfiguration config = JsonConvert.DeserializeObject<DatabaseConfiguration>(jsonContent);

                string connectionString = $"server={config.Server};port={config.Port};database={config.Database};user={config.User};password={config.Password}";

                // Modify connection string in the runtime configuration
                System.Configuration.Configuration configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
                configuration.ConnectionStrings.ConnectionStrings["ConnectionString"].ConnectionString = connectionString;
                configuration.Save();

                // Continue with Web API initialization
            }
            catch (Exception ex)
            {
                // Handle exception (log or throw, depending on your needs)
                throw new Exception("Error configuring the database connection.", ex);
            }
        }
    }
}
