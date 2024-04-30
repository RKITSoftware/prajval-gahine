﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace FirmAdvanceDemo.Models.DTO.DataAnnotations
{
    public class ValidateMonthYearAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext context)
        {
            string[] lstYearKey = context.ActionArguments.Select(kvp => kvp.Key).Where(key => key.ToLower().Contains("year")).ToArray();
            foreach (string yearKey in lstYearKey)
            {
                int year = (int)context.ActionArguments[yearKey];
                if (year < 1)
                {
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, $"{yearKey} cannot have a negative value.");
                    return;
                }
            }

            string[] lstMonthKey = context.ActionArguments.Select(kvp => kvp.Key).Where(key => key.ToLower().Contains("month")).ToArray();
            foreach(string monthKey in lstMonthKey)
            {
                int month = (int)context.ActionArguments[monthKey];
                if (month < 1 || month > 12)
                {
                    context.Response = context.Request.CreateErrorResponse(HttpStatusCode.PreconditionFailed, $"{monthKey} is not in valid range.");
                    return;
                }
            }
        }
    }
}