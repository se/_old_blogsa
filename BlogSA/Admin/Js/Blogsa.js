var Blogsa = new Object();
Blogsa.BaseUrl = "";
Blogsa.TrashPost = function(elm, PostID) {
    var mainObj = $(elm).parent();
    $.ajax({
        type: "GET",
        url: Blogsa.BaseUrl + "Admin/Control/Ajax.ashx",
        data: "act=post&process=trash&id=" + PostID
               , success: function(data) {
                   if (data == "OK") {
                       $(mainObj).find("#post_trash_" + PostID).hide();
                       $(mainObj).find("#post_draft_" + PostID).show();
                       $(mainObj).find("#post_publish_" + PostID).show();
                       $(mainObj).css("background-color", "#FAF23E");
                       $(mainObj).stop().animate({ backgroundColor: '#f4f4f4' }, 300);
                   }
               }, error: function(xhr) {
                   alert(xhr.status);
                   alert(xhr.statusText);
               }
    });
}

Blogsa.DraftPost = function(elm, PostID) {
    var mainObj = $(elm).parent();
    $.ajax({
        type: "GET",
        url: Blogsa.BaseUrl + "Admin/Control/Ajax.ashx",
        data: "act=post&process=draft&id=" + PostID
               , success: function(data) {
                   if (data == "OK") {
                       $(mainObj).find("#post_trash_" + PostID).show();
                       $(mainObj).find("#post_draft_" + PostID).hide();
                       $(mainObj).find("#post_publish_" + PostID).show();
                       $(mainObj).css("background-color", "#FAF23E");
                       $(mainObj).stop().animate({ backgroundColor: '#f4f4f4' }, 300);
                   }
               }, error: function(xhr) {
                   alert(xhr.status);
                   alert(xhr.statusText);
               }
    });
}

Blogsa.PublishPost = function(elm, PostID) {
    var mainObj = $(elm).parent();
    $.ajax({
        type: "GET",
        url: Blogsa.BaseUrl + "Admin/Control/Ajax.ashx",
        data: "act=post&process=publish&id=" + PostID
               , success: function(data) {
                   if (data == "OK") {
                       $(mainObj).find("#post_trash_" + PostID).show();
                       $(mainObj).find("#post_draft_" + PostID).show();
                       $(mainObj).find("#post_publish_" + PostID).hide();
                       $(mainObj).css("background-color", "#FAF23E");
                       $(mainObj).stop().animate({ backgroundColor: '#f4f4f4' }, 300);
                   }
               }, error: function(xhr) {
                   alert(xhr.status);
                   alert(xhr.statusText);
               }
    });
}

Blogsa.DeleteComment = function(elm, CommentID) {
    var mainObj = $(elm).parent();
    $.ajax({
        type: "GET",
        url: Blogsa.BaseUrl + "Admin/Control/Ajax.ashx",
        data: "act=comment&process=delete&id=" + CommentID
               , success: function(data) {
                   if (data == "OK") {
                       $(mainObj).empty();
                       $(mainObj).html("<span>Deleted...</span>");
                       $(mainObj).css("background-color", "#DE0D00");
                       $(mainObj).stop().animate({ backgroundColor: '#f4f4f4' }, 300);
                   }
               }, error: function(xhr) {
                   alert(xhr.status);
                   alert(xhr.statusText);
               }
    });
}

Blogsa.CommentApprove = function(elm, CommentID) {
    var mainObj = $(elm).parent();
    $.ajax({
        type: "GET",
        url: Blogsa.BaseUrl + "Admin/Control/Ajax.ashx",
        data: "act=comment&process=approve&id=" + CommentID
               , success: function(data) {
                   if (data == "OK") {
                       $(mainObj).find("#comment_approve_" + CommentID).hide();
                       $(mainObj).find("#comment_unapprove_" + CommentID).show();
                       $(mainObj).css("background-color", "#B2FF46");
                       $(mainObj).stop().animate({ backgroundColor: '#f4f4f4' }, 300);
                   }
               }, error: function(xhr) {
                   alert(xhr.status);
                   alert(xhr.statusText);
               }
    });
}

Blogsa.CommentUnApprove = function(elm, CommentID) {
    var mainObj = $(elm).parent();
    $.ajax({
        type: "GET",
        url: Blogsa.BaseUrl + "Admin/Control/Ajax.ashx",
        data: "act=comment&process=unapprove&id=" + CommentID
               , success: function(data) {
                   if (data == "OK") {
                       $(mainObj).find("#comment_approve_" + CommentID).show();
                       $(mainObj).find("#comment_unapprove_" + CommentID).hide();
                       $(mainObj).css("background-color", "#FAF23E");
                       $(mainObj).stop().animate({ backgroundColor: '#f4f4f4' }, 300);
                   }
               }, error: function(xhr) {
                   alert(xhr.status);
                   alert(xhr.statusText);
               }
    });
}