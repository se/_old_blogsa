<%@ Control Language="C#" ClassName="RecentPosts" %>
<%@ Import Namespace="System.Data" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        rpPosts.DataSource = BSPost.GetPosts(PostTypes.Article, PostStates.Published, 5);
        rpPosts.DataBind();
    }
</script>
<div class="widget">
    <div class="title">
        <span>
            <%=Language.Get["RecentPosts"] %></span></div>
    <div class="content">
        <ul>
            <asp:Repeater runat="server" ID="rpPosts">
                <ItemTemplate>
                    <li><a href="<%#BSHelper.GetLink((int)Eval("PostID")) %>">
                        <%#Eval("Title").ToString().Length < 100 ? Eval("Title").ToString() : Eval("Title").ToString().Substring(0, 99)%>
                    </a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>
