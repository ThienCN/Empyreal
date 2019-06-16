$(document).ready(function () {
    ProductHome.Events();
});

var ProductHome = new function () {
    this.Events = function () {
        // Hien thi mau sac va them class active vao cac element
        $(".size-item").first().addClass("active");
        $(".size-option.color").first().addClass("active");
        $(".color-item").first().addClass("active");

        // Check to display Xem them
        if ($(".content-hide").height() >= 500) {
            $("#show-more-btn").css("display", "block");
        }

        // Xem them/thu gon noi dung
        $("#show-more-btn").click(function () {
            $(".content-hide").toggleClass("show");
            if ($(".content-hide").attr("data-more") == 0) {
                $(".content-hide").attr("data-more", "1");
                $("#show-more-btn").text("Thu gọn nội dung");
            }
            else {
                $(".content-hide").attr("data-more", "0");
                $("#show-more-btn").text("Xem thêm nội dung");
            }
        });

        // Show/Close write review content
        $("#write-review-btn").click(function () {
            //$(".your-review").toggleClass("show");
            if ($(".your-review").attr("data-more") == 0) {
                $(".your-review").slideDown(500);
                $(".your-review").attr("data-more", "1");
                $("#write-review-btn").text("Đóng");
            }
            else {
                $(".your-review").slideUp(300);
                $(".your-review").attr("data-more", "0");
                $("#write-review-btn").text("Viết nhận xét của bạn");
            }
        });

        // PRODUCT ZOOM
        $('#product-main-view .product-view').zoom();
        $('.product-details .products-right .products-zoom').zoom();

        // PRODUCT DETAILS SLICK
        $('#product-main-view').slick({
            infinite: true,
            speed: 300,
            dots: false,
            arrows: false,
            fade: true,
            adaptiveHeight: true,
            asNavFor: '#product-view'
        });

        $('#product-view').slick({
            slidesToShow: 4,
            slidesToScroll: 1,
            accessibility: false,
            dots: false,
            focusOnSelect: true,
            centerMode: true,
            asNavFor: '#product-main-view'
        });

        // Update price, quantity of product when change type of product
        $('#option-list li').click(function () {
            $("#loader").modal("show");

            $this = $(this);
            $parents = $this.parents(':eq(2)'); // Get second parent of element <li>
            $sizeid = $this.find("span").attr("data-sizeid"); // Get size ID of element <li>
            $element = $("[data-color-sizeid=" + $sizeid + "]"); // Xac dinh the color de hien thi ung voi $sizeid

            // Chuyen class active sang the <li> cua kich thuoc duoc chon
            $this.parent().find(".active").removeClass("active");
            $this.addClass("active");

            // Hien thi gia cua san pham
            $("#product-price").text($element.find("li:nth-child(2)").attr("data-price"));
            $("#product-old-price").text($element.find("li:nth-child(2)").attr("data-old-price"));               

            // Hien thi so luong cua san pham
            $(".product-quantity").attr("data-quantity", $element.find("li:nth-child(2)").attr("data-quantity"));
            $(".product-quantity").text($element.find("li:nth-child(2)").attr("data-quantity") + " sản phẩm có sẵn");

            // Enable button
            $("input[name='quantity']").val("1");
            $(".plus-button").prop('disabled', false);
            $(".minus-button").prop('disabled', true);

            // Hien thi mau sac tuong ung voi size
            $parents.find(".color.active").removeClass("active");
            $element.addClass("active");
            $element.find("li.active").removeClass("active");
            $element.find("li:nth-child(2)").addClass("active");

            $("#loader").modal("hide");
        });

        // Update price, quantity of product when change color of product
        $('.color-item').click(function () {
            $("#loader").modal("show");

            $this = $(this);

            // Chuyen class active sang the <li> cua kich thuoc duoc chon
            $this.parent().find(".active").removeClass("active");
            $this.addClass("active");

            // Hien thi gia cua san pham
            $("#product-price").text($this.attr("data-price"));
            $("#product-old-price").text($this.attr("data-old-price"));

            // Hien thi so luong cua san pham
            $(".product-quantity").attr("data-quantity", $this.attr("data-quantity"));
            $(".product-quantity").text($this.attr("data-quantity") + " sản phẩm có sẵn");

            // Enable button
            $("input[name='quantity']").val("1");
            $(".plus-button").prop('disabled', false);
            $(".minus-button").prop('disabled', true);

            $("#loader").modal("hide");
        });

        // Chon toan bo input
        $("input[name='quantity']").click(function () {
            $(this).select();
        });

        // Chi nhap so
        $("input[name='quantity']").keydown(function (e) {
            // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl/cmd+A
                (e.keyCode == 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: Ctrl/cmd+C
                (e.keyCode == 67 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: Ctrl/cmd+X
                (e.keyCode == 88 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right
                (e.keyCode >= 35 && e.keyCode <= 39)) {
                // let it happen, don't do anything
                return;
            }
            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        });

        // Click button + tang so luong len 1
        $('[id="plus-button"]').click(function () {
            var $quantity = parseInt($(this).parent().find($(".product-quantity")).attr("data-quantity"));
            //alert($quantity);
            var $this = $(this).parent().find($("input[name='quantity']"));

            //alert($this.val());
            if ($this.val() < $quantity) {
                $this.val(parseInt($this.val()) + 1);
                $(this).parent().find($(".minus-button")).prop('disabled', false);
            }
            if ($this.val() == $quantity) {
                $(this).parent().find($(".plus-button")).prop('disabled', true);
            }
        });

        // Click button - giam so luong xuong 1
        $('[id="minus-button"]').click(function () {
            var $this = $(this).parent().find($("input[name='quantity']"));

            if ($this.val() > 1) {
                $this.val(parseInt($this.val()) - 1);
                $(this).parent().find($(".plus-button")).prop('disabled', false);
            }
            if ($this.val() == 1) {
                $(this).parent().find($(".minus-button")).prop('disabled', true);
            }
        });

        // Enable/Disable button + - 
        var t = false;
        $("input[name='quantity']").focus(function () {
            var $this = $(this);
            var $quantity = parseInt($(this).parent().find($(".product-quantity")).attr("data-quantity"));
            t = setInterval(
                function () {
                    if (($this.val() <= $quantity || $this.val() >= $quantity) && $this.val().length != 0) {
                        if ($this.val() <= 1) {
                            $this.val(1);
                            $this.parent().find($(".plus-button")).prop('disabled', false);
                            $this.parent().find($(".minus-button")).prop('disabled', true);
                        }

                        if ($this.val() >= $quantity) {
                            $this.val($quantity);
                            $this.parent().find($(".plus-button")).prop('disabled', true);
                            $this.parent().find($(".minus-button")).prop('disabled', false);
                        }
                    }
                    if ($this.val() > 1 && $this.val() < $quantity) {
                        $this.parent().find($(".plus-button")).prop('disabled', false);
                        $this.parent().find($(".minus-button")).prop('disabled', false);
                    }
                });
        });
        $("input[name='quantity']").blur(function () {
            if (t != false) {
                window.clearInterval(t)
                t = false;
            }
            if ($(this).val() == "" || $(this).val() == null || $(this).val().length == 0) {
                this.value = "1";
                $(this).prev().prop('disabled', true);
                $(this).next().prop('disabled', false);
            }
        });

        $('#login_popup_submit').click(function () {
            $.ajax({
                url: "/Login/PopupSignIn",
                type: "POST",
                data: $("#login_popup_form").serialize(),
                success: function (result) {
                    if (result.isSuccess) {
                        $("#login-modal").modal("hide");
                        $.ajax({
                            url: "/Home/GetPartial",
                            type: "GET",
                            contentType: "application/html; charset=utf-8",
                            dataType: "html",
                            success: function (result) {
                                $("#user_content").html(result);
                            },
                            error: function (XMLHttpRequest, textStatus, errorThrown) {
                                alert("Some error get partial");
                            }
                        });
                        ProductHome.AddToCart();
                    }
                    else {
                        $("#error-summary").html(result.message);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Some error login");
                }
            });
        });

        // Event when click to button Them vao gio hang
        $("button#add-to-cart").click(function () {
            ProductHome.AddToCart();
        });

        // Event when click to button Mua ngay
        $("#quick-buy").click(function () {
            $("#addToCartModal").modal("show");
        });
    }

    this.AddToCart = function () {
        var $liactive = $(".size-option.color.active").find('li.active');
        //alert($($liactive.find("#item-color")).text());
        var postData = {
            productDetailId: $liactive.attr("data-id"),
            quantity: $('input[name="quantity"]').val()
        };
        $.ajax({
            url: "/Home/AddToCart",
            type: "POST",
            datatype: "json",
            data: postData,
            success: function (data) {
                if (data.isSuccess) {
                    $("#addToCartModal").modal("show");
                }
                else {
                    if (data.message == "null")
                        $("#login-modal").modal("show");
                    else if (data.message == "cart-error")
                        alert("Thêm mới giỏ hàng không thành công");
                    else alert("Thêm sản phẩm vào giỏ hàng không thành công");
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Some error add to cart");
            }
        });
    }
    
}

//// PRICE SLIDER
//var slider = document.getElementById('price-slider');
//if (slider) {
//    noUiSlider.create(slider, {
//        start: [1, 999],
//        connect: true,
//        tooltips: [true, true],
//        format: {
//            to: function (value) {
//                return value.toFixed(2) + '$';
//            },
//            from: function (value) {
//                return value;
//            }
//        },
//        range: {
//            'min': 1,
//            'max': 999
//        }
//    });
//}