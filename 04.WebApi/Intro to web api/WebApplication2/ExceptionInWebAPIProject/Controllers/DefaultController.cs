﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ExceptionInWebAPIProject.Controllers
{
    /// <summary>
    /// Controller class to handle request at controller default
    /// </summary>
    public class DefaultController : ApiController
    {
        /// <summary>
        /// DefaultController Action to get a sample data
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        public IHttpActionResult GetData()
        {
            return Ok("Prajval Gahine");
        }
    }
}