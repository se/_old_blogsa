<%@ Page Language="C#" MasterPageFile="~/Setup/Setup.master" AutoEventWireup="true"
    CodeFile="Access.aspx.cs" Inherits="Setup_Access" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="divError" runat="server">
        <div class="errorblock">
            <span class="errortitle">
                <%=Language.Setup["Error"] %></span>
            <asp:Label ID="lblError" runat="server" CssClass="errorlabel"></asp:Label>
            <%=Language.Setup["ErrorReason2"] %></div>
    </div>
    <div class="block">
        <span class="steptitle">
            <%=Language.Setup["AccessDatabaseSetup"] %></span>
    </div>
    <div class="block">
        <div class="line">
            <span class="label">
                <%=Language.Setup["DatabaseName"] %></span>
            <asp:TextBox CssClass="input" ID="txtDatabaseName" runat="server" AutoCompleteType="Disabled">Blogsa</asp:TextBox>
        </div>
    </div>
    <div id="divSetup" runat="server">
        <div class="block">
            <asp:LinkButton runat="server" ID="btnStep1" CssClass="linkbutton" OnClick="btnInstall_Click"><%=Language.Setup["Continue"] %></asp:LinkButton>
            <asp:LinkButton runat="server" ID="btnCancel" CssClass="linkbutton" OnClick="btnCancel_Click"><%=Language.Setup["Cancel"] %></asp:LinkButton>
        </div>
    </div>
</asp:Content>
