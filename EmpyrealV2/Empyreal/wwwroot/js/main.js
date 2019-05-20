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

    // PRODUCTS SLICK
    $('#product-slick-1').slick({
        slidesToShow: 6,
        slidesToScroll: 4,
        autoplay: true,
        infinite: true,
        speed: 300,
        dots: true,
        arrows: false,
        appendDots: '.product-slick-dots-1',
        responsive: [{
            breakpoint: 991,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        },
        {
            breakpoint: 480,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }
        ]
    });

    $('#product-slick-2').slick({
        slidesToShow: 3,
        slidesToScroll: 2,
        autoplay: true,
        infinite: true,
        speed: 300,
        dots: true,
        arrows: false,
        appendDots: '.product-slick-dots-2',
        responsive: [{
            breakpoint: 991,
            settings: {
                slidesToShow: 1,
                slidesToScroll: 1
            }
        },
        {
            breakpoint: 480,
            settings: {
                dots: false,
                arrows: true,
                slidesToShow: 1,
                slidesToScroll: 1
            }
        }
        ]
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
    
    $(".icon-btn-edit-small").click(function () {
        var ref = $(this).val();
        //console.log(ref);

        if (ref != -1) {
            var r = confirm("Sản phẩm này đã tồn tại trong kho.\nBạn vẫn muốn xóa sản phẩm này???");
            if (r == true) {
                $(this).parents(':eq(2)').remove();
                //alert($(this).parents(':eq(2)').find("#item_Size").val());
                var dataDelete = (JSON.parse(`{
                "DetailId": "`+ $(this).parents(':eq(2)').find("#item_Id").val() + `"
            }`));
                $.ajax({
                    url: '/Admin/DeleteDetailProduct',
                    method: 'POST',
                    datatype: "json",
                    data: dataDelete,
                    async: false,
                    success: function (result) {
                        alert("Xóa sản phẩm thành công!");

                    }
                })

            }
            else {

            }
            //$(this).parents(':eq(2)').remove();
        }
    });

    $('select[name=selectByCatalog]').change(function () {
        //alert(this.value);
        $("#selectByCatalogForm").submit();
    });
    $(".icon-btn-edit-image-small").click(function () {
        //alert($(this).val());
        var r = confirm("Bạn muốn xóa hình ảnh này???");
        if (r == true) {
            if ($(this).val() != null) {
                var dataDeleteImage = (JSON.parse(`{
                "ImageId": "`+ $(this).val() + `"
            }`));

                $.ajax({
                    url: '/Admin/DeleteImageProduct',
                    method: 'POST',
                    datatype: "json",
                    data: dataDeleteImage,
                    async: false,
                    success: function (result) {
                        alert("Xóa hình ảnh thành công!");
                        $(this).parents(':eq(1)').remove();
                    }
                })
            }

        }

    });

 

    $("#btnSubmitEditProductForm").click(function () {
        //alert("yes");
        var totalSize = 0;
        var file = document.getElementById("Files");
        for (var temp = 0; temp < file.files.length; temp++) {
            totalSize += file.files[temp].size;
        }
        var totalSizeMb = Math.ceil(totalSize / 1024 / 1024);

        if (totalSizeMb < 20) {
            $('input[name="item.Price"]').each(function () {
                $(this).val(plainNumber($(this).val()));
                //console.log($(this).val());
            });
            PostDataEdit();
            if (detailList.length == 0) {
                alert("Hãy thêm chi tiết sản phẩm!");
                return;
            }
            var flag = false;
            for (var i = 0; i < detailList.length - 1; i++) {
                if (flag == true) break;
                else {
                    for (var j = (i + 1); j < detailList.length; j++) {
                        let elementFrist = detailList[i];
                        let elementNext = detailList[j];
                        if ((elementFrist.Size == elementNext.Size) && (elementFrist.Color == elementNext.Color)) {
                            // Yes/No confirm
                            var r = confirm("Sản phẩm [ " + elementNext.Size + " , " + elementNext.Color + " ] đã bị trùng lặp!\nHệ thông sẽ lưu trữ thông tin sản phẩm cuối cùng\n Bạn có đồng ý không?");
                            if (r == true) {
                                var dataDelete =
                                    (JSON.parse(`{ "DetailId": "` + elementFrist.DetailId + `"}`));
                                $.ajax({
                                    url: '/Admin/DeleteDetailProduct',
                                    method: 'POST',
                                    datatype: "json",
                                    data: dataDelete,
                                    async: false,
                                    success: function () {
                                        detailList.splice(i, 1); // Xóa phẩn tử trong Mảng Detail
                                    }
                                })

                                submitData(detailList);
                                $("#EditProductFormSubmit").submit();

                            }
                            flag = true;
                            break;

                        }

                    }
                }

            }
            if (flag == false) {

                submitData(detailList);
                $("#EditProductFormSubmit").submit();

            }


        }
        else {
            alert("Tổng dung lượng Image được thêm phải < 20mb!\nTổng hiện tại là " + totalSizeMb + "mb");
        }

    });

    var detailList;
    var dataList = new Object();
    //var i = 0;
    function PostData() {

        detailList = new Array();
        $(".product-detail-group").each(function () {
            //
            detailList.push(JSON.parse(`{
                
                "Size": "`+ $(this).find("#Size").val() + `",
                "Color": "` + $(this).find("#Color").val() + `",
                "Quantity": "` + $(this).find("#Quantity").val() + `",
                "Price": "` + $(this).find("#Price").val() + `"
            }`));
            //
        });
        submitData(detailList);
    };
    function PostDataEdit() {
        detailList = new Array();
        $(".product-detail-group").each(function () {
            //

            //var i = quantity;
            detailList.push(JSON.parse(`{
                "DetailId":"`+ $(this).find("#item_Id").val() + `",
                "Size": "`+ $(this).find("#item_Size").val() + `",
                "Color": "` + $(this).find("#item_Color").val() + `",
                "Quantity": "` + $(this).find("#item_Quantity").val() + `",
                "Price": "` + $(this).find("#item_Price").val() + `"
            }`));
            //
        });

        //alert(detailList[0].Size);

    };
    function submitData(list) {
        var postData = {
            DataList: list
        };
        $.ajax({
            url: '/Admin/AddList',
            method: 'POST',
            datatype: "json",
            data: postData,
            async: false,
            success: function (result) {
                //alert(data);
            }
        })
    }

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

})(jQuery);
//End jQuery

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
    //console.log($(input).val());

    var value = input.value,
        plain = plainNumber(value),
        reversed = reverseNumber(plain),
        reversedWithDots = reversed.match(/.{1,3}/g).join('.'),
        normal = reverseNumber(reversedWithDots);
    
    input.value = normal;
}

/** Empyreal: Thư viện dùng chung
 *  
 * */
var Empyreal = new function () {
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

    
    this.Events = new function () {
        // Multiple images preview in browser
        this.ImagePreview = function (input, placeToInsertImagePreview) {

            $(placeToInsertImagePreview).empty();

            if (input.files) {
                var length = input.files.length;

                for (var i = 0; i < length; i++) {
                    var reader = new FileReader();

                    reader.onload = function (event) {
                        $($.parseHTML('<img>')).attr('src', event.target.result).attr('class', 'imgpreview').appendTo(placeToInsertImagePreview);

                    }
                    //alert(input.files[i].name);
                    reader.readAsDataURL(input.files[i]);
                }
            }
        };
    };
    //end Empyreal.Event
}

 