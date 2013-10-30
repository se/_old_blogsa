using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Admin_Widgets_RecentComments : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        rpLastComments.DataSource = BSComment.GetComments(CommentStates.All, 5);
        rpLastComments.DataBind();
        if (rpLastComments.Items.Count == 0)
        {
            ltNoData.Text = Language.Admin["CommentNotFound"];
        }
    }
}
