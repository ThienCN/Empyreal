///////////////////////////////////////////////////////////////////
///                          Trang chủ                          ///
///////////////////////////////////////////////////////////////////


$(document).ready(function () {
    HomeIndex.AllEvents();
});

var HomeIndex = new function () {
    this.Mode = "New";

    this.AllEvents = function () {
        HomeIndex.Events.Load_ProductTop("Top");
        //HomeIndex.Events.Load_ProductTop("New");
        //HomeIndex.Events.Load_ProductTop("Random");
        //setTimeout(HomeIndex.Events.Load_ProductTop("Top"),5000);
        //setTimeout(HomeIndex.Events.Load_ProductTop("New"), 5000);
        //setTimeout(HomeIndex.Events.Load_ProductTop("Random"), 5000);

    };
    this.Events = new function () {


        this.Load_ProductTop = function (mode) {
            let Mode = 1;
            var data = {
                mode: mode
            };
            $.ajax({
                url: "/ProductPartial/CreateProductView",
                async: false, // defalt = true
                type: "Get",
                data: data,
                dataType: "html",
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    if (result) {
                        if (mode == "Top") {
                            $("#product-slick-1").append($(result).fadeIn(2000));
                            $.getScript("/js/Setslick/setslick1.js");
                            return;

                        } else if (mode == "New") {
                            $("#product-new").append($(result).fadeIn(5000));
                            $.getScript("/js/Setslick/setslick2.js");
                            return;
                        }
                        $("#product-random").html(result)
                        return;
                    }
                    return false;
                },
                error: function (e) {
                    alert('Error: ' + e);
                }
            });
        }
    }
}
$(window).scroll(function () {
    var position = $(window).scrollTop();
    var bottom = ($(document).height() - $(window).height());
    let mode = HomeIndex.Mode;
    
    if (position >= (bottom / 2)) {
        if (mode == "New") {
            HomeIndex.Events.Load_ProductTop(mode);
            HomeIndex.Mode = "Random";
        }
        else if (mode == "Random"){
            HomeIndex.Events.Load_ProductTop(mode);
            HomeIndex.Mode = "";
        }
    };
});