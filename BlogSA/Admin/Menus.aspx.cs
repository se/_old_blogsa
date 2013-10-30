using System;
using System.Web.UI.WebControls;

public partial class Admin_Menus : BSAdminPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        cbxDefault.Text = Language.Admin["Default"];

        GenerateHeaderButtons();

        if (!Page.IsPostBack)
        {
            string MenuID = Request.QueryString["MenuID"];
            string p = Request.QueryString["p"];

            int iMenuID = 0;
            int.TryParse(MenuID, out iMenuID);

            if (iMenuID != 0)
            {
                divSaveMenu.Visible = true;
                divSaveMenuSide.Visible = true;
                divMenus.Visible = false;
                GetMenuGroup(iMenuID);
            }
            else if (p != null && p == "AddMenu")
            {
                divMenuItems.Visible = false;
                divSaveMenu.Visible = true;
                divSaveMenuSide.Visible = true;
                divMenus.Visible = false;
            }
            else
            {
                gvMenus.DataBind();
            }
        }
    }

    /// <summary>
    /// Generate View and Table Header Buttons
    /// </summary>
    private void GenerateHeaderButtons()
    {
        // View Buttons
        voDefault.Items.Add(Language.Admin["ShowAll"], "~/Admin/Menus.aspx");

        // Action Buttons
        LinkButton lbDelete = new LinkButton();
        lbDelete.Click += btnDelete_Click;
        lbDelete.OnClientClick = "return confirm('" + Language.Admin["PostDeleteConfirm"] + "');";
        lbDelete.Text = Language.Admin["Delete"];
        lbDelete.CssClass = "bsbtn small red";
        gvhDefault.Buttons.Add(lbDelete);
    }

    private void GetMenuGroup(int iMenuID)
    {
        BSMenuGroup menuGroup = BSMenuGroup.GetMenuGroup(iMenuID);

        if (menuGroup != null)
        {
            txtTitle.Text = menuGroup.Title;
            txtDescription.Text = menuGroup.Description;
            cbxDefault.Checked = menuGroup.Default;

            rpMenuItems.DataSource = menuGroup.Menu;
            rpMenuItems.DataBind();
        }

        int menuID = 0;
        int.TryParse(Request["ItemID"], out menuID);

        if (menuID > 0)
        {
            BSMenu menu = BSMenu.GetMenu(menuID);
            if (menu != null)
            {
                txtMenuTitle.Text = menu.Title;
                txtMenuUrl.Text = menu.Url;
                txtMenuDescription.Text = menu.Description;
                txtMenuTarget.Text = menu.Target;
            }
        }
    }

    protected void gv_DataBinding(object sender, EventArgs e)
    {
        gvMenus.DataSource = BSMenuGroup.GetMenuGroups();
    }

    protected void gv_RowCreated(object sender, GridViewRowEventArgs e)
    {
    }

    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    protected void btnSaveMenu_Click(object sender, EventArgs e)
    {
        string p = Request["p"];

        if (!String.IsNullOrEmpty(p) && p.Equals("AddMenu"))
        {
            BSMenuGroup menuGroup = new BSMenuGroup();
            menuGroup.Default = cbxDefault.Checked;
            menuGroup.Title = txtTitle.Text;
            menuGroup.Description = txtDescription.Text;
            menuGroup.Code = BSHelper.CreateCode(menuGroup.Title);
            menuGroup.LanguageCode = bsucDefault.LangaugeCode;

            if (menuGroup.Save())
            {
                Response.Redirect(String.Format("Menus.aspx?MenuID={0}", menuGroup.MenuGroupID));
            }
        }
        else
        {
            int iMenuGroupID = 0;
            int.TryParse(Request["MenuID"], out iMenuGroupID);

            if (iMenuGroupID > 0)
            {
                BSMenuGroup menuGroup = BSMenuGroup.GetMenuGroup(iMenuGroupID);
                if (menuGroup != null)
                {
                    string[] strSort = hfMenuItems.Value.Split(';');

                    for (int i = 0; i < strSort.Length; i++)
                    {
                        if (!String.IsNullOrEmpty(strSort[i]))
                        {
                            int menuID = Convert.ToInt32(strSort[i]);
                            foreach (BSMenu bsMenu in menuGroup.Menu)
                            {
                                if (bsMenu.MenuID == menuID)
                                {
                                    bsMenu.Sort = (short)(i + 1);
                                }
                            }
                        }
                    }

                    menuGroup.Default = cbxDefault.Checked;
                    menuGroup.Title = txtTitle.Text;
                    menuGroup.Code = BSHelper.CreateCode(menuGroup.Title);
                    menuGroup.Description = txtDescription.Text;

                    if (menuGroup.Save())
                    {
                        GetMenuGroup(menuGroup.MenuGroupID);
                    }
                }
            }
        }
    }
    protected void btnAddMenuItem_Click(object sender, EventArgs e)
    {
        int iMenuGroupID = 0;
        int.TryParse(Request["MenuID"], out iMenuGroupID);

        if (iMenuGroupID > 0)
        {
            BSMenuGroup menuGroup = BSMenuGroup.GetMenuGroup(iMenuGroupID);
            if (menuGroup != null)
            {
                BSMenu menu = null;

                int menuID = 0;
                int.TryParse(Request["ItemID"], out menuID);

                if (menuID > 0)
                    menu = BSMenu.GetMenu(menuID);

                if (menu == null)
                {
                    menu = new BSMenu();
                    menu.MenuGroupID = menuGroup.MenuGroupID;
                    menu.ObjectType = ObjectTypes.Custom;
                    menu.Sort = (short)(menuGroup.Menu.Count + 1);
                    menu.MenuType = MenuTypes.Single;
                }

                menu.Title = txtMenuTitle.Text;
                menu.Description = txtMenuTitle.Text;
                menu.Url = txtMenuUrl.Text;
                menu.Target = txtMenuTarget.Text;

                if (menu.Save())
                {
                    txtMenuTarget.Text = String.Empty;
                    txtMenuTitle.Text = String.Empty;
                    txtMenuDescription.Text = String.Empty;
                    txtMenuUrl.Text = String.Empty;

                    Response.Redirect(String.Format("Menus.aspx?MenuID={0}", menuGroup.MenuGroupID));
                }
            }
        }
    }
    protected void rpMenuItems_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName.Equals("DeleteMenuItem"))
        {
            int iMenuItemID = Convert.ToInt32(e.CommandArgument);
            BSMenu menu = BSMenu.GetMenu(iMenuItemID);
            if (menu!=null)
            {
                if (menu.Remove())
                {
                    GetMenuGroup(menu.MenuGroupID);
                }
            }
        }
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        bool bRemove = false;

        for (int i = 0; i < gvMenus.Rows.Count; i++)
        {
            CheckBox cb = gvMenus.Rows[i].FindControl("cb") as CheckBox;
            if (cb.Checked)
            {
                string PostID = (gvMenus.Rows[i].FindControl("ltMenuGroupID") as Literal).Text;
                int iPostID = 0;

                int.TryParse(PostID, out iPostID);

                BSMenuGroup bsMenuGroup = BSMenuGroup.GetMenuGroup(iPostID);
                bRemove = bsMenuGroup.Remove();
            }
        }
        if (bRemove)
        {
            MessageBox1.Message = Language.Admin["MenuDeleted"];
            MessageBox1.Type = MessageBox.ShowType.Information;

            gvMenus.DataBind();
        }
    }
}