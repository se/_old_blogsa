<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BSGridViewFooter.ascx.cs"
    Inherits="Admin_Content_BSGridViewFooter" %>
<span class="Ap">
    <label>
        <%=Language.Admin["Page"] %>:</label>
    <asp:DropDownList AutoPostBack="true" OnSelectedIndexChanged="gv_ddl_SelectedIndexChanged"
        ID="ddlPageSelector" runat="server">
    </asp:DropDownList>
</span><span class="Ap">
    <label>
        <asp:Literal Text="0" ID="ltPageStart" runat="server" />
        -
        <asp:Literal Text="0" ID="ltPageEnd" runat="server" />
        /
        <asp:Literal Text="0" runat="server" ID="ltTotal" />
    </label>
    <ul class="bstable-button-groups">
        <li onclick="__doPostBack('<%=btnPrevious.UniqueID %>','');">
            <asp:LinkButton ID="btnPrevious" CommandName="Page" CommandArgument="Prev"  UseSubmitBehavior="false" runat="server"><div class="bstable-left-button">
            </div></asp:LinkButton>
        </li>
        <li onclick="__doPostBack('<%=btnNext.UniqueID %>','');">
            <asp:LinkButton ID="btnNext" CommandName="Page" CommandArgument="Next" UseSubmitBehavior="false" runat="server"><div class="bstable-right-button">
            </div></asp:LinkButton>
        </li>
    </ul>
</span>