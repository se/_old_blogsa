using System.Collections;
using System.Globalization;
using System.Text;
using System.Web.Security;
using System;
using System.Security.Principal;
using System.Collections.Generic;
using System.Web.UI.WebControls;

public partial class Admin_Master : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (Blogsa.ActiveUser == null)
        {
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
            Response.End();
        }
        else if (Page.User.IsInRole("editor"))
        {
            string strRequestPage = Request.Url.Segments[Request.Url.Segments.Length - 1];
            List<string> lstEditorPages = new List<string>();
            lstEditorPages.Add("add.aspx");
            lstEditorPages.Add("autosaves.aspx");
            lstEditorPages.Add("editor.aspx");
            lstEditorPages.Add("mediabrowser.aspx");
            lstEditorPages.Add("posts.aspx");
            lstEditorPages.Add("users.aspx");
            if (!lstEditorPages.Contains(strRequestPage.ToLower()))
            {
                Response.Redirect("Editor.aspx");
            }
        }

        StringBuilder sb = new StringBuilder();

        foreach (DictionaryEntry dictionaryEntry in Language.Admin)
            sb.AppendLine(String.Format("BSClient.Language.Admin['{0}']={1};", dictionaryEntry.Key, Newtonsoft.Json.JsonConvert.SerializeObject(dictionaryEntry.Value)));


        foreach (DictionaryEntry dictionaryEntry in Language.Get)
            sb.AppendLine(String.Format("BSClient.Language.Get['{0}']={1};", dictionaryEntry.Key, Newtonsoft.Json.JsonConvert.SerializeObject(dictionaryEntry.Value)));

        Page.ClientScript.RegisterClientScriptBlock(Page.GetType(), "BSClient_Admin_Language", sb.ToString(), true);
    }
}
