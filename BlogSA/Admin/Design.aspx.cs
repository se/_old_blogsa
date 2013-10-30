using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Admin_Design : BSAdminPage
{
    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

        string p = Request["p"];
        if (!String.IsNullOrEmpty(p) && p.Equals("Settings"))
        {
            divCurrent.Visible = false;
            divThemes.Visible = false;
            divSettings.Visible = true;
            divThemesSide.Visible = true;

            CallSettings();
        }
        else
        {
            divCurrent.Visible = true;
            divThemes.Visible = true;
            divSettings.Visible = false;
            divThemesSide.Visible = false;

            CallTheme();
            AllThemes();
        }
    }

    private void CallSettings()
    {
        ltThemeName.Text = BSTheme.Current.Name;
        BSThemeSettings settings = BSTheme.Current.Settings;
        foreach (BSThemeSetting themeSetting in settings)
        {
            Admin_Content_EditControl ec = (Admin_Content_EditControl)LoadControl("~/Admin/Content/EditControl.ascx");

            ec.Key = themeSetting.Key;
            ec.Title = themeSetting.Title;
            ec.Value = themeSetting.Value;
            ec.ControlType = themeSetting.Type;

            phThemeSettings.Controls.Add(ec);
        }
    }

    private void CallTheme()
    {
        try
        {
            List<BSTheme> dataSource = new List<BSTheme>();
            dataSource.Add(BSTheme.Current);
            rpTheme.DataSource = dataSource;
            rpTheme.DataBind();

            lblThemeError.Text = "";
            lblThemeError.Visible = false;
        }
        catch (Exception ex)
        {
            lblThemeError.Visible = true;
            lblThemeError.Text = "<b>" + Language.Admin["CurrentTheme"] + " : </b>" + Blogsa.Settings["theme"] + "<br/><br/>";
            lblThemeError.Text += "<b>" + Language.Admin["Error"] + " : </b>" + ex.Message;
            rpTheme.DataSource = "";
            rpTheme.DataBind();
        }
    }

    private void AllThemes()
    {
        rpAllThemes.DataSource = BSTheme.GetThemes();
        rpAllThemes.DataBind();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        foreach (Control control in phThemeSettings.Controls)
        {
            Admin_Content_EditControl ec = (Admin_Content_EditControl)control;

            string settingName = String.Format("{0}_{1}", BSTheme.Current.Name, ec.Key);

            BSSetting s = BSSetting.GetSetting(settingName) ?? new BSSetting();

            s.Main = false;
            s.Title = ec.Title;
            s.Name = settingName;
            s.Value = ec.Value;
            s.Visible = false;

            if (s.Save())
            {
                if (Blogsa.Settings[settingName] != null)
                    Blogsa.Settings[settingName] = s;
                else
                    Blogsa.Settings.Add(s);

                BSTheme.Current.Settings[ec.Key].Value = s.Value;
            }
        }

        MessageBox1.Message = Language.Admin["SettingSaved"];
        MessageBox1.Type = MessageBox.ShowType.Information;
    }

    protected void rpAllThemes_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "UseTheme")
        {
            BSSetting bsSetting = BSSetting.GetSetting("theme");
            bsSetting.Value = e.CommandArgument.ToString();

            if (bsSetting.Save())
            {
                Blogsa.ActiveTheme = bsSetting.Value;
                BSTheme.Current = null;
                MessageBox1.Message = Language.Admin["ThemeUpgraded"];
                Response.Redirect("Design.aspx");
            }
        }
    }
}
