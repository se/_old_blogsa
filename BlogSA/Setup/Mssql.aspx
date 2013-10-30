<%@ Page Language="C#" MasterPageFile="~/Setup/Setup.master" AutoEventWireup="true"
    CodeFile="Mssql.aspx.cs" Inherits="Setup_Mssql" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="divError" runat="server">
        <div class="errorblock">
            <span class="errortitle">
                <%=Language.Setup["Error"] %></span>
            <asp:Label ID="lblError" runat="server" CssClass="errorlabel"></asp:Label>
            <%=Language.Setup["ErrorReason1"] %>
        </div>
    </div>
    <div id="divMain" runat="server">
        <div class="block">
            <span class="steptitle">
                <%=Language.Setup["Step1"] %>
                -
                <%=Language.Setup["InstallType"] %></span></div>
        <div class="block">
            <span class="title">
                <%=Language.Setup["SelectSetupType"] %></span><asp:RadioButtonList ID="rblInstallMethod"
                    runat="server" Font-Size="12pt">
                    <asp:ListItem Selected="True" Value="ToWeb"></asp:ListItem>
                    <asp:ListItem Value="ToLocal"></asp:ListItem>
                </asp:RadioButtonList>
        </div>
        <div class="block">
            <asp:LinkButton ID="btnMssqlContinue" CssClass="linkbutton" runat="server" OnClick="btnStep1_Click"><%=Language.Setup["Continue"] %></asp:LinkButton>
            <asp:LinkButton ID="btnMssqlCancel" CssClass="linkbutton" runat="server" OnClick="btnCancel_Click"><%=Language.Setup["Cancel"] %></asp:LinkButton>
        </div>
    </div>
    <div id="divSetup" runat="server">
        <div class="block">
            <span class="steptitle">
                <%=Language.Setup["Step2"]%>
                -
                <%=Language.Setup[(string)Session["Type"]]%></span></div>
        <div class="block">
            <span class="title">
                <%=Language.Setup["ServerParameters"]%></span>
            <div class="line">
                <span class="label">
                    <%=Language.Setup["ServerAddress"]%></span>
                <asp:TextBox CssClass="input" ID="txtWebServer" runat="server" AutoCompleteType="Disabled">localhost</asp:TextBox>
            </div>
            <div class="line">
                <span class="label">
                    <%=Language.Setup["UserName"]%></span>
                <asp:TextBox CssClass="input" ID="txtWebUser" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="line">
                <span class="label">
                    <%=Language.Setup["Password"]%></span>
                <asp:TextBox CssClass="input" ID="txtWebPass" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="line">
                <span class="label">
                    <%=Language.Setup["DatabaseName"]%></span>
                <asp:TextBox CssClass="input" ID="txtWebCatalog" runat="server" AutoCompleteType="Disabled"></asp:TextBox>
            </div>
            <div class="line">
                <span class="label">
                    <%=Language.Setup["TrustedConnection"]%></span>
                <asp:CheckBox ID="cbTrusted" CssClass="checkbox" runat="server" />
            </div>
        </div>
        <div class="block">
            <asp:LinkButton ID="btnMssqlInstall" CssClass="linkbutton" runat="server" OnClick="btnMssqlSetup_Click"><%=Language.Setup["Continue"] %></asp:LinkButton>
            <asp:LinkButton ID="btnMssqlGoBack" CssClass="linkbutton" runat="server" OnClick="btnMssqlGoBack_Click"><%=Language.Setup["GoBack"] %></asp:LinkButton>
        </div>
    </div>
</asp:Content>
