using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contents_DefaultTemplate : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        bool mainPage = Request.Url.ToString().ToLower().Equals(Blogsa.Url.ToLower() + "default.aspx") || Request.Url.ToString().ToLower() == Blogsa.Url.ToLower();

        Controls.Add(mainPage ? LoadControl(Templates.Default) : LoadControl(Templates.View));
    }
}