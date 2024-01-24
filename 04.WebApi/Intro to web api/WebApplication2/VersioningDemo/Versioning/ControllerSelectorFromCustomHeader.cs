﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Controllers;
using System.Net.Http;
using System.Web.Http.Routing;

namespace VersioningDemo.Versioning
{
    /// <summary>
    /// Class for selecting controller based on custom header
    /// </summary>
    public class ControllerSelectorFromCustomHeader : DefaultHttpControllerSelector
    {
        /// <summary>
        /// configuration of http app
        /// </summary>
        private HttpConfiguration _config;

        /// <summary>
        /// Constructor to create an instance of ControllerSelectorFromCustomHeader
        /// </summary>
        /// <param name="config">Http configuration object</param>
        public ControllerSelectorFromCustomHeader(HttpConfiguration config) : base(config)
        {
            _config = config;
        }

        /// <summary>
        /// Method to select controller based on version specified in Custom header
        /// </summary>
        /// <param name="request">Http request info</param>
        /// <returns>Description of the controller</returns>
        public override HttpControllerDescriptor SelectController(HttpRequestMessage request)
        {

            // get all possible controllers
            IDictionary<string, HttpControllerDescriptor> controllers = this.GetControllerMapping();

            // get the request route
            IHttpRouteData routeData = request.GetRouteData();

            // get the controller name from route data
            string controllerName = routeData.Values["controller"].ToString();
            string apiVersion = null;

            string versionCustomHeader = "Employee-Version";
            // get version info from the custom header
            // check if the header is present in the request headers
            if (request.Headers.Contains(versionCustomHeader))
            {
                apiVersion = (string)request.Headers.GetValues(versionCustomHeader).FirstOrDefault().Split(',')[0];
            }

            if(apiVersion == "1" || apiVersion == "2")
            {
                // change the controller name based on the version value
                controllerName += "v" + apiVersion;
            }
            else
            {
                // no such apiversion exists
                return null;
            }

            // create http controller object
            HttpControllerDescriptor controllerDescriptor;

            if(controllers.TryGetValue(controllerName, out controllerDescriptor))
            {
                return controllerDescriptor;
            }
            // no such controller exists
            return null;
        }
    }
}