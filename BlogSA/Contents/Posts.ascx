<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Posts.ascx.cs" Inherits="Contents_Posts" %>
<asp:Repeater runat="server" ID="rpPosts">
    <HeaderTemplate>
        <div class="posts">
    </HeaderTemplate>
    <ItemTemplate>
        <asp:PlaceHolder runat="server" OnInit="phPost_OnInit" ID="phPost"></asp:PlaceHolder>
    </ItemTemplate>
    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>
<div style="clear:both;display:block;height:1px;"></div>
<asp:PlaceHolder ID="BSPlaceHolderPaging" runat="server"></asp:PlaceHolder>
