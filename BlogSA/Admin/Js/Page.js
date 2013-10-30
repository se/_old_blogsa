// Blogsa.Net Page Script File

var ballon = {};

ballon.animate = function () {

    up();

    function up() {
        $(".ballon").animate({
            "top": "-10px",
            "easing": "easein"
        }, "slow", function () {
            down();
        });
    }

    function down() {
        $(".ballon").animate({
            "top": "0px",
            "easing": "easein"
        }, "slow", function () {
            up();
        });
    }
};

$(document).ready(
    function () {
        if ($(".ballon span").text() != "0") {
            ballon.animate();
        }

        $(".bsbutton").click(function () {
            $(this).blur();
        });
    });

function HepsiniSec(spanChk) {
    var oItem = spanChk.children;
    var theBox = (spanChk.type == "checkbox") ?
        spanChk : spanChk.children.item[0];
    xState = theBox.checked;
    elm = theBox.form.elements;
    for (i = 0; i < elm.length; i++)
        if (elm[i].type == "checkbox" &&
              elm[i].id != theBox.id) {
            if (elm[i].checked != xState)
                elm[i].click();
        }
}
function SecimKontrol(cb) {
    if (!cb.checked) {
        var cbAll = document.getElementById('chkAll');
        cbAll.checked = 0;

        //cb.parentElement.parentElement.style.backgroundColor = 'transparent';
    }
    else {
        //cb.parentElement.parentElement.style.backgroundColor = '#F4F4F4';
    }
}