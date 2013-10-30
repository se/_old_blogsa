// TinyMCE Call
try {
    tinyMCE.init({
        // General options
        debug: true,
        remove_linebreaks: false,
        remove_trailing_nbsp: false,
        forced_root_block: false,
        verify_html: false,
        mode: "textareas",
        theme: "blogsa",
        plugins: "safari,pagebreak,style,layer,table,save,advhr,advimage,advlink,inlinepopups,insertdatetime,preview,media,searchreplace,print,contextmenu,paste,directionality,fullscreen,noneditable,visualchars,nonbreaking,xhtmlxtras,template",
        noneditable_leave_contenteditable : true,

        // Theme options
        theme_advanced_buttons1: "autosave,bold,italic,underline,strikethrough,bullist,numlist,|,justifyleft,justifycenter,justifyright,|,link,unlink,pagebreak,fullscreen,autosavecall",
        theme_advanced_buttons2: "formatselect,pastetext,pasteword,|,outdent,indent,blockquote,|,undo,redo,|,image,|,forecolor,removeformat,|,charmap,media",
        theme_advanced_toolbar_location: "top",
        theme_advanced_toolbar_align: "left",
        theme_advanced_statusbar_location: "bottom",
        theme_advanced_resizing: true,
        theme_advanced_resize_horizontal: false,
        // Relative URL
        relative_urls: false,
        remove_script_host: false,
        document_base_url: "../../",
        // Example content CSS (should be your site CSS)
        content_css: "Admin/css/main.css",

        // Drop lists for link/image/media/template dialogs
        template_external_list_url: "lists/template_list.js",
        external_link_list_url: "lists/link_list.js",
        external_image_list_url: "lists/image_list.js",
        media_external_list_url: "lists/media_list.js",

        setup: function(ed) {
            if (typeof (autosavemode) != "undefined" && autosavemode == true) {
                ed.onKeyUp.add(function(ed, e) {
                    var strTitle = $("#ctl00_cph1_txtTitle").val();
                    var strContent = ed.getContent();
                    if (beforeContent != strContent && strContent != ed.startContent) {
                        if (tinymceAutoSaveTimer != null) clearTimeout(tinymceAutoSaveTimer);
                        tinymceAutoSaveTimer = setTimeout(function() {
                            $("#tinymceInfo").html("<img src='Images/Icons/posts.png' style='vertical-align:middle;'/> " + lngPostMemorySaving);
                            $.post("Control/AutoSave.ashx?act=savedraft", { title: strTitle, content: strContent },
                      function(data) {
                          var strMessage = "<img src='Images/Icons/posts.png' style='vertical-align:middle;'/>" + data;
                          strMessage += " " + lngPostSaved;
                          strMessage += " - <a href='javascript:OpenAutoSaves();'>" + lngPostsInMemory + "</a>";
                          $("#tinymceInfo").html(strMessage);
                          tinymceAutoSaveTimer = null;
                          beforeContent = strContent;
                      });
                        }, 15000);
                    }
                });

                ed.addButton('autosavecall', {
                    title: 'Auto Saves',
                    image: 'Images/Icons/autosave.png',
                    onclick: function() {
                        OpenAutoSaves();
                    }
                });
            }
        }

    });

    var tinymceAutoSaveTimer;
    var beforeContent = "";
}
catch (err)
{ }