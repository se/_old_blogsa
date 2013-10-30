<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Library.aspx.cs" Inherits="Admin_Posts" %>

<%@ Register Src="Content/Tags.ascx" TagName="Tags" TagPrefix="uc3" %>
<%@ Register Src="Content/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="Content/Categories.ascx" TagName="Categories" TagPrefix="uc1" %>
<%@ Register Src="Content/Pager.ascx" TagName="Pager" TagPrefix="uc4" %>
<%@ Register Src="Content/TinyMCE.ascx" TagName="TinyMCE" TagPrefix="uc5" %>
<%@ Register Src="Content/UploadFile.ascx" TagName="UploadFile" TagPrefix="BlogsaUserControls" %>
<asp:Content ContentPlaceHolderID="toppanel" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=gvPosts.ClientID %> tr").mouseenter(function () {
                $(this).find(".edit-buttons").show();
            });
            $("#<%=gvPosts.ClientID %> tr").mouseleave(function () {
                $(this).find(".edit-buttons").hide();
            });
        });
    </script>
    <div id="top-panel">
        <div id="panel">
            <ul>
                <li><a href="Library.aspx" class="library">
                    <%=Language.Admin["Library"]%></a></li>
                <li><a href="Library.aspx?p=new" class="add">
                    <%=Language.Admin["AddMedia"]%></a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cph1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#mnLibrary").attr("class", "current");
        });
    </script>
    <uc2:MessageBox runat="server" ID="MessageBox1" Type="Information" />
    <div id="wrapper">
        <div class="content">
            <div id="divLibrary" runat="server">
                <div class="rightnow">
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Library"] %></span>
                        <br />
                    </h3>
                    &nbsp;
                    <div class="dProcess">
                        <asp:LinkButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="bsbtn small red"><%=Language.Admin["Delete"] %></asp:LinkButton>
                        | <span>
                            <%=Language.Admin["Filter"] %>
                            : <a href="?">
                                <%=Language.Admin["ShowAll"] %></a></span></div>
                    <div>
                        <asp:GridView ID="gvPosts" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                            GridLines="None" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                            OnRowCreated="gv_RowCreated" OnDataBinding="gvPosts_DataBinding">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <input id="chkAll" onclick="javascript:HepsiniSec(this);" type="checkbox" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cb" onclick="javascript:SecimKontrol(this);" runat="server" />
                                        <asp:Literal runat="server" Text='<%#Eval("PostID") %>' Visible="false" ID="PostID"></asp:Literal>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <%=Language.Admin["Preview"] %>
                                    </HeaderTemplate>
                                    <ItemStyle Width="100px" />
                                    <HeaderStyle Width="100px" />
                                    <ItemTemplate>
                                        <img src="<%#GetThumbnail((short)Eval("Show"),(string)Eval("Title"),(string)Eval("Code")) %>"
                                            style="max-width: 100px; max-height: 100px; padding: 5px; margin: 5px; border: 2px solid #f4f4f4;
                                            background: #f7f7f7;" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <%=Language.Admin["Title"] %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <a href='<%#Eval("PostID","?PostID={0}") %>'>
                                            <asp:Literal ID="lTitle" runat="server" Text='<%# Eval("Title") %>' /></a><br />
                                        <span style="font-size: 10px">
                                            <%# Eval("Date") %></span><br />
                                        <div style="display: block; height: 24px; width: 200px;">
                                            <div class="edit-buttons" style="display: none">
                                                <a class="sbtn bsgray" href="<%#"../?FileID="+Eval("PostID") %>"><span>
                                                    <%=Language.Admin["LookAtFile"] %></span></a>&nbsp;<a class="sbtn bsblue" href="<%#"?FileID="+Eval("PostID") %>">
                                                        <span>
                                                            <%=Language.Admin["EditMedia"]%></span></a>&nbsp;<a class="sbtn" target="_blank"
                                                                href="<%#"../FileHandler.ashx?FileID="+Eval("PostID") %>"><span>
                                                                    <%=Language.Admin["Download"] %></span></a>
                                            </div>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <%=Language.Admin["Author"] %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#BSUser.GetUser((int)Eval("UserID")).Name%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <%=Language.Admin["CommentCount"] %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <a href="Comments.aspx?PostID=<%#Eval("PostID") %>" style="background: url('images/comment.png') no-repeat;
                                            font-size: 10px; text-align: center; color: #fff; display: block; width: 24px;
                                            height: 24px; line-height: 18px;">
                                            <%#BSComment.GetCommentsByPostID((int)Eval("PostID"), CommentStates.Approved).Count%>
                                        </a>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <%=Language.Admin["DownloadCount"] %>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%#BSPost.GetPost((int)Eval("PostID")).ReadCount%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <strong>
                                    <%=Language.Admin["NoMedia"] %></strong></EmptyDataTemplate>
                            <HeaderStyle CssClass="thead" />
                            <PagerTemplate>
                                <uc4:Pager ID="Pager1" runat="server" />
                            </PagerTemplate>
                            <PagerSettings Position="Top" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div id="divAddMedia" runat="server">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["AddMedia"] %></span></div>
                <div class="rightnow">
                    <p>
                        <%=Language.Admin["AddMediaDescription"] %></p>
                    <br />
                    <BlogsaUserControls:UploadFile runat="server" ID="ufBlogsa"></BlogsaUserControls:UploadFile>
                </div>
            </div>
            <div id="divEditMedia" runat="server">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["EditMedia"] %></span></div>
                <div class="rightnow">
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["FileName"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox CssClass="txtbox" ID="txtTitle" runat="server" Enabled="false"></asp:TextBox><br>
                        <strong>
                            <%=Language.Admin["FileOpenAddress"] %>: </strong>
                        <%=Blogsa.Url %><asp:Literal runat="Server" ID="ltOpenAddress"></asp:Literal><br />
                        <strong>
                            <%=Language.Admin["FileShowAddress"]%>: </strong>
                        <%=Blogsa.Url %><asp:Literal runat="Server" ID="ltShowAddress"></asp:Literal><br />
                        <strong>
                            <%=Language.Admin["FileDownloadAddress"]%>: </strong>
                        <%=Blogsa.Url %><asp:Literal runat="Server" ID="ltDownloadAdress"></asp:Literal><br />
                    </p>
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Description"] %></span>
                        <br />
                    </h3>
                    <uc5:TinyMCE ID="tmceContent" runat="server" />
                </div>
            </div>
        </div>
        <div class="sidebar">
            <div id="divEditMediaSide" runat="server" visible="false">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["FileKind"]%></span></div>
                <asp:DropDownList ID="ddState" runat="server" Width="100%">
                    <asp:ListItem Selected="True" Value="0"></asp:ListItem>
                    <asp:ListItem Value="1"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <asp:LinkButton ID="btnSavePost" runat="server" class="bsbtn small green block" OnClick="btnSavePost_Click"><%=Language.Admin["Save"] %></asp:LinkButton>
                <br />
                <uc1:Categories ID="Categories1" runat="server" />
                <uc3:Tags ID="Tags1" runat="server"></uc3:Tags>
                <div class="sidecontent">
                    <asp:CheckBox ID="cblAddComment" runat="server" Checked="True" /></div>
            </div>
        </div>
    </div>
</asp:Content>
