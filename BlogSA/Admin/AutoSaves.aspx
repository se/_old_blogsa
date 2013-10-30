<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoSaves.aspx.cs" Inherits="Admin_AutoSaves" %>

<%@ Register Src="Content/Upload.ascx" TagName="Upload" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="Js/Jquery.js" type="text/javascript"></script>
    <script src="Js/Jquery.Center.js" type="text/javascript"></script>
    <link href="Css/button.css" rel="stylesheet" type="text/css" />
    <link href="Css/default.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function AddContent(ContentID) {
            var strContent = $("#" + ContentID).text();
            window.parent.tinyMCE.execCommand('mceSetContent', false, strContent);
            $(window.parent.document).find("#divMediaBrowser").hide();
            $(window.parent.document).find(".media-overlay").hide();
        }
    </script>
    <style type="text/css">
        body
        {
            margin: 0;
            padding: 0;
            font-size: 10pt;
            font-family: Arial,Tahoma,Verdana;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:Literal runat="Server" ID="ltNoData"></asp:Literal>
    <div class="drgcontent" style="min-height: 300px; background: #f7f7f7">
        <div class="feeddiv">
            <asp:LinkButton runat="server" ID="btnDeleteAllAutoSave" CssClass="bsbutton red"
                OnClick="btnDeleteAllAutoSave_Click"><span><%=Language.Admin["DeleteAllAutoSave"]%></span></asp:LinkButton>
        </div>
        <asp:Repeater runat="Server" ID="rpAutoSaves">
            <ItemTemplate>
                <div class="feeddiv">
                    <a class="sbtn bsgray" href='javascript:;' onclick="AddContent('content<%#Eval("PostID") %>');">
                        <span>
                            <%#Language.Admin["Add"] %></span></a>&nbsp;<a class="sbtn bsred" onclick="return confirm('<%=Language.Admin["PostDeleteConfirm"] %>');"
                                href="?act=delete&PostID=<%#Eval("PostID") %>"><span>
                                    <%#Language.Admin["Delete"] %></span></a> <span class="feedtitle">
                                        <%#Eval("Date")%>
                                        - <b>
                                            <%#Eval("Title")%></b></span>
                    <div id="content<%#Eval("PostID") %>" style="display: none;">
                        <%#Eval("Content") %>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
