<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Pages.aspx.cs" Inherits="Admin_Pages" %>

<%@ Register Src="Content/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc1" %>
<%@ Register Src="Content/Pager.ascx" TagName="Pager" TagPrefix="uc2" %>
<%@ Register Src="Content/TinyMCE.ascx" TagName="TinyMCE" TagPrefix="uc3" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSViewOptions" Src="~/Admin/Content/BSViewOptions.ascx" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSGridViewHeader" Src="~/Admin/Content/BSGridViewHeader.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <script type="text/javascript">
        $("#mnPages").attr("class", "current");
    </script>
    <uc1:MessageBox ID="MessageBox1" Type="Information" runat="server" />
    <div id="wrapper">
        <div class="content">
            <div id="divPosts" runat="server">
                <div class="rightnow">
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Pages"] %></span>
                        <br />
                    </h3>
                    &nbsp;
                    <BlogsaUserControls:BSViewOptions runat="server" ID="voDefault" />
                    <BlogsaUserControls:BSGridViewHeader ID="gvhDefault" Search="false" runat="server" />
                    <asp:GridView ID="gvPosts" runat="server" CssClass="bstable" AutoGenerateColumns="False"
                        BorderWidth="0px" GridLines="None" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
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
                                    <%=Language.Admin["Date"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <span title="<%#Eval("Date") %>">
                                        <%# ((DateTime)Eval("Date")).ToShortDateString() %></span>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
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
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["Show"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#(Int16)Eval("Show")==0?Language.Admin["Both"]:(Int16)Eval("Show")==1?Language.Admin["OnlySideBar"]:(Int16)Eval("Show")==2?Language.Admin["OnlyMenu"]:Language.Admin["AnyOne"] %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["State"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#BSHelper.GetPostState(Eval("State").ToString(),(DateTime)Eval("Date")) %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <strong>
                                <%=Language.Admin["PageNotFound"] %></strong>
                        </EmptyDataTemplate>
                        <PagerTemplate>
                            <uc2:Pager ID="Pager1" runat="server" />
                        </PagerTemplate>
                        <HeaderStyle CssClass="thead" />
                        <PagerSettings Position="Top" />
                    </asp:GridView>
                </div>
            </div>
            <div id="divAddPost" visible="false" runat="server">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["EditPage"] %></span>
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
                    <uc3:TinyMCE ID="tmcePageContent" runat="server" />
                </div>
            </div>
        </div>
        <div class="sidebar">
            <div id="divAddPostSide" runat="server" visible="false">
                <asp:DropDownList ID="ddState" runat="server" Width="100%">
                    <asp:ListItem Selected="True" Value="1"></asp:ListItem>
                    <asp:ListItem Value="0"></asp:ListItem>
                </asp:DropDownList>
                <br />
                <br />
                <div class="sidecontent">
                    <asp:CheckBox ID="cblAddComment" runat="server" Checked="True" />
                </div>
                <br />
                <br />
                <asp:LinkButton runat="server" ID="btnSavePage" OnClick="btnSavePage_Click" CssClass="bsbutton green block"><span><%=Language.Admin["Save"] %></span></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="toppanel">
    <div id="top-panel">
        <div id="panel">
            <ul>
                <li><a href="Pages.aspx" class="pages">
                    <%=Language.Admin["Pages"] %></a></li>
                <li><a href="Add.aspx?p=Page" class="new-page">
                    <%=Language.Admin["AddNewPage"] %></a></li>
            </ul>
        </div>
    </div>
</asp:Content>
