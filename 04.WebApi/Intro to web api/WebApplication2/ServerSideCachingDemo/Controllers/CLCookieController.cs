using ServerSideCachingDemo.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace ServerSideCachingDemo.Controllers
{
    [RoutePrefix("api/cookie")]
    public class CLCookieController : ApiController
    {

        [HttpGet]
        [Route("")]
        public HttpResponseMessage GetCookie()
        {
            // check if session-id cookie already present
            CookieHeaderValue cookieFromRq = Request.Headers.GetCookies("session-id").FirstOrDefault();

            // if present then validate cookie => if not exists then do below step
            if(cookieFromRq != null )
            {
                // check for cookie validity
                if (BLCookie.IsSessionActive(cookieFromRq["session-id"].Value))
                {
                    return Request.CreateResponse(new { message = "Cookie already present" });
                }
            }

            // if not then get fresh-session-id and add it in cookie
            BLCookie.lstActiveSession.Add(BLCookie.nextSessionId);
            CookieHeaderValue cookie = new CookieHeaderValue("session-id", (BLCookie.nextSessionId++).ToString());
            cookie.Expires = DateTime.Now.AddMinutes(20);
            cookie.Domain = Request.RequestUri.Host;
            cookie.Path = "/";
            HttpResponseMessage response = new HttpResponseMessage();
            response.Headers.AddCookies(new List<CookieHeaderValue> { cookie });
            return response;
        }
    }
}
