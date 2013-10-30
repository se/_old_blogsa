<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LanguagePicker.ascx.cs"
    Inherits="Admin_Content_LanguagePicker" %>
<div class="title">
    <%=Language.Admin["Language"] %></div>
<div class="sidecontent">
    <asp:DropDownList runat="server" ID="ddlLanguages" Width="100%">
    </asp:DropDownList>
</div>
