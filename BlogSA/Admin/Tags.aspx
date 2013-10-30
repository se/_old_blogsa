<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Tags.aspx.cs" Inherits="Admin_Tags" %>

<%@ Register Src="Content/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="Content/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSViewOptions" Src="~/Admin/Content/BSViewOptions.ascx" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSGridViewHeader" Src="~/Admin/Content/BSGridViewHeader.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <script type="text/javascript">
        $("#mnTags").attr("class", "current");
    </script>
    <uc2:MessageBox ID="MessageBox1" runat="server" />
    <div id="wrapper">
        <div class="content">
            <div id="divPosts" runat="server">
                <div class="rightnow">
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Tags"] %></span>
                        <br />
                    </h3>
                    &nbsp;
                    <BlogsaUserControls:BSViewOptions runat="server" ID="voDefault" />
                    <BlogsaUserControls:BSGridViewHeader ID="gvhDefault" runat="server" />
                    <asp:GridView ID="gvItems" runat="server" CssClass="bstable" AutoGenerateColumns="False" BorderWidth="0px"
                        GridLines="None" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                        OnRowCreated="gv_RowCreated" OnDataBinding="gvItems_DataBinding">
                        <Columns>
                            <asp:TemplateField HeaderText="Se&#231;">
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:HepsiniSec(this);" type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb" onclick="javascript:SecimKontrol(this);" runat="server" />
                                    <asp:Literal runat="server" Text='<%#Eval("TermID") %>' Visible="false" ID="TermID"></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle Width="10%" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["Name"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href='<%#Eval("TermID","?TermID={0}") %>'>
                                        <asp:Literal ID="lName" runat="server" Text='<%# Eval("Name") %>' /></a>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Width="90%" />
                            </asp:TemplateField>
                        </Columns>
                        <PagerTemplate>
                            <uc1:Pager ID="Pager1" runat="server" />
                        </PagerTemplate>
                        <HeaderStyle CssClass="thead" />
                        <PagerSettings Position="Top" />
                        <EmptyDataTemplate>
                            <strong>
                                <%=Language.Admin["TagNotFound"] %></strong></EmptyDataTemplate>
                    </asp:GridView>
                </div>
            </div>
            <div id="divEditTerm" runat="server">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["EditTag"] %></span></div>
                <div class="rightnow">
                    <h3 class="reallynow">
                        <span>
                            <%=Language.Admin["Name"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox CssClass="txtbox" ID="txtCatName" runat="server"></asp:TextBox>&nbsp;</p>
                </div>
            </div>
        </div>
        <div class="sidebar">
            <div id="divAddTerm" runat="server" visible="true">
                <div class="title">
                    <%=Language.Admin["Name"] %></div>
                <div class="sidecontent">
                    <asp:TextBox ID="txtName" runat="server" CssClass="sidetxtbox"></asp:TextBox></div>
                <asp:LinkButton runat="server" ID="btnAdd" CssClass="bsbtn green block" OnClick="btnAdd_Click"><%=Language.Admin["AddTag"] %></asp:LinkButton>
            </div>
            <div id="divSideEditTerm" visible="false" runat="server">
                <div class="title">
                    <%=Language.Admin["EditTag"] %></div>
                <br />
                <asp:LinkButton runat="server" ID="btnSave" CssClass="bsbtn green block" OnClick="btnSave_Click"><%=Language.Admin["Save"] %></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="toppanel">
    <div id="top-panel">
        <div id="panel">
            <ul>
                <li><a href="Tags.aspx" class="tags">
                    <%=Language.Admin["Tags"] %></a></li>
            </ul>
        </div>
    </div>
</asp:Content>
