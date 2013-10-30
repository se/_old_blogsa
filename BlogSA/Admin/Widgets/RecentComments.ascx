<%@ Control Language="C#" AutoEventWireup="true" CodeFile="RecentComments.ascx.cs"
    Inherits="Admin_Widgets_RecentComments" %>
<link href="Css/comment.css" rel="stylesheet" type="text/css" />
<script src="Js/Comments.js" type="text/javascript"></script>

<div class="drgmain" id="wgRecentComments" name="RecentComments">
    <div class="drgbox">
        <div class="drgtitle">
            <span class="spantitle">
                <%=Language.Admin["RecentComments"] %></span>&nbsp;<a href="Comments.aspx" class="sbtn bsgray">
                    <span>
                        <%=Language.Admin["SeeAll"] %></span></a>
        </div>
        <div class="drgcontent">
            <div id="commentactions">
                <asp:Literal runat="Server" ID="ltNoData"></asp:Literal>
                <asp:Repeater ID="rpLastComments" runat="server">
                    <ItemTemplate>
                        <div class='commentview<%#(bool)Eval("Approve") == false ? " passive" : " active" %> small'>
                            <div class="commentbox" style="text-align: center">
                                <img class="avatar small" src="<%#BSHelper.GetGravatar((string)Eval("EMail")) %>"
                                    alt="" />
                                <div class="actionlist">
                                    <b>
                                        <%#Eval("UserName") %></b> |
                                    <%#Eval("Date") %>
                                    | <a href="javascript:;" itemid="<%#Eval("CommentID") %>" class="approve sbtn" style="<%#(bool)Eval("Approve")==true?"display:none": "" %>">
                                        <span><%=Language.Admin["DoApprove"] %></span></a> <a href="javascript:;" itemid="<%#Eval("CommentID") %>" class="unapprove sbtn bsblue"
                                            style="<%#!(bool)Eval("Approve")==true?"display:none": "" %>"><span><%=Language.Admin["DoUnApprove"] %></span></a>
                                    &nbsp;<a href="javascript:;" message="<%#Language.Admin["CommentDeleteConfirm"] %>"
                                        itemid="<%#Eval("CommentID") %>" class="delete sbtn bsred"><span>Sil</span></a>
                                </div>
                                <div class="comment">
                                    <%#Eval("Content") %>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</div>
