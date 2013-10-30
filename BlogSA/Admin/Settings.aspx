<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Settings.aspx.cs" Inherits="Admin_Settings" %>

<%@ Register Src="Content/MessageBox.ascx" TagName="MessageBox" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="toppanel" runat="Server">
    <script type="text/javascript">
        $("#mnSettings").attr("class", "current");
    </script>
    <div id="top-panel">
        <div id="panel">
            <ul>
                <li><a href="?p=main" class="setting">
                    <%=Language.Admin["BlogSettings"]%></a></li>
                <li><a href="?p=perma" class="setting">
                    <%=Language.Admin["PermalinkSettings"]%></a></li>
                <li><a href="?p=postcomment" class="setting">
                    <%=Language.Admin["PostCommentSettings"]%></a></li>
                <li><a href="?p=paging" class="setting">
                    <%=Language.Admin["PagingSettings"]%></a></li>
                <li><a href="?p=mail" class="setting">
                    <%=Language.Admin["MailSettings"]%></a></li>
                <li><a href="?p=library" class="setting">
                    <%=Language.Admin["LibrarySettings"]%></a></li>
            </ul>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">
    <uc2:MessageBox ID="MessageBox1" runat="server" />
    <div id="wrapper">
        <div class="content">
            <div id="divSettings" runat="server">
                <div class="contenttitle">
                    <asp:Label runat="server" ID="lblTitle"></asp:Label></div>
                <div class="rightnow" runat="Server" id="divControls">
                    <div runat="Server" id="divMainSettings" visible="false">
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["BlogName"]%></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtblog_name" runat="server"></asp:TextBox>&nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["BlogNameDescription"] %>
                            </span>
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["BlogDescription"]%></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtblog_description" runat="server"></asp:TextBox>&nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["BlogDescriptionDescription"]%>
                            </span>
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["BlogLanguage"]%></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:DropDownList runat="Server" ID="ddllanguage">
                            </asp:DropDownList>
                            &nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["BlogLanguageDescription"]%>
                            </span>
                        </p>
                        <h3 class="reallynow">
                            <span><%=Language.Admin["DefaultAdminLanguage"] %></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:DropDownList runat="Server" ID="ddladmin_language">
                            </asp:DropDownList>
                            &nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["BlogLanguageDescription"]%>
                            </span>
                        </p>
                        <h3 class="reallynow">
                            <span><%=Language.Admin["MultipleLanguageSelect"]%></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:DropDownList runat="Server" ID="ddlmultilanguage">
                                <asp:ListItem Value="1"></asp:ListItem>
                                <asp:ListItem Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<br />
                            <span class="descinfo">İçeriklerinizi birden fazla dilde yayınlayabilmenize olanak sağlar.
                                Örn:
                                <%=Blogsa.Url + "tr/ornek-yazi.aspx" %>
                            </span>
                        </p>
                    </div>
                    <div id="divPermaLinkSettings" runat="server" visible="false">
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["BlogPermalink"]%></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:RadioButtonList runat="Server" ID="rblpermalink">
                                <asp:ListItem Value="{name}.aspx">Default</asp:ListItem>
                                <asp:ListItem Value="{id}.aspx">Numeric</asp:ListItem>
                                <asp:ListItem Value="{year}/{month}/{day}/{name}.aspx">Date and Name</asp:ListItem>
                                <asp:ListItem Value="{year}/{name}.aspx">Year and Name</asp:ListItem>
                                <asp:ListItem Value="{year}/{id}.aspx">Year and Number</asp:ListItem>
                                <asp:ListItem Value="{custom}">Custom</asp:ListItem>
                            </asp:RadioButtonList>
                            <%=Language.Admin["PermaCustomStructure"] %>
                            :
                            <asp:TextBox runat="Server" ID="txtpermaexpression"></asp:TextBox>
                            &nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["BlogPermalinkDescription"]%>
                            </span>
                        </p>
                    </div>
                    <div id="divPostCommentSettings" runat="Server" visible="false">
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["BlogShowPostCount"]%></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtshow_post_count" runat="server"></asp:TextBox>&nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["BlogShowPostCountDescription"]%>
                            </span>
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["BlogAddCommentApprove"]%></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:DropDownList runat="Server" ID="ddladd_comment_approve">
                                <asp:ListItem Value="1"></asp:ListItem>
                                <asp:ListItem Value="0"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["BlogAddCommentApproveDescription"]%>
                            </span>
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["BlogCommentSendMail"]%></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:DropDownList runat="Server" ID="ddlcomment_sendmail">
                                <asp:ListItem Value="true"></asp:ListItem>
                                <asp:ListItem Value="false"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["BlogCommentSendMailDescription"]%>
                            </span>
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["BlogRecentPostCount"]%></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtrecent_posts_count" runat="server"></asp:TextBox>&nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["BlogRecentPostCountDescription"]%>
                            </span>
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["BlogRecentCommentsCount"]%></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtrecent_comments_count" runat="server"></asp:TextBox>&nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["BlogRecentCommentsCountDescription"]%>
                            </span>
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["BlogAllowedHtmlTags"]%></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtallowed_html_tags" runat="server"></asp:TextBox>&nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["blogallowedhtmltagsdescription"]%>
                            </span>
                        </p>
                    </div>
                    <div id="divPagingSettings" runat="Server" visible="false">
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["BlogPaging"]%></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:DropDownList runat="Server" ID="ddlpaging">
                                <asp:ListItem Value="true"></asp:ListItem>
                                <asp:ListItem Value="false"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["BlogPagingDescription"]%>
                            </span>
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["BlogPagingStyle"]%></span>
                            <br />
                        </h3>
                        <p class="youhave">
                            <asp:DropDownList runat="Server" ID="ddlpagingstyle">
                                <asp:ListItem Value="0"></asp:ListItem>
                                <asp:ListItem Value="1"></asp:ListItem>
                                <asp:ListItem Value="2"></asp:ListItem>
                                <asp:ListItem Value="3"></asp:ListItem>
                                <asp:ListItem Value="4"></asp:ListItem>
                                <asp:ListItem Value="5"></asp:ListItem>
                                <asp:ListItem Value="6"></asp:ListItem>
                                <asp:ListItem Value="7"></asp:ListItem>
                                <asp:ListItem Value="8"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["BlogPagingStyleDescription"]%>
                            </span>
                        </p>
                    </div>
                    <div runat="Server" id="divMailSettings" visible="false">
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["SmtpName"]%></span><br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtsmtp_name" runat="server"></asp:TextBox>&nbsp;<br />
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["SmtpEmail"]%></span><br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtsmtp_email" runat="server"></asp:TextBox>&nbsp;<br />
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["SmtpServer"]%></span><br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtsmtp_server" runat="server"></asp:TextBox>&nbsp;<br />
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["SmtpPort"]%></span><br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtsmtp_port" runat="server"></asp:TextBox>&nbsp;<br />
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["SmtpUser"]%></span><br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtsmtp_user" runat="server"></asp:TextBox>&nbsp;<br />
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["SmtpPass"]%></span><br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtsmtp_pass" TextMode="Password" runat="server"></asp:TextBox>&nbsp;<br />
                            <span class="descinfo">
                                <%=Language.Admin["SmtpPassDescription"]%>
                            </span>
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["SmtpUseSsl"]%></span><br />
                        </h3>
                        <p class="youhave">
                            <asp:DropDownList runat="Server" ID="ddlsmtp_usessl">
                                <asp:ListItem Value="true"></asp:ListItem>
                                <asp:ListItem Value="false"></asp:ListItem>
                            </asp:DropDownList>
                        </p>
                    </div>
                    <div runat="Server" id="divLibrarySettings" visible="false">
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["ThumbnailSetting"]%></span><br />
                        </h3>
                        <p class="youhave">
                            <asp:DropDownList runat="Server" ID="ddllibrary_usethumbnail">
                                <asp:ListItem Value="true"></asp:ListItem>
                                <asp:ListItem Value="false"></asp:ListItem>
                            </asp:DropDownList>
                            &nbsp;<br />
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["ThumbnailImageSize"]%></span><br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtlibrary_thumbnail_width" runat="server"></asp:TextBox>
                            <%=Language.Admin["ImageWidth"]%>
                            <asp:TextBox CssClass="txtbox" ID="txtlibrary_thumbnail_height" runat="server"></asp:TextBox>
                            <%=Language.Admin["ImageHeight"]%>&nbsp;<br />
                        </p>
                        <h3 class="reallynow">
                            <span>
                                <%=Language.Admin["WebImageSize"]%></span><br />
                        </h3>
                        <p class="youhave">
                            <asp:TextBox CssClass="txtbox" ID="txtlibrary_thumbnail_web_width" runat="server"></asp:TextBox>
                            <%=Language.Admin["ImageWidth"]%>
                            <asp:TextBox CssClass="txtbox" ID="txtlibrary_thumbnail_web_height" runat="server"></asp:TextBox>
                            <%=Language.Admin["ImageHeight"]%>&nbsp;<br />
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <div class="sidebar">
            <div id="divSettingsSide" runat="server">
                <div class="title">
                    <%=Language.Admin["SaveSettings"] %></div>
                <asp:LinkButton runat="server" ID="btnSave" OnClick="btnSave_Click" CssClass="bsbtn green block"><%=Language.Admin["Save"] %></asp:LinkButton><br />
                &nbsp;</div>
        </div>
    </div>
</asp:Content>
