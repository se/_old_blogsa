<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.master" AutoEventWireup="true"
    CodeFile="Editor.aspx.cs" Inherits="Admin_Editor" %>

<%@ Register Src="Widgets/Now.ascx" TagName="Now" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="toppanel" runat="Server">
    <link href="Css/main.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        $("#mnDefault").attr("class", "current");
    </script>

    <div id="wrapper">
        <div class="content" style="width: 960px">
            <div class="drgmain" style="float: left; margin-right: 4px">
                <div class="drgbox">
                    <div class="drgtitle">
                        <span class="spantitle">
                            <%=Language.Admin["Yours"]%></span>
                    </div>
                    <div class="drgcontent">
                        <div class="feed">
                            <div class="feeddiv">
                                <span class="font16color">
                                    <%=BSHelper.GetPostCountForUserID(Blogsa.ActiveUser.UserID)%>
                                </span><span class="font14color">
                                    <%=Language.Admin["Post"] %>
                                </span>
                            </div>
                            <div class="feeddiv">
                                <span class="font16color">
                                    Comment Count
                                </span><span class="font14color">
                                    <%=Language.Admin["Comment"] %>
                                </span>&nbsp; <span class="font10color">|Comment Count UnApproved
                                    <%=Language.Admin["Unapproved"] %>|</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <uc1:Now ID="Now1" runat="server" />
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cph1" runat="Server">
</asp:Content>
