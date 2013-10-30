<%@ Control Language="C#" ClassName="CategoriesBlock" %>
<%@ Import Namespace="System.Collections.Generic" %>
<script runat="server">
    public string GetLinks()
    {
        string Tags = "";
        List<BSTerm> categories = BSTerm.GetTerms(TermTypes.Category);
        foreach (BSTerm category in categories)
        {
            string _Link = BSHelper.GetPermalink("Category", category.Code, Blogsa.UrlExtension);
            Tags += "<li><a href=\"" + _Link + "\">" + category.Name + "</a></li>";
        }
        return Tags;
    }
</script>
<div class="widget">
    <div class="title">
        <span>
            <%=Language.Get["Categories"] %></span></div>
    <div class="content">
        <ul>
            <%=GetLinks()%>
        </ul>
    </div>
</div>
