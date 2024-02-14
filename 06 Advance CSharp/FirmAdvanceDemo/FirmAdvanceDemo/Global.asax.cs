using System;
using System.Web.Http;

namespace FirmAdvanceDemo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Request hit");
        }
    }
}


//port - 53809
// 7776000
// an expired encypted refresh token - f2IZ6FjP%2bLApf3yaHlTgxGNk7w2x6L%2bu45%2bXfX16hhFf2DqgL9LPY5A375jefsz41sp7bJQGE8eGXlPCgf0h30eRt735mpeKICrPWu9h3ghzsvdwsXh9sbAq9ArW%2bDky%2fL07XrbtBlPCet3A6PSV%2btawQWuFY35Chn%2bkfMkEq3I%3d 
// an expired access token - eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6IjEiLCJleHBpcmVzIjoiMTcwNzExNjU4OSJ9.6GpG2ttPt2lRYfEVr5cS8rS9mCi9J1U5Y9xedgV8Y_8