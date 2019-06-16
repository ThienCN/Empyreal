$(document).ready(function () {
    Payment.Events();
});

var Payment = new function () {
    this.Events = function () {
        // Change receiver
        $("input[name='is-change-receiver']").on("click", function () {
            if ($(this).attr("data-animation") == 0) {
                $(this).attr("data-animation", "1");
                $(".payment-sub").slideDown(500);
                $("input[name='IsRelative']").val("true");
            }
            else {
                $(this).attr("data-animation", "0");
                $(".payment-sub").slideUp(500);
                $("input[name='IsRelative']").val("false");
            }
        });

        if ($("input[name='IsRelative']").val() == "true") {
            $("input[name='is-change-receiver']").prop("checked", false);
            $("input[name='is-change-receiver']").attr("data-animation", "1");
            $(".payment-sub").slideDown(500);
        }

        // Validate change receiver form
        $("#receiver-form").validate({
            rules: {
                RelativeName: {
                    required: true,
                    minlength: 6
                },
                RelativePhoneNumber: {
                    required: true,
                    digits: true,
                    minlength: 10,
                    maxlength: 10
                }
            },
            messages: {
                RelativeName: {
                    required: "Vui lòng nhập họ tên",
                    minlength: jQuery.validator.format("Họ tên ít nhất {0} ký tự")
                },
                RelativePhoneNumber: {
                    required: "Vui lòng nhập số điện thoại",
                    digits: "Số điện thoại chỉ bao gồm số",
                    minlength: jQuery.validator.format("Số điện thoại phải là {0} số"),
                    maxlength: jQuery.validator.format("Số điện thoại phải là {0} số")
                }
            }
        });

        //$("#is-change-receiver").click(function () {
        //    var input = $(this).find("input");

        //    if (input.is(':checked')) {
        //        input.prop("checked", false);
        //        $(".payment-sub").slideDown(500);
        //    }
        //    else {
        //        input.prop("checked", true);
        //        $(".payment-sub").slideUp(500);
        //    }
        //});
    }
}