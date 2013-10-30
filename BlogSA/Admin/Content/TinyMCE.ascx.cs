using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Content_TinyMCE : System.Web.UI.UserControl
{
    public string Content
    {
        set { txtContent.Text = value; }
        get { return txtContent.Text; }
    }
}
