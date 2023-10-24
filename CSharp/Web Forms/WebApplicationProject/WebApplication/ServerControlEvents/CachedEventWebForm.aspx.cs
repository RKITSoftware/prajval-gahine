using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2
{
    public partial class CachedEventWebForm : System.Web.UI.Page
    {
        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
            Response.Write("Text Changed<br/>");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write("Button Clicked");
        }
    }
}