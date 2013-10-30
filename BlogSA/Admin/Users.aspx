<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Users.aspx.cs" Inherits="Admin_Users" %>

<%@ Register Src="Content/MessageBox.ascx" TagName="MessageBox" TagPrefix="Blogsa" %>
<%@ Register Src="Content/Pager.ascx" TagName="Pager" TagPrefix="Blogsa" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSViewOptions" Src="~/Admin/Content/BSViewOptions.ascx" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSGridViewHeader" Src="~/Admin/Content/BSGridViewHeader.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="toppanel" runat="Server">
    <script type="text/javascript">
        $("#mnUsers").attr("class", "current");
    </script>
    <div id="top-panel">
        <div id="panel">
            <ul>
                <%if (User.IsInRole("admin"))
                  {%>
                <li><a href="?p=AddUser" class="new-user">
                    <%=Language.Admin["AddNewUser"]%></a></li>
                <%} %>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">
    <Blogsa:MessageBox ID="MessageBox1" runat="server" />
    <div id="wrapper">
        <div class="content">
            <div id="divUsers" runat="server">
                <div class="rightnow">
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Users"] %></span>
                        <br />
                    </h3>
                    &nbsp;
                    <BlogsaUserControls:BSViewOptions runat="server" ID="voDefault" />
                    <BlogsaUserControls:BSGridViewHeader ID="gvhDefault" runat="server" />
                    <asp:GridView ID="gvUsers" CssClass="bstable" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                        GridLines="None" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                        OnRowCreated="gv_RowCreated" OnDataBinding="gvUsers_DataBinding">
                        <PagerSettings Position="Top" />
                        <Columns>
                            <asp:TemplateField HeaderText="Se&#231;">
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:HepsiniSec(this);" type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb" onclick="javascript:SecimKontrol(this);" runat="server" />
                                    <asp:Literal runat="server" Text='<%#Eval("UserID") %>' Visible="false" ID="ltUserID"></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["UserName"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href='<%#Eval("UserID","?UserID={0}") %>'>
                                        <asp:Literal ID="lName" runat="server" Text='<%# Eval("UserName") %>' /></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["Name"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="lURL" runat="server" Text='<%#Eval("Name") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["Mail"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("Email","<a href=\"mailto:{0}\">{0}</a>") %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%#Language.Admin[(string)Eval("Role")] %>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <%=Language.Admin["Role"] %>
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["Posts"] %>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%#BSHelper.GetPostCountForUserID((int)Eval("UserID")) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["Comments"] %>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%#BSHelper.GetCommentCount((int)Eval("UserID"), CommentStates.Approved) %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                            <Blogsa:Pager ID="Pager1" runat="server" />
                        </PagerTemplate>
                        <HeaderStyle CssClass="thead" />
                        <EmptyDataTemplate>
                            <strong>
                                <%=Language.Admin["UserNotFound"] %></strong>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
            <div id="divSaveUser" runat="server" visible="false">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["SaveUser"] %></span>
                </div>
                <div class="rightnow">
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["UserName"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox ID="txtUserName" runat="server" Width="705px" Enabled="False"></asp:TextBox>
                        <br />
                        <span style="font-size: 10px; color: gray; font-style: italic;">
                            <%=Language.Admin["UserNameDescription"] %>
                        </span>
                    </p>
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Password"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox ID="txtPassword" runat="server" Width="705px" TextMode="Password"></asp:TextBox>
                        <br />
                        <span style="font-size: 10px; color: gray; font-style: italic;">
                            <%=Language.Admin["PasswordDescription"] %>
                        </span>
                    </p>
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Name"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox ID="txtName" runat="server" Width="705px"></asp:TextBox>
                        <br />
                        <span style="font-size: 10px; color: gray; font-style: italic;">
                            <%=Language.Admin["NameDescription"] %>
                        </span>
                    </p>
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Mail"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox ID="txtEmail" runat="server" Width="705px"></asp:TextBox>
                        <br />
                        <span style="font-size: 10px; color: gray; font-style: italic;">
                            <%=Language.Admin["MailDescription"] %>
                        </span>
                    </p>
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["WebSite"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox ID="txtWebPage" runat="server" Width="705px"></asp:TextBox>
                        <br />
                        <span style="font-size: 10px; color: gray; font-style: italic;">
                            <%=Language.Admin["WebSiteDescription"] %>
                        </span>
                    </p>
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Role"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:RadioButtonList ID="rblRole" runat="server">
                            <asp:ListItem Value="user" Selected="True"></asp:ListItem>
                            <asp:ListItem Value="editor"></asp:ListItem>
                            <asp:ListItem Value="admin"></asp:ListItem>
                        </asp:RadioButtonList>
                        <span style="font-size: 10px; color: gray; font-style: italic;">
                            <%=Language.Admin["RoleDescription"] %>
                        </span>
                    </p>
                </div>
            </div>
        </div>
        <div class="sidebar">
            <div id="divSaveUserSide" runat="server" visible="false">
                <asp:LinkButton ID="btnSaveUser" runat="server" CssClass="bsbutton green block" OnClick="btnSaveUser_Click"><span><%=Language.Admin["Save"] %></span></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
