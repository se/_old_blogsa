<%@ Control Language="C#" ClassName="Search" %>
<%@ Import Namespace="System.Data"%>

<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        ListItemCollection LIC = BSHelper.LanguagesByFolder("Setup/Languages/");
        foreach (ListItem item in LIC)
        {
            HyperLink hl = new HyperLink();
            hl.Text = item.Text + "&nbsp;";
            hl.NavigateUrl = "~/?lang=" + item.Value;
            ltLangs.Controls.Add(hl);
        }
        
        string strLang = Request["lang"];
        if (strLang!=null)
        {
            Session["lang"] = strLang;
            Response.Redirect(Request.UrlReferrer.ToString());
        }
    }
</script>
<div class="widget">
<div class="title"><span><%=Language.Get["SelectLanguage"] %></span></div>
<div class="content" style="text-align:center">
    <asp:Label runat="server" ID="ltLangs"></asp:Label>
</div>
</div>