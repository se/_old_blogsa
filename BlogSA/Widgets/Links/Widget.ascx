<%@ Control Language="C#" ClassName="LinksBlock" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Collections.Generic" %>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public string GetLinks()
    {
        string tags = "";

        List<BSLink> links = BSLink.GetLinks();

        foreach (BSLink link in links)
        {
            tags += string.Format("<li><a title=\"{2}\" href=\"{0}\" target=\"{1}\">{3}</a></li>", link.Url, link.Target, link.Description, link.Name);
        }
        return tags;
    }
</script>
<div class="widget">
    <div class="title">
        <span>
            <%=Language.Get["Links"] %></span></div>
    <div class="content">
        <ul>
            <%=GetLinks()%>
        </ul>
    </div>
</div>
