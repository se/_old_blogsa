using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Contents_Comments : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (BSPost.CurrentPost != null && BSPost.CurrentPost.PostID != 0 && BSPost.CurrentPost.AddComment)
        {
            List<BSComment> comments = BSPost.CurrentPost.GetComments(CommentStates.Approved);
            if (comments.Count > 0)
            {
                rpComments.DataSource = comments;
                rpComments.DataBind();
            }
            else if (BSPost.CurrentPost.AddComment)
            {
                Literal l = new Literal();
                l.Text = String.Format("<b>{0}</b>", Language.Get["NoComment"]);
                Controls.AddAt(0, l);
            }
        }
    }

    protected void phComment_OnInit(object sender, EventArgs e)
    {
        ((PlaceHolder)sender).Controls.Add(LoadControl(Templates.Comment));
    }
}