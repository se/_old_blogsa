<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Menus.aspx.cs" Inherits="Admin_Menus" %>

<%@ Register Src="Content/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="Content/Pager.ascx" TagName="Pager" TagPrefix="uc1" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSViewOptions" Src="~/Admin/Content/BSViewOptions.ascx" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSGridViewHeader" Src="~/Admin/Content/BSGridViewHeader.ascx" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="LanguagePicker" Src="~/Admin/Content/LanguagePicker.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="toppanel" runat="Server">
    <script type="text/javascript">
        $("#mnMenus").attr("class", "current");

        $(document).ready(function () {
            $(".menu-add-menu-items ul").sortable({
                'update': function (a, b) {
                    loadMenuItems();
                }
            });

            loadMenuItems();

            function loadMenuItems() {
                var menu = $(".menu-add-menu-items ul li");
                var sort = "";
                for (var i = 0; i < menu.size() ; i++) {
                    sort += $(menu[i]).attr("id").slice(9) + ";";
                }

                $("#<%=hfMenuItems.ClientID %>").val(sort);
            }
        });
    </script>
    <div id="top-panel">
        <div id="panel">
            <ul>
                <%if (User.IsInRole("admin"))
                  {%>
                <li><a href="?" class="posts">
                    <%=Language.Admin["Menus"]%></a></li>
                <li><a href="?p=AddMenu" class="new-post">
                    <%=Language.Admin["AddMenu"]%></a></li>
                <%} %>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">
    <uc2:MessageBox ID="MessageBox1" runat="server" />
    <div id="wrapper">
        <div class="content">
            <div id="divMenus" runat="server">
                <div class="rightnow">
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Menus"]%></span>
                        <br />
                    </h3>
                    &nbsp;
                    <BlogsaUserControls:BSViewOptions runat="server" ID="voDefault" />
                    <BlogsaUserControls:BSGridViewHeader ID="gvhDefault" runat="server" />
                    <asp:GridView ID="gvMenus" runat="server" AutoGenerateColumns="False" CssClass="bstable"
                        GridLines="None" AllowPaging="True" OnPageIndexChanging="gv_PageIndexChanging"
                        OnRowCreated="gv_RowCreated" OnDataBinding="gv_DataBinding">
                        <PagerSettings Position="Top" />
                        <Columns>
                            <asp:TemplateField ItemStyle-CssClass="tcc" HeaderStyle-CssClass="tcc">
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:HepsiniSec(this);" type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb" onclick="javascript:SecimKontrol(this);" runat="server" />
                                    <asp:Literal runat="server" Text='<%#Eval("MenuGroupID") %>' Visible="false" ID="ltMenuGroupID"></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle CssClass="bstable-select-header" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["Title"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href='<%#Eval("MenuGroupID","?MenuID={0}") %>'>
                                        <%#Eval("Title") %></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["Description"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%#Eval("Description") %>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["MenuItem"] %>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%#((BSMenuGroup)Container.DataItem).Menu.Count %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <%=Language.Admin["Default"] %>
                                </HeaderTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <%#(Boolean)Eval("Default") ? Language.Admin["Yes"] : String.Empty%>
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
                            <%=Language.Admin["MenuNotFound"] %>
                        </EmptyDataTemplate>
                        <PagerTemplate>
                            <uc1:Pager ID="Pager1" runat="server" />
                        </PagerTemplate>
                        <HeaderStyle CssClass="thead" />
                    </asp:GridView>
                </div>
            </div>
            <div id="divSaveMenu" runat="server" visible="false">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["SaveMenu"] %></span>
                </div>
                <div class="rightnow">
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Title"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox ID="txtTitle" runat="server" Width="705px"></asp:TextBox>
                        <br />
                        <span style="font-size: 10px; color: gray; font-style: italic;"></span>
                    </p>
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Description"] %></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox ID="txtDescription" TextMode="MultiLine" runat="server" Width="705px"></asp:TextBox>
                        <br />
                        <span style="font-size: 10px; color: gray; font-style: italic;"></span>
                    </p>
                    <div runat="server" id="divMenuItems">
                        <h3 class="reallynow"><span>
                            <%=Language.Admin["MenuItems"] %></span>
                            <br />
                        </h3>
                        <div class="youhave">
                            <span style="display: block; padding: 0px 10px 5px 10px;">Öğelerin sırasını değiştirmek
                                için sürükleyip bırakabilir, düzenlemek için menü öğesine tıklayabilirsiniz.
                            </span>
                            <div class="menu-add-menu-items">
                                <ul>
                                    <asp:Repeater runat="server" ID="rpMenuItems" OnItemCommand="rpMenuItems_ItemCommand">
                                        <ItemTemplate>
                                            <li id="menuitem_<%#Eval("MenuID") %>">
                                                <asp:LinkButton ID="lbDelete" CommandName="DeleteMenuItem" CommandArgument='<%#Eval("MenuID") %>'
                                                    runat="server" CssClass="sbtn bsred"><span>&nbsp;</span></asp:LinkButton><a href="?MenuID=<%#Eval("MenuGroupID") %>&ItemID=<%#Eval("MenuID") %>">
                                                        <%#Eval("Title") %></a></li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                                <asp:HiddenField runat="server" ID="hfMenuItems" />
                            </div>
                            <div class="menu-menu-items">
                                <div class="menu-menu-items-title">
                                    Yeni Menü Öğesi Ekle
                                </div>
                                <div class="frmRow">
                                    <label>
                                        Başlık
                                    </label>
                                    <asp:TextBox runat="server" CssClass="txtinput" ID="txtMenuTitle" />
                                </div>
                                <div class="frmRow">
                                    <label>
                                        Açıklama
                                    </label>
                                    <asp:TextBox runat="server" CssClass="txtinput" ID="txtMenuDescription" />
                                </div>
                                <div class="frmRow">
                                    <label>
                                        Adres
                                    </label>
                                    <asp:TextBox runat="server" CssClass="txtinput" ID="txtMenuUrl" />
                                </div>
                                <div class="frmRow">
                                    <label>
                                        Hedef
                                    </label>
                                    <asp:TextBox runat="server" CssClass="txtinput" ID="txtMenuTarget" />
                                </div>
                                <div class="frmRow">
                                    <br />
                                    <asp:LinkButton Width="60px" CssClass="bsbutton green block" runat="server" ID="btnAddMenuItem"
                                        OnClick="btnAddMenuItem_Click"><span>Ekle</span></asp:LinkButton>
                                </div>
                            </div>
                            <div class="clear">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="sidebar">
            <div id="divSaveMenuSide" runat="server" visible="false">
                <asp:CheckBox ID="cbxDefault" runat="server" />
                &nbsp;<br /><br />
                <BlogsaUserControls:LanguagePicker runat="server" ID="bsucDefault" />
                &nbsp;
                <asp:LinkButton ID="btnSaveMenu" runat="server" CssClass="bsbutton green block" OnClick="btnSaveMenu_Click"><span><%=Language.Admin["Save"] %></span></asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>
