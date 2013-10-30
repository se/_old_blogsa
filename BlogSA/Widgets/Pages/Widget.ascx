<%@ Control Language="C#" ClassName="PagesBlock" %>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            rpPages.DataSource = BSPost.GetPosts(PostTypes.Page, PostStates.Published, 0);
            rpPages.DataBind();
        }
    }
</script>
<div class="widget">
    <div class="title">
        <span>
            <%=Language.Get["Pages"] %></span></div>
    <div class="content">
        <span>
            <ul>
                <asp:Repeater runat="server" ID="rpPages">
                    <ItemTemplate>
                        <li><span>
                            <%#Eval("LinkedTitle") %></span></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </span>
    </div>
</div>
