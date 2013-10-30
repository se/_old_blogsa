<%@ Control Language="C#" AutoEventWireup="true" Inherits="PostTemplate" %>
<div class="post">
  <div class="titlehead">
    <h2 class="title">
      <a href="<%#Post.Link %>">
        <%#Post.Title %>
      </a>
    </h2>
    <div class="date">
      <%#Post.Date.Day %><br />
      <%#Post.Date.ToString("MMM") %>
    </div>
  </div>
  <div class="header" style="display: none">
    <div style="float: left">
      <%#Post.UserName%></div>
    <div style="float: right;">
      <%#Post.Date.ToShortDateString() + " "  + Post.Date.ToShortTimeString()%></div>
    <div style="clear: both; height: 1px;">
    </div>
  </div>
  <div class="entry">
    <asp:PlaceHolder runat="Server" ID="Content"></asp:PlaceHolder>
  </div>
  <div style="clear: both">
  </div>
  <div class="terms" runat="server" Visible='<%#Post.Type == PostTypes.Article %>'>
    <span>
      <%#Language.Get["Tags"] %>
      : </span>
    <%#Post.Tags %><br />
    <span>
      <%#Language.Get["Categories"] %>
      : </span>
    <%#Post.Categories%><br />
    <span>
      <%#Language.Get["Comments"] %>
      : </span><a href="<%# Post.Link + "#Comments" %>">
        <%#Post.CommentCount %>
        <%=Language.Get["Comment"] %></a> <a class="button" href="<%# Post.Link+ "#WriteComment" %>">
          <%=Language.Get["WriteComment"]%></a>
  </div>
</div>
