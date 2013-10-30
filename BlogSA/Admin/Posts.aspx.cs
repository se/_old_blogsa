using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Posts : BSAdminPage
{
    /// <summary>
    /// Page Pre Render
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_PreRender(object sender, EventArgs e)
    {
        GetControlsLanguage();
        string Msg = Request.QueryString["Message"];
        switch (Msg)
        {
            case "1":
                BSPost p = BSPost.GetPost(Convert.ToInt32(Request["PostID"]));
                MessageBox1.Message = Language.Admin["PostSaved"] + " - <a class=\"sbtn bsdarkblue\" href=\"" + p.Link + "\"><span>"
                + Language.Admin["ShowPost"] + "</span></a>"; break;
            default: break;
        }
        ClientScript.RegisterClientScriptBlock(this.GetType(), "autosavemode", "autosavemode=true;", true);
    }

    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        gvhDefault.SearchButton.Click += new EventHandler(lbSearch_Click);

        GenerateHeaderButtons();

        if (!Page.IsPostBack)
        {
            HideAll();
            string PostID = Request.QueryString["PostID"];
            int iPostID = 0;

            int.TryParse(PostID, out iPostID);

            if (iPostID > 0)
            {
                BSPost bsPost = BSPost.GetPost(iPostID);

                divAddPost.Visible = true;
                divAddPostSide.Visible = true;
                if (bsPost != null)
                {
                    tmceContent.Content = bsPost.Content;
                    txtTitle.Text = bsPost.Title;
                    cblAddComment.Checked = bsPost.AddComment;
                    ddState.SelectedValue = bsPost.State.ToString();
                    Categories1.TermType = TermTypes.Category;
                    Categories1.LoadData(bsPost.PostID);
                    Tags1.LoadTags(bsPost.PostID);
                    rblDate.Items[0].Text = Language.Admin["NowPublish"];
                    rblDate.Items[1].Text = Language.Admin["ChangeDateTime"];

                    dtsDateTime.SelectedDateTime = bsPost.Date;
                }
                else
                {
                    Response.Redirect("Posts.aspx");
                }
            }
            else
            {
                divPosts.Visible = true;
                gvPosts.DataBind();
            }
        }
    }

    /// <summary>
    /// Generate View and Table Header Buttons
    /// </summary>
    private void GenerateHeaderButtons()
    {
        // View Buttons
        voDefault.Items.Add(Language.Admin["ShowAll"], "~/Admin/Posts.aspx");
        voDefault.Items.Add(Language.Admin["Published"], "~/Admin/Posts.aspx?state=1");
        voDefault.Items.Add(Language.Admin["Drafted"], "~/Admin/Posts.aspx?state=0");

        // Action Buttons
        LinkButton lbDelete = new LinkButton();
        lbDelete.Click += lbDelete_Click;
        lbDelete.OnClientClick = "return confirm('" + Language.Admin["PostDeleteConfirm"] + "');";
        lbDelete.Text = Language.Admin["Delete"];
        lbDelete.CssClass = "bsbtn small red";
        gvhDefault.Buttons.Add(lbDelete);
    }

    /// <summary>
    /// Search Post(s) click action.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void lbSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("Posts.aspx?Search=" + Server.HtmlEncode(gvhDefault.SearchText));
    }

    /// <summary>
    /// Fill Control Language Words
    /// </summary>
    private void GetControlsLanguage()
    {
        ddState.Items[0].Text = Language.Admin["Publish"];
        ddState.Items[1].Text = Language.Admin["Draft"];
        ddState.Items[2].Text = Language.Admin["DeletedPost"];
        cblAddComment.Text = Language.Admin["CommentAdd"];
    }

    /// <summary>
    /// Delete Post(s) click action.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        bool bRemove = false;

        for (int i = 0; i < gvPosts.Rows.Count; i++)
        {
            CheckBox cb = gvPosts.Rows[i].FindControl("cb") as CheckBox;
            if (cb.Checked)
            {
                string PostID = (gvPosts.Rows[i].FindControl("PostID") as Literal).Text;
                int iPostID = 0;

                int.TryParse(PostID, out iPostID);

                BSPost bsPost = BSPost.GetPost(iPostID);
                bRemove = bsPost.Remove();
            }
        }
        if (bRemove)
        {
            MessageBox1.Message = Language.Admin["PostDeleted"];
            MessageBox1.Type = MessageBox.ShowType.Information;

            gvPosts.DataBind();
        }
    }

    /// <summary>
    /// Save Post(s) click action.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSavePost_Click(object sender, EventArgs e)
    {
        try
        {
            string PostID = Request.QueryString["PostID"];
            int iPostID = 0;

            int.TryParse(PostID, out iPostID);

            BSPost bsPost = BSPost.GetPost(iPostID);

            bsPost.Title = txtTitle.Text;
            bsPost.Code = BSHelper.CreateCode(txtTitle.Text);
            bsPost.Content = tmceContent.Content;
            bsPost.State = (PostStates)Convert.ToInt16(ddState.SelectedValue);
            bsPost.AddComment = cblAddComment.Checked;
            bsPost.UpdateDate = DateTime.Now;

            if (rblDate.SelectedValue == "1")
            {
                bsPost.Date = dtsDateTime.SelectedDateTime;
            }

            Categories1.SaveData(bsPost.PostID);
            Tags1.SaveTags(bsPost.PostID);

            if (bsPost.Save())
                Response.Redirect("Posts.aspx?PostID=" + PostID + "&Message=1");
            else
            {
                MessageBox1.Message = Language.Admin["PostError"];
                MessageBox1.Type = MessageBox.ShowType.Error;
            }
        }
        catch (Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }

    /// <summary>
    /// Hide All States.
    /// </summary>
    private void HideAll()
    {
        divPosts.Visible = false;
        divAddPost.Visible = false;
        divAddPostSide.Visible = false;
    }

    /// <summary>
    /// Post's grid view row created.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            BSHelper.SetPagerButtonStates(((GridView)sender), e.Row, this);
        }
    }
    
    /// <summary>
    /// Post's grid view page index changed.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        gvPosts.DataBind();
    }
    
    /// <summary>
    /// Post's grid view data binding.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvPosts_DataBinding(object sender, EventArgs e)
    {
        int iUserID = 0;
        int.TryParse(Request["UserID"], out iUserID);

        int iState = -1;
        if (!String.IsNullOrEmpty(Request["state"]))
            int.TryParse(Request["state"], out iState);

        PostStates postState;

        switch (iState)
        {
            case 1: postState = PostStates.Published;
                break;
            case 0: postState = PostStates.Draft;
                break;
            case 2: postState = PostStates.Removed;
                break;
            default: postState = PostStates.All;
                break;
        }


        string searchText = Request["Search"];

        List<BSPost> posts;

        GridView gv = (GridView)sender;
        if (!String.IsNullOrEmpty(searchText))
            posts = User.IsInRole("editor") ? BSPost.GetPostsByTitle(searchText, Blogsa.ActiveUser.UserID) : BSPost.GetPostsByTitle(searchText, 0);
        else if (User.IsInRole("editor"))
            posts = BSPost.GetPostsByColumnValue("UserID", Blogsa.ActiveUser.UserID, 0, String.Empty, PostTypes.Article, postState);
        else if (Request["UserID"] != null && iUserID != 0)
            posts = BSPost.GetPostsByColumnValue("UserID", Blogsa.ActiveUser.UserID, 0, String.Empty, PostTypes.Article, postState);
        else
            posts = BSPost.GetPosts(PostTypes.Article, postState, 0);

        gv.DataSource = posts;
        gv.Attributes.Add("totalItemCount", posts.Count.ToString(CultureInfo.InvariantCulture));
    }
}
