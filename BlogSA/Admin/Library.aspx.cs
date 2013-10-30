using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Posts : BSAdminPage
{
    protected void Page_PreRender(object sender, EventArgs e)
    {

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        btnDelete.OnClientClick = "return confirm('" + Language.Admin["CategoryDeleteConfirm"] + "');";
        HideAll();

        int iFileID = 0;
        int.TryParse(Request["FileID"], out iFileID);

        if (!string.IsNullOrEmpty(Request["p"]) && Request["p"].Equals("new"))
            divAddMedia.Visible = true;
        else if (iFileID != 0)
        {
            divEditMedia.Visible = true;
            divEditMediaSide.Visible = true;

            divEditMedia.Visible = true;
            divEditMediaSide.Visible = true;
            if (!IsPostBack)
            {
                BSPost bsPost = BSPost.GetPost(iFileID);
                if (bsPost != null)
                {
                    tmceContent.Content = bsPost.Content;
                    txtTitle.Text = bsPost.Title;
                    cblAddComment.Checked = bsPost.AddComment;
                    ddState.SelectedValue = bsPost.State.ToString();
                    Categories1.TermType = TermTypes.Category;
                    Categories1.LoadData(bsPost.PostID);
                    Tags1.LoadTags(bsPost.PostID);
                    ddState.Items[0].Text = Language.Admin["HiddenFile"];
                    ddState.Items[1].Text = Language.Admin["PublicFile"];
                    cblAddComment.Text = Language.Admin["CommentAdd"];

                    string strFolder = bsPost.Show == PostVisibleTypes.Hidden ? "Files/" : bsPost.Show == PostVisibleTypes.Public ? "Images/" : "Files/";

                    ltOpenAddress.Text = "Upload/" + strFolder + bsPost.Title;
                    ltShowAddress.Text = "?FileID=" + bsPost.PostID;
                    ltDownloadAdress.Text = "FileHandler.ashx?FileID=" + bsPost.PostID;
                }
                else
                    Response.Redirect("Library.aspx");
            }
        }
        else
        {
            if (!IsPostBack)
                gvPosts.DataBind();
            divLibrary.Visible = true;
        }
    }
    private void GetControlsLanguage()
    {

    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        string Command = "";
        for (int i = 0; i < gvPosts.Rows.Count; i++)
        {
            CheckBox cb = gvPosts.Rows[i].FindControl("cb") as CheckBox;
            if (cb.Checked)
            {
                Literal literal = gvPosts.Rows[i].FindControl("PostID") as Literal;
                if (literal != null)
                {
                    string PostID = literal.Text;
                    BSPost bsPost = BSPost.GetPost(Convert.ToInt32(PostID));
                    string filePath = string.Empty;
                    string filePathWeb = string.Empty;
                    string filePathThumbnail = string.Empty;
                    if (bsPost.Show == 0)
                        filePath = "~/Upload/Files/" + bsPost.Title;
                    else if (bsPost.Show == PostVisibleTypes.Public)
                    {
                        filePath = "~/Upload/Images/" + bsPost.Title;
                        filePathWeb = "~/Upload/Images/_w/" + bsPost.Title;
                        filePathThumbnail = "~/Upload/Images/_t/" + bsPost.Title;
                    }
                    else if (bsPost.Show == PostVisibleTypes.Custom)
                        filePath = "~/Upload/Medias/" + bsPost.Title;
                    filePath = Server.MapPath(filePath);
                    filePathWeb = Server.MapPath(filePathWeb);
                    filePathThumbnail = Server.MapPath(filePathThumbnail);
                    try
                    {
                        if (System.IO.File.Exists(filePath))
                            System.IO.File.Delete(filePath);
                        if (System.IO.File.Exists(filePathWeb))
                            System.IO.File.Delete(filePathWeb);
                        if (System.IO.File.Exists(filePathThumbnail))
                            System.IO.File.Delete(filePathThumbnail);
                    }
                    catch
                    { }

                    if (bsPost.Remove())
                        Command = "OK";
                }
            }
        }
        if (Command != "")
        {
            MessageBox1.Message = Language.Admin["MediaDeleted"];
            MessageBox1.Type = MessageBox.ShowType.Information;

            gvPosts.DataBind();
        }
    }
    protected void btnSavePost_Click(object sender, EventArgs e)
    {
        try
        {
            int iFileID = 0;
            int.TryParse(Request["FileID"], out iFileID);

            BSPost bsPost = BSPost.GetPost(iFileID);

            bsPost.Content = tmceContent.Content;
            bsPost.State = (PostStates)Convert.ToInt16(ddState.SelectedValue);
            bsPost.AddComment = cblAddComment.Checked;
            bsPost.UpdateDate = DateTime.Now;

            Categories1.SaveData(bsPost.PostID);
            Tags1.SaveTags(bsPost.PostID);
            if (bsPost.Save())
                Response.Redirect("Library.aspx?FileID=" + bsPost.PostID + "&Message=1");
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
    public void HideAll()
    {
        divLibrary.Visible = false;
        divAddMedia.Visible = false;
        divEditMediaSide.Visible = false;
        divEditMedia.Visible = false;
    }
    protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
            BSHelper.SetPagerButtonStates(((GridView)sender), e.Row, this);
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        gvPosts.DataBind();
    }
    protected void gvPosts_DataBinding(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        gv.DataSource = BSPost.GetPosts(PostTypes.File, PostStates.All, 0);
    }
}
