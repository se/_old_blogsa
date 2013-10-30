using System;
using System.Collections;
using System.Threading;
using System.ComponentModel;

public partial class Admin_Add : BSAdminPage
{
    private ObjectTypes ObjectType
    {
        get
        {
            string p = Request.QueryString["p"];

            if (User.IsInRole("editor"))
                p = "Post";

            switch (p)
            {
                case "Post":
                    return ObjectTypes.Article;
                case "Page":
                    return ObjectTypes.Page;
                case "Link":
                    return ObjectTypes.Link;
                default:
                    return ObjectTypes.Article;
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetControlsLanguage();
            HideAll();

            switch (ObjectType)
            {
                case ObjectTypes.Article:
                    divAddPost.Visible = true;
                    bucPostSettings.Visible = true;
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "autosavemode", "autosavemode=true;", true);
                    break;
                case ObjectTypes.Page:
                    divAddPage.Visible = true;
                    bucPostSettings.Visible = true;
                    break;
                case ObjectTypes.Link:
                    divAddLink.Visible = true;
                    bucPostSettings.Visible = true;
                    break;
                default:
                    divAddPost.Visible = true;
                    bucPostSettings.Visible = true;

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "autosavemode", "autosavemode=true;", true);
                    break;
            }
        }
    }
    private void GetControlsLanguage()
    {
        rblLinkTarget.Items[0].Text = Language.Admin["Standard"];
    }

    private void HideAll()
    {
        divAddPost.Visible = false;
        divAddLink.Visible = false;
        divAddPage.Visible = false;

        bucPostSettings.Visible = false;
    }

    private void SavePost()
    {
        try
        {
            BSPost bsPost = new BSPost();
            bsPost.UserID = Blogsa.ActiveUser.UserID;
            bsPost.Title = txtTitle.Text;
            bsPost.Code = BSHelper.CreateCode(txtTitle.Text);
            bsPost.Content = tmcePostContent.Content;
            bsPost.State = bucPostSettings.State;
            bsPost.AddComment = bucPostSettings.AddComment;
            bsPost.LanguageCode = bucPostSettings.LanguageCode;

            bsPost.Date = bucPostSettings.Date;

            if (bsPost.Save())
            {
                bucPostSettings.Save(bsPost.PostID);

                Response.Redirect("Posts.aspx?PostID=" + bsPost.PostID + "&Message=1");
            }
            else
            {
                MessageBox1.Message = "Error";
                MessageBox1.Type = MessageBox.ShowType.Error;
            }
        }
        catch (Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }

    private void SaveLink()
    {
        try
        {
            BSLink link = new BSLink();
            link.Name = txtLinkTitle.Text;
            link.Description = txtLinkDescription.Text;
            link.Url = txtLinkURL.Text;
            link.Target = rblLinkTarget.SelectedValue;

            if (link.Save())
            {
                //Categories2.TermType = TermTypes.LinkCategory;
                //Categories2.SaveData(link.LinkID);
                Response.Redirect("Links.aspx?LinkID=" + link.LinkID + "&Message=1");
            }
            else
            {
                MessageBox1.Message = "Error";
                MessageBox1.Type = MessageBox.ShowType.Error;
            }
        }
        catch (Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }

    private void SavePage()
    {
        try
        {
            BSPost bsPost = new BSPost();

            bsPost.Title = txtPageTitle.Text;
            bsPost.Code = BSHelper.CreateCode(txtPageTitle.Text);
            bsPost.Content = tmcePageContent.Content;
            bsPost.State = bucPostSettings.State;
            bsPost.AddComment = bucPostSettings.AddComment;
            bsPost.UpdateDate = DateTime.Now;
            bsPost.UserID = Blogsa.ActiveUser.UserID;
            bsPost.Type = PostTypes.Page;

            if (bsPost.Save())
            {
                Response.Redirect("Pages.aspx?PostID=" + bsPost.PostID + "&Message=1");
            }
            else
            {
                MessageBox1.Message = "Error";
                MessageBox1.Type = MessageBox.ShowType.Error;
            }
        }
        catch (Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        switch (ObjectType)
        {
            case ObjectTypes.Article:
                SavePost();
                break;
            case ObjectTypes.Page:
                SavePage();
                break;
            case ObjectTypes.Link:
                SaveLink();
                break;
            default:
                SavePost();
                break;
        }
    }
}
