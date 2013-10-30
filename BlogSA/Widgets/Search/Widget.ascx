<%@ Control Language="C#" ClassName="Search" %>
<%@ Import Namespace="System.Data" %>
<script type="text/javascript">
    function keyEnter(event) {
        if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {
            __doPostBack('<%=btnSearch.UniqueID%>', '');
            return false;
        }
        else return true;
    }
</script>
<script runat="server">

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Search.aspx?q=" + txtSearchText.Text);
    }
</script>
<div class="widget">
    <div class="title">
        <span>
            <%=Language.Get["Search"] %></span></div>
    <div class="content" style="text-align: center">
        <asp:Panel runat="Server" ID="pnlSearch" DefaultButton="btnSearch">
            <asp:TextBox runat="server" ID="txtSearchText" onkeydown="keyEnter(event)"></asp:TextBox>&nbsp;
            <asp:LinkButton OnClick="btnSearch_Click" runat="Server" ID="btnSearch"><%=Language.Get["DoSearch"] %></asp:LinkButton></asp:Panel>
    </div>
</div>
