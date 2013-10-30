using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Editor : BSAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (User.IsInRole("admin")) Response.Redirect("Default.aspx");
    }
}
