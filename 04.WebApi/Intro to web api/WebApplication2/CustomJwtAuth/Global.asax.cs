using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace CustomJwtAuth
{
    public class MyJwt
    {
        public static bool IsJwtValid(string jwt, string secretKey)
        {
            string[] jwtArray = jwt.Split('.');
            string jwtHeader = jwtArray[0];
            string jwtPayload = jwtArray[1];
            string jwtHash = jwtArray[2];

            string payload = jwtHeader + "." + jwtPayload;

            // calculate hmac-sha-256 for the header and payload
            var hash = new HMACSHA256(Encoding.UTF8.GetBytes(secretKey));
            byte[] digest = hash.ComputeHash(Encoding.UTF8.GetBytes(payload));

            string digestBase64 = Convert.ToBase64String(digest)
                .Replace('+', '-')
                .Replace('/', '_')
                .Replace("=", ""); ;


            System.Diagnostics.Debug.WriteLine(jwtHash);
            System.Diagnostics.Debug.WriteLine(digestBase64);
            System.Diagnostics.Debug.WriteLine("");

            // if jwt is valid then check for expiry time
            if (jwtHash.Equals(digestBase64))
            {
                // if not expired then return true else false

                // get current time
                TimeSpan span = DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
                long currTotalSecond = (long)span.TotalSeconds;

                // decode jwtPayload from Base64
                string paddedJwtPayload = jwtPayload + new string('=', (4 - (jwtPayload.Length % 4)));
                byte[] encodedData = Convert.FromBase64String(paddedJwtPayload);
                string decodedData = Encoding.UTF8.GetString(encodedData);

                // deserialize the jwtPayload decoded string
                var jwtPayloadObj = JwtPayload.Deserialize(decodedData);

                // get exp (expiry) claim
                object expiryTotalSecond;
                jwtPayloadObj.TryGetValue("exp", out expiryTotalSecond);

                long exp = (long)expiryTotalSecond;

                // compare expiry and curr time
                if(exp >= currTotalSecond)
                {
                    return true;
                }
            }
            return false;
        }
    }
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_BeginRequest(Object sender, EventArgs e) //Not triggered with PUT
        {

            if(Context.Request.Url.AbsoluteUri != "http://localhost:6372/api/gettoken")
            {
                // E.g., tokenType = Bearer and tokenValue = jwt
                string tokenType = string.Empty;
                string tokenValue = string.Empty;

                HttpApplication context = (HttpApplication)sender;
                string[] headers = context.Request.Headers.ToString().Split('&');

                // get the authorization header and it's value
                for (int i = 0; i < headers.Length; i++)
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

                // if jwt not exists return unauthorized
                if (tokenType == "")
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.Redirect("http://localhost:6372/api/gettoken");
                    context.Response.End();
                }

                // check jwt validity
                string secretKey = "mykeyisthisbecausemykeyisthisbecause";
                bool isValid = MyJwt.IsJwtValid(tokenValue, secretKey);

                // if jwt is invalid (corrupted-jwt or jwt-expired) then return unauthorized
                if (!isValid)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    context.Response.End();
                }
                // else let user enjoy the service
            }
        }
        protected void Application_Start()
        {

            GlobalConfiguration.Configuration.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
