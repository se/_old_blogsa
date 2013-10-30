using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Web.Security;

public partial class Admin_MediaBrowser : BSAdminPage
{
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
        try
        {
            int sFilter = 1;

            string s = Request["s"];

            int.TryParse(s, out sFilter);

            FileTypes fileType = (FileTypes)(short)sFilter;

            rpFiles.DataSource = BSPost.GetPosts(PostTypes.File, PostVisibleTypes.All, fileType, 0);
            rpFiles.DataBind();

            if (rpFiles.Items.Count == 0)
                ltNoFile.Text = "<span style=\"padding:5px;margin:5px;text-align:center;display:block;border:1px solid #e7e7e7;background:#f4f4f4;font-weight:bold\">"
                    + Language.Admin["NoMedia"] + "</span>";
        }
        catch (System.Exception ex)
        {
            ltNoFile.Text = ex.Message;
        }
    }

    public string GetThumbnail(short sLibraryType, string strName, string strExtension)
    {
        string strThumb = string.Empty;
        if (sLibraryType == 0)
        {
            if (strExtension.Equals("doc") || strExtension.Equals("docx") || strExtension.Equals("pdf") || strExtension.Equals("xls") ||
            strExtension.Equals("xlsx") || strExtension.Equals("txt"))
                strThumb = "Images/FileTypes/document.png";
            else if (strExtension.Equals("zip") || strExtension.Equals("rar") || strExtension.Equals("tar") || strExtension.Equals("7z"))
                strThumb = "Images/FileTypes/zip.png";
            else
                strThumb = "Images/FileTypes/file.png";
        }
        else if (sLibraryType == 2)
            strThumb = "Images/FileTypes/media.png";
        else if (sLibraryType == 1)
        {
            if (Convert.ToBoolean(Blogsa.Settings["library_usethumbnail"].Value))
                strThumb = "../Upload/Images/_t/" + strName;
            else
                strThumb = "../Upload/Images/" + strName;
        }
        return strThumb;
    }
}
