using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Content_Pager : System.Web.UI.UserControl
{
    protected void gv_ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView gv = (GridView)ddlPageSelector.Parent.Parent.Parent.Parent.Parent;
        gv.PageIndex = ((DropDownList)sender).SelectedIndex;
        gv.DataBind();
    }
}
