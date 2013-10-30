<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cph1">
    <link href="Css/main.css" rel="stylesheet" type="text/css" />

    <script src="Js/Default.js" type="text/javascript"></script>

    <script type="text/javascript">
        $("#mnDefault").attr("class", "current");
    </script>

    <div id="wrapper">
        <div class="content" style="width: 960px">
            <asp:Literal runat="Server" ID="ltError"></asp:Literal>
            <ul id="leftPanel" class="uldrg">
                <asp:PlaceHolder ID="phLeftPanel" runat="server"></asp:PlaceHolder>
            </ul>
            <ul id="rightPanel" class="uldrg">
                <asp:PlaceHolder ID="phRightPanel" runat="server"></asp:PlaceHolder>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="toppanel">
    <div id="top-panel">
        <div id="panel">
            <%=Language.Admin["UsingVersion"]%>&nbsp;:&nbsp;<b><%=Blogsa.Version %></b>&nbsp;|&nbsp;<%=Language.Admin["LastVersion"]%>&nbsp;:&nbsp;<b><%=Blogsa.LatestVersion %></b>
        </div>
    </div>
</asp:Content>
