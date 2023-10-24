using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2.Controls.ButtonControls
{
    public partial class ButtonControlCommandEventWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // there are two ways to hookup event handler to event
            // i) In design using html
            // ii) programatically using delegates.
            // attaching command event handler to command event dynamically using EventHandler delegate.
            Button1.Click += new EventHandler(Button1_Click);
            Button1.Command += new CommandEventHandler(Button_Command);

        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Write(Button1.Text + " clicked");
        }
        protected void Button_Command(object sender, CommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "create":
                    Label1.Text = "Record created";
                    break;
                case "update":
                    Label1.Text = "Record updated";
                    break;
                case "delete":
                    if (e.CommandArgument == "first")
                    {
                        Label1.Text = "First record deleted";
                    }
                    else if (e.CommandArgument == "last")
                    {
                        Label1.Text = "Last record deleted";
                    }
                    break;
                default:
                    Label1.Text = "Please press correct button";
                    break;
            }
        }
    }
}