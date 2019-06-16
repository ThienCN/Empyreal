$(document).ready(function () {
    CheckOut.Events();
});

var CheckOut = new function () {
    this.Events = function () {
        // Set background color
        $("body").css("background", "#f1f1f1");

        // Change dropdownlist to chosen
        $(".province-select").chosen({
            disable_search_threshold: 10
        });
        $(".district-select").chosen();
        $(".ward-select").chosen();

        // Show add new address
        $("#add-new-address").click(function () {
            if ($("#new-address").hasClass("show")) {
                $("#new-address").addClass("opacity");
                setTimeout(function () {
                    $("#new-address").removeClass("opacity");
                }, 300);
            }
            else {
                $("#new-address").addClass("show");
            }
            $("#footer").removeClass("fixed");
            //$("input[type='text']").val("");
            $("textarea").val("");
        });

        // Hidden add new address form
        if ($("#old-order").length > 0) {
            $("#new-address").addClass("display-none");
            $("#footer").addClass("fixed");
        }

        // Validate shipping form
        $("#shipping-form").validate({
            rules: {
                Name: {
                    required: true,
                    minlength: 6
                },
                PhoneNumber: {
                    required: true,
                    digits: true,
                    minlength: 10,
                    maxlength: 10
                },
                Province: "required",
                District: "required",
                Ward: "required",
                Address: {
                    required: true,
                    minlength: 6
                },
                AddressType: "required"
            },
            messages: {
                Name: {
                    required: "Vui lòng nhập họ tên của bạn",
                    minlength: jQuery.validator.format("Họ tên ít nhất {0} ký tự")
                },
                PhoneNumber: {
                    required: "Cho chúng tôi biết số điện thoại để tiện liên lạc",
                    digits: "Số điện thoại chỉ bao gồm số",
                    minlength: jQuery.validator.format("Số điện thoại phải là {0} số"),
                    maxlength: jQuery.validator.format("Số điện thoại phải là {0} số")
                },
                Province: "Vui lòng chọn Tỉnh/Thành phố",
                District: "Vui lòng chọn Quận/Huyện",
                Ward: "Vui lòng chọn Phường/Xã",
                Address: {
                    required: "Chúng tôi cần địa chỉ để giao hàng cho bạn",
                    minlength: jQuery.validator.format("Địa chỉ ít nhất {0} ký tự")
                },
                AddressType: "required"
            }
        });

        // Event dropdownlist Province change
        $("#Province").change(function () {
            $provinceID = $(this).val();

            $.ajax({
                url: "/CheckOut/ProvinceChange",
                type: "POST",
                datatype: "json",
                data: { provinceID: $provinceID },
                success: function (result) {
                    if (result.isSuccess) {
                        // Bind districts of province
                        $("#District").html("");
                        $.each(result.districts, function (i, district) {
                            $("#District").append($('<option></option>')
                                .val(district.id).html(district.name))
                        });
                        $('.district-select').trigger("chosen:updated");

                        // Bind wards of district
                        $("#Ward").html("");
                        $.each(result.wards, function (i, ward) {
                            $("#Ward").append($('<option></option>')
                                .val(ward.id).html(ward.name))
                        });
                        $('.ward-select').trigger("chosen:updated");
                    }
                    else {
                        alert(result.message);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Some error get district");
                }
            });
        });

        // Event dropdownlist District change
        $("#District").change(function () {
            $districtID = $(this).val();

            $.ajax({
                url: "/CheckOut/DistrictChange",
                type: "POST",
                datatype: "json",
                data: { districtID: $districtID },
                success: function (result) {
                    if (result.isSuccess) {
                        // Bind wards of district
                        $("#Ward").html("");
                        $.each(result.wards, function (i, ward) {
                            $("#Ward").append($('<option></option>')
                                .val(ward.id).html(ward.name))
                        });
                        $('.ward-select').trigger("chosen:updated");
                    }
                    else {
                        alert(result.message);
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Some error get district");
                }
            });
        });
    }
}