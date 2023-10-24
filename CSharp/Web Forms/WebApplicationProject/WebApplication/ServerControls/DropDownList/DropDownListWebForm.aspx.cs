using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication2.Controls.DropDownList
{
    public partial class DropDownListWebForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // two ways to populate dropdownlist:
            // i) at design time using html (items property)
            // ii) dynamically at runtime
            if (!IsPostBack)
            {
                ListItem maleListItem = new ListItem("Male", "1");  // Text, Value
                ListItem femaleListItem = new ListItem("Female", "2");

                DropDownList1.Items.Add(maleListItem);
                DropDownList1.Items.Add(femaleListItem);
            }
        }
    }
}