<%@ Control Language="C#" AutoEventWireup="true" CodeFile="BSViewOptions.ascx.cs"
    Inherits="Admin_Content_BSViewOptions" %>
<div class="bstable-view">
    <div class="bstable-view-buttons">
        <%=Language.Admin["Filter"] %>:
        <asp:Literal ID="ltView" runat="server" />
    </div>
</div>
