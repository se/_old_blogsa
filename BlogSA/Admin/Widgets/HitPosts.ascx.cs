using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;

public partial class Admin_Widgets_HitPosts : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        rpPopuler5Post.DataSource = BSPost.GetPosts(PostTypes.Article, PostStates.Published, 5);
        rpPopuler5Post.DataBind();

        if (rpPopuler5Post.Items.Count == 0) ltNoData.Text = Language.Admin["NoPost"];
    }
}
