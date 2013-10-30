$(document).ready(function () {
    $("#commentactions .approve").bind("click", function () {
        CommentAction("approve", $(this).attr("itemid"), this);
        $(this).parent().parent().parent().slideUp();
    });
    $("#commentactions .unapprove").bind("click", function () {
        CommentAction("unapprove", $(this).attr("itemid"), this);
        $(this).parent().parent().parent().slideUp();
    });
    $("#commentactions .delete").bind("click", function () {
        if (confirm($(this).attr("Message"))) {
            CommentAction("delete", $(this).attr("itemid"), this);
            $(this).parent().parent().parent().hide("slow");
        }
    });
});

function CommentAction(act, commid, elm) {
    $.ajax({
        type: "GET",
        url: "Control/ActionHandler.ashx",
        data: "p=" + act + "_comment&id=" + commid
               , success: function (result) {
                   result = eval(result);
                   if (result.Success) {

                       if (result.Result > 0) {
                           $(".ballon").parent().find("a").css("padding-right", "30px");
                           $(".ballon").show();
                           $(".ballon span").text(result.Result);
                           ballon.animate();
                       }
                       else {
                           $(".ballon").parent().find("a").css("padding-right", "5px");
                           $(".ballon").hide();
                           $(".ballon").stop();
                       }

                       if (act == "approve") {
                           $(elm).css("display", "none");
                           $(elm).parent().find(".unapprove").css("display", "inline-block");
                           $(elm).parent().parent().parent().removeClass("passive");
                           $(elm).parent().parent().parent().addClass("active");
                       }
                       else if (act == "unapprove") {
                           $(elm).css("display", "none");
                           $(elm).parent().find(".approve").css("display", "inline-block");
                           $(elm).parent().parent().parent().removeClass("active");
                           $(elm).parent().parent().parent().addClass("passive");
                       }
                       if (act != "delete") {
                           $(elm).parent().parent().parent().slideDown();
                       }
                   }
               }, error: function (xhr) {
                   alert(xhr.status);
                   alert(xhr.statusText);
               }
    });
}