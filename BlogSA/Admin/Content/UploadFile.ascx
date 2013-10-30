<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UploadFile.ascx.cs" Inherits="Admin_Content_UploadFile" %>
<script type="text/javascript" src="Js/swfupload/swfupload.js"></script>
<script type="text/javascript" src="Js/swfupload/handlers.js"></script>
<link href="Js/swfupload/css/default.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
    var Blogsa = { Language: {} };
    Blogsa.Language.FileLoading = '';
    Blogsa.Language.FileReceived = '';
    Blogsa.Language.FileUploading = '';
    Blogsa.Language.AllFilesReceived = '';
    
    Blogsa.Language.FileLoading = '<%=Language.Admin["FileLoading"] %>';
    Blogsa.Language.FileReceived = '<%=Language.Admin["FileReceived"] %>';
    Blogsa.Language.FileUploading = '<%=Language.Admin["FileUploading"] %>';
    Blogsa.Language.AllFilesReceived = '<%=Language.Admin["AllFilesReceived"] %>';
    Blogsa.Language.Show = '<%=Language.Admin["Show"] %>';

    var swfu;
    window.onload = function () {

        var auth = '<% = Request.Cookies[FormsAuthentication.FormsCookieName]==null ? string.Empty : Request.Cookies[FormsAuthentication.FormsCookieName].Value %>';
        var ASPSESSID = '<%= Session.SessionID %>';

        swfu = new SWFUpload({
            // Backend Settings
            upload_url: "Control/Upload.ashx",
            post_params: {
                "ASPSESSID": ASPSESSID,
                "AUTHID": auth
            },

            // File Upload Settings
            file_size_limit: "1000 MB",
            file_types: "*.*",
            file_types_description: "All Files",
            file_upload_limit: "0",    // Zero means unlimited

            // Event Handler Settings - these functions as defined in Handlers.js
            //  The handlers are not part of SWFUpload but are part of my website and control how
            //  my website reacts to the SWFUpload events.
            file_queue_error_handler: fileQueueError,
            file_dialog_complete_handler: fileDialogComplete,
            upload_progress_handler: uploadProgress,
            upload_error_handler: uploadError,
            upload_success_handler: uploadSuccess,
            upload_complete_handler: uploadComplete,
            button_cursor: SWFUpload.CURSOR.HAND,

            // Button settings
            button_image_url: "Js/swfupload/images/swf_upload_button.png",
            button_placeholder_id: "spanButtonPlaceholder",
            button_width: 119,
            button_height: 25,
            button_text: '<span class="button"><%=Language.Admin["SelectFile"] %></span>',
            button_text_style: '.button { font-family: Arial; font-size: 12pt; margin:0 5px; display:block; }',
            button_text_top_padding: 3,
            button_text_left_padding: 24,

            // Flash Settings
            flash_url: "Js/swfupload/swfupload.swf", // Relative to this file

            custom_settings: {
                upload_target: "divFileProgressContainer"
            },

            // Debug Settings
            debug: false
        });
    };
</script>
<style type="text/css">
    .upload_file
    {
        display: block;
        padding: 10px;
    }
</style>
<div class="upload_file">
    <span id="spanButtonPlaceholder" style="cursor: pointer"></span>
    <div id="divFileProgressContainer" class="progress-container-all">
    </div>
    <div id="files">
        <ul>
        </ul>
        <div class="clear">
        </div>
    </div>
</div>
