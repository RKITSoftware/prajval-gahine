using ServerSideCachingDemo.BL;
using ServerSideCachingDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Caching;
using System.Web.Http;
using ServerSideCachingDemo.Caching;
using System.Web;
using System.Threading;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json.Linq;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ServerSideCachingDemo.Controllers
{
    [RoutePrefix("api/user")]
    public class CLUserController : ApiController
    {

        private static Cache _myCache;

        static CLUserController()
        {
            _myCache = CacheManager.CacheSSCD;
        }

        [HttpGet]
        [Route("users-from-bl")]
        public IHttpActionResult GetUserFromBL()
        {
            // check if list of users is present in the cache
            List<USR01> lstUser = (List<USR01>)_myCache.Get("lstUser");
            if (lstUser != null)
            {
                // if cache has user-list then return it in response
                return Ok(new { users = lstUser });
            }

            // else get user-list from the source
            lstUser = BLUser.GetUsers();

            TimeSpan ts = new TimeSpan(0, 0, 5);

            // and add the result into cache too
            _myCache.Insert(
                "lstUser",
                lstUser,
                null,
                DateTime.MaxValue,
                ts
             );


            //if (HttpContext.Current.Cache == System.Web.HttpRuntime.Cache)
            //{
            //    System.Diagnostics.Debug.WriteLine("Cache is same");
            //}

            // return the user-list
            return Ok(new { users = lstUser });
        }

        [HttpGet]
        [Route("users-from-file")]
        public IHttpActionResult GetUserFromFile()
        {
            List<USR01> lstUser = (List<USR01>)_myCache.Get("lstUserFromFile");

            if (lstUser != null)
            {
                return Ok(new { users = lstUser });
            }

            string filePath = HttpContext.Current.Server.MapPath(@"~/App_Data/user.json");

            using (StreamReader sr = new StreamReader(filePath))
            {
                Thread.Sleep(5000);
                lstUser = JsonConvert.DeserializeObject<List<USR01>>(sr.ReadToEnd());
            }

            CacheDependency cacheDependency = new CacheDependency(
                filePath, 
                DateTime.Now.AddSeconds(5)
            );

            _myCache.Insert(
                "lstUserFromFile",
                lstUser,
                cacheDependency,
                DateTime.Now.AddSeconds(20),
                TimeSpan.Zero
            );

            return Ok(new { users = lstUser });
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult GetUser(int id)
        {
            // access the session-id cookie
            CookieHeaderValue cookie = Request.Headers.GetCookies("session-id").FirstOrDefault();
            string sessionId = cookie["session-id"].Value;

            if (!BLCookie.IsSessionActive(sessionId)){
                return BadRequest("Session expired");
            }

            USR01 user = (USR01)_myCache.Get(sessionId.ToString());
            if(user != null)
            {
                return Ok(new { user = user});
            }

            // get user from BLUser class
            user = BLUser.GetUser(id);

            // since user is null -> u will need to cache
            _myCache.Insert(sessionId, user, null, DateTime.Now.AddMinutes(20), TimeSpan.Zero);

            return Ok(new { user = user });
        }
    }
}
