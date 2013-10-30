using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Pages : BSAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenerateHeaderButtons();

        ddState.Items[1].Text = Language.Admin["Draft"];
        ddState.Items[0].Text = Language.Admin["Publish"];
        cblAddComment.Text = Language.Admin["CommentAdd"];

        if (!Page.IsPostBack)
        {
            HideAll();
            int iPostID = 0;
            int.TryParse(Request["PostID"], out iPostID);

            if (iPostID != 0)
            {
                divAddPost.Visible = true;
                divAddPostSide.Visible = true;
                BSPost bsPost = BSPost.GetPost(iPostID);
                if (bsPost != null)
                {
                    txtTitle.Text = bsPost.Title;
                    tmcePageContent.Content = bsPost.Content;
                    ddState.SelectedValue = bsPost.State.ToString();
                    cblAddComment.Checked = bsPost.AddComment;
                }
                else
                {
                    Response.Redirect("Pages.aspx");
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
        voDefault.Items.Add(Language.Admin["ShowAll"], "~/Admin/Pages.aspx");
        voDefault.Items.Add(Language.Admin["Published"], "~/Admin/Pages.aspx?state=1");
        voDefault.Items.Add(Language.Admin["Drafted"], "~/Admin/Pages.aspx?state=0");

        // Action Buttons
        LinkButton lbDelete = new LinkButton();
        lbDelete.Click += btnDelete_Click;
        lbDelete.OnClientClick = "return confirm('" + Language.Admin["PostDeleteConfirm"] + "');";
        lbDelete.Text = Language.Admin["Delete"];
        lbDelete.CssClass = "bsbtn small red";
        gvhDefault.Buttons.Add(lbDelete);
    }

    protected void Page_PreRender(object sender, EventArgs e)
    {
        string Msg = Request.QueryString["Message"];
        switch (Msg)
        {
            case "1": MessageBox1.Message = Language.Admin["PageSaved"]; break;
            default: break;
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        bool bRemoved = false;
        for (int i = 0; i < gvPosts.Rows.Count; i++)
        {
            CheckBox cb = gvPosts.Rows[i].FindControl("cb") as CheckBox;
            if (cb.Checked)
            {
                Literal literal = gvPosts.Rows[i].FindControl("PostID") as Literal;
                if (literal != null)
                {
                    int iPostID = 0;
                    int.TryParse(literal.Text, out iPostID);
                    bRemoved = BSPost.GetPost(iPostID).Remove();
                }
            }
        }
        if (bRemoved)
        {
            MessageBox1.Message = Language.Admin["PageDeleted"];
            MessageBox1.Type = MessageBox.ShowType.Information;
            MessageBox1.Visible = true;
            gvPosts.DataBind();
        }
    }
    public void HideAll()
    {
        divPosts.Visible = false;
        divAddPost.Visible = false;
        divAddPostSide.Visible = false;
    }
    protected void btnSavePage_Click(object sender, EventArgs e)
    {
        int iPostID = 0;
        int.TryParse(Request["PostID"], out iPostID);
        if (iPostID != 0)
        {
            BSPost bsPost = BSPost.GetPost(iPostID);

            bsPost.Title = txtTitle.Text;
            bsPost.Code = BSHelper.CreateCode(txtTitle.Text);
            bsPost.Content = tmcePageContent.Content;
            bsPost.State = (PostStates)short.Parse(ddState.SelectedValue);
            bsPost.AddComment = cblAddComment.Checked;
            bsPost.UpdateDate = DateTime.Now;

            if (bsPost.Save())
            {
                MessageBox1.Message = Language.Admin["PageSaved"];
                MessageBox1.Type = MessageBox.ShowType.Information;
            }
            else
            {
                MessageBox1.Message = Language.Admin["PageError"];
            }
        }
    }
    protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Pager)
        {
            BSHelper.SetPagerButtonStates(((GridView)sender), e.Row, this);
        }
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        ((GridView)sender).PageIndex = e.NewPageIndex;
        gvPosts.DataBind();
    }
    protected void gv_ddl_SelectedIndexChanged(object sender, EventArgs e)
    {
        gvPosts.PageIndex = ((DropDownList)sender).SelectedIndex;
        gvPosts.DataBind();
    }
    protected void gvPosts_DataBinding(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        gv.DataSource = BSPost.GetPosts(PostTypes.Page, PostStates.All, 0);
    }
}
