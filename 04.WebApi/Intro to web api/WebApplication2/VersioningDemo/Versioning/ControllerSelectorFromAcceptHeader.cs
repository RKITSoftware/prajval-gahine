using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;

namespace VersioningDemo.Versioning
{
    /// <summary>
    /// Class for selecting controller based on accept header
    /// </summary>
    public class ControllerSelectorFromAcceptHeader : DefaultHttpControllerSelector
    {
        /// <summary>
        /// configuration of http app
        /// </summary>
        private HttpConfiguration _config;


        /// <summary>
        /// Constructor to create an instance of ControllerSelectorFromAcceptHeader
        /// </summary>
        /// <param name="config">Http configuration object</param>
        public ControllerSelectorFromAcceptHeader(HttpConfiguration config): base(config)
        {
            _config = config;
        }


        /// <summary>
        /// Method to select controller based on version specified in accept header
        /// </summary>
        /// <param name="request">Http request info</param>
        /// <returns>Description of the controller</returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            // get all controllers
            IDictionary<string, HttpControllerDescriptor> controllers = this.GetControllerMapping();

            // get route info
            IHttpRouteData routeData = request.GetRouteData();

            // get controller name from route data
            string controllerName = routeData.Values["controller"].ToString();
            string apiVersion = null;

            // get version info from accept header
            var acceptHeader  = request.Headers.Accept.Where(header => header.Parameters.Count(value => value.Name == "Employee-Version") > 0);

            if(acceptHeader.Any())
            {
                apiVersion = acceptHeader.First().Parameters.First(x => x.Name == "Employee-Version").Value;
            }

            if(apiVersion == "1" || apiVersion == "2")
            {
                controllerName += $"v{apiVersion}";
            }
            else
            {
                return null;
            }

            // search for the controller in controllers variable
            HttpControllerDescriptor controllerDescriptor;
            if(controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            return null;
        }
    }
}