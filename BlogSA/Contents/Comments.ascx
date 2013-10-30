<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Comments.ascx.cs" Inherits="Contents_Comments" %>
<a id="comments" name="comments" href=""></a>
<asp:Repeater runat="server" ID="rpComments">
    <ItemTemplate>
        <a id='<%#Eval("CommentID","Comment{0}") %>' name='<%#Eval("CommentID","Comment{0}") %>'>
        </a>
        <asp:PlaceHolder ID="phComment" OnInit="phComment_OnInit" runat="server"></asp:PlaceHolder>
    </ItemTemplate>
</asp:Repeater>
