<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RelatedPosts.ascx.cs"
    Inherits="Contents_RelatedPosts" %>
<a id="RelatedPosts"></a>
<div class="relatedposts">
    <div class="title">
        <%=Language.Get["RelatedPosts"] %>
    </div>
    <div class="contents">
        <ul>
            <asp:Repeater runat="Server" ID="rpRelatedPosts">
                <ItemTemplate>
                    <li><a href="<%#Eval("Link") %>">
                        <%#Eval("Title") %></a></li>
                </ItemTemplate>
            </asp:Repeater>
        </ul>
    </div>
</div>
