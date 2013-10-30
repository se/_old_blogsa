<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Now.ascx.cs" Inherits="Admin_Widgets_Now" %>
<div class="drgmain" id="wgNow" name="Now">
    <div class="drgbox">
        <div class="drgtitle">
            <span class="spantitle">
                <%=Language.Admin["Now"]%></span>
        </div>
        <div class="drgcontent">
            <div class="feed">
                <div class="feeddiv">
                    <div class="feedleft">
                        <span class="font16color">
                            <%=BSPost.GetPosts().Count%>
                        </span><span class="font14color">
                            <%=Language.Admin["Post"] %>
                        </span>
                    </div>
                    <div class="feedright">
                        <span class="font16color">
                            <%=BSComment.GetComments(CommentStates.All).Count%>
                        </span><span class="font14color">
                            <%=Language.Admin["Comment"] %>
                        </span>&nbsp; <span class="font10color">|<%=BSComment.GetComments(CommentStates.UnApproved).Count%>
                            <%=Language.Admin["Unapproved"] %>|</span>
                    </div>
                </div>
                <div class="feeddiv">
                    <div class="feedleft">
                        <span class="font16color">
                            <%=BSPost.GetPosts(PostTypes.Page,PostStates.Published,0).Count%>
                        </span><span class="font14color">
                            <%=Language.Admin["Page"] %>
                        </span>
                    </div>
                    <div class="feedright">
                        <span class="font16color">
                            <%=BSPost.GetReadCounts(0)%>
                        </span><span class="font14color">
                            <%=Language.Admin["Readed"] %>
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
