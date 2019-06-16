$(document).ready(function () {
    CartHome.Events();
});

var CartHome = new function () {
    this.Events = function () {
        //if ($(".cart-infor").height() < 470) {
        //    $("#footer").addClass("fixed");
        //}
        //else {
        //    $("#footer").removeClass("fixed");
        //}

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
            $("#loader").modal("show");
            var $quantity = parseInt($(this).parent().find($(".product-quantity")).attr("data-quantity"));
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
            $("#loader").modal("hide");
        });

        // Click button - giam so luong xuong 1
        $('[id="minus-button"]').click(function () {
            $("#loader").modal("show");
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
            $("#loader").modal("hide");
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

        // Update summary price of Cart when change value of input
        $("input[name='quantity']").keyup(CartHome.Delay(function () {
            $("#loader").modal("show");
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
            $("#loader").modal("hide");
        }, 300));

        // Event focus out of input
        $("input[name='quantity']").focusout(function () {
            CartHome.InputFocusOut(this);
        });

        // Select 1 item
        $("span#checkmark-select-item").click(function () {
            $("#loader").modal("show");
            CartHome.SumPrice(this, true);
            $("#loader").modal("hide");
        });

        // Select all item
        $("span#checkmark-select-all").click(function () {

            $("#loader").modal("show");
            var $this = $(this);
            if (!$this.prev().is(":checked")) {
                $("input#cart-select-item").prop("checked", false);
                $("#summary-price").val("0");
                $("span#checkmark-select-item").each(function () {
                    CartHome.SumPrice(this, false);
                    $(this).prev().prop("checked", true);
                });
            }
            else if ($this.prev().is(":checked")) {
                $("span#checkmark-select-item").each(function () {
                    CartHome.SumPrice(this, false);
                    $(this).prev().prop("checked", false);
                });
            }
            $("#loader").modal("hide");
        });

        // Event when click to remove item in cart
        $("span#btn-cart-delete").click(function () {
            var $this = $(this);
            if (confirm("Bạn chắc chắn muốn xóa sản phẩm này?")) {
                // Enable loader
                $("#loader").modal("show");

                var $cartDetailId = $this.next().val();
                $.ajax({
                    url: "/Home/RemoveItemInCart",
                    type: "POST",
                    datatype: "json",
                    data: { cartDetailId: $cartDetailId },
                    success: function (data) {
                        if (data.isSuccess) {
                            $this.parents(':eq(5)').remove();
                            var empty = $("#cart-left .checkout-item").length
                            if (empty == 0) {
                                $("#cart-left").html(`<h4>Bạn chưa có sản phẩm nào</h4>
                                <a class="primary-btn" href="/Home/Index"> Tiếp tục mua sắm</a>`);
                            }
                            //setTimeout(function () {
                            //    $("#loader").modal("hide");
                            //}, 2000);
                            $("#loader").modal("hide");
                            CartHome.ShowToast(true, data.message);

                            k--;
                            $("#item-sum").html(k); // Set count of product in cart
                            isAll = 0;

                            // Clear all checked of input
                            $("input#cart-select-all").prop("checked", false);
                            $("span#checkmark-select-item").each(function () {
                                $(this).prev().prop("checked", false);
                            });
                            $("#summary-price").val("0");
                            var summaryPrice = parseFloat($("#summary-price").val());
                            $("#checkout-summary-temp-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
                            $("#checkout-summary-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
                        }
                        else {
                            CartHome.ShowToast(false, data.message);
                        }
                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        CartHome.ShowToast(false, "Some error remove");
                    }
                });

                //if ($(".cart-infor").height() < 470) {
                //    $("#footer").addClass("fixed");
                //}
                //else {
                //    $("#footer").removeClass("fixed");
                //}
            }
        });

        $("#cart-confirm").click(function () {
            var postData = new Array();
            var $id = 0;
            var $sl = 0;
            var k = 0;

            $("input[name='cart-select-item']").each(function () {
                $this = $(this);
                if ($this.is(':checked')) {
                    $id = $this.parents(':eq(3)').find("#item-ID").val();
                    $sl = $this.parents(':eq(3)').find("input[name='quantity']").val()
                    postData.push($id);
                    postData.push($sl);
                    k++;
                }
            });

            if (k > 0) {
                $.ajax({
                    url: "/CheckOut/BuyedProduct",
                    type: "POST",
                    data: { lst: postData },
                    async: false,
                    success: function (data) {
                        if (data.isSuccess) {
                            window.location = data.url;
                        }
                        else
                            CartHome.ShowToast(false, data.message);

                    },
                    error: function (XMLHttpRequest, textStatus, errorThrown) {
                        CartHome.ShowToast(false, "Some error remove");
                    }
                });
            }
            else {
                CartHome.ShowToast(false, "Vui lòng chọn món hàng để thanh toán");
            }
        });
    }

    // Get sum price
    var k = $("#item-sum").attr("data-count"); // Get count product of cart
    var isAll = 0; // Check selected all product
    this.SumPrice = function (input, bool) {
        var $this = $(input).parents(':eq(3)');
        var $price = $this.find("#item-price").val();
        var $quantity = $this.find("input[name='quantity']").val();
        if (!$(input).prev().is(":checked")) { // Chon them san pham cong them tien
            //alert($price * $quantity);
            $("#summary-price").val($price * $quantity + parseFloat($("#summary-price").val()));
            var summaryPrice = parseFloat($("#summary-price").val());
            $("#checkout-summary-temp-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
            $("#checkout-summary-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
            if (bool) isAll++;
            else isAll = k;
        }
        else if ($(input).prev().is(":checked")) { // Bo chon san pham tru tien
            if ($("input[name='cart-select-all']").is(":checked") && bool) {
                $("input[name='cart-select-all']").prop("checked", false);
            }

            $("#summary-price").val(parseFloat($("#summary-price").val()) - $price * $quantity);
            var summaryPrice = parseFloat($("#summary-price").val());
            $("#checkout-summary-temp-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
            $("#checkout-summary-value").text(summaryPrice.toLocaleString('vi', { style: 'currency', currency: 'VND' }));
            if (bool) isAll--;
            else isAll = 0;
        }

        if (isAll == k && bool) { // If selected all product, prop input check all
            $("input[name='cart-select-all']").prop("checked", true);
        }
    }

    // Focus out of input
    this.InputFocusOut = function (input) {
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

    // Show toast
    this.ShowToast = function (bool, message) {
        var $toast = $("#snackbar");
        if (bool) { // Success
            $toast.addClass("show success");
            $toast.html(message);
        }
        else { // Error
            $toast.addClass("show error");
            $toast.html(message);
        }
        setTimeout(function () {
            $toast.removeClass("show");
            $toast.removeClass("success");
            $toast.removeClass("error");
        }, 2000);
    }

    this.Delay = function (callback, ms) {
        var timer = 0;
        return function () {
            var context = this, args = arguments;
            clearTimeout(timer);
            timer = setTimeout(function () {
                callback.apply(context, args);
            }, ms || 0);
        };
    }
}