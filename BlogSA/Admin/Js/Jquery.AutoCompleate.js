jQuery(
    function($) {
        $.fn.fcbkcomplete = function(opt) {

            function init() {
                createFCBK();
                preSet();
                addInput();
            }

            function createFCBK() {
                element.hide();
                element.attr("multiple", "multiple");
                if (element.attr("name").indexOf("[]") == -1) {
                    element.attr("name", element.attr("name") + "[]");
                }

                holder = $(document.createElement("ul"));
                holder.attr("class", "holder");
                element.after(holder);

                complete = $(document.createElement("div"));
                complete.addClass("facebook-auto");
                complete.append('<div class="default">' + options.complete_text + "</div>");

                feed = $(document.createElement("ul"));
                feed.attr("id", elemid + "_feed");

                complete.prepend(feed);
                holder.after(complete);
            }

            function preSet() {
                element.children("option").each(
	                function(i, option) {
	                    option = $(option);
	                    addItem(option.text(), option.val(), true);
	                    var li = $(document.createElement("li"));
	                    li.attr("rel", option.val()).text(option.text());
	                    feed.prepend(li);
	                }
	            );
            }

            function addItem(title, value, preadded) {
                if (value) {
                    var li = document.createElement("li");
                    var txt = document.createTextNode(title);
                    var aclose = document.createElement("a");

                    $(li).attr({ "class": "bit-box", "rel": value });
                    $(li).prepend(txt);
                    $(aclose).attr({ "class": "closebutton", "href": "#" });

                    li.appendChild(aclose);
                    holder.append(li);

                    $(aclose).click(
                    function() {
                        $(this).parent("li").fadeOut("fast",
                            function() {
                                var el = $(this);
                                element.children("option[value=" + el.attr("rel") + "]").removeAttr("selected");
                                el.remove();
                                writeTextBox();
                            }
                        );
                        return false;
                    }
                );

                    if (!preadded) {
                        $("#" + elemid + "_annoninput").remove();
                        addInput();
                        if (element.children("option[value=" + value + "]").length) {
                            element.children("option[value=" + value + "]").get(0).setAttribute("selected", "selected");
                        }
                        else {
                            var option = $(document.createElement("option"));
                            option.attr("value", value).get(0).setAttribute("selected", "selected");
                            option.text(title);
                            element.append(option);
                        }
                    }
                    feed.hide();
                }
            }
            var ai = 0;
            var inp;

            function writeTextBox() {
                var str = $("#" + options.textboxid).val("");

                $(".holder li.bit-box").each(function() {
                    var strValue = $(this).attr("rel");
                    var strText = $(this).text();

                    var str = $("#" + options.textboxid).val();
                    $("#" + options.textboxid).val(str + "{id='" + strValue + "',title='" + strText + "'}");
                });
            }

            function addInput() {
                var li = $(document.createElement("li"));
                var input = $(document.createElement("input"));

                li.attr({ "class": "bit-input", "id": elemid + "_annoninput" });
                input.attr({ "type": "text", "class": "maininput" });
                holder.append(li.append(input));

                input.focus(
                    function() {
                        complete.fadeIn("fast");
                    }
                );

                input.blur(
                    function() {
                        complete.fadeOut("fast");
                    }
                );

                holder.click(
                    function() {
                        input.focus();
                        if (feed.length && input.val().length) {
                            feed.show();
                        }
                        else {
                            feed.hide();
                            complete.children(".default").show();
                        }
                    }
                );
                input.keypress(function(event) {
                    if (event.keyCode == 39 || event.keyCode == 34)
                        return false;
                });
                input.keyup(
                    function(event) {
                        if (event.keyCode != 40 && event.keyCode != 38) {
                            counter = 0;
                            var etext = input.val();
                            addTextItem(etext);
                            if (options.json_url) {
                                if (options.json_cache && cache.length > 0) {
                                    addCacheItems(cache, etext);
                                    bindEvents();

                                }
                                else {
                                    $.getJSON(options.json_url + "&tag=" + etext, null,
                                        function(data) {
                                            addCacheItems(data, etext);
                                            cache = data;
                                            bindEvents();
                                        }
                                    );
                                }
                            }
                            else {
                                bindEvents();
                            }
                            complete.children(".default").hide();
                            feed.show();
                        }
                    }
                );

                if (ai != 0) {
                    $(input).focus();
                }
                else {
                    ai = 1;
                }
                inp = input;
                writeTextBox();
            }

            function addCacheItems(data, input) {
                feed.children("li[fckb=2]").remove();
                $.each(data,
                    function(i, val) {
                        if (val.caption) {
                            var li = document.createElement("li");
                            $(li).attr({ "rel": val.value, "fckb": "2" });
                            if (feedFilter($(li), val.caption, input)) {
                                feed.append(li);
                                counter++;
                            }
                        }
                    }
                );
                defaultFilter(input);
            }


            function defaultFilter(input) {
                if (options.filter_case || options.filter_hide) {
                    var flag;
                    feed.children("li:not([fckb])").removeClass("hidden");
                    feed.children("li:not([fckb])").each(
                        function(i, val) {
                            var item = $(val);
                            if (options.filter_case) {
                                flag = item.text().indexOf(input.toLowerCase());
                                eval("var caption = item.text().replace(/(.*)(" + input.toLowerCase() + ")(.*)/i,'$1<em>$2</em>$3');");
                                item.html(caption);
                            }
                            else {
                                flag = item.text().toLowerCase().indexOf(input.toLowerCase());
                                item.html(item.text());
                            }

                            if (flag == -1 && options.filter_hide) {
                                item.addClass("hidden");
                            }
                            else {
                                counter++;
                            }
                        }
                    );
                }
                else {
                    counter += feed.children("li:not([fckb])").length;
                }
                if (counter > options.height) {
                    feed.css({ "height": (options.height * 24) + "px", "overflow": "auto" });
                }
                else {
                    feed.css("height", "auto");
                }
            }

            function feedFilter(item, caption, input) {
                if (options.filter_case || options.filter_hide) {
                    if (options.filter_hide) {
                        if (caption.toLowerCase().indexOf(input.toLowerCase()) != -1) {
                            eval("caption = caption.replace(/(.*)(" + input + ")(.*)/i,'$1<em>$2</em>$3');");
                            item.html(caption);
                            return true;
                        }
                    }
                    else {
                        eval("caption = caption.replace(/(.*)(" + input + ")(.*)/i,'$1<em>$2</em>$3');");
                        item.html(caption);
                        return true;
                    }
                }
                else {
                    item.html(caption);
                    return true;
                }
            }

            function bindFeedEvent() {
                feed.children("li").mouseover(
		            function() {
		                feed.children("li").removeClass("auto-focus");
		                $(this).addClass("auto-focus");
		                focuson = $(this);
		            }
                );

                feed.children("li").mouseout(
		            function() {
		                $(this).removeClass("auto-focus");
		                focuson = null;
		            }
                );
            }

            function removeFeedEvent() {
                feed.children("li").unbind("mouseover");
                feed.children("li").unbind("mouseout");
                feed.mousemove(
		            function() {
		                bindFeedEvent();
		                feed.unbind("mousemove");
		            }
		        );
            }

            function bindEvents() {
                var maininput = $("#" + elemid + "_annoninput").children(".maininput");
                bindFeedEvent();
                feed.children("li").unbind("click");
                feed.children("li").click(
                    function() {
                        var option = $(this);
                        addItem(option.text(), option.attr("rel"));
                        feed.hide();
                        complete.hide();
                    }
                );

                maininput.unbind("keydown");
                maininput.keydown(
                    function(event) {
                        if (event.keyCode == 13 && focuson != null) {
                            var option = focuson;
                            addItem(option.text(), option.attr("rel"));
                            complete.hide();
                            event.preventDefault();
                            $(inp).focus();
                        }

                        if (event.keyCode == 40) {
                            removeFeedEvent();
                            if (focuson == null || focuson.length == 0) {
                                focuson = feed.children("li:visible:first");
                                feed.get(0).scrollTop = 0;
                            }
                            else {
                                focuson.removeClass("auto-focus");
                                focuson = focuson.nextAll("li:visible:first");
                                var prev = parseInt(focuson.prevAll("li:visible").length, 10);
                                var next = parseInt(focuson.nextAll("li:visible").length, 10);
                                if ((prev > Math.round(options.height / 2) || next <= Math.round(options.height / 2)) && typeof (focuson.get(0)) != "undefined") {
                                    feed.get(0).scrollTop = parseInt(focuson.get(0).scrollHeight, 10) * (prev - Math.round(options.height / 2));
                                }
                            }
                            feed.children("li").removeClass("auto-focus");
                            focuson.addClass("auto-focus");
                        }
                        if (event.keyCode == 38) {
                            removeFeedEvent();
                            if (focuson == null || focuson.length == 0) {
                                focuson = feed.children("li:visible:last");
                                feed.get(0).scrollTop = parseInt(focuson.get(0).scrollHeight, 10) * (parseInt(feed.children("li:visible").length, 10) - Math.round(options.height / 2));
                            }
                            else {
                                focuson.removeClass("auto-focus");
                                focuson = focuson.prevAll("li:visible:first");
                                var prev = parseInt(focuson.prevAll("li:visible").length, 10);
                                var next = parseInt(focuson.nextAll("li:visible").length, 10);
                                if ((next > Math.round(options.height / 2) || prev <= Math.round(options.height / 2)) && typeof (focuson.get(0)) != "undefined") {
                                    feed.get(0).scrollTop = parseInt(focuson.get(0).scrollHeight, 10) * (prev - Math.round(options.height / 2));
                                }
                            }
                            feed.children("li").removeClass("auto-focus");
                            focuson.addClass("auto-focus");
                        }
                    }
                );
            }

            function addTextItem(value) {
                if (options.newel) {
                    feed.children("li[fckb=1]").remove();
                    if (value.length == 0) {
                        return;
                    }
                    var li = $(document.createElement("li"));
                    li.attr({ "rel": value, "fckb": "1" }).html(value);
                    feed.prepend(li);
                    counter++;
                }
            }

            var options = $.extend({
                json_url: null,
                json_cache: false,
                height: "10",
                newel: false,
                filter_case: false,
                filter_hide: false,
                textboxid: null,
                complete_text: "Write somethink..."
            }, opt);

            //system variables
            var holder = null;
            var feed = null;
            var complete = null;
            var textboxid = null;
            var counter = 0;
            var cache = {};
            var focuson = null;

            var element = $(this);
            var elemid = element.attr("id");

            init();

            return this;
        };
    }
);