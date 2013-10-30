using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;

public partial class Admin_Categories : BSAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenerateHeaderButtons();
        if (!Page.IsPostBack)
        {
            HideAll();

            ddlParentCategory.DataTextField = "Name";
            ddlParentCategory.DataValueField = "TermID";

            ddlParentCategory.DataSource = BSTerm.GetTerms(TermTypes.Category);
            ddlParentCategory.DataBind();

            string TermID = Request.QueryString["TermID"];
            int iTermID = 0;

            int.TryParse(TermID, out iTermID);

            if (iTermID > 0)
            {
                divEditTerm.Visible = true;
                divSideEditTerm.Visible = true;
                BSTerm bsTerm = BSTerm.GetTerm(iTermID);

                if (bsTerm != null)
                {
                    txtCatName.Text = bsTerm.Name;
                    txtCatDescription.Text = bsTerm.Description;
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
        voDefault.Items.Add(Language.Admin["ShowAll"], "~/Admin/Categories.aspx");

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
        bool bSomeOne = false;
        bool bCategoriesRemoved = false;
        for (int i = 0; i < gvItems.Rows.Count; i++)
        {
            CheckBox cb = gvItems.Rows[i].FindControl("cb") as CheckBox;
            if (cb.Checked)
            {
                string TermID = (gvItems.Rows[i].FindControl("TermID") as Literal).Text;
                int iTermID = int.Parse(TermID);

                List<BSTerm> categories = BSTerm.GetTermsBySubID(TermTypes.Category, iTermID);

                if (categories.Count == 0)
                {
                    BSTerm bsTerm = BSTerm.GetTerm(iTermID);
                    if (bsTerm != null)
                    {
                        bsTerm.Remove();
                    }
                    bCategoriesRemoved = true;
                }
                else
                    bSomeOne = true;
            }
        }
        if (bSomeOne)
        {
            MessageBox1.Message = Language.Admin["CategoryHaveSub"];
            MessageBox1.Type = MessageBox.ShowType.Information;
        }
        else if (bCategoriesRemoved)
        {
            MessageBox1.Message = Language.Admin["CategoryDeleted"];
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

    private string CategoryType
    {
        get
        {
            string termType = "category";
            if (!String.IsNullOrEmpty(Request["type"]) && Request["type"].Equals("linkcategory"))
                termType = "linkcategory";

            return termType;
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtName.Text.Trim() != "")
        {
            BSTerm bsTerm = new BSTerm();
            bsTerm.Name = txtName.Text;
            bsTerm.Description = txtDescription.Text;
            bsTerm.Type = BSTerm.GetTermType(CategoryType);
            bsTerm.Code = BSHelper.CreateCode(bsTerm.Name);

            bsTerm.SubID = int.Parse(ddlParentCategory.SelectedValue);

            if (bsTerm.Save())
            {
                MessageBox1.Message = Language.Admin["CategorySaved"];
                MessageBox1.Type = MessageBox.ShowType.Information;
                gvItems.DataBind();
                txtName.Text = string.Empty;
                txtDescription.Text = string.Empty;
            }
            else
                MessageBox1.Message = Language.Admin["CategoryError"];
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string TermID = Request.QueryString["TermID"];
        int iTermID = 0;

        int.TryParse(TermID, out iTermID);

        if (!String.IsNullOrEmpty(txtCatName.Text.Trim()) && iTermID > 0)
        {
            BSTerm bsTerm = BSTerm.GetTerm(iTermID);
            bsTerm.Name = txtCatName.Text;
            bsTerm.Description = txtCatDescription.Text;
            bsTerm.Code = BSHelper.CreateCode(txtCatName.Text);
            //term.SubID = int.Parse(ddlParentCategory.SelectedValue);

            if (bsTerm.Save())
            {
                MessageBox1.Message = Language.Admin["CategorySaved"];
                MessageBox1.Type = MessageBox.ShowType.Information;
                gvItems.DataBind();
                txtName.Text = string.Empty;
                txtDescription.Text = string.Empty;
            }
            else
                MessageBox1.Message = Language.Admin["CategoryError"];
        }
        else
            Response.Redirect("Categories.aspx");
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
        string type = Request["type"];
        TermTypes termType = TermTypes.Category;
        if (!String.IsNullOrEmpty(type))
        {
            termType = BSTerm.GetTermType(type);
        }

        List<BSTerm> categories = BSTerm.GetTermsBySubID(termType, 0);
        List<string> lstNames = new List<string>();

        for (int i = 0; i < categories.Count; i++)
        {
            string strParents = string.Empty;
            BSTerm category = categories[i];

            while (category.SubID != 0)
            {
                BSTerm subCategory = BSTerm.GetTerm(category.SubID);
                strParents = subCategory.Name + " > " + strParents;
            }
            lstNames.Add(strParents + category.Name);
        }

        ((GridView)sender).DataSource = categories;
        if (!IsPostBack)
        {
            ddlParentCategory.Items.Insert(0, new ListItem(Language.Admin["None"], "0"));
        }
    }
}
