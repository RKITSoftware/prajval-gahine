﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FirmAdvanceDemo.Utitlity
{
    public static class ResponseWrapper
    {
        public static object Wrap(string message, object data)
        {
            return new { message = message, data = data };
        }
    }
}