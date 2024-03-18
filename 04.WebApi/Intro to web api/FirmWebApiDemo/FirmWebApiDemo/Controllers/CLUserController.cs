using FirmWebApiDemo.Authentication;
using FirmWebApiDemo.BL;
using FirmWebApiDemo.Models;
using FirmWebApiDemo.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;

namespace FirmWebApiDemo.Controllers
{
    /// <summary>
    /// User controller class
    /// </summary>
    [RoutePrefix("api/user")]
    public class CLUserController : ApiController
    {
        //static CLUserController()
        //{
        //    var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
        //    var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

        //    var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
        //    var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();

        //    toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));
        //}

        /// <summary>
        /// Http Get action to get all users
        /// </summary>
        /// <returns>IHttpActionResult instance</returns>
        [HttpGet]
        [BearerAuthentication]
        [BasicAuthorization(Roles = "admin")]
        [Route("v1")]
        public IHttpActionResult GetUsersV1()
        {
            BLUser bLUser = new BLUser();
            List<USR01> lstUser = bLUser.GetUsers();
            return Ok(ResponseWrapper.Wrap("List of users", lstUser));
        }

        /// <summary>
        /// Http Get action to get all users without their password
        /// </summary>
        /// <returns>IHttpActionResult instance</returns>
        [HttpGet]
        [BearerAuthentication]
        [BasicAuthorization(Roles = "admin")]
        [Route("v2")]
        public IHttpActionResult GetUsersV2()
        {
            BLUser bLUser = new BLUser();
            List<USR01> lstUser = bLUser.GetUsers();
            object lstUserV2 = lstUser.Select(u => new
            {
                r01f01 = u.r01f01,
                r01f02 = u.r01f02,
                r01f04 = u.r01f04
            }
            );
            return Ok(ResponseWrapper.Wrap("List of users", lstUserV2)); ;
        }

        /// <summary>
        /// Http Post action method to create a user
        /// </summary>
        /// <param name="UserEmployee"></param>
        /// <returns>Object that can be referred by IHttpActionResult</returns>
        [HttpPost]
        [BearerAuthentication]
        [BasicAuthorization(Roles = "admin")]
        [Route("")]
        public IHttpActionResult PostUser(USREMP UserEmployee)
        {
            BLUser bLUser = new BLUser();
            ResponseStatusInfo responseStatusInfo = bLUser.AddUser(UserEmployee);

            if (responseStatusInfo.IsRequestSuccessful == false)
            {
                return ResponseMessage(Request.CreateErrorResponse(responseStatusInfo.StatusCode, responseStatusInfo.Message));

            }
            return Ok(ResponseWrapper.Wrap(responseStatusInfo.Message, responseStatusInfo.Data));
        }

        /// <summary>
        /// Http Delete action method to delete a user
        /// </summary>
        /// <param name="id">UserId</param>
        /// <returns>Object that can be referred by IHttpActionResult</returns>
        [HttpDelete]
        [BearerAuthentication]
        [BasicAuthorization(Roles = "admin")]
        [Route("")]
        public IHttpActionResult DeleteUser(int id)
        {
            BLUser bLUser = new BLUser();
            ResponseStatusInfo responseStatusInfo = bLUser.RemoveUser(id);

            if (responseStatusInfo.IsRequestSuccessful == false)
            {
                return ResponseMessage(Request.CreateErrorResponse(responseStatusInfo.StatusCode, responseStatusInfo.Message));

            }
            return Ok(ResponseWrapper.Wrap(responseStatusInfo.Message, responseStatusInfo.Data));
        }
    }
}
