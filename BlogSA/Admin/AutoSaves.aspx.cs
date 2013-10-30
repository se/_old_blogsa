using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.Security;

public partial class Admin_AutoSaves : BSAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["act"]) && Request["act"].Equals("delete"))
        {
            string strPostID = Request["PostID"];
            int iPostID = 0;

            int.TryParse(strPostID, out iPostID);

            if (iPostID > 0)
            {
                BSPost bsPost = BSPost.GetPost(iPostID);
                if (bsPost != null)
                {
                    bsPost.Remove();
                }
            }

            Response.Redirect("AutoSaves.aspx");
        }
        rpAutoSaves.DataSource = BSPost.GetPosts(PostTypes.AutoSave, PostStates.Draft, 10);
        rpAutoSaves.DataBind();
    }

    protected void Page_Init(object sender, EventArgs e)
    {
        if (Session["ActiveUser"] == null)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }

        if (Page.User.IsInRole("editor"))
        {
            string strRequestPage = Request.Url.Segments[Request.Url.Segments.Length - 1];
            List<string> lstEditorPages = new List<string>();
            lstEditorPages.Add("add.aspx");
            lstEditorPages.Add("autosaves.aspx");
            lstEditorPages.Add("editor.aspx");
            lstEditorPages.Add("mediabrowser.aspx");
            lstEditorPages.Add("posts.aspx");
            lstEditorPages.Add("users.aspx");
            if (!lstEditorPages.Contains(strRequestPage.ToLower()))
            {
                Response.Redirect("Editor.aspx");
            }
        }

        if (Blogsa.Settings["language"] != null)
            Blogsa.RefreshBlogAdminLanguage(Blogsa.Settings["language"].ToString());
        else
            Response.Redirect(Request.Url.ToString());
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        btnDeleteAllAutoSave.OnClientClick = "return confirm('" + Language.Admin["AreYouSure"] + "');";
    }

    protected void btnDeleteAllAutoSave_Click(object sender, EventArgs e)
    {
        List<BSPost> posts = BSPost.GetPosts(PostTypes.AutoSave, PostStates.Draft, 10);
        foreach (BSPost post in posts)
        {
            post.Remove();
        }
        Response.Redirect("AutoSaves.aspx");
    }
}
