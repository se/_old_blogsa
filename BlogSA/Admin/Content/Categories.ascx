<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Categories.ascx.cs" Inherits="Admin_Content_Categories" %>
<div class="title">
    <%=Language.Admin["Categories"] %></div>
<div class="sidecontent" runat="server" id="divCats" style="max-height:100px;overflow:auto;">
    <asp:CheckBoxList ID="cblCats" runat="server" CellPadding="0" CellSpacing="0" RepeatLayout="Flow">
    </asp:CheckBoxList>
</div>
