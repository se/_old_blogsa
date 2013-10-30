<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Design.aspx.cs" Inherits="Admin_Design" %>

<%@ Register Src="Content/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<%@ Register Src="Content/EditControl.ascx" TagName="EditControl" TagPrefix="Blogsa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="toppanel" runat="Server">
    <script type="text/javascript">
        $("#mnView").attr("class", "current");
    </script>
    <div id="top-panel">
        <div id="panel">
            <ul>
                <%if (Page.User.IsInRole("admin"))
                  {%>
                <li><a href="Design.aspx" class="widgets">
                    <%=Language.Admin["Design"]%></a></li>
                <li><a href="?p=Settings" class="setting">
                    <%=Language.Admin["Settings"]%></a></li>
                <% }%>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">
    <uc2:MessageBox ID="MessageBox1" runat="server" />
    <div id="wrapper">
        <div class="content">
            <div id="divSettings" runat="server" style="clear: both;">
                <div class="ctl-box">
                    <div class="ctl-box-title">
                        <asp:Literal ID="ltThemeName" runat="server" />
                        - Theme Settings
                    </div>
                    <asp:PlaceHolder runat="server" ID="phThemeSettings" />
                    &nbsp;
                </div>
            </div>
            <div id="divCurrent" runat="server">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["CurrentTheme"] %></span>
                </div>
                <div class="rightnow" style="clear: both;">
                    <asp:Label runat="server" ID="lblThemeError" Visible="false" Style="padding-right: 10px;
                        display: block; padding-left: 10px; padding-bottom: 10px; padding-top: 10px"></asp:Label>
                    <asp:Repeater runat="server" ID="rpTheme">
                        <ItemTemplate>
                            <h3 class="reallynow">
                                <span>
                                    <%#Eval("name") %></span><%if (BSTheme.Current.Settings.Count > 0)
                                                               { %>
                                <a href="?p=Settings" class="setting"><%=Language.Admin["Settings"] %></a>
                                <%} %>
                                <br />
                            </h3>
                            <p class="youhave">
                                <img style="border: 3px solid gray; margin: 0px 5px 5px 0px; float: left; clear: both;"
                                    src='<%#"../Themes/"+Eval("folder")+"/"+Eval("screenshot") %>' alt='<%#Eval("name") %>' />
                                <b>
                                    <%=Language.Admin["ThemeName"] %>
                                    : </b>
                                <%#Eval("name") %><br />
                                <b>
                                    <%=Language.Admin["Version"] %>
                                    : </b>
                                <%#Eval("version") %><br />
                                <b>
                                    <%=Language.Admin["ThemeCreator"] %>
                                    : </b>
                                <%#Eval("author") %><br />
                                <b>
                                    <%=Language.Admin["WebSite"] %>
                                    : </b><a target="_blank" href='<%#Eval("website") %>'>
                                        <%#Eval("website") %></a><br />
                                <b>
                                    <%=Language.Admin["UpgradeAddress"] %>
                                    : </b><a target="_blank" href='<%#Eval("updateurl") %>'>
                                        <%#Eval("updateurl") %></a>
                                <br style="clear: both;" />
                                <div style="display: block; margin: 10px;">
                                    <b>
                                        <%=Language.Admin["Description"] %>
                                        : </b>
                                    <%#Eval("description") %><br />
                                </div>
                            </p>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <br />
            <div id="divThemes" runat="server" style="clear: both;">
                <div class="contenttitle">
                    <span>
                        <%=Language.Admin["UsableThemes"] %></span>
                </div>
                <div class="rightnow">
                    <asp:Repeater runat="server" ID="rpAllThemes" OnItemCommand="rpAllThemes_ItemCommand">
                        <ItemTemplate>
                            <div style="width: 215px; float: left; text-align: center; padding: 15px;">
                                <img src='../Themes/<%#Eval("Folder") %>/<%#Eval("Screenshot") %>' alt="" style="border: 3px solid gray;
                                    width: 200px; height: 130px; background-image: url('../Resources/Images/ThemePreview.jpg')" />
                                <br />
                                <strong>
                                    <%#Eval("Name") %></strong>
                                <br />
                                <br />
                                <a target="_blank" href="<%=Blogsa.Url %>?theme=<%#Eval("Name") %>"
                                    class="bsbutton dark"><span>
                                        <%=Language.Admin["Show"] %></span></a>
                                <asp:LinkButton runat="server" ID="btnUseTheme" CssClass="bsbutton" CommandName="UseTheme"
                                    CommandArgument='<%# ((BSTheme)Container.DataItem).Folder %>'><span><%=Language.Admin["UseThisTheme"] %></span></asp:LinkButton>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
        <div class="sidebar">
            <div id="divThemesSide" runat="server">
                <div class="title">
                    <%=Language.Admin["SaveSettings"] %>
                </div>
                <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="bsbutton green block"><span><%=Language.Admin["Save"] %></span></asp:LinkButton><br />
                &nbsp;
            </div>
        </div>
    </div>
</asp:Content>
