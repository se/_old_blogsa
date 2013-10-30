using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

public class CommentTemplate : UserControl
{
    protected BSComment Comment
    {
        get { return (BSComment)((RepeaterItem)base.NamingContainer).DataItem; }
    }
    public override void DataBind()
    {
        if (Page.User.IsInRole("admin"))
        {
            Comment.Content += "<div class=\"post-edit-box\">"
               + "<a href=\"" + ResolveUrl("~/Admin/Comments.aspx?CommentID=" + Comment.CommentID) + "\">" + Language.Get["Edit"] + "</a>"
               + "<a id=\"comment_delete_" + Comment.CommentID + "\" href=\"javascript:;\" onclick=\"Blogsa.DeleteComment(this," + Comment.CommentID + ");\">" + Language.Get["Delete"] + "</a>"
               + "<a id=\"comment_approve_" + Comment.CommentID + "\" href=\"javascript:;\" " + (!Comment.Approve ? "style=\"display:none;\"" : "") + " onclick=\"Blogsa.CommentApprove(this," + Comment.CommentID + ");\">" + Language.Get["Approve"] + "</a>"
               + "<a id=\"comment_unapprove_" + Comment.CommentID + "\" href=\"javascript:;\" " + (Comment.Approve ? "style=\"display:none;\"" : "") + " onclick=\"Blogsa.CommentUnApprove(this," + Comment.CommentID + ");\">" + Language.Get["UnApprove"] + "</a>"
               + "<span></span>"
               + "</div>";
        }
        CancelEventArgs eventArgs = new CancelEventArgs();
        BSComment.OnShowing(this.Comment, eventArgs);

        if (!eventArgs.Cancel)
        {
            base.DataBind();
            BSComment.OnShowed(this.Comment, EventArgs.Empty);
        }
    }
}