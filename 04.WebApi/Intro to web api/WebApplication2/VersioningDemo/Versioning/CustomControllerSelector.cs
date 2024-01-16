using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
    /// Class for selecting controller based on query paramter
    /// </summary>
    public class CustomControllerSelector : DefaultHttpControllerSelector
    {
        /// <summary>
        /// configuration of http app
        /// </summary>
        HttpConfiguration _config;


        /// <summary>
        /// Constructor to create an instance of CustomControllerSelector
        /// </summary>
        /// <param name="config">Http configuration object</param>
        public CustomControllerSelector(HttpConfiguration config) : base(config)
        {
            this._config = config;
        }

        /// <summary>
        /// Method to select controller based on version specified in query paramter
        /// </summary>
        /// <param name="request">Http request info</param>
        /// <returns>Description of the controller</returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {
            //get all possible api controllers
            IDictionary<string, HttpControllerDescriptor> controller = this.GetControllerMapping();

            // get route information
            IHttpRouteData routeData = request.GetRouteData();

            // now get the controller name
            string controllerName = routeData.Values["controller"].ToString();
            string apiVersion = null;

            // get query string from uri
            NameValueCollection versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);

            // check if api version in QS is null
            if (versionQueryString["version"] != null)
            {
                apiVersion = versionQueryString["version"];
            }

            // if vesion is 1 then select the EmployeeV1 api
            if(apiVersion == "1" || apiVersion == "2")
            {
                // update the controller name
                controllerName += "v" + apiVersion;
            }
            else
            {
                // return null
                return null;
            }

            // create the controller descriptor object
            HttpControllerDescriptor controllerDescriptor;
            if (controller.TryGetValue(controllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            // return null for no such controller exists
            return null;
        }
    }
}