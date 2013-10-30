using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Links : BSAdminPage
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        string Msg = Request.QueryString["Message"];
        switch (Msg)
        {
            case "1": MessageBox1.Message = Language.Admin["LinkSaved"]; break;
            default: break;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        GenerateHeaderButtons();
        Categories1.TermType = TermTypes.LinkCategory;
        rblLinkTarget.Items[0].Text = Language.Admin["Standard"];
        if (!Page.IsPostBack)
        {
            string Msg = Request.QueryString["Message"];
            switch (Msg)
            {
                case "1": MessageBox1.Message = Language.Admin["PostSaved"]; break;
                default: break;
            }
            HideAll();

            int iLinkID = 0;
            int.TryParse(Request["LinkID"], out iLinkID);

            if (iLinkID > 0)
            {
                BSLink link = BSLink.GetLink(iLinkID);

                if (link != null)
                {
                    Categories1.TermType = TermTypes.LinkCategory;
                    Categories1.LoadData(link.LinkID);
                    divAddLink.Visible = true;
                    divAddPostSide.Visible = true;

                    txtLinkTitle.Text = link.Name;
                    txtLinkDescription.Text = link.Description;
                    txtLinkURL.Text = link.Url;
                    rblLinkTarget.SelectedValue = link.Target;
                    lpLinkLanguage.LangaugeCode = link.LanguageCode;
                }
                else
                {
                    Response.Redirect("Links.aspx");
                }
            }
            else
            {
                divPosts.Visible = true;
                gvLinks.DataBind();
            }
        }
    }
    /// <summary>
    /// Generate View and Table Header Buttons
    /// </summary>
    private void GenerateHeaderButtons()
    {
        // View Buttons
        voDefault.Items.Add(Language.Admin["ShowAll"], "~/Admin/Links.aspx");

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
        bool bRemove = false;
        for (int i = 0; i < gvLinks.Rows.Count; i++)
        {
            CheckBox cb = gvLinks.Rows[i].FindControl("cb") as CheckBox;
            if (cb.Checked)
            {
                Literal literal = gvLinks.Rows[i].FindControl("LinkID") as Literal;
                if (literal != null)
                {
                    int iLinkId = 0;
                    int.TryParse(literal.Text, out iLinkId);

                    BSLink link = BSLink.GetLink(iLinkId);

                    if (link != null)
                    {
                        bRemove = link.Remove();
                    }
                }
            }
        }
        if (bRemove)
        {
            MessageBox1.Message = Language.Admin["LinkDeleted"];
            MessageBox1.Type = MessageBox.ShowType.Information;
            MessageBox1.Visible = true;
            gvLinks.DataBind();
        }
    }

    private void HideAll()
    {
        divPosts.Visible = false;
        divAddLink.Visible = false;
        divAddPostSide.Visible = false;
    }
    protected void btnSaveLink_Click(object sender, EventArgs e)
    {
        int iLinkID = 0;
        int.TryParse(Request["LinkID"], out iLinkID);

        if (iLinkID > 0)
        {
            BSLink link = BSLink.GetLink(iLinkID);
            link.Name = txtLinkTitle.Text;
            link.Description = txtLinkDescription.Text;
            link.Url = txtLinkURL.Text;
            link.Target = rblLinkTarget.SelectedValue;
            link.LanguageCode = lpLinkLanguage.LangaugeCode;

            if (link.Save())
            {
                Categories1.TermType = TermTypes.LinkCategory;
                Categories1.SaveData(link.LinkID);
                MessageBox1.Message = Language.Admin["LinkSaved"];
                MessageBox1.Type = MessageBox.ShowType.Information;
            }
            else
            {
                MessageBox1.Message = Language.Admin["LinkError"];
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
        gvLinks.DataBind();
    }
    protected void gvLinks_DataBinding(object sender, EventArgs e)
    {
        GridView gv = (GridView)sender;
        gv.DataSource = BSLink.GetLinks();
    }
}
