using System;

public class BSMasterPage : System.Web.UI.MasterPage
{
    protected override void OnLoad(EventArgs e)
    {
        if (Blogsa.ActiveUser != null && Blogsa.ActiveUser.Role.Contains("admin") && Page.Header.FindControl("blogsa") == null)
        {
            System.Web.UI.HtmlControls.HtmlLink hlBlogsa = new System.Web.UI.HtmlControls.HtmlLink();
            hlBlogsa.ID = "blogsa";
            hlBlogsa.Href = ResolveUrl("~/Admin/Css/blogsa.css");
            hlBlogsa.Attributes.Add("rel", "stylesheet");
            hlBlogsa.Attributes.Add("type", "text/css");
            Page.Header.Controls.Add(hlBlogsa);
        }
        base.OnLoad(e);
    }
}