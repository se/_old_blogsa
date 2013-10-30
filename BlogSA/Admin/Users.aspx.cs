using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_Users : BSAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        GenerateHeaderButtons();

        rblRole.Items[0].Text = Language.Admin["User"];
        rblRole.Items[1].Text = Language.Admin["Editor"];
        rblRole.Items[2].Text = Language.Admin["Admin"];
        if (!Blogsa.ActiveUser.Role.Equals("admin"))
            rblRole.Enabled = false;
        if (!Page.IsPostBack)
        {
            string UserID = Request.QueryString["UserID"];
            string p = Request.QueryString["p"];
            if (User.IsInRole("editor"))
                UserID = Blogsa.ActiveUser.UserID.ToString();

            int iUserID = 0;
            int.TryParse(UserID, out iUserID);

            if (iUserID != 0)
            {
                divSaveUser.Visible = true;
                divSaveUserSide.Visible = true;
                divUsers.Visible = false;
                txtUserName.Enabled = false;
                GetUser(Convert.ToInt32(UserID));
            }
            else if (p != null && p == "AddUser")
            {
                txtUserName.Enabled = true;
                divSaveUser.Visible = true;
                divSaveUserSide.Visible = true;
                divUsers.Visible = false;
            }
            else
            {
                gvUsers.DataBind();
            }
        }
    }

    /// <summary>
    /// Generate View and Table Header Buttons
    /// </summary>
    private void GenerateHeaderButtons()
    {
        // View Buttons
        voDefault.Items.Add(Language.Admin["ShowAll"], "~/Admin/Users.aspx");

        // Action Buttons
        LinkButton lbDelete = new LinkButton();
        lbDelete.Click += btnDelete_Click;
        lbDelete.OnClientClick = "return confirm('" + Language.Admin["LinkDeleteConfirm"] + "');";
        lbDelete.Text = Language.Admin["Delete"];
        lbDelete.CssClass = "bsbtn small red";
        gvhDefault.Buttons.Add(lbDelete);
    }

    protected void btnSaveUser_Click(object sender, EventArgs e)
    {
        int iUserID = 0;
        int.TryParse(Request["UserID"], out iUserID);

        if (User.IsInRole("editor"))
            iUserID = Blogsa.ActiveUser.UserID;

        BSUser user = BSUser.GetUser(iUserID);

        if (user == null)
        {
            user = new BSUser();
            user.UserName = txtUserName.Text;
            user.Password = BSHelper.GetMd5Hash(txtPassword.Text);
        }
        else if (!String.IsNullOrEmpty(txtPassword.Text))
            user.Password = BSHelper.GetMd5Hash(txtPassword.Text);

        if (Blogsa.ActiveUser.Role.Equals("admin"))
            user.Role = rblRole.SelectedValue;
        else
            user.Role = "user";

        user.UserName = txtUserName.Text;

        user.Name = txtName.Text;
        user.Email = txtEmail.Text;
        user.WebPage = txtWebPage.Text;

        if (user.UserID != 1)
            user.Role = rblRole.SelectedValue;

        if (user.Save())
        {
            MessageBox1.Message = Language.Admin["UserSaved"];
            MessageBox1.Type = MessageBox.ShowType.Information;
        }
        else
        {
            MessageBox1.Message = "Error";
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        bool bUserRemoved = false;
        for (int i = 0; i < gvUsers.Rows.Count; i++)
        {
            CheckBox cb = gvUsers.Rows[i].FindControl("cb") as CheckBox;
            if (cb.Checked)
            {
                Literal literal = gvUsers.Rows[i].FindControl("ltUserID") as Literal;
                if (literal != null)
                {
                    string UserID = literal.Text;
                    int iUserID = Convert.ToInt32(UserID);
                    bUserRemoved = BSUser.GetUser(iUserID).Remove();
                }
            }
        }
        if (bUserRemoved)
        {
            MessageBox1.Message = Language.Admin["UserDeleted"];
            MessageBox1.Type = MessageBox.ShowType.Information;
            MessageBox1.Visible = true;
            gvUsers.DataBind();
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
        gvUsers.DataBind();
    }
    protected void gvUsers_DataBinding(object sender, EventArgs e)
    {
        ((GridView)sender).DataSource = BSUser.GetUsers();
    }

    private void GetUser(int UserID)
    {
        BSUser user = BSUser.GetUser(UserID);
        if (user != null)
        {
            txtUserName.Text = user.UserName;
            txtName.Text = user.Name;
            txtEmail.Text = user.Email;
            txtWebPage.Text = user.WebPage;
            rblRole.SelectedValue = user.Role;
            rblRole.Enabled = user.UserID != 1;
        }
    }
}
