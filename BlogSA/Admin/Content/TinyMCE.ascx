<%@ Control Language="C#" AutoEventWireup="true" CodeFile="TinyMCE.ascx.cs" Inherits="Admin_Content_TinyMCE" %>
<link href="Css/tinymce.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../Resources/tiny_mce/tiny_mce.js"></script>
<script type="text/javascript" src="Js/TinyMCE.js"></script>
<script type="text/javascript">
    var lngPostsInMemory = '<%=Language.Admin["PostsInMemory"] %>';
    var lngPostMemorySaving = '<%=Language.Admin["PostMemorySaving"] %>';
    var lngPostSaved = '<%=Language.Admin["PostSaved"] %>';
    
    function showHtml(tag) {
        var h = $('.tinymce_textbox').outerHeight();
        tinyMCE.execCommand('mceRemoveControl', true, '<%=txtContent.ClientID %>');
        $(tag).parent().parent().find("a").removeClass("selected");
        $(tag).addClass("selected");
        $('#<%=txtContent.ClientID %>').height(h);
    }
    function showDesign(tag) {
        var h = $('.tinymce_textbox').height();
        tinyMCE.execCommand('mceAddControl', true, '<%=txtContent.ClientID %>');
        $(tag).parent().parent().find("a").removeClass("selected");
        $(tag).addClass("selected");
    }

    function OpenMediaBrowser() {
        $("#ifrmMediaBrowser").attr("src", "MediaBrowser.aspx");
        $("#divMediaBrowser").dialog({
            title: '<%=Language.Admin["InsertMedia"]%>',
            width: 760,
            height: 480
        });
    }
    
    function OpenAutoSaves() {
        //$("#ifrmMediaBrowser").attr("src", "AutoSaves.aspx");
    }

    function CloseMediaBrowser() {
        $("#divMediaBrowser").dialog('close');
    }

    $(document).ready(function () {
        $("#mediabrowser_tabs").tabs({ height: 350 });
    });
</script>
<div id="tinymceInfo" style="display: block; padding: 5px 12px 0 12px;">
</div>
<div style="padding: 10px; display: block; position: relative;">
    <div class="tinymce_actions">
        <a href="javascript:OpenMediaBrowser()" class="add-image image-button"><span>
            <%=Language.Admin["InsertMedia"]%></span></a>
        <div class="tinymce_tab_holder">
            <ul class="tinymce_tabs">
                <li><a href="javascript:void(0)" class="selected" onclick="showDesign(this);"><span>
                    <%=Language.Admin["Design"] %></span></a></li>
                <li><a href="javascript:void(0)" onclick="showHtml(this);"><span>HTML</span></a></li>
            </ul>
            <div class="clear">
            </div>
        </div>
    </div>
    <div style="clear: both; height: 0px;">
    </div>
    <div class="tinymce_textbox">
        <asp:TextBox CssClass="txtboxarea" ID="txtContent" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>
</div>
<div id="divMediaBrowser" class="media-browser">
    <!--MODAL DIALOG-->
    <div id="mediabrowser_tabs">
        <ul>
            <li><a href="MediaBrowser.aspx?s=1">
                <%=Language.Admin["LibraryPicture"] %></a></li>
            <li><a href="MediaBrowser.aspx?s=2">
                <%=Language.Admin["LibraryVideoSound"] %></a></li>
            <li><a href="MediaBrowser.aspx?s=0">
                <%=Language.Admin["LibraryFile"] %></a></li>
        </ul>
    </div>
    <!--MODAL DIALOG-->
</div>
