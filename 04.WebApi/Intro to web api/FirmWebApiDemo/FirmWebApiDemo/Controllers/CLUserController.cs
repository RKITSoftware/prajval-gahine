using FirmWebApiDemo.Authentication;
using FirmWebApiDemo.BL;
using FirmWebApiDemo.Models;
using FirmWebApiDemo.Utility;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;

namespace FirmWebApiDemo.Controllers
{
    [BasicAuthentication]
    public class CLUserController : ApiController
    {
        /// <summary>
        /// Http Get action to get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetUsers()
        {
            BLUser bLUser = new BLUser();
            List<USR01> users = bLUser.GetUsers();
            return Ok(ResponseWrapper.Wrap("List of users", users));
        }

        /// <summary>
        /// Http Post action method to create a user
        /// </summary>
        /// <param name="newUserData"></param>
        /// <returns>Object that can be referred by IHttpActionResult</returns>
        [HttpPost]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult PostUser(JObject newUserData)
        {
            BLUser bLUser = new BLUser();
            ResponseStatusInfo responseStatusInfo = bLUser.AddUser(newUserData);

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
        [BasicAuthorization(Roles = "admin")]
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
