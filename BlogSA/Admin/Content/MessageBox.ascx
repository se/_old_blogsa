<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MessageBox.ascx.cs" Inherits="MessageBox" %>
<div class="ui-widget">
    <div runat="server" id="divOuter" class="ui-state-highlight ui-corner-all" style="margin-top: 10px; padding: 0 .7em;">
        <p>
            <span runat="server" id="spanIcon" class="ui-icon ui-icon-info" style="float: left; margin-right: .3em;"></span>
            <asp:Literal ID="ltMessage" runat="server" /></p>
    </div>
</div>
