using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirmWebApiDemo.Authentication;
using FirmWebApiDemo.Models;
using FirmWebApiDemo.Utility;
using Newtonsoft.Json.Linq;

namespace FirmWebApiDemo.Controllers
{
    [BasicAuthentication]
    public class CLEmployeeController : ApiController
    {

        //get all employees admin
        [HttpGet]
        [BasicAuthorization(Roles = "admin")]
        public IHttpActionResult GetEmployees()
        {
            List<EMP01> employees = EMP01.GetEmployees();

            return Ok(ResponseWrapper.Wrap("List of employees", employees));
        }



        // get another employee attendance - admin

        // get my employee data

        // get my attendance
    }
}
