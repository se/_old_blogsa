using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Comments : BSAdminPage
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        rblState.Items[0].Text = "<b style=\"color:#0AAF00\">" + Language.Admin["Approved"] + "</b>";
        rblState.Items[1].Text = "<b style=\"color:#F0C100\">" + Language.Admin["UnApproved"] + "</b>";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            string commentID = Request["CommentID"];
            int iCommentID = 0;

            int.TryParse(commentID, out iCommentID);

            if (iCommentID > 0)
            {
                divComments.Visible = false;
                divEditComment.Visible = true;
                divSideEditComment.Visible = true;

                BSComment bsComment = BSComment.GetComment(iCommentID);
                txtName.Text = bsComment.UserName;
                txtWebSite.Text = bsComment.WebPage;
                txtComment.Text = bsComment.Content;
                txtEMail.Text = bsComment.Email;
                ltIP.Text = bsComment.IP;
                rblState.SelectedValue = bsComment.Approve ? "1" : "0";
                ltCommentedPost.Text = BSPost.GetPost(bsComment.PostID).LinkedTitle;

                // DateTime
                txtDateDay.Text = bsComment.Date.Day.ToString("00");
                txtDateMonth.Text = bsComment.Date.Month.ToString("00");
                txtDateYear.Text = bsComment.Date.Year.ToString("0000");

                txtTimeHour.Text = bsComment.Date.Hour.ToString("00");
                txtTimeMinute.Text = bsComment.Date.Minute.ToString("00");
                txtTimeSecond.Text = bsComment.Date.Second.ToString("00");
            }
            else
            {
                divComments.Visible = true;
                divEditComment.Visible = false;
                divSideEditComment.Visible = false;
                gvItems.DataBind();
            }
        }
        btnDelete.OnClientClick = "return confirm('" + Language.Admin["CommentDeleteConfirm"] + "');";
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        bool bRemoved = false;
        for (int i = 0; i < gvItems.Rows.Count; i++)
        {
            CheckBox cb = gvItems.Rows[i].FindControl("cb") as CheckBox;
            if (cb.Checked)
            {
                int iCommentID = 0;
                Literal literal = gvItems.Rows[i].FindControl("CommentID") as Literal;
                if (literal != null)
                    int.TryParse(literal.Text, out iCommentID);

                BSComment bsComment = BSComment.GetComment(iCommentID);
                if (bsComment != null)
                {
                    bRemoved = bsComment.Remove();
                }

            }
        }
        if (bRemoved)
        {
            MessageBox1.Message = Language.Admin["CommentDeleted"];
            MessageBox1.Type = MessageBox.ShowType.Information;
            MessageBox1.Visible = true;
            gvItems.DataBind();
        }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        ApproveUnApprove(1);
        gvItems.DataBind();
    }

    protected void btnUnApprove_Click(object sender, EventArgs e)
    {
        ApproveUnApprove(0);
        gvItems.DataBind();
    }

    public void ApproveUnApprove(int approve)
    {
        bool bApproved = false;
        for (int i = 0; i < gvItems.Rows.Count; i++)
        {
            CheckBox cb = gvItems.Rows[i].FindControl("cb") as CheckBox;
            if (cb.Checked)
            {
                int iCommentID = 0;
                Literal literal = gvItems.Rows[i].FindControl("CommentID") as Literal;
                if (literal != null)
                    int.TryParse(literal.Text, out iCommentID);

                BSComment bsComment = BSComment.GetComment(iCommentID);

                if (bsComment != null)
                {
                    bApproved = bsComment.DoApprove(approve == 1);
                }
            }
        }
        if (bApproved)
        {
            MessageBox1.Message = approve == 1 ? Language.Admin["SelectedApproved"] : Language.Admin["SelectedUnApproved"];
            MessageBox1.Type = MessageBox.ShowType.Information;
            MessageBox1.Visible = true;
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
        ((GridView)sender).DataBind();
    }
    protected void gvItems_DataBinding(object sender, EventArgs e)
    {
        string postID = Request["PostID"];
        int iPostID = 0;

        int.TryParse(postID, out iPostID);

        if (iPostID != 0)
            ((GridView)sender).DataSource = BSComment.GetCommentsByPostID(iPostID, CommentStates.All);
        else
            ((GridView)sender).DataSource = BSComment.GetComments(CommentStates.All);
    }
    protected void btnSavePost_Click(object sender, EventArgs e)
    {
        try
        {
            int iCommentID = 0;
            int.TryParse(Request["CommentID"], out iCommentID);

            BSComment bsComment = BSComment.GetComment(iCommentID);
            bsComment.UserName = txtName.Text;
            bsComment.Content = txtComment.Text;
            bsComment.Email = txtEMail.Text;
            bsComment.WebPage = txtWebSite.Text;

            bsComment.Date = new DateTime(
                Convert.ToInt16(txtDateYear.Text),
                Convert.ToInt16(txtDateMonth.Text),
                Convert.ToInt16(txtDateDay.Text),
                Convert.ToInt16(txtTimeHour.Text),
                Convert.ToInt16(txtTimeMinute.Text),
                Convert.ToInt16(txtTimeSecond.Text));

            bsComment.Approve = rblState.SelectedValue.Equals("1");

            if (bsComment.Save())
            {
                MessageBox1.Message = Language.Admin["CommentSaved"];
                MessageBox1.Type = MessageBox.ShowType.Information;
            }
            else
            {
                MessageBox1.Message = "Error";
                MessageBox1.Type = MessageBox.ShowType.Error;
            }
        }
        catch (System.Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }
}

