<%@ Control Language="C#" AutoEventWireup="true" Inherits="PostTemplate" %>
<div class="title">
    <a href="<%#Post.Link %>">
        <h2>
            <%#Post.Title %></h2>
    </a>
</div>
<div class="content">
    <div class="contentinfo">
        <span style="font-weight: bold;">
            <%=Language.Get["Date"]%>
        </span>
        <%#Post.Date %>
        - <span style="font-weight: bold;">
            <%=Language.Get["Author"]%></span> <span style="color: #FF0F00; font-weight: bold;">
                <%#Post.UserName %></span></div>
    <div class="contentdescription">
        <asp:PlaceHolder ID="Content" runat="server" />
    </div>
    <div style="clear: both">
    </div>
    <div class="contentterms">
        <span style="font-weight: bold;">#
            <%=Language.Get["Categories"] %>
            : </span>
        <%#Post.Categories %><br>
        <span style="font-weight: bold;">#
            <%=Language.Get["Tags"] %>
            : </span>
        <%#Post.Tags %><br />
        <div runat="server" visible='<%#Eval("AddComment") %>' id="divCom">
            <b>#
                <%=Language.Get["Comments"] %>
                : </b><a href="<%# Post.Link + "#Comments" %>">
                    <%#Post.CommentCount %>
                    <%=Language.Get["Comment"] %></a> <a class="button" href="<%# Post.Link+ "#WriteComment" %>">
                        <span>
                            <%=Language.Get["WriteComment"]%></span></a>
        </div>
    </div>
</div>
