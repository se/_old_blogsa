<%@ Control Language="C#" AutoEventWireup="true" Inherits="CommentTemplate" %>
<div class="description <%#Comment.IsAdmin ? "bs-ct-admin" : "" %>">
    <div class="avatar">
        <img src="<%#Comment.GravatarLink %>" alt="Avatar" />
    </div>
    <div class="comment">
        <%#Comment.Content %>
    </div>
    <div class="terms">
        <b>
            <%=Language.Get["CommentAuthor"] %>
            : </b><a style="color: Red;" href="<%#Comment.WebPage %>"
                rel="nofollow">
                <%#Comment.UserName%>
            </a>- <b>
                <%=Language.Get["Date"] %>
                : </b>
        <%#Comment.Date %>
    </div>
</div>
