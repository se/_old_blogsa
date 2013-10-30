<%@ Control Language="C#"  %>

<script runat="server">
</script>

<div class="widget">
    <div class="title">
        <span>
            <%=Language.Get["Feed"] %></span></div>
    <div class="content">
        <a href="<%=Blogsa.Url %>Feed.aspx" style="text-decoration: none;display:block;
            background:url('<%=Blogsa.Url %>Widgets/Feeder/rssicon.gif') no-repeat;padding-left:20px;">
            RSS</a>
        <a href="<%=Blogsa.Url %>Feed.aspx?Comments" style="text-decoration: none;display:block;
            background:url('<%=Blogsa.Url %>Widgets/Feeder/rssicon.gif') no-repeat;padding-left:20px;">
            <%=Language.Get["Comments"] %>
            RSS</a>
    </div>
</div>
