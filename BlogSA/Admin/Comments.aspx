<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Comments.aspx.cs" Inherits="Admin_Comments" %>

<%@ Register Src="Content/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="Content/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="toppanel" runat="Server">
    <script type="text/javascript">
        $("#mnComments").attr("class", "current");

    </script>
    <link href="Css/comment.css" rel="stylesheet" type="text/css" />
    <script src="Js/Comments.js" type="text/javascript"></script>
    <div id="top-panel">
        <div id="panel">
            <ul>
                <li><a href="Comments.aspx" class="comments">
                    <%=Language.Admin["Comments"]%></a></li></ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">
    <uc2:MessageBox ID="MessageBox1" runat="server" />
    <div id="wrapper">
        <div class="content">
            <div id="divComments" runat="server">
                <div class="rightnow">
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Comments"]%></span>
                        <br />
                    </h3>
                    &nbsp;
                    <div class="dProcess">
                        <asp:LinkButton ID="btnApprove" runat="server" OnClick="btnApprove_Click" CssClass="bsbtn small green"><%=Language.Admin["DoApprove"] %></asp:LinkButton>
                        <asp:LinkButton ID="btnUnApprove" runat="server" OnClick="btnUnApprove_Click" CssClass="bsbtn small  blue"><%=Language.Admin["DoUnApprove"] %></asp:LinkButton>
                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="bsbtn small red"><%=Language.Admin["Delete"] %></asp:LinkButton>
                        | <span>
                            <%=Language.Admin["Filter"] %>
                            :
                            <input id="chkAll" onclick="javascript:HepsiniSec(this);" type="checkbox" />
                            <label for="chkAll">
                                <%=Language.Admin["SelectAll"] %></label>
                            | <a href="?">
                                <%=Language.Admin["ShowAll"] %></a> | <a class="sbtn"><span>&nbsp;</span></a><a href="?state=1">
                                    <%=Language.Admin["Approved"] %>
                                    (<%=BSComment.GetComments(CommentStates.Approved).Count %>)</a> | <a class="sbtn bsblue">
                                        <span>&nbsp;</span></a> <a href="?state=0">
                                            <%=Language.Admin["UnApproved"] %>
                                            (<%=BSComment.GetComments(CommentStates.UnApproved).Count%>)</a></span>
                    </div>
                    <div id="commentactions">
                        <asp:GridView ID="gvItems" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                            GridLines="None" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                            OnRowCreated="gv_RowCreated" OnDataBinding="gvItems_DataBinding">
                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <div class='commentview<%#(bool)Eval("Approve") == false ? " passive" : " active" %>'>
                                            <div class="commentbox" style="text-align: center">
                                                <asp:Literal Visible="false" runat="Server" Text='<%#Eval("CommentID") %>' ID="CommentID"></asp:Literal>
                                                <img class="avatar" src="<%#BSHelper.GetGravatar((string)Eval("EMail")) %>" alt="" />
                                                <div class="actionlist">
                                                    <div style="float: left; vertical-align: middle;">
                                                        <asp:CheckBox Style="vertical-align: middle;" ID="cb" onclick="javascript:SecimKontrol(this);"
                                                            runat="server" /></div>
                                                    <b>
                                                        <%#Eval("UserName") %></b> |
                                                    <%#Eval("Date") %>
                                                    | IP:<%#Eval("IP") %>
                                                    | <a href="javascript:;" itemid="<%#Eval("CommentID") %>" class="approve sbtn" style="<%#(bool)Eval("Approve")==true?"display:none": "" %>">
                                                        <span>
                                                            <%=Language.Admin["DoApprove"] %></span></a> <a href="javascript:;" itemid="<%#Eval("CommentID") %>"
                                                                class="unapprove sbtn bsblue" style="<%#!(bool)Eval("Approve")==true?"display:none": "" %>">
                                                                <span>
                                                                    <%=Language.Admin["DoUnApprove"] %></span></a> &nbsp;<a href="javascript:;" message="<%#Language.Admin["CommentDeleteConfirm"] %>"
                                                                        itemid="<%#Eval("CommentID") %>" class="delete sbtn bsred"><span><%=Language.Admin["Delete"] %></span></a>
                                                    &nbsp;<a href="?CommentID=<%#Eval("CommentID") %>" class="sbtn bsgray"><span><%=Language.Admin["EditComment"] %></span></a>
                                                </div>
                                                <div class="comment">
                                                    <%#Eval("Content") %>
                                                </div>
                                                <div class="info">
                                                    <%=Language.Admin["CommentedPost"] %>
                                                    <%#BSPost.GetPost((int)Eval("PostID")).LinkedTitle%><br />
                                                    Email : <a href="mailto:<%#Eval("EMail") %>">
                                                        <%#Eval("EMail") %></a><br />
                                                    Web site : <a target="_blank" href="<%#Eval("WebPage") %>">
                                                        <%#Eval("WebPage")%></a>
                                                </div>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <%=Language.Admin["CommentNotFound"] %>
                            </EmptyDataTemplate>
                            <PagerTemplate>
                                <uc1:Pager ID="Pager1" runat="server" />
                            </PagerTemplate>
                            <HeaderStyle CssClass="thead" />
                            <PagerSettings Position="Top" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div id="divEditComment" visible="false" runat="server">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["EditComment"] %></span></div>
                <div class="rightnow">
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Name"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox CssClass="txtbox" ID="txtName" runat="server"></asp:TextBox>&nbsp;</p>
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Comment"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox CssClass="txtbox" ID="txtComment" TextMode="MultiLine" Height="100"
                            runat="server"></asp:TextBox>&nbsp;</p>
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Mail"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox CssClass="txtbox" ID="txtEMail" runat="server"></asp:TextBox>&nbsp;</p>
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["WebSite"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox CssClass="txtbox" ID="txtWebSite" runat="server"></asp:TextBox>&nbsp;</p>
                </div>
            </div>
        </div>
        <div class="sidebar">
            <div runat="Server" id="divSideEditComment" visible="false">
                <div class="title">
                    <%=Language.Admin["State"]%></div>
                <asp:RadioButtonList runat="Server" ID="rblState">
                    <asp:ListItem Value="1"></asp:ListItem>
                    <asp:ListItem Value="0"></asp:ListItem>
                </asp:RadioButtonList>
                <div class="title">
                    <%=Language.Admin["CommentedPost"]%></div>
                <asp:Literal runat="Server" ID="ltCommentedPost"></asp:Literal><br />
                &nbsp;
                <div class="title">
                    <%=Language.Admin["Date"]%></div>
                <div id="divChanged" style="padding: 5px; border: 1px solid #e7e7e7;">
                    <%=Language.Admin["Date"] %>:
                    <br />
                    <asp:TextBox runat="server" ID="txtDateMonth" Width="20px" />:
                    <asp:TextBox runat="server" ID="txtDateDay" Width="20px" />:
                    <asp:TextBox runat="server" ID="txtDateYear" Width="30px" /><br />
                    <%=Language.Admin["Time"] %>:
                    <br />
                    <asp:TextBox runat="server" ID="txtTimeHour" Width="20px" />:
                    <asp:TextBox runat="server" ID="txtTimeMinute" Width="20px" />:
                    <asp:TextBox runat="server" ID="txtTimeSecond" Width="20px" />
                </div>
                <div class="title">
                    <%=Language.Admin["IP"]%></div>
                <asp:Literal runat="server" ID="ltIP"></asp:Literal>
                <br />
                <br />
                <asp:LinkButton ID="btnSavePost" runat="server" class="bsbutton green block" OnClick="btnSavePost_Click"><span><%=Language.Admin["Save"] %></span></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
