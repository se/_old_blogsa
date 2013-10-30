<%@ Control Language="C#" AutoEventWireup="true" Inherits="CommentTemplate" %>
<div class="comment">
<div class="title">
<div class="avatar"><img src="<%#Comment.GravatarLink %>" alt="" /></div>
<b><a href="<%#Comment.WebPage %>" rel="nofollow"><%#Comment.UserName %></a></b>
<span class="right"><%#Comment.Date.ToShortDateString() + " " + Comment.Date.ToShortTimeString()  %></span>
</div>
<div class="content"><%#Comment.Content %></div>
</div>