using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Routing;

namespace JwtAuthProject
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest(Object sender, EventArgs e) //Not triggered with PUT
        {
            //your code
            // step 1: somehow access the jwt token
            string tokenType = string.Empty;
            string tokenValue = string.Empty;

            HttpApplication context = (HttpApplication) sender;
            //System.Diagnostics.Debug.WriteLine(context.Request.Headers.ToString().Split('&').FirstOrDefault(header => header.Split('=')[0].Equals("Authorization")));
            string[] headers = context.Request.Headers.ToString().Split('&');

            for (int i = 0; i< headers.Length; i++)
            {
                string[] header = headers[i].Split('=');
                string headerKey = header[0];
                string headerValue = header[1];
                if (headerKey == "Authorization")
                {
                    string[] token = headerValue.Split('+');
                    tokenType = token[0];
                    tokenValue = token[1];
                }
            }


            System.Diagnostics.Debug.WriteLine("prajval");
            System.Diagnostics.Debug.WriteLine(tokenType);
            System.Diagnostics.Debug.WriteLine(tokenValue);
            System.Diagnostics.Debug.WriteLine("gahine");

            // if jwt not exists
            if(tokenType == "")
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                context.Response.End();
            }

            // check jwt validity
            var jwtHandler = new JwtSecurityTokenHandler();


            // if not exits or invalid jwt token
            // send invalid jwt in response


            // if valid jwt token then let user enjoy the service

        }
        protected void Application_Start()
        {
            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
