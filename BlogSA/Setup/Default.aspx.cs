using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;

public partial class Setup_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Blogsa.IsInstalled)
        {
            Response.Redirect("Compleated.aspx");
        }
        if (!Page.IsPostBack)
        {
            HideAll();
            switch ((string)Session["Step"])
            {
                case "PermissionControl":
                    divStepPermissionControl.Visible = true;
                    GetPermission();
                    break;
                case "InstallType":
                    divStepInstallType.Visible = true;
                    break;
                case "Mssql":
                    Response.Redirect("Mssql.aspx");
                    break;
                case "Finish":
                    Response.Redirect("Compleated.aspx");
                    break;
                case "Access":
                    Response.Redirect("Access.aspx");
                    break;
                default: divStepWellcome.Visible = true;
                    break;
            }
        }
    }

    public void HideAll()
    {
        divStepInstallType.Visible = false;
        divStepPermissionControl.Visible = false;
    }

    private void GetPermission()
    {
        ArrayList AL = new ArrayList();
        try
        {
            FileStream file = File.Open(Server.MapPath("~/web.config"), FileMode.Append);
            file.Close();
            AL.Add(Language.Setup["YesWrite"] + " Web.config [" + Language.Setup["File"] + "]");
        }
        catch
        {
            AL.Add(Language.Setup["NoWrite"] + " Web.config [" + Language.Setup["File"] + "]");
        }
        try
        {
            StreamWriter Sw = new StreamWriter(Server.MapPath("~/App_Data/accesscontrol.txt"));
            Sw.Write("please delete this file");
            Sw.Close();
            File.Delete(Server.MapPath("~/App_Data/accesscontrol.txt"));
            AL.Add(Language.Setup["YesWrite"] + " App_Data [" + Language.Setup["Folder"] + "]");
        }
        catch
        {
            AL.Add(Language.Setup["NoWrite"] + " App_Data [" + Language.Setup["Folder"] + "]");
        }
        try
        {
            StreamWriter Sw = new StreamWriter(Server.MapPath("~/Upload/accesscontrol.txt"));
            Sw.Write("please delete this file");
            Sw.Close();
            File.Delete(Server.MapPath("~/Upload/accesscontrol.txt"));
            AL.Add(Language.Setup["YesWrite"] + " Upload [" + Language.Setup["Folder"] + "]");
        }
        catch
        {
            AL.Add(Language.Setup["NoWrite"] + " Upload [" + Language.Setup["Folder"] + "]");
        }
        ulFileFolder.InnerHtml = string.Empty;
        foreach (string item in AL)
            ulFileFolder.InnerHtml += "<li style=\"position:relative;\">" + item + "</li>";
    }
    protected void Cancel_Click(object sender, EventArgs e)
    {
        string strLang = Session["lang"].ToString();
        Session.Abandon();
        Response.Redirect("Default.aspx?lang=" + strLang);
    }
    protected void btnPermissionCheckAgain_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.ToString());
    }
    protected void btnPermissionContinue_Click(object sender, EventArgs e)
    {
        Session["Step"] = "InstallType";
        Response.Redirect("Default.aspx");
    }
    protected void btnPermissionCheckAgain_Click1(object sender, EventArgs e)
    {
        GetPermission();
        Response.Redirect("Default.aspx");
    }
    protected void btnStart_Click(object sender, EventArgs e)
    {
        Session["Step"] = "PermissionControl";
        Response.Write(Session["Step"]);
        Response.Redirect("Default.aspx");
    }
}
