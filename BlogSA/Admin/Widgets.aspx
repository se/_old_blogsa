<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Widgets.aspx.cs" Inherits="Admin_Widgets" %>

<%@ Register Src="Content/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="Content/TinyMCE.ascx" TagName="TinyMCE" TagPrefix="uc1" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSGridViewFooter" Src="~/Admin/Content/BSGridViewFooter.ascx" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSViewOptions" Src="~/Admin/Content/BSViewOptions.ascx" %>
<%@ Register TagPrefix="BlogsaUserControls" TagName="BSGridViewHeader" Src="~/Admin/Content/BSGridViewHeader.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="toppanel" runat="Server">
    <script type="text/javascript">
        $("#mnWidgets").attr("class", "current");

        $(document).ready(function () {

            $('.divSelectNormal a').click(function () {
                var ph = $(this).parent().parent().find('.divSelectable select').val();
                if (ph != null) {
                    $(this).parent().hide();
                    $(this).parent().parent().find('.divSelectable').show();
                }
                else {
                    alert('<%=Language.Admin["NoPlaceHolder"] %>');
                }
            });

            $('.divSelectable .aSave').click(function () {
                var ph = $(this).parent().find('select').val();
                var widgetid = $(this).parent().find('input').val();
                if (ph != null) {
                    var otheritem = $(this).parent();
                    var item = $(this).parent().parent().find('.divSelectNormal');
                    var loading = $(this).parent().parent().find('.loading');
                    $(otheritem).hide();
                    $(loading).show();
                    $.ajax({
                        type: "GET",
                        url: "Control/Ajax.ashx",
                        data: "act=saveplace&placename=" + ph + "&widgetid=" + widgetid
                   , success: function (data) {
                       $(item).show();
                       $(otheritem).hide();
                       $(item).find('span').html(ph + '&nbsp;');
                       $(loading).hide();
                   }, error: function (xhr) {
                       alert(xhr.status);
                       alert(xhr.statusText);
                   }
                    });
                }
            });
            $('.divSelectable .aCancel').click(function () {
                $(this).parent().hide();
                $(this).parent().parent().find('.divSelectNormal').show();
            });

            $("#ulWidgets").sortable();

            $("#btnSaveSort").click(function () {

                var items = $("#ulWidgets li");
                var sended = "";
                for (var i = 0; i < items.length; i++) {
                    sended += items[i].id;
                    sended += "|";
                }

                $("#WWDMessage").ajaxStart(function () {
                    $(this).html('<img style="width:16px;height:16px;vertical-align:middle;" src="Images/Loading.gif" /> <%=Language.Admin["Saving"] %>');
                });

                $.ajax({
                    type: "GET",
                    url: "Control/Ajax.ashx?act=savesortlist",
                    data: "sortlist=" + sended,
                    error: function () {
                        $("#WWDMessage").html('<span style="color:#ff0000"><%=Language.Admin["WidgetError"] %></span>');
                    },
                    success: function (data) { $("#WWDMessage").text('');/*location = "Widgets.aspx";*/ }
                });
            });
        });
    </script>
    <div id="top-panel">
        <div id="panel">
            <ul>
                <li><a href="Widgets.aspx" class="widgets">
                    <%=Language.Admin["Widgets"] %></a></li>
                <li><a href="Widgets.aspx?p=AddWidget" class="new-widget">
                    <%=Language.Admin["AddWidget"] %></a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">
    <uc2:MessageBox ID="MessageBox1" runat="server" />
    <div id="wrapper">
        <div class="content">
            <div class="contenttitle">
                <span>
                    <%=Language.Admin["Widgets"] %></span>
            </div>
            <div id="divEditWidget" runat="Server" visible="false">
                <div class="rightnow">
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Title"]%></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:TextBox CssClass="txtbox" ID="txtTitle" runat="server"></asp:TextBox>
                    </p>
                    <div id="divWidgetContent" runat="Server" visible="false">
                        <h3 class="reallynow"><span>
                            <%=Language.Admin["Content"]%></span>
                            <br />
                        </h3>
                        <uc1:TinyMCE ID="tmceDescription" runat="server" />
                    </div>
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["Place"]%></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:DropDownList runat="Server" ID="ddlPlace">
                        </asp:DropDownList>
                    </p>
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["State"]%></span>
                        <br />
                    </h3>
                    <p class="youhave">
                        <asp:DropDownList runat="Server" ID="ddlVisible">
                        </asp:DropDownList>
                    </p>
                </div>
            </div>
            <div id="divWidgets" runat="server" visible="false">
                <div class="rightnow">
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["UsingWidgets"] %></span>
                        <br />
                    </h3>
                    <BlogsaUserControls:BSViewOptions runat="server" ID="voDefault" />
                    <BlogsaUserControls:BSGridViewHeader ID="gvhDefault" Search="false" runat="server" />
                    <asp:GridView ID="gvWidgets" runat="server" PageSize="10" AutoGenerateColumns="False"
                        CssClass="bstable" OnRowCommand="gvWidgets_RowCommand" OnPreRender="gvWidgets_PreRender">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input id="chkAll" onclick="javascript:HepsiniSec(this);" type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cb" onclick="javascript:SecimKontrol(this);" runat="server" />
                                    <asp:Literal runat="server" Text='<%#Eval("WidgetID") %>' Visible="false" ID="WidgetID"></asp:Literal>
                                </ItemTemplate>
                                <ItemStyle CssClass="bstable-select-header" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle Width="35%" />
                                <HeaderTemplate>
                                    <%=Language.Admin["Title"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <a href="?WidgetID=<%#Eval("WidgetID") %>">
                                        <%# Eval("Title") %>
                                    </a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle Width="20%" />
                                <HeaderTemplate>
                                    <%=Language.Admin["FolderName"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# Eval("FolderName") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle Width="25%" />
                                <HeaderTemplate>
                                    <%=Language.Admin["Place"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <div class="divSelectNormal">
                                        <a href="javascript:;">
                                            <asp:Label runat="server" ID="lblPlace" Text='<%# Eval("PlaceHolder") %>'>
                                            </asp:Label></a>
                                    </div>
                                    <div class="divSelectable" style="display: none">
                                        <asp:Literal ID="ltSelectPlace" runat="server"></asp:Literal>
                                        <a class="aCancel" style="vertical-align: middle" href="javascript:;">
                                            <img alt="" src="Images/Icons/cancel.png" /></a><a class="aSave" style="vertical-align: middle"
                                                href="javascript:;">
                                                <img alt="" src="Images/save.png" /></a>
                                        <input type="hidden" value='<%#Eval("WidgetID") %>' />
                                    </div>
                                    <div class="loading" style="display: none">
                                        <img style="width: 16px; height: 16px; vertical-align: middle;" src="Images/Loading.gif" />
                                        <%=Language.Admin["Saving"] %>
                                    </div>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle Width="15%" />
                                <HeaderTemplate>
                                    <%=Language.Admin["State"] %>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# (bool)Eval("Visible")?Language.Admin["Active"]:Language.Admin["Passive"] %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                        </Columns>
                        <EmptyDataTemplate>
                            <%=Language.Admin["WidgetNotFound"] %>
                        </EmptyDataTemplate>
                        <HeaderStyle CssClass="thead" />
                        <PagerStyle CssClass="en" />
                        <PagerTemplate>
                            <BlogsaUserControls:BSGridViewFooter runat="server" />
                        </PagerTemplate>
                        <RowStyle CssClass="sortrow" />
                        <AlternatingRowStyle CssClass="alternate sortrow"/>
                    </asp:GridView>
                </div>
                <br />
                <div class="rightnow">
                    <h3 class="reallynow"><span>
                        <%=Language.Admin["AllWidgets"] %></span>
                        <br />
                    </h3>
                    <asp:Repeater ID="rpAllWidgets" runat="server" OnItemDataBound="rpAllWidgets_ItemDataBound"
                        OnItemCommand="rpAllWidgets_ItemCommand">
                        <ItemTemplate>
                            <div class="Widget">
                                <div class="Left">
                                    <img alt="" src='../Widgets/<%#Eval("Name") %>/screenshot.jpg' />
                                </div>
                                <div class="Right">
                                    <b>
                                        <%=Language.Admin["WidgetName"] %>
                                        : </b>
                                    <asp:Literal ID="name" runat="server" /><br />
                                    <b>
                                        <%=Language.Admin["Description"] %>
                                        : </b>
                                    <asp:Literal ID="description" runat="server" /><br />
                                    <b>
                                        <%=Language.Admin["Version"] %>
                                        : </b>
                                    <asp:Literal ID="version" runat="server" /><br />
                                    <b>
                                        <%=Language.Admin["WidgetCreator"] %>
                                        : </b>
                                    <asp:Literal ID="author" runat="server" /><br />
                                    <b>
                                        <%=Language.Admin["WebSite"] %>
                                        : </b>
                                    <asp:Literal ID="website" runat="server" /><br />
                                    <b>
                                        <%=Language.Admin["UpgradeAddress"] %>
                                        : </b>
                                    <asp:Literal ID="updateurl" runat="server" /><br />
                                    <b>
                                        <%=Language.Admin["FolderName"] %>
                                        : </b>
                                    <asp:Literal Text='<%#Eval("Name") %>' ID="foldername" runat="server" /><br />
                                </div>
                                <div class="Process">
                                    <a target="_blank" href="<%=Blogsa.Url%>?widget=<%#Eval("Name") %>" class="bsbtn small">
                                        <%=Language.Admin["Show"] %></a>
                                    <asp:LinkButton runat="server" ID="btnAdd" CssClass="bsbtn small green" CommandName="Add"
                                        CommandArgument='<%#Eval("Name") %>'><%=Language.Admin["Add"] %></asp:LinkButton>
                                </div>
                                <div style="clear: both">
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="sidebar">
            <div id="divEditWidgetSide" runat="Server" visible="false">
                <asp:LinkButton ID="btnSaveWidget" CssClass="bsbtn block green" runat="server" OnClick="btnSaveWidget_Click"><%=Language.Admin["Save"] %></asp:LinkButton>
            </div>
            <div id="divWidgetsSide" runat="server" visible="false">
                <div class="title">
                    <%=Language.Admin["FastSorting"] %>
                </div>
                <div id="WWDMessage">
                </div>
                <div class="WDD">
                    <ul id="ulWidgets">
                        <%=Admin.GetWidgets() %>
                    </ul>
                    <br />
                    <a href="#" id="btnSaveSort" class="bsbtn green block">
                        <%=Language.Admin["Save"] %></a>
                </div>
                <br />
                <span style="font-size: 11px; color: gray; font-style: italic;">
                    <%=Language.Admin["SortingDescription"] %>
                </span>
            </div>
        </div>
    </div>
</asp:Content>
