using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Tags : BSAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenerateHeaderButtons();
        if (!Page.IsPostBack)
        {
            HideAll();
            int iTermID = 0;
            int.TryParse(Request["TermID"], out iTermID);

            if (iTermID != 0)
            {
                divEditTerm.Visible = true;
                divSideEditTerm.Visible = true;

                BSTerm bsTerm = BSTerm.GetTerm(iTermID);

                if (bsTerm != null)
                {
                    txtCatName.Text = bsTerm.Name;
                }
                else
                {
                    Response.Redirect("Links.aspx");
                }
            }
            else
            {
                divPosts.Visible = true;
                divAddTerm.Visible = true;
                gvItems.DataBind();
            }
        }
    }

    /// <summary>
    /// Generate View and Table Header Buttons
    /// </summary>
    private void GenerateHeaderButtons()
    {
        // View Buttons
        voDefault.Items.Add(Language.Admin["ShowAll"], "~/Admin/Tags.aspx");

        // Action Buttons
        LinkButton lbDelete = new LinkButton();
        lbDelete.Click += btnDelete_Click;
        lbDelete.OnClientClick = "return confirm('" + Language.Admin["LinkDeleteConfirm"] + "');";
        lbDelete.Text = Language.Admin["Delete"];
        lbDelete.CssClass = "bsbtn small red";
        gvhDefault.Buttons.Add(lbDelete);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        bool bRemoved = false;
        for (int i = 0; i < gvItems.Rows.Count; i++)
        {
            CheckBox cb = gvItems.Rows[i].FindControl("cb") as CheckBox;
            if (cb.Checked)
            {
                Literal literal = gvItems.Rows[i].FindControl("TermID") as Literal;
                if (literal != null)
                {
                    int iTermID = 0;
                    int.TryParse(literal.Text, out iTermID);

                    BSTerm bsTerm = BSTerm.GetTerm(iTermID);
                    if (bsTerm != null)
                    {
                        bRemoved = bsTerm.Remove();
                    }
                }
            }
        }
        if (bRemoved)
        {
            MessageBox1.Message = Language.Admin["TagDeleted"];
            MessageBox1.Type = MessageBox.ShowType.Information;
            MessageBox1.Visible = true;
            gvItems.DataBind();
        }
    }

    private void HideAll()
    {
        divPosts.Visible = false;
        divEditTerm.Visible = false;
        divAddTerm.Visible = false;
        divSideEditTerm.Visible = false;
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtName.Text.Trim() != "")
        {
            string code = BSHelper.CreateCode(txtName.Text);

            BSTerm bsTerm = BSTerm.GetTerm(code, TermTypes.Tag);

            if (bsTerm == null)
            {
                bsTerm = new BSTerm();
                bsTerm.Name = txtName.Text;
                bsTerm.Type = TermTypes.Tag;
                bsTerm.Code = code;
            }

            bsTerm.Save();

            if (bsTerm.Save())
            {
                MessageBox1.Message = Language.Admin["TagSaved"];
                MessageBox1.Type = MessageBox.ShowType.Information;
                gvItems.DataBind();
                txtName.Text = string.Empty;
            }
            else
            {
                MessageBox1.Message = "Error";
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        int iTermID = 0;
        int.TryParse(Request["TermID"], out iTermID);

        if (iTermID > 0)
        {
            BSTerm bsTerm = BSTerm.GetTerm(iTermID);

            bsTerm.Name = txtCatName.Text;
            bsTerm.Code = BSHelper.CreateCode(txtCatName.Text);
            if (bsTerm.Save())
            {
                MessageBox1.Message = Language.Admin["TagSaved"];
                MessageBox1.Type = MessageBox.ShowType.Information;
                gvItems.DataBind();
                txtName.Text = string.Empty;
            }
            else
            {
                MessageBox1.Message = "Error";
            }
        }
        else
        {
            Response.Redirect("Categories.aspx");
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
        gvItems.DataBind();
    }
    protected void gvItems_DataBinding(object sender, EventArgs e)
    {
        ((GridView)sender).DataSource = BSTerm.GetTerms(TermTypes.Tag);
    }
}
