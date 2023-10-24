using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2.Controls.ButtonControls
{
    public partial class ButtonControlsWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("Submit Button clicked");
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Write("Submit Image Button clicked");
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Write("Submit Link Button clicked");
        }
    }
}