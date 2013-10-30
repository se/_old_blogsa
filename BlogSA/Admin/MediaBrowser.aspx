<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MediaBrowser.aspx.cs" Inherits="Admin_MediaBrowser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Css/button.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function addMedia(name, id, type) {
            if (type == 1) {
                window.parent.tinyMCE.execCommand('mceInsertContent', false, '<img src="Upload/Images/' + name + '" alt="' + name + '" >');
            }
            else {
                window.parent.tinyMCE.execCommand('mceInsertContent', false, '<a href="FileHandler.ashx?FileID=' + id + '">' + name + '</a>');
            }
            window.parent.CloseMediaBrowser();
        }
    </script>
    <style type="text/css">
        .media-upload
        {
            border-top: 1px solid #e7e7e7;
            padding: 10px;
            display: block;
            overflow: auto;
            height: 95px;
        }
        .media-images
        {
            display: block;
            height: 220px;
            overflow: auto;
        }
        .media-image-click
        {
            float: left;
            width: 110px;
            height: 130px;
            margin: 5px;
            cursor: pointer;
            display: block;
            border: 2px solid transparent;
            text-align: center;
        }
        .media-image-click:hover
        {
            border: 2px solid #e7e7e7;
        }
        .media-image-click:hover span
        {
            color: #ff0000;
        }
        .media-image-click span
        {
            display: block;
            text-align: center;
            font-size: 10px;
            color: #666;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="media-images">
        <asp:Literal runat="Server" ID="ltNoFile"></asp:Literal>
        <asp:Repeater runat="Server" ID="rpFiles">
            <ItemTemplate>
                <div class="media-image-click" onclick="addMedia('<%#Eval("Title") %>','<%#Eval("PostID") %>','<%#Eval("Show") %>');">
                    <img style="max-width: 100px; max-height: 100px;" src="<%#GetThumbnail((short)Eval("Show"),(string)Eval("Title"),(string)Eval("Code")) %>" />
                    <span>
                        <%#Eval("Title") %></span>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    </form>
</body>
</html>
