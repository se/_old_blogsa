using System;

public class BSThemedPage : BSPage
{
    protected override void OnPreInit(EventArgs e)
    {
        try
        {
            string strCurrentTheme = Blogsa.Settings["theme"] != null ? Blogsa.Settings["theme"].ToString() : null;
            if (Request["theme"] != null)
                strCurrentTheme = Request["theme"];
            strCurrentTheme = BSHelper.CreateCode(strCurrentTheme);
            Blogsa.ActiveTheme = strCurrentTheme;
            if (System.IO.File.Exists(base.Server.MapPath("~/Themes/" + strCurrentTheme + "/Master.master")))
                MasterPageFile = "~/Themes/" + strCurrentTheme + "/Master.master";
            else
                MasterPageFile = "~/Themes/Default/Master.master";
            if (Blogsa.ActiveUser != null && Blogsa.ActiveUser.Role.Equals("admin"))
            {
                this.ClientScript.RegisterClientScriptInclude("Jquery", ResolveUrl("~/Admin/Js/jquery-1.6.4.min.js"));
                this.ClientScript.RegisterClientScriptInclude("Blogsa", ResolveUrl("~/Admin/Js/Blogsa.js"));
                this.ClientScript.RegisterStartupScript(GetType(), "BaseUrl", "Blogsa.BaseUrl='" + Blogsa.Url + "';", true);
            }
        }
        catch(Exception ex) {
            new BSLog(ex).Save();
        }
        base.OnPreInit(e);
    }
}

