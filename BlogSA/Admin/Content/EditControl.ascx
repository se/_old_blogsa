<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EditControl.ascx.cs" Inherits="Admin_Content_EditControl" %>
<div class="ctl-box-item">
    <label runat="server" id="label">
        <asp:Literal Text="Control" ID="ltTitle" runat="server" />
    </label>
    <asp:TextBox runat="server" ID="tb" CssClass="ctl-box-txt wp100" Visible="false" />
</div>
