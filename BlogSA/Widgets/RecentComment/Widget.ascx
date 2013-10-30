<%@ Control Language="C#" ClassName="RecentComments" %>
<%@ Import Namespace="System.Data" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        rpComments.DataSource = BSComment.GetComments(CommentStates.Approved, Convert.ToInt32(Blogsa.Settings["recent_comments_count"].Value));
        rpComments.DataBind();
    }
</script>
<div class="widget">
    <div class="title">
        <span>
            <%=Language.Get["RecentComments"] %></span></div>
    <div class="content">
        <ul>
            <asp:Repeater runat="server" ID="rpComments">
                <ItemTemplate>
                    <li><a href="<%#BSHelper.GetLink((int)Eval("PostID")) %><%#Eval("CommentID","#Comment{0}") %>">
                        <b>
                            <%#Eval("UserName")%>:</b>
                        <asp:Literal runat="server" Text='<%# Eval("Content").ToString().Length < 100 ? Eval("Content").ToString() : Eval("Content").ToString().Substring(0,99) %>'
                            Mode="Transform"></asp:Literal>
                    </a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>
