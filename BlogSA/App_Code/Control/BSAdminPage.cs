using System;

public class BSAdminPage : BSPageBase
{
    protected override void OnPreInit(EventArgs e)
    {
        if (!Blogsa.IsInstalled)
            Response.Redirect("~/Setup/Default.aspx");

        Title = String.Format("{0} - {1}", Blogsa.Title, Blogsa.Description);

        base.OnPreInit(e);
    }

    protected override void OnInit(EventArgs e)
    {
        Page.UICulture = Blogsa.CurrentBlogAdminLanguage;
        base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        base.Header.DataBind();
        base.OnLoad(e);
    }
    protected override void Render(System.Web.UI.HtmlTextWriter writer)
    {
        base.Render(new RewriteForm(writer));
    }
}

