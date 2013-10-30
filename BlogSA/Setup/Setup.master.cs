using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Xml;

public partial class Setup_Setup : System.Web.UI.MasterPage
{
    protected void Page_Init(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Request["lang"]))
            Session["lang"] = Request["lang"];
        if (Session["lang"] == null)
        {
            string strSystemLang = BSHelper.GetSystemLanguage.Split('-').GetValue(0).ToString();
            Session["lang"] = strSystemLang != "" ? strSystemLang : "en";
        }
        Language.Setup = BSHelper.GetLanguageDictionary("Setup/Languages/" + Session["lang"].ToString() + ".xml");
        
        if (Blogsa.IsInstalled && !Request.Url.LocalPath.ToLowerInvariant().EndsWith("completed.aspx"))
            Response.Redirect("Completed.aspx");
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetLanguage();
    }

    private void GetLanguage()
    {
        ListItemCollection LIC = BSHelper.LanguagesByFolder("Setup/Languages/");
        foreach (ListItem item in LIC)
            ddLanguage.Items.Add(item);
        ddLanguage.SelectedValue = (string)Session["lang"];
    }

    protected void ddLanguage_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["lang"] = ((DropDownList)sender).SelectedValue;
        Language.Setup = BSHelper.GetLanguageDictionary("Setup/Languages/" + Session["lang"].ToString() + ".xml");
    }
}
