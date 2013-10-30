<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Add.aspx.cs" Inherits="Admin_Add" %>

<%@ Register Src="Content/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="Content/Categories.ascx" TagName="Categories" TagPrefix="uc1" %>
<%@ Register Src="Content/TinyMCE.ascx" TagName="TinyMCE" TagPrefix="uc4" %>
<%@ Register Src="Content/LanguagePicker.ascx" TagName="LanguagePicker" TagPrefix="BlogsaUserControls" %>
<%@ Register Src="Content/PostSettings.ascx" TagName="PostSettings" TagPrefix="BlogsaUserControls" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <uc2:MessageBox ID="MessageBox1" Type="Information" runat="server" />
    <div id="wrapper">
        <div class="content">
            <div id="divAddPost" runat="server">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["AddNewPost"] %></span></div>
                <div class="rightnow">
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Title"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox CssClass="txtbox" ID="txtTitle" runat="server"></asp:TextBox>&nbsp;</p>
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Content"] %></span>
                        <br />
                    </h3>
                    <uc4:TinyMCE ID="tmcePostContent" runat="Server" />
                </div>
            </div>
            <div id="divAddPage" runat="server">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["AddNewPage"] %></span></div>
                <div class="rightnow">
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Title"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox CssClass="txtbox" ID="txtPageTitle" runat="server"></asp:TextBox>&nbsp;</p>
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Content"] %></span>
                        <br />
                    </h3>
                    <uc4:TinyMCE ID="tmcePageContent" runat="server" />
                </div>
            </div>
            <div id="divAddLink" runat="server">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["AddNewLink"] %></span></div>
                <div class="rightnow">
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Title"] %></span><br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox ID="txtLinkTitle" CssClass="txtbox" runat="server" Width="705px"></asp:TextBox>
                    </p>
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Url"] %></span><br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox ID="txtLinkURL" CssClass="txtbox" runat="server" Text="http://" Width="705px"></asp:TextBox>
                    </p>
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Description"] %></span><br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox ID="txtLinkDescription" CssClass="txtbox" runat="server" Width="705px"></asp:TextBox>
                    </p>
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Target"] %></span><br />
                    </h3>
                    <p class="youhave">
                        <asp:RadioButtonList ID="rblLinkTarget" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem>_top</asp:ListItem>
                            <asp:ListItem>_blank</asp:ListItem>
                        </asp:RadioButtonList>
                        &nbsp;</p>
                </div>
            </div>
        </div>
        <div class="sidebar">
            <!-- ADD AN Post Settings -->
            <asp:LinkButton ID="btnSave" CssClass="bsbtn block green" runat="server" OnClick="btnSave_Click"><%=Language.Admin["Save"] %></asp:LinkButton>
            <br />
            <BlogsaUserControls:PostSettings runat="server" ID="bucPostSettings" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="toppanel">
    <div id="top-panel">
        <div id="panel">
            <ul>
                <li><a href="?p=Post" class="new-post">
                    <%=Language.Admin["AddNewPost"] %></a></li>
                <%if (User.IsInRole("admin"))
                  {%>
                <li><a href="?p=Page" class="new-page">
                    <%=Language.Admin["AddNewPage"] %></a></li>
                <li><a href="?p=Link" class="new-link">
                    <%=Language.Admin["AddNewLink"] %></a></li>
                <%} %>
            </ul>
        </div>
    </div>
</asp:Content>
