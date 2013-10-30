using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for CommentFormBase
/// </summary>
public class CommentFormBase : UserControl
{
    protected override void OnLoad(EventArgs e)
    {
        this.Visible = BSPost.CurrentPost.AddComment;

        if (!this.Visible)
        {
            return;
        }

        try
        {
            Button btnSave = this.FindControl("btnSave") as Button;
            CheckBox cbxNotifyMe = this.FindControl("cbxNotifyMe") as CheckBox;
            Literal ltInfo = this.FindControl("ltInfo") as Literal;


            btnSave.Text = Language.Get["Send"];
            if (cbxNotifyMe != null)
                cbxNotifyMe.Text = Language.Get["NotifyMe"];

            string Message = (string)Session["WaitApprove"];
            if (Message == "OK")
            {
                if (Blogsa.Settings["add_comment_approve"].Value.Equals("0"))
                {
                    ltInfo.Text = Language.Get["CommentAdded"] + "<br/>&nbsp;";
                    Session["WaitApprove"] = null;
                }
            }
        }
        catch
        {

        }
        base.OnLoad(e);
    }

    protected override void OnInit(EventArgs e)
    {
        Button btnSave = this.FindControl("btnSave") as Button;
        if (btnSave != null)
        {
            btnSave.Click += new EventHandler(btnSave_Click);
        }
        base.OnInit(e);
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (Page.IsValid || Session["ActiveUser"] != null)
        {
            TextBox txtSecurityCode = this.FindControl("txtSecurityCode") as TextBox;
            TextBox txtEmail = this.FindControl("txtEmail") as TextBox;
            TextBox txtWebSite = this.FindControl("txtWebSite") as TextBox;
            TextBox txtName = this.FindControl("txtName") as TextBox;
            TextBox txtComment = this.FindControl("txtComment") as TextBox;
            Literal ltInfo = this.FindControl("ltInfo") as Literal;

            CheckBox cbxNotifyMe = this.FindControl("cbxNotifyMe") as CheckBox;

            if (txtSecurityCode.Text.Equals(Session["SecurityCode" + BSPost.CurrentPost.PostID]))
            {
                BSComment bsComment = new BSComment();
                bsComment.UserName = Server.HtmlEncode(txtName.Text);
                bsComment.Email = txtEmail.Text;
                bsComment.Content = BSHelper.GetEncodedHtml(txtComment.Text, true);
                bsComment.IP = HttpContext.Current.Request.UserHostAddress;

                if (cbxNotifyMe != null)
                {
                    bsComment.NotifyMe = cbxNotifyMe.Checked;
                }

                bsComment.WebPage = txtWebSite.Text;
                bsComment.Date = DateTime.Now;
                bsComment.PostID = BSPost.CurrentPost.PostID;

                bool approve;
                bool.TryParse(Blogsa.Settings["add_comment_approve"].Value, out approve);
                bsComment.Approve = approve;

                if (Blogsa.ActiveUser != null)
                {
                    bsComment.UserID = Blogsa.ActiveUser.UserID;
                    bsComment.UserName = Blogsa.ActiveUser.Name;
                    bsComment.Email = Blogsa.ActiveUser.Email;
                    bsComment.Approve = HttpContext.Current.User.IsInRole("admin");
                }

                if (bsComment.Save())
                {
                    if (Convert.ToBoolean(Blogsa.Settings["comment_sendmail"].Value) &&
                        ((Blogsa.ActiveUser != null && Blogsa.ActiveUser.UserID != bsComment.UserID)
                        || Blogsa.ActiveUser == null))
                    {
                        System.Threading.ThreadPool.QueueUserWorkItem(delegate
                        {
                            BSHelper.SendMail(Language.Get["YouHaveNewComment"],
                            Blogsa.Settings["smtp_email"].ToString(), Blogsa.Settings["smtp_name"].ToString(),
                            Blogsa.Settings["smtp_email"].ToString(), Blogsa.Settings["smtp_name"].ToString(),
                            string.Format(Language.Get["CommentMailContent"],
                            "<b>" + bsComment.UserName + "</b>", "<b><a href=\"" + BSPost.CurrentPost.Link + "\">" +
                            BSPost.CurrentPost.Title + "</a></b>", "<b>" +
                            BSPost.CurrentPost.CommentCount + "</b>"), true);
                        });
                    }

                    if (bsComment.Approve)
                        Response.Redirect(BSPost.CurrentPost.Link + "#Comment" + bsComment.CommentID);
                    else
                    {
                        Session["WaitApprove"] = "OK";
                        Response.Redirect(BSPost.CurrentPost.Link + "#WriteComment");
                    }
                }
                else
                {
                    ltInfo.Text = "Comment Save Error";
                }
            }
            else
            {
                ltInfo.Text = Language.Get["YourSecurityCodeWrong"];
            }
        }
    }
}