<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Categories.aspx.cs" Inherits="Admin_Categories" %>

<%@ Register Src="Content/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="Content/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSViewOptions" Src="~/Admin/Content/BSViewOptions.ascx" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSGridViewHeader" Src="~/Admin/Content/BSGridViewHeader.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <script type="text/javascript">
        $("#mnCategories").attr("class", "current");
    </script>
    <uc2:MessageBox ID="MessageBox1" runat="server" />
    <div id="wrapper">
        <div class="content">
            <div id="divPosts" runat="server">
                <div class="rightnow">
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Categories"]%></span>
                        <br />
                    </h3>
                    &nbsp;
                    <BlogsaUserControls:BSViewOptions runat="server" ID="voDefault" />
                    <BlogsaUserControls:BSGridViewHeader ID="gvhDefault" runat="server" />
                    <asp:GridView ID="gvItems" runat="server" CssClass="bstable" AutoGenerateColumns="False" BorderWidth="0px"
                        GridLines="None" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                        OnRowCreated="gv_RowCreated" OnDataBinding="gvItems_DataBinding">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:HepsiniSec(this);" type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb" onclick="javascript:SecimKontrol(this);" runat="server" />
                                    <asp:Literal runat="server" Text='<%#Eval("TermID") %>' Visible="false" ID="TermID"></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["Name"]%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href='<%#Eval("TermID","?TermID={0}") %>'>
                                        <asp:Literal ID="lName" runat="server" Text='<%# Eval("Name") %>' /></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["CategoryType"]%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("Type") %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["Description"]%>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal ID="lDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <strong>
                                <%=Language.Admin["CategoryNotFound"]%></strong>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="thead" />
                        <PagerTemplate>
                            <uc1:Pager ID="Pager1" runat="server" />
                        </PagerTemplate>
                        <PagerSettings Position="Top" />
                    </asp:GridView>
                </div>
            </div>
            <div id="divEditTerm" runat="server">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["EditCategory"]%></span></div>
                <div class="rightnow">
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Name"]%></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox CssClass="txtbox" ID="txtCatName" runat="server"></asp:TextBox>&nbsp;</p>
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Description"]%></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox CssClass="txtbox" ID="txtCatDescription" runat="server" Height="70px"
                            TextMode="MultiLine"></asp:TextBox>&nbsp;</p>
                </div>
            </div>
        </div>
        <div class="sidebar">
            <div id="divAddTerm" runat="server" visible="true">
                <div class="title">
                    <%=Language.Admin["Name"]%></div>
                <div class="sidecontent">
                    <asp:TextBox ID="txtName" CssClass="sidetxtbox" runat="server"></asp:TextBox></div>
                <div class="title">
                    <%=Language.Admin["ParentCategory"]%></div>
                <div class="sidecontent">
                    <asp:DropDownList runat="Server" Width="170px" ID="ddlParentCategory">
                    </asp:DropDownList>
                </div>
                <div class="title">
                    <%=Language.Admin["Description"]%></div>
                <div class="sidecontent">
                    <asp:TextBox ID="txtDescription" runat="server" CssClass="sidetxtbox" Height="63px"
                        TextMode="MultiLine"></asp:TextBox></div>
                <asp:LinkButton ID="btnAdd" runat="server" OnClick="btnAdd_Click" CssClass="bsbutton block green"><span><%=Language.Admin["Save"]%></span></asp:LinkButton>&nbsp;
            </div>
            <div id="divSideEditTerm" visible="false" runat="server">
                <div class="title">
                    <%=Language.Admin["EditCategory"]%></div>
                <br />
                <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click" CssClass="bsbutton block green"><span><%=Language.Admin["Save"]%></span></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="toppanel">
    <div id="top-panel">
        <div id="panel">
            <ul>
                <li><a href="Categories.aspx" class="categories">
                    <%=Language.Admin["Categories"] %></a></li>
                <li><a href="Categories.aspx?type=linkcategory" class="categories">
                    <%=Language.Admin["LinkCategory"] %></a></li>
            </ul>
        </div>
    </div>
</asp:Content>
