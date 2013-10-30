<%@ Page Title="" Language="C#" MasterPageFile="~/Setup/Setup.master" AutoEventWireup="true"
    CodeFile="Completed.aspx.cs" Inherits="Setup_Completed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <div id="divStepCompleate" runat="server" visible="false">
        <div class="block">
            <div class="title compleatetitle">
                <%=Language.Setup["Congratulations"]%>
            </div>
            <asp:Literal ID="ltCompleate" runat="server" />
        </div>
        <div class="block">
            <div class="title">
                <%=Language.Setup["WhatDoYouWantNow"] %>
            </div>
            <a href="../Default.aspx" class="linkbutton">
                <%=Language.Setup["GoWebsite"] %></a><a href="../Admin/" class="linkbutton">
                    <%=Language.Setup["GoAdminPanel"] %></a>
        </div>
    </div>
    <div id="divStepBeforeInstalled" runat="server" visible="false">
        <div class="block" style="left: 0px; top: 0px">
            <%=Language.Setup["Setupped"] %>
        </div>
        <div class="block">
            <div class="title">
                <%=Language.Setup["WhatDoYouWantNow"] %>
            </div>
            <a href="../Default.aspx" class="linkbutton">
                <%=Language.Setup["GoWebsite"] %></a><a href="../Admin/" class="linkbutton">
                    <%=Language.Setup["GoAdminPanel"] %></a>
        </div>
    </div>
</asp:Content>
