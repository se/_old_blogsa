<%@ Control Language="C#" AutoEventWireup="true" Inherits="PostTemplate" %>
<div class="postItem">
    <div class="postItemHeaderHolder">
        <div class="categs">
            <%#Post.Categories%>
        </div>
        <div class="meta">
            <div>
                <%#Post.Date.ToString("MMMM dd, yyyy")%>
            </div>
            <div class="icoAuthor">
                <%#Post.UserName%>
            </div>
            <div class="icoComments">
                <a href="<%#Post.Link %>#comments">
                    <%#Post.CommentCount %>
                    <%#Language.Get["Comment"]%></a>
            </div>

            <!-- Place this tag in your head or just before your close body tag -->
            <script type="text/javascript" src="http://apis.google.com/js/plusone.js"></script>
            <!-- Place this tag where you want the +1 button to render -->
            <g:plusone size="medium" href="<%#Post.Link %>"></g:plusone>
            <a blogsa="" class="twitter-share-button" count="horizontal" data-http:localhost=""
                data-text="<%#Post.Title%>" data-url="<%#Post.Link%>" data-via="blogsa"
                href="http://twitter.com/share">Tweet
            </a>
        </div>
        <h1><a href="<%#Post.Link %>">
            <%#Post.Title %></a></h1>
    </div>
    <div class="postItemContentInner">
        <asp:PlaceHolder runat="Server" ID="Content"></asp:PlaceHolder>
    </div>
    <br />
    <div class="tags">
        <%#Post.Tags %>
    </div>
    <br />
    <iframe src="http://www.facebook.com/plugins/like.php?href=<%#Post.Link%>&amp;layout=standard&amp;show_faces=false&amp;width=450&amp;action=like&amp;font&amp;colorscheme=light&amp;height=35"
        scrolling="no" frameborder="0" style="border: none; overflow: hidden; width: 100%;
        height: 35px;" allowtransparency="true"></iframe>
</div>
