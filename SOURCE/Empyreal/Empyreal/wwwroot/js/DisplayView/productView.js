$(document).ready(function () {
    //Check to display Xem them
    if ($(".content-hide").height() >= 500) {
        $("#show-more-btn").css("display", "block");
    }

    $('#option-list li').click(function () {
        $("#product-price").text($(this).attr("asp-data-price"));
        $("#product-old-price").text($(this).attr("asp-data-old-price"));
        $(this).parent().find(".active").removeClass("active");
        $(this).addClass("active");
        $(".product-quantity").attr("asp-data-quantity", $(this).attr("asp-data-quantity"));
        $(".product-quantity").text($(this).attr("asp-data-quantity") + " sản phẩm có sẵn");
        $("input[name='quantity']").val("1");
        $(".plus-button").prop('disabled', false);
        $(".minus-button").prop('disabled', true);
    });

    $("#write-review-btn").click(function () {
        $(".your-review").toggleClass("show");
        if ($(".your-review").attr("data-more") == 0) {
            $(".your-review").attr("data-more", "1");
            $("#write-review-btn").text("Đóng");
        }
        else {
            $(".your-review").attr("data-more", "0");
            $("#write-review-btn").text("Viết nhận xét của bạn");
        }
    });

    //Xem them/thu gon noi dung
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

    // PRODUCT ZOOM
    $('#product-main-view .product-view').zoom();
    $('.product-details .products-right .products-zoom').zoom();

    // PRICE SLIDER
    var slider = document.getElementById('price-slider');
    if (slider) {
        noUiSlider.create(slider, {
            start: [1, 999],
            connect: true,
            tooltips: [true, true],
            format: {
                to: function (value) {
                    return value.toFixed(2) + '$';
                },
                from: function (value) {
                    return value;
                }
            },
            range: {
                'min': 1,
                'max': 999
            }
        });
    }

    //Chon toan bo input
    $("input[name='quantity']").click(function () {
        $(this).select();
    });

    //Chi nhap so
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

    //Click button + tang so luong len 1
    $('[id="plus-button"]').click(function () {
        var $quantity = parseInt($(this).parent().find($(".product-quantity")).attr("asp-data-quantity"));
        //alert($quantity);
        var $this = $(this).parent().find($("input[name='quantity']"));
        var $checked = $(this).parents(':eq(4)').find($("input[name='cart-select-item']"));
        var $price = $(this).parent().find($("input[name='item-price']")).val();

        //alert($this.val());
        if ($this.val() < $quantity) {
            $this.val(parseInt($this.val()) + 1);
            $(this).parent().find($(".minus-button")).prop('disabled', false);
        }
        if ($this.val() == $quantity) {
            $(this).parent().find($(".plus-button")).prop('disabled', true);
        }
        if ($checked.is(":checked")) {
            $("#summary-price").val(parseFloat($("#summary-price").val()) + parseFloat($price));
            var summaryPrice = parseFloat($("#summary-price").val());
            $("#checkout-summary-temp-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
            $("#checkout-summary-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
        }
    });

    //Click button - giam so luong xuong 1
    $('[id="minus-button"]').click(function () {
        var $this = $(this).parent().find($("input[name='quantity']"));
        var $checked = $(this).parents(':eq(4)').find($("input[name='cart-select-item']"));
        var $price = $(this).parent().find($("input[name='item-price']")).val();

        if ($this.val() > 1) {
            $this.val(parseInt($this.val()) - 1);
            $(this).parent().find($(".plus-button")).prop('disabled', false);
        }
        if ($this.val() == 1) {
            $(this).parent().find($(".minus-button")).prop('disabled', true);
        }
        if ($checked.is(":checked")) {
            $("#summary-price").val(parseFloat($("#summary-price").val()) - parseFloat($price));
            var summaryPrice = parseFloat($("#summary-price").val());
            $("#checkout-summary-temp-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
            $("#checkout-summary-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
        }
    });

    var t = false;
    $("input[name='quantity']").focus(function () {
        var $this = $(this);
        var $quantity = parseInt($(this).parent().find($(".product-quantity")).attr("asp-data-quantity"));
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

    $("input[name='quantity']").keyup(function () {
        if ($(this).val().length > 0) {
            var sumPrice = 0;
            $(".checkout-item").each(function () {
                var $this = $(this);
                if ($this.find($("input[name='cart-select-item']")).is(":checked") && $this.find($("input[name='quantity']")).val() != "") {
                    sumPrice += parseFloat($this.find($("input[name='item-price']")).val()) * parseInt($this.find($("input[name='quantity']")).val());
                }
            });
            //alert(sumPrice);
            $("#summary-price").val(sumPrice);
            $("#checkout-summary-temp-value").text(sumPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
            $("#checkout-summary-value").text(sumPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
        }
    });

    $("input[name='quantity']").blur(function () {
        if (t != false) {
            window.clearInterval(t)
            t = false;
        }

        focusOut(this);
    })

    $('#login_popup_form').submit(function (e) {
        e.preventDefault();
        var $this = $(this);
        $.ajax({
            url: $this.attr("action"),
            type: "POST",
            data: $this.serialize(),
            success: function (result) {
                if (result.message == "success") {
                    $("#login-modal").modal("hide");
                    $.ajax({
                        url: "/Home/GetPartial",
                        type: "GET",
                        contentType: "application/html; charset=utf-8",
                        dataType: "html",
                        success: function (result) {
                            $("#user_content").html(result);
                        }
                    });
                    AddToCart();
                }
            }
        });
    });


    $("button#add-to-cart").click(function () {
        AddToCart();
    });

    $("#quick-buy").click(function () {
        $("#addToCartModal").modal("show");
    });

    $("span#checkmark-select-item").click(function () {
        sumPrice(this, true);
    });

    $("span#checkmark-select-all").click(function () {
        var $this = $(this);
        if (!$this.prev().is(":checked")) {
            $("input#cart-select-item").prop("checked", false);
            $("#summary-price").val("0");
            $("span#checkmark-select-item").each(function () {
                sumPrice(this, false);
                $(this).prev().prop("checked", true);
            });
        }
        else if ($this.prev().is(":checked")) {
            $("span#checkmark-select-item").each(function () {
                sumPrice(this, false);
                $(this).prev().prop("checked", false);
            });
        }
    });

    $("span#btn-cart-delete").click(function () {
        var $this = $(this);
        if (confirm("Bạn chắc chắn muốn xóa sản phẩm này?")) {
            var $cartDetailId = $this.next().val();
            $.ajax({
                url: "/Home/RemoveItemInCart",
                type: "POST",
                datatype: "json",
                data: { CartDetailId: $cartDetailId },
                success: function (data) {
                    if (data.message == "fail") {
                        alert("Sản phẩm không tồn tại");
                    }
                    else {
                        $this.parents(':eq(5)').remove();
                        var empty = 0;
                        $(".checkout-item").each(function () {
                            empty++;
                        });
                        if (empty == 0) {
                            $("#cart-left").html(`<h4>Bạn chưa có sản phẩm nào</h4>
                                <a class="primary-btn" href="/Home/Index"> Tiếp tục mua sắm</a>`);
                        }
                    }
                }
            });
        }
    });
});

function focusOut(input) {
    if ($(input).val() == "" || $(input).val() == null || $(input).val().length == 0) {
        input.value = "1";
        $(input).prev().prop('disabled', true);
        $(input).next().prop('disabled', false);

        var sumPrice = 0;
        $(".checkout-item").each(function () {
            var $this = $(this);
            if ($this.find($("input[name='cart-select-item']")).is(":checked") && $this.find($("input[name='quantity']")).val() != "") {
                sumPrice += parseFloat($this.find($("input[name='item-price']")).val()) * parseInt($this.find($("input[name='quantity']")).val());
            }
        });
        //alert(sumPrice);
        $("#summary-price").val(sumPrice);
        $("#checkout-summary-temp-value").text(sumPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
        $("#checkout-summary-value").text(sumPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
    }
}

function keyUp(input) {
    if ($(input).val() == "" || $(input).val() == null || $(input).val().length == 0) {
        input.value = "0";
    }
}

var k = 0;
function sumPrice(input, bool) {
    var $this = $(input).parents(':eq(3)');
    var $price = $this.find("#item-price").val();
    var $quantity = $this.find("input[name='quantity']").val();
    if (!$(input).prev().is(":checked")) {
        //alert($price * $quantity);
        $("#summary-price").val($price * $quantity + parseFloat($("#summary-price").val()));
        var summaryPrice = parseFloat($("#summary-price").val());
        $("#checkout-summary-temp-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
        $("#checkout-summary-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
        if (bool) k++;
        else k = 2;
    }
    else if ($(input).prev().is(":checked")) {
        if ($("input[name='cart-select-all']").is(":checked") && bool) {
            $("input[name='cart-select-all']").prop("checked", false);
        }

        $("#summary-price").val(parseFloat($("#summary-price").val()) - $price * $quantity);
        var summaryPrice = parseFloat($("#summary-price").val());
        $("#checkout-summary-temp-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
        $("#checkout-summary-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
        if (bool) k--;
        else k = 0;
    }

    //alert(k);
    if (k == 2 && bool) {
        $("input[name='cart-select-all']").prop("checked", true);
    }
}

function AddToCart() {
    var $liactive = $("#option-list").find('li.active');
    //alert($($liactive.find("#item-color")).text());
    var postData = {
        ProductDetailId: $liactive.attr("asp-data-id"),
        Quantity: $('input[name="quantity"]').val()
    };
    $.ajax({
        url: "/Home/AddToCart",
        type: "POST",
        datatype: "json",
        data: postData,
        success: function (data) {
            if (data.message == "null") {
                $("#login-modal").modal("show");
            }
            else if (data.message == "success") {
                $("#addToCartModal").modal("show");
            }
        }
    });
}