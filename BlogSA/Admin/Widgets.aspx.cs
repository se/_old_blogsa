using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Label = System.Web.UI.WebControls.Label;

public partial class Admin_Widgets : BSAdminPage
{
    protected void Page_PreRender(object sender, EventArgs e)
    {
        string Msg = Request.QueryString["Message"];
        switch (Msg)
        {
            case "1":
                MessageBox1.Message = Language.Admin["WidgetSaved"];
                MessageBox1.Type = MessageBox.ShowType.Information;
                break;
            default: break;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            GenerateHeaderButtons();
            if (!Page.IsPostBack)
            {
                GetWidgets();
                GetAllWidgets();

                ddlVisible.Items.Add(new ListItem(Language.Admin["Active"], "True"));
                ddlVisible.Items.Add(new ListItem(Language.Admin["Passive"], "False"));

                string fileName = Server.MapPath("~/Themes/" + Blogsa.Settings["theme"] + "/Settings.xml");
                string temp = BSHelper.GetXmlSingleNodeValue(fileName, "//theme//places");
                if (temp.Trim() != "")
                {
                    string[] strPlaceHolders = temp.Split(',');
                    foreach (string t in strPlaceHolders)
                        ddlPlace.Items.Add(t);
                }
                else
                    ddlPlace.Items.Add("Default");

                if (Request["WidgetID"] != null)
                {
                    divEditWidget.Visible = true;
                    divEditWidgetSide.Visible = true;

                    int iWidgetID = 0;
                    int.TryParse(Request["WidgetID"], out iWidgetID);

                    BSWidget bsWidget = BSWidget.GetWidget(iWidgetID);

                    if (bsWidget != null)
                    {
                        txtTitle.Text = bsWidget.Title;
                        tmceDescription.Content = bsWidget.Description;
                        if (bsWidget.Type == WidgetTypes.Page)
                            divWidgetContent.Visible = true;

                        ddlVisible.SelectedValue = bsWidget.Visible.ToString();
                        ddlPlace.SelectedValue = bsWidget.PlaceHolder;

                    }
                    else
                        Response.Redirect("Widgets.aspx");
                }
                else if (Request["p"] != null && Request["p"] == "AddWidget")
                {
                    divEditWidget.Visible = true;
                    divEditWidgetSide.Visible = true;
                    divWidgetContent.Visible = true;
                }
                else
                {
                    divWidgets.Visible = true;
                    divWidgetsSide.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }

    private void GenerateHeaderButtons()
    {
        // View Buttons
        voDefault.Items.Add(Language.Admin["ShowAll"], "~/Admin/Widgets.aspx");
        voDefault.Items.Add(Language.Admin["Actived"], "~/Admin/Widgets.aspx?state=1");
        voDefault.Items.Add(Language.Admin["Passived"], "~/Admin/Widgets.aspx?state=0");

        // Action Buttons
        LinkButton lbActivateWidgets = new LinkButton();
        lbActivateWidgets.Click += lbActivateWidgets_Click;
        lbActivateWidgets.Text = Language.Admin["doactive"];
        lbActivateWidgets.CssClass = "bsbtn small blue";
        gvhDefault.Buttons.Add(lbActivateWidgets);

        LinkButton lbDeactivateWidgets = new LinkButton();
        lbDeactivateWidgets.Click += lbDeactivateWidgets_Click;
        lbDeactivateWidgets.Text = Language.Admin["dopassive"];
        lbDeactivateWidgets.CssClass = "bsbtn small black";
        gvhDefault.Buttons.Add(lbDeactivateWidgets);

        LinkButton lbDelete = new LinkButton();
        lbDelete.Click += lbDelete_Click;
        lbDelete.OnClientClick = "return confirm('" + Language.Admin["WidgetDeleteConfirm"] + "');";
        lbDelete.Text = Language.Admin["Delete"];
        lbDelete.CssClass = "bsbtn small red";
        gvhDefault.Buttons.Add(lbDelete);


    }

    private void GetPlaces()
    {
        try
        {
            string fileName = Server.MapPath("~/Themes/" + Blogsa.Settings["theme"] + "/Settings.xml");
            string temp = BSHelper.GetXmlSingleNodeValue(fileName, "//theme//places");
            if (temp.Trim() != "")
            {
                string[] strPlaceHolders = temp.Split(',');

                foreach (GridViewRow gvR in gvWidgets.Rows)
                {
                    Label label = gvR.FindControl("lblPlace") as Label;
                    if (label != null)
                    {
                        string strPlace = label.Text;
                        string htmlTags = "<select style=\"vertical-align:middle;\" id=\"selectPlace\">";
                        for (int i = 0; i < strPlaceHolders.Length; i++)
                        {
                            htmlTags += strPlace.Trim() == strPlaceHolders[i].Trim() ? "<option selected>" : "<option>";
                            htmlTags += strPlaceHolders[i] + "</option>";
                        }
                        htmlTags += "</select>";

                        Literal lt = gvR.FindControl("ltSelectPlace") as Literal;
                        lt.Text = htmlTags;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }

    private void GetAllWidgets()
    {
        DirectoryInfo dInfo = new DirectoryInfo(Server.MapPath("~/Widgets"));
        rpAllWidgets.DataSource = dInfo.GetDirectories();
        rpAllWidgets.DataBind();
    }

    private void GetWidgetXml(string FolderName, RepeaterItem RPI)
    {
        try
        {
            DataSet DS = new DataSet();
            DS.ReadXml(Server.MapPath(string.Format("~/Widgets/{0}/Widget.xml", FolderName)));
            foreach (DataRow dRow in DS.Tables[0].Rows)
            {
                for (int i = 0; i < dRow.ItemArray.Length; i++)
                {
                    string property = DS.Tables[0].Columns[i].ColumnName;
                    Literal LC = (Literal)RPI.FindControl(property);
                    if (LC != null)
                    {
                        LC.Text = dRow[i].ToString();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }

    private void GetWidgets()
    {
        try
        {
            short sState = -1;
            if (!short.TryParse(Request["state"], out sState))
            {
                sState = -1;
            }

            bool bVisible = sState == 1;

            if (sState == 1 || sState == 0)
                gvWidgets.DataSource = BSWidget.GetWidgets(bVisible);
            else
                gvWidgets.DataSource = BSWidget.GetWidgets();

            gvWidgets.DataBind();
        }
        catch (Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }

    private void VisibleUnVisible(bool bVisible)
    {
        try
        {
            bool bDone = false;
            for (int i = 0; i < gvWidgets.Rows.Count; i++)
            {
                CheckBox cb = gvWidgets.Rows[i].FindControl("cb") as CheckBox;
                if (cb.Checked)
                {
                    Literal literal = gvWidgets.Rows[i].FindControl("WidgetID") as Literal;
                    if (literal != null)
                    {
                        int iWidgetID = Convert.ToInt32(literal.Text);

                        BSWidget bsWidget = BSWidget.GetWidget(iWidgetID);
                        if (bsWidget != null)
                        {
                            bsWidget.Visible = bVisible;
                            bDone = bsWidget.Save();
                        }
                    }
                }
            }
            if (bDone)
            {
                MessageBox1.Message = bVisible ? Language.Admin["BeActive"] : Language.Admin["BePassive"];
                MessageBox1.Type = MessageBox.ShowType.Information;
                MessageBox1.Visible = true;
            }
            GetWidgets();
        }
        catch (Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }

    protected void lbActivateWidgets_Click(object sender, EventArgs e)
    {
        VisibleUnVisible(true);
    }
    protected void lbDeactivateWidgets_Click(object sender, EventArgs e)
    {
        VisibleUnVisible(false);
    }
    protected void rpAllWidgets_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        GetWidgetXml(((Literal)e.Item.FindControl("foldername")).Text, e.Item);
    }
    protected void rpAllWidgets_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Add")
            {
                Hashtable hT = new Hashtable();
                string folderName = e.CommandArgument.ToString();
                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(Server.MapPath("~/Widgets/" + folderName + "/Widget.xml"));
                XmlNode node = xDoc.SelectSingleNode("//widget/title");
                if (node != null)
                {
                    string title = node.InnerText;
                    XmlNode xnType = xDoc.SelectSingleNode("//widget/type");

                    string strError = string.Empty;

                    bool bSaved = false;

                    if (xnType == null || (!xnType.InnerText.ToLower().Equals("page")))
                    {
                        BSWidget bsWidget = new BSWidget();
                        bsWidget.Title = title;
                        bsWidget.FolderName = folderName;
                        bsWidget.Type = 0;
                        bsWidget.Sort = (short)(gvWidgets.Rows.Count + 1);
                        bsWidget.PlaceHolder = "Default";
                        bsWidget.Visible = true;
                        bSaved = bsWidget.Save();
                    }
                    else if (xnType.InnerText.ToLower().Equals("page"))
                        strError = Language.Admin["NotSideBarWidget"];

                    if (bSaved)
                    {
                        MessageBox1.Message = Language.Admin["WidgetAdded"];
                        MessageBox1.Type = MessageBox.ShowType.Information;
                    }
                    else
                    {
                        MessageBox1.Message = strError;
                        MessageBox1.Type = MessageBox.ShowType.Error;
                    }
                }
                GetWidgets();
                GetAllWidgets();
            }
        }
        catch (Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }
    protected void lbDelete_Click(object sender, EventArgs e)
    {
        try
        {
            bool bRemove = false;
            for (int i = 0; i < gvWidgets.Rows.Count; i++)
            {
                CheckBox cb = gvWidgets.Rows[i].FindControl("cb") as CheckBox;
                if (cb.Checked)
                {
                    Literal literal = gvWidgets.Rows[i].FindControl("WidgetID") as Literal;
                    if (literal != null)
                    {
                        int iWidgetID = Convert.ToInt32(literal.Text);
                        BSWidget bsWidget = BSWidget.GetWidget(iWidgetID);
                        bRemove = bsWidget != null && bsWidget.Remove();
                    }
                }
            }
            if (bRemove)
            {
                MessageBox1.Message = Language.Admin["WidgetDeleted"];
                MessageBox1.Type = MessageBox.ShowType.Information;
                MessageBox1.Visible = true;
            }
            GetWidgets();
            GetAllWidgets();
        }
        catch (Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }
    protected void gvWidgets_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "UD")
            {
                string[] values = e.CommandArgument.ToString().Split('|');
                int val = -1;
                if (values[0] == "U")
                    val = -1;
                else
                    val = +1;

                int iWidgetID = Convert.ToInt32(values[1]);

                BSWidget bsWidget = BSWidget.GetWidget(iWidgetID);
                bsWidget.Sort += (short)val;

                if (bsWidget.Save())
                {
                    MessageBox1.Type = MessageBox.ShowType.Error;
                    MessageBox1.Message = "Error";
                }
                GetWidgets();
            }
        }
        catch (Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }
    protected void gvWidgets_PreRender(object sender, EventArgs e)
    {
        GetPlaces();
    }
    protected void btnSaveWidget_Click(object sender, EventArgs e)
    {
        try
        {
            BSWidget bsWidget;

            MessageBox1.Type = MessageBox.ShowType.Error;

            if ("AddWidget".Equals(Request["p"]))
            {
                bsWidget = new BSWidget();
                bsWidget.Title = txtTitle.Text;
                bsWidget.Description = tmceDescription.Content;
                bsWidget.Visible = Convert.ToBoolean(ddlVisible.SelectedValue);
                bsWidget.PlaceHolder = ddlPlace.SelectedValue;
                bsWidget.LanguageCode = Blogsa.CurrentBlogLanguage;
                bsWidget.Type = WidgetTypes.Page;
                if (bsWidget.Save())
                    Response.Redirect("Widgets.aspx?Message=1");
                else
                    MessageBox1.Message = "Error";
            }
            else
            {
                bsWidget = BSWidget.GetWidget(Convert.ToInt32(Request["WidgetID"]));
                
                if (!divWidgetContent.Visible)
                    bsWidget.Description = string.Empty;
                else
                {
                    bsWidget.Title = txtTitle.Text;
                    bsWidget.Description = tmceDescription.Content;
                }

                if (bsWidget.Save())
                    Response.Redirect("Widgets.aspx?Message=1");
                else
                    MessageBox1.Message = "Error";
            }
        }
        catch (Exception ex)
        {
            MessageBox1.Message = ex.Message;
            MessageBox1.Type = MessageBox.ShowType.Error;
        }
    }
}
