<%@ Control Language="C#" Inherits="WidgetBase" %>
<asp:PlaceHolder runat="server" ID="Dynamic"></asp:PlaceHolder>
<asp:PlaceHolder runat="server" ID="Static">
    <div class="widget">
        <div class="title">
            <span>
                <%=Widget.Title %></span></div>
        <div class="content">
            <span>
                <%=Widget.Description %></span>
        </div>
    </div>
</asp:PlaceHolder>
