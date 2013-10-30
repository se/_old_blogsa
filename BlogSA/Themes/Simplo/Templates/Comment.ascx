<%@ Control Language="C#" AutoEventWireup="true" Inherits="CommentTemplate" %>
<div class="comment">
    <div class="title">
        <div class="avatar">
            <img src="<%#Comment.GravatarLink %>" alt="" /></div>
    </div>
    <div class="content">
        <div class="content_title">
            <strong class="comment-meta"><a href="<%#Comment.WebPage %>" rel="nofollow">
                <%#Comment.UserName %></a></strong> <span class="right">
                    <%#Comment.Date.ToShortDateString() + " " + Comment.Date.ToShortTimeString()  %></span>
        </div>
        <div class="comment_content">
            <%#Comment.Content %>
        </div>
        <div class="clear">
        </div>
    </div>
</div>
