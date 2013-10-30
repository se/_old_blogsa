<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Posts.aspx.cs" Inherits="Admin_Posts" %>

<%@ Register Src="Content/Tags.ascx" TagName="Tags" TagPrefix="uc3" %>
<%@ Register Src="Content/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="Content/Categories.ascx" TagName="Categories" TagPrefix="uc1" %>
<%@ Register Src="Content/Pager.ascx" TagName="Pager" TagPrefix="uc4" %>
<%@ Register Src="Content/TinyMCE.ascx" TagName="TinyMCE" TagPrefix="uc5" %>
<%@ Register Src="Content/DateTimeSelector.ascx" TagName="DateTimeSelector" TagPrefix="BlogsaUserControls" %>
<%@ Register Src="Content/BSViewOptions.ascx" TagName="BSViewOptions" TagPrefix="BlogsaUserControls" %>
<%@ Register Src="Content/BSGridViewHeader.ascx" TagName="BSGridViewHeader" TagPrefix="BlogsaUserControls" %>
<%@ Register Src="Content/BSGridViewFooter.ascx" TagName="BSGridViewFooter" TagPrefix="BlogsaUserControls" %>
<asp:Content ContentPlaceHolderID="toppanel" runat="server">
    <div id="top-panel">
        <div id="panel">
            <ul>
                <%if (Page.User.IsInRole("admin"))
                  {%>
                <li><a href="Posts.aspx" class="posts">
                    <%=Language.Admin["Posts"]%></a></li>
                <li><a href="Add.aspx" class="new-post">
                    <%=Language.Admin["AddNewPost"] %></a></li>
                <% }%>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cph1" runat="Server">
    <style type="text/css">
        .table-tags a {
            float: left;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mnManage").attr("class", "current");
            $("#divChange").click(function () {
                $('#divChangedatetime').slideToggle();
            });
            $('#<%=rblDate.ClientID %> input:first').click(function () {
                $('#divChanged').slideUp();
            });
            $('#<%=rblDate.ClientID %> input:last').click(function () {
                $('#divChanged').slideDown();
            });
        });
    </script>
    <uc2:MessageBox runat="server" ID="MessageBox1" Type="Information" />
    <div id="wrapper">
        <div class="content">
            <div id="divPosts" runat="server">
                <div class="rightnow">
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Posts"] %></span>
                        <br />
                    </h3>
                    <BlogsaUserControls:BSViewOptions runat="server" ID="voDefault" />
                    <BlogsaUserControls:BSGridViewHeader ID="gvhDefault" Search="true" runat="server" />
                    <asp:GridView ID="gvPosts" CssClass="bstable" runat="server" AutoGenerateColumns="False"
                        AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging" OnRowCreated="gv_RowCreated"
                        OnDataBinding="gvPosts_DataBinding">
                        <Columns>
                            <asp:TemplateField ItemStyle-CssClass="tcc" HeaderStyle-CssClass="tcc">
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:HepsiniSec(this);" type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb" onclick="javascript:SecimKontrol(this);" runat="server" />
                                    <asp:Literal runat="server" Text='<%#Eval("PostID") %>' Visible="false" ID="PostID"></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle CssClass="bstable-select-header" />
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-CssClass="tcc" HeaderStyle-CssClass="tcc">
                                <HeaderTemplate>
                                    <%=Language.Admin["Date"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span title="<%#Eval("Date") %>">
                                        <%# ((DateTime)Eval("Date")).ToShortDateString() %></span>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["Title"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href='<%#Eval("PostID","?PostID={0}") %>'>
                                        <asp:Literal ID="lTitle" runat="server" Text='<%# Eval("Title") %>' /></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="tcc" ItemStyle-CssClass="tcc">
                                <HeaderTemplate>
                                    <%=Language.Admin["Author"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href="?UserID=<%#Eval("UserID") %>">
                                        <%#BSUser.GetUser((int)Eval("UserID")).Name%></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="tcc" ItemStyle-CssClass="tcc">
                                <HeaderTemplate>
                                    <%=Language.Admin["Categories"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="lCats" runat="server" Text='<%#BSPost.GetPost((int)Eval("PostID")).GetCategoriesHtml() %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="tcc" ItemStyle-CssClass="tcc">
                                <HeaderTemplate>
                                    <%=Language.Admin["Tags"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="table-tags"><%#BSPost.GetPost((int)Eval("PostID")).GetTagsHtml() %></div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="tcc" ItemStyle-CssClass="tcc">
                                <HeaderTemplate>
                                    <%=Language.Admin["Comment"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#BSComment.GetCommentsByPostID((int)Eval("PostID"),CommentStates.Approved).Count%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="tcc" ItemStyle-CssClass="tcc">
                                <HeaderTemplate>
                                    <%=Language.Admin["Readed"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#BSPost.GetPost((int)Eval("PostID")).ReadCount%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="tcc" ItemStyle-CssClass="tcc">
                                <HeaderTemplate>
                                    <%=Language.Admin["State"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#BSHelper.GetPostState(Eval("State").ToString(),(DateTime)Eval("Date")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderStyle-CssClass="tcc" ItemStyle-CssClass="tcc">
                                <HeaderTemplate>
                                    <%=Language.Admin["Language"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#BSHelper.GetPostDisplayLanguage((String)Eval("LanguageCode")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <strong>
                                <%=Language.Admin["NoPost"] %></strong>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="thead" />
                        <PagerStyle CssClass="en" />
                        <PagerTemplate>
                            <BlogsaUserControls:BSGridViewFooter runat="server" />
                        </PagerTemplate>
                        <AlternatingRowStyle CssClass="alternate"></AlternatingRowStyle>
                    </asp:GridView>
                </div>
            </div>
            <div id="divAddPost" visible="false" runat="server">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["EditPost"] %></span>
                </div>
                <div class="rightnow">
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Title"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox CssClass="txtbox" ID="txtTitle" runat="server"></asp:TextBox>&nbsp;
                    </p>
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Content"] %></span>
                        <br />
                    </h3>
                    <uc5:TinyMCE ID="tmceContent" runat="server" />
                </div>
            </div>
        </div>
        <div class="sidebar">
            <div id="divAddPostSide" runat="server" visible="false">
                <asp:DropDownList ID="ddState" runat="server" Width="100%">
                    <asp:ListItem Selected="True" Value="1"></asp:ListItem>
                    <asp:ListItem Value="0"></asp:ListItem>
                    <asp:ListItem Value="2"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <asp:LinkButton ID="btnSavePost" runat="server" class="bsbtn green block" OnClick="btnSavePost_Click"><%=Language.Admin["Save"] %></asp:LinkButton>
                <br />
                <div id="divChange" style="cursor: pointer; padding: 5px; border: 1px solid #e7e7e7;
                    background: #f4f4f4; display: block;">
                    <%=Language.Admin["ChangePublishDate"] %>
                </div>
                <div style="display: none;" id="divChangedatetime">
                    <asp:RadioButtonList ID="rblDate" runat="server" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="0"></asp:ListItem>
                        <asp:ListItem Text="" Value="1"></asp:ListItem>
                    </asp:RadioButtonList>
                    <div id="divChanged" style="display: none; padding: 5px; border: 1px solid #e7e7e7;">
                        <BlogsaUserControls:DateTimeSelector runat="server" ID="dtsDateTime" />
                    </div>
                </div>
                <br />
                <uc1:Categories ID="Categories1" runat="server" />
                <uc3:Tags ID="Tags1" runat="server"></uc3:Tags>
                <div class="title">
                    <%=Language.Admin["PostSettings"] %>
                </div>
                <div class="sidecontent">
                    <asp:CheckBox ID="cblAddComment" runat="server" Checked="True" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
