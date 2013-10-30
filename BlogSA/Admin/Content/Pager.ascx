<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Pager.ascx.cs" Inherits="Admin_Content_Pager" %>
<div style="display: block; padding: 4px;">
    <div style="float: left;">
        <%=Language.Admin["GoPage"] %>
        <asp:DropDownList Style="vertical-align: middle;" ID="ddlPageSelector" runat="server"
            AutoPostBack="true" OnSelectedIndexChanged="gv_ddl_SelectedIndexChanged">
        </asp:DropDownList>
    </div>
    <div style="float: right;">
        <asp:LinkButton CommandName="Page" CommandArgument="First" runat="server" ID="btnFirst"
            UseSubmitBehavior="false"><img src="Images/Icons/pagefirst.png" /></asp:LinkButton>
        <asp:LinkButton CommandName="Page" CommandArgument="Prev" runat="server" ID="btnPrevious"
            UseSubmitBehavior="false"><img src="Images/Icons/pagebefore.png" /></asp:LinkButton>
        <asp:LinkButton CommandName="Page" CommandArgument="Next" runat="server" ID="btnNext"
            UseSubmitBehavior="false"><img src="Images/Icons/pagenext.png" /></asp:LinkButton>
        <asp:LinkButton CommandName="Page" CommandArgument="Last" runat="server" ID="btnLast"
            UseSubmitBehavior="false"><img src="Images/Icons/pagelast.png" /></asp:LinkButton>
    </div>
</div>
