(function ($) {
    "use strict"

    // NAVIGATION
    var responsiveNav = $('#responsive-nav'),
        catToggle = $('#responsive-nav .category-nav .category-header'),
        catList = $('#responsive-nav .category-nav .category-list'),
        catLi = $('#responsive-nav .category-nav .category-list li'),
        catLiShowOnHover = $('#responsive-nav .show-on-hover'),
        changePass = $('#content-right .account-profile .profile-form .password-group'),
        menuToggle = $('#responsive-nav .menu-nav .menu-header'),
        menuList = $('#responsive-nav .menu-nav .menu-list');

    catLi.hover(function () {
        $(this).toggleClass('open');
    });

    catLiShowOnHover.hover(function () {
        catList.toggleClass('open');
    });

    // HOME SLICK
    $('#home-slick').slick({
        autoplay: true,
        infinite: true,
        speed: 300,
        arrows: true
    });

    //write-review-btn


    // CHANGE PASS
    $("input[name='is-change-pass']").on("click", function () {
        if ($(this).attr("data-animation") == 0) {
            $(this).attr("data-animation", "1");
            $("#password-change").show(200);
            $("input[name='isChangePass']").val("true");
        }
        else {
            $(this).attr("data-animation", "0");
            $("#password-change").hide(200);
            $("input[name='isChangePass']").val("false");
        }
    });

    if ($("input[name='isChangePass']").val() == "true") {
        $("input[name='is-change-pass']").attr('checked', 'checked');
        $("input[name='is-change-pass']").attr("data-animation", "1");
        $("#password-change").show(200);
    }

    // Menu left active
    var url = window.location;
    // Will also work for relative and absolute hrefs
    $('ul.sidebar-left a').filter(function () {
        return this.href == url;
    }).parent().addClass('active');
    //$("li.have-sub").click(function () {
    //    $(this).toggleClass("active");
    //})


    $('select[name=catalogSelect]').change(function () {
        //alert(this.value);
        $("#selectByCatalogForm").submit();
    });
    
    $('#Files').bind('change', function () {
        var totalSize = 0;
        for (var temp = 0; temp < this.files.length; temp++) {
            totalSize += this.files[temp].size;
        }
        if (totalSize < (20 * 1024 * 1024)) {
            //
        }
        //alert('This file size is: ' + this.files.length + "MB");
    });  

    $("#KeySearch").autocomplete({
        autoFocus: false, // auto forcus the first element
        minLength: 1,
        classes: {
            "ui-autocomplete": "empyreal-complete",
        },
        appendTo: $("#OptionsOfSearch"),
        source: function GetComplete(request, response) {
            $.ajax({
                url: '/Search/ProductSearch',
                type: "POST",
                dataType: "json",
                data: { KeySearch: request.term },
                success: function success(result) {
                    response($.map(result, function (item) {
                        return { label: item.name, value: item.name, image: item.url }
                    }))
                }

            })
        },
        focus: function (event, ui) {
            $("#SearchPreview").attr("src", ui.item.image);
        },
        close: function (event, ui) {
            $("#SearchPreview").hide();
        },
        open: function (event, ui) {
            $("#SearchPreview").show();
        },
        create: function (event, ui) {
            $("div[role='status']").hide();
        },
    });
    $("#SearchPreview").hide();
})(jQuery);
//End jQuery

function keyUp(input) {
    if ($(input).val() == "" || $(input).val() == null || $(input).val().length == 0) {
        input.value = "0";
    }
}

function reverseNumber(input) {
    return [].map.call(input, function (x) {
        return x;
    }).reverse().join('');
}

function plainNumber(number) {
    return number.split('.').join('');
}

function splitInDots(input) {
    keyUp(input);

    var value = input.value,
        plain = plainNumber(value),
        reversed = reverseNumber(plain),
        reversedWithDots = reversed.match(/.{1,3}/g).join('.'),
        normal = reverseNumber(reversedWithDots);
    $(input).value = normal;
    $(input).val(normal);
}

/** Empyreal: Thư viện dùng chung
 *  
 * */
var Empyreal = new function () {
    this.ERROR_MSG = `<div style="text-align: left; width: auto;">
                                    <a id="message" title="Error"> - {0}</a>
                                </div>`;
    this.Controls = new function () {
        this.CreateComboBox = function (typeName, element) {
            var div = "<div></div>";
            var data = {
                TypeName: typeName
            };
            $.ajax({
                url: "/ComboBox/CreateComboBox",
                async: false, // !important: Xử lý đồng bộ
                type: "Get",
                dataType: "html",
                data: data,
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    if (result) {
                        return element.append(result);
                    }
                    return false;
                },
                error: function (e) {
                    alert('Error: ' + e);
                }
            });
        };
    };
    //end Empyreal.Control

    this.Common = new function () {
        this.ShowMessage = function (message) {
            if (message.length > 0) {

                $(".message").show();
                for (var index = 0; index < message.length; index++) {
                    var msg = Empyreal.ERROR_MSG.format(message[index]);
                    $(".message").append(msg);
                    $(".message").focus();
                    $("html, body").animate({ scrollTop: 0 }, "slow");


                }
                
            }

        }
        this.ClearMessage = function () {
            $(".message").hide();
            $(".message #message").remove();
        }
    }
    this.Events = new function () {
        // Multiple images preview in browser
        this.ImagePreview = function (input, placeToInsertImagePreview) {

            $(placeToInsertImagePreview).empty();

            if (input.files) {
                var length = input.files.length;

                for (var i = 0; i < length; i++) {
                    var reader = new FileReader();

                    reader.onload = function (event) {
                        $($.parseHTML('<img>')).attr('src', event.target.result).attr('class', 'imgpreview col-md-3').appendTo(placeToInsertImagePreview);
                    }
                    //alert(input.files[i].name);
                    reader.readAsDataURL(input.files[i]);
                }
            }
        };
    };
    //end Empyreal.Event
}


 // string.format
String.prototype.format = function () {
    var a = this;
    for (var k in arguments) {
        a = a.replace(new RegExp("\\{" + k + "\\}", 'g'), arguments[k]);
    }
    return a
}