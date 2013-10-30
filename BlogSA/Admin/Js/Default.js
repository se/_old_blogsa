$(document).ready(function() {
    $("#leftPanel , #rightPanel").sortable({
        cursor: 'move',
        handle: 'div.drgtitle .spantitle',
        placeholder: 'dragging',
        connectWith: '.uldrg',
        update: function(event, ui) {
            var lPnl = "";
            var rPnl = "";
            $("#leftPanel .drgmain").each(function() {
                lPnl += $(this).attr('name') + ",";
            });
            $("#rightPanel .drgmain").each(function() {
                rPnl += $(this).attr('name') + ",";
            });
            $.ajax({
                type: "GET",
                url: "Control/Ajax.ashx",
                data: "act=widgetposition&left=" + lPnl + "&right=" +rPnl
               , success: function(data) {
                   if (data == "OK") {
                   }
               }, error: function(xhr) {
                   alert(xhr.status);
                   alert(xhr.statusText);
               }
            });
        }
    });
});