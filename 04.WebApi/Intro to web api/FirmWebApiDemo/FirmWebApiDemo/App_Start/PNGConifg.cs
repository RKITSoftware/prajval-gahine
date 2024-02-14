using FirmWebApiDemo;
using FirmWebApiDemo.App_Start;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


[assembly: PreApplicationStartMethod(typeof(PNGConifg), "Register")]
namespace FirmWebApiDemo.App_Start
{

    [Obsolete]
    public class PNGConifg
    {
        public static void Register()
        {
            System.Diagnostics.Debug.WriteLine("PNG");
        }
    }
}