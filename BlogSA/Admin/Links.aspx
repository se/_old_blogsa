<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Links.aspx.cs" Inherits="Admin_Links" %>

<%@ Register Src="Content/Categories.ascx" TagName="Categories" TagPrefix="uc3" %>
<%@ Register Src="Content/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="Content/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSGridViewHeader" Src="~/Admin/Content/BSGridViewHeader.ascx" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSViewOptions" Src="~/Admin/Content/BSViewOptions.ascx" %>
<%@ Register TagPrefix="blogsausercontrols" TagName="languagepicker" Src="~/Admin/Content/LanguagePicker.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cph1" runat="Server">
    <script type="text/javascript">
        $("#mnLinks").attr("class", "current");
    </script>
    <uc2:MessageBox ID="MessageBox1" Type="Information" runat="server" />
    <div id="wrapper">
        <div class="content">
            <div id="divPosts" runat="server">
                <div class="rightnow">
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Links"] %></span>
                        <br />
                    </h3>
                    &nbsp;
                    <blogsausercontrols:BSViewOptions runat="server" ID="voDefault" />
                    <blogsausercontrols:BSGridViewHeader ID="gvhDefault" Search="false" runat="server" />
                    <asp:GridView ID="gvLinks" CssClass="bstable" runat="server" AutoGenerateColumns="False"
                        BorderWidth="0px"
                        GridLines="None" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                        OnRowCreated="gv_RowCreated" OnDataBinding="gvLinks_DataBinding">
                        <PagerSettings Position="Top" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:HepsiniSec(this);" type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb" onclick="javascript:SecimKontrol(this);" runat="server" />
                                    <asp:Literal runat="server" Text='<%#Eval("LinkID") %>' Visible="false" ID="LinkID"></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Başlık">
                                <ItemTemplate>
                                    <a href='<%#Eval("LinkID","?LinkID={0}") %>'>
                                        <asp:Literal ID="lName" runat="server" Text='<%# Eval("Name") %>' /></a>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <%=Language.Admin["Title"] %>
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Literal ID="lURL" runat="server" Text='<%#Eval("Url") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderTemplate>
                                    <%=Language.Admin["Url"] %>
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Literal ID="lDescription" runat="server" Text='<%#Eval("Description") %>'></asp:Literal>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <%=Language.Admin["Description"] %>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["Target"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Literal runat="server" Text='<%#Eval("Target") %>'></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <strong>
                                <%=Language.Admin["LinkNotFound"] %></strong>
                        </EmptyDataTemplate>
                        <PagerTemplate>
                            <uc1:Pager ID="Pager1" runat="server" />
                        </PagerTemplate>
                        <HeaderStyle CssClass="thead" />
                    </asp:GridView>
                </div>
            </div>
            <div id="divAddLink" runat="server">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["EditLink"] %></span>
                </div>
                <div class="rightnow">
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Title"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox ID="txtLinkTitle" runat="server" Width="705px"></asp:TextBox>
                    </p>
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Url"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox ID="txtLinkURL" runat="server" Text="http://" Width="705px"></asp:TextBox>
                    </p>
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Description"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox ID="txtLinkDescription" runat="server" Width="705px"></asp:TextBox>
                    </p>
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Target"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:RadioButtonList ID="rblLinkTarget" runat="server" RepeatDirection="Horizontal"
                            RepeatLayout="Flow">
                            <asp:ListItem Value="" Selected="True"></asp:ListItem>
                            <asp:ListItem>_top</asp:ListItem>
                            <asp:ListItem>_blank</asp:ListItem>
                        </asp:RadioButtonList>
                        &nbsp;
                    </p>
                </div>
            </div>
        </div>
        <div class="sidebar">
            <div id="divAddPostSide" runat="server" visible="false">
                <blogsausercontrols:languagepicker runat="server" ID="lpLinkLanguage" />
                <uc3:Categories runat="Server" ID="Categories1" />
                <asp:LinkButton runat="server" ID="btnSaveLink" OnClick="btnSaveLink_Click" CssClass="bsbutton green block"><span><%=Language.Admin["Save"] %></span></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="toppanel">
    <div id="top-panel">
        <div id="panel">
            <ul>
                <li><a href="Links.aspx" class="links">
                    <%=Language.Admin["Links"] %></a></li>
                <li><a href="Add.aspx?p=Link" class="new-link">
                    <%=Language.Admin["AddNewLink"] %></a></li>
            </ul>
        </div>
    </div>
</asp:Content>
