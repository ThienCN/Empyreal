$(document).ready(function () {
    console.log("debug");
    ProductUpdate.Init();

    ProductUpdate.Events();
});

var ProductUpdate = new function () {
    this.STT = 1;
    this.FORM = "ProductFormSubmit";
    this.FIELD_PRODUCT_ID = "ProductId";
    this.isUpdate = ""
    this.btnSubmit = "btnSubmitProductForm";
    this.formSubmit = "ProductFormSubmit";
    this.ListColor = {};
    this.ListSize = {};

    this.Layout = new function () {
        this.QuantityForm = `<div class="col-md-5"><label class="col-md-4 control-label">Số lượng</label>
                                <div class="col-md-8">
                                    <div class="select-wrap">
                                        <input type="number" class="form-control" name="Quantity" id="Quantity" value="1" />
                                </div></div></div>`;
        this.PriceForm = `<div class="col-md-5"><label class="col-md-4 control-label">Đơn giá</label>
                                <div class="col-md-8">
                                    <div class="select-wrap">
                                        <input type="number" class="form-control" name="PriceText" id="PriceText" onkeyup="splitInDots(this)" value="0" "/>
                                    </div></div></div><div class="col-md-2 price-text">(đồng)</div>`;
        this.DeleteForm = `<div class="icon-padding col-md-2">
                                        <button class="table-controls control-danger icon-btn-delete-detail" id="btnDeteleDetail" title="Xóa chi tiết sản phẩm" type="button" onclick="ProductUpdate.DeleteDetail(this)"><i class="fas fa-minus"></i></button>
                                    </div>`;
        this.Input_DetailID = `<input type="hidden" id="ID" name="ProductDetailID" value="0" />`;
        this.Input_Delete_DetailID = `<input type="hidden" id="DeleteDetailID" name="DeleteDetailID" value="{0}" />`;
        this.Input_Delete_ImageID = `<input type="hidden" id="DeleteImageID" name="DeleteImageID" value="{0}" />`;

        this.Input_History = `<input type="hidden" id="History" name="History" value="{0}" />`;

        this.Input_PriceID = `<input type="hidden" id="PriceId" name="PriceId" value="0" />`;

    }

    this.Init = function () {
        ProductUpdate.isUpdate = ($("#IsUpdate").val() == "true");
        let productID = $("#" + ProductUpdate.FIELD_PRODUCT_ID).val();
        if (!ProductUpdate.isUpdate || productID == 0 || productID == "0") {
            // Thêm mới
            $("#historyModal").remove();
            $("#historyPopup").remove();

        }
    }
    this.ShowPrice = function () {
        let listInputPrice = $("input[name='PriceText']");
        for (index = 0; index < listInputPrice.length; index++) {
            let item = $(listInputPrice[index]);
            item.trigger("keyup");
        }
    }

    this.DefaultValue = function () {
        let listContainer = $(".product-detail-group");
        for (index = 0; index < listContainer.length; index++) {
            let item = $(listContainer[index]).find("#Size,#Color");
            let id = $(listContainer[index]).find("#ID").val();
            if (id != 0) {
                item.find('option:not(:selected)').attr('disabled', true);
                item.attr("readonly", true);
            }
        }

    }
    this.Event_PageNumber = function () {
        $("#historyModal .pagination .PagedList-skipToPrevious").remove();
        $("#historyModal .pagination .PagedList-skipToNext").remove();
        $("#historyModal .pagination a").attr("href", "###");


        $("#historyModal .pagination a").prop("onclick", null).off("click");
        $("#historyModal .pagination a").bind("click", function HistoryPaged_Click(e) {
            let element = e.target;
            let productID = $("#" + ProductUpdate.FIELD_PRODUCT_ID).val();

            let page = element.text;
            let detail = productID;
            let pageSize = 5;

            ProductUpdate.GetHistoryPartial(detail, page, pageSize);
        });

    }
    this.GetHistoryPartial = function (detail, page, pageSize) {

        var data = {
            detail: detail,
            page: page,
            pageSize: pageSize,
        };
        $.ajax({
            url: "/History/GetHistoryPartial",
            async: false, // !important: Xử lý đồng bộ
            type: "Get",
            dataType: "html",
            data: data,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                var myModal = $("#historyModal");
                //update the modal's body with the response received
                myModal.find("div.modal-body").html(result);
                // Show the modal
                myModal.modal("show");
                ProductUpdate.Event_PageNumber();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Some error get partial");
            }
        });
    }

    this.DeleteDetail = function (e) {
        var formDetail = $(e).parents(':eq(2)');

        var detailID = formDetail.find("#ID").val();
        console.log(detailID);

        if (detailID > 0) {
            var result = confirm("Sản phẩm này đã tồn tại trong kho.\nBạn vẫn muốn xóa sản phẩm này???");
            if (result == true) {

                // Input has value = DetailID
                var element = ProductUpdate.Layout.Input_Delete_DetailID.format(detailID);
                $("#" + ProductUpdate.FORM).append(element);
                $(formDetail).remove();
            }
            else {
                // Do nothings
            };
        } else {
            $(formDetail).remove();
        }
    }

    this.Events = function () {

        ProductUpdate.ShowPrice();
        ProductUpdate.DefaultValue();

        // Button: Xóa hình ảnh
        $(".icon-btn-edit-image-small").click(function () {
            //alert($(this).val());
            var r = confirm("Bạn muốn xóa hình ảnh này???");
            if (r == true) {
                let imageID = $(this).val();
                var element = ProductUpdate.Layout.Input_Delete_ImageID.format(imageID);
                $("#" + ProductUpdate.FORM).append(element);
                $($(this).parent()).remove();
            }

        });

        //Button: Xóa chi tiết sản phẩm
        //$(".icon-btn-delete-detail").on("click",function () {
        //    var formDetail = $(this).parents(':eq(2)');

        //    var detailID = formDetail.find("#ID").val();
        //    console.log(detailID);

        //    if (detailID > 0) {
        //        var result = confirm("Sản phẩm này đã tồn tại trong kho.\nBạn vẫn muốn xóa sản phẩm này???");
        //        if (result == true) {

        //            // Input has value = DetailID
        //            var element = ProductUpdate.Layout.Input_Delete_DetailID.format(detailID);
        //            console.log(element);
        //            $("#" + ProductUpdate.FORM).append(element);
        //            $(formDetail).remove();
        //        }
        //        else {
        //            // Do nothings
        //        };
        //    } else {
        //        $(this).parents(':eq(2)').remove();
        //    }
        //});

        //Button: Thêm chi tiết sản phẩm
        $("#btnDetail").click(function () {
            let STT = "0" + ProductUpdate.STT;
            var Container = "<div id='DetailForm" + STT + "' class='detail-form product-detail-group'></div>";
            $(".product-detail").append(Container);
            var detailForm = $($("#DetailForm" + STT));
            detailForm.append(formContainer);

            // Container Type
            var TypeContainer = "<div class='detail-group row' id='TypeGroup" + STT + "'></div>";
            detailForm.append(TypeContainer);
            // Group Type
            var typeGroup = $("#TypeGroup" + STT);
            //Khởi tạo combobox = Ajax return (Partial View) 
            typeGroup.append(ProductUpdate.Layout.Input_DetailID);
            Empyreal.Controls.CreateComboBox("Size", typeGroup);
            Empyreal.Controls.CreateComboBox("Color", typeGroup);
            typeGroup.append(ProductUpdate.Layout.DeleteForm);
            //$(".icon-btn-delete-detail").bind('click', ProductUpdate.DeleteDetail(this));
            // Container Form
            var formContainer = "<div class='quantity-group row' id='FormGroup" + STT + "'></div>";
            detailForm.append(formContainer);
            // Form Type
            var formGroup = $("#FormGroup" + STT);
            //Khởi tạo control
            formGroup.append(ProductUpdate.Layout.QuantityForm);
            formGroup.append(ProductUpdate.Layout.PriceForm);
            formGroup.append(ProductUpdate.Layout.Input_PriceID);
            //
            ProductUpdate.STT++;


        });

        //Image Review
        $("#Files").unbind("change");

        $("#Files").bind("change", function () {
            Empyreal.Events.ImagePreview(this, 'div.gallery');
        });

        // Show History
        $("#historyPopup").click(function () {
            let productID = $("#" + ProductUpdate.FIELD_PRODUCT_ID).val();
            var data = {
                detail: productID,
                page: "",
                pageSize: "10",
            };
            $.ajax({
                url: "/History/GetHistoryPartial",
                async: false, // !important: Xử lý đồng bộ
                type: "Get",
                dataType: "html",
                data: data,
                contentType: 'application/json; charset=utf-8',
                success: function (result) {
                    var myModal = $("#historyModal");
                    //update the modal's body with the response received
                    myModal.find("div.modal-body").html(result);
                    // Show the modal
                    myModal.modal("show");
                    ProductUpdate.Event_PageNumber();
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Some error get partial");
                }
            });
        });

        //Button: Cập nhật sản phẩm
        $("#" + ProductUpdate.btnSubmit).click(function () {
            var check = customerCheck();


            // History
            let history = [];
            if ($("#IsUpdate").val() == "true") {
                history = ProductUpdate.CheckHistory();
            }
            for (let i = 0; i < history.length; i++) {
                let change = history[i];
                // element has value = change
                var element = ProductUpdate.Layout.Input_History.format(change);
                $("#" + ProductUpdate.FORM).append(element);
            }


            if (check == true) {
                console.log(history);
                $("#IsError").val(check);
                return false; // preven default
            }

            // Success
            $("#IsError").val(check);
            $('#' + ProductUpdate.FORM).submit();
        });

    };

    /**
     * Get Size of Image Files
     * return size;
     * @since [Lương Mỹ] Create [24/05/2019]
     * */
    this.GetSizeImage = function () {
        var totalSize = 0;
        var file = document.getElementById("Files");
        for (var temp = 0; temp < file.files.length; temp++) {
            totalSize += file.files[temp].size;
        }
        var totalSizeMb = Math.ceil(totalSize / 1024 / 1024);
        return totalSizeMb;
    }

    /**
     * Get Data ProductDetail
     * return Array;
     * @since [Lương Mỹ] Create [13/05/2019]
     * */
    this.GetDataDetail = function () {
        var data = [];
        var grpDetail = $(".product-detail-group");
        for (let i = 0; i < grpDetail.length; i++) {
            var container = $(grpDetail[i]);
            let id = container.find("#ID").val();
            let size = container.find("#Size").val();
            let color = container.find("#Color").val();
            let quantity = container.find("#Quantity").val();
            let price = container.find("#PriceText").val();
            data.push({
                DetailID: id,
                Size: size,
                Color: color,
                Quantity: quantity,
                Price: price,
            });
        }
        return data;
    }

    this.CheckHistory = function () {
        var history = [];
        var productOld = JSON.parse($("#ProductOld").val());
        var detailsNew = ProductUpdate.GetDataDetail();
        var productName = $("#ProductName").val();
        var catalogID = $("#Catalog").val();
        var description = $("#Description").val();

        //check
        if (productOld["ProductName"] != productName) {
            let change = "Tên sản phẩm: {0} -> {1}".format(productOld["ProductName"], productName);
            history.push(change);
        }
        //if (productOld["Description"] != description) {
        //    let change = "Mô tả đã bị thay đổi";
        //    history.push(change);
        //}
        if (productOld["Catalog"] != catalogID) {
            let change = "Danh mục: {0} -> {1}".format(productOld["Catalog"], catalogID);
            history.push(change);
        }
        var detailOld = productOld.ProductDetails;

        var detailCheck = [];
        for (let j = 0; j < detailsNew.length; j++) {
            let CreatedNew = detailsNew[j];

            if (CreatedNew["DetailID"] == 0) { // tạo mới chi tiết
                let color = getOptionsText("Color", CreatedNew["Color"]);
                let size = getOptionsText("Size", CreatedNew["Size"]);
                let change = "Thêm chi tiết: Màu: {0} - Kích thước: {1}".format(size, color);
                history.push(change);
                continue;
            }
            detailCheck.push(CreatedNew);
        };
        // Kiểm tra chi tiết bị xóa + Thay đổi
        for (let i = 0; i < detailOld.length; i++) {
            let flag_Exist = false;
            let old = detailOld[i];

            for (let j = 0; j < detailCheck.length; j++) {
                let CreatedNew = detailCheck[j];
                let color = getOptionsText("Color", CreatedNew["Color"]);
                let size = getOptionsText("Size", CreatedNew["Size"]);
                let listChange = [];
                let masterStr = "Chỉnh sửa Chi tiết {0} - {1} <br>".format(size, color);
                let tabTag = "&nbsp;&nbsp;&nbsp;&nbsp;";
                if (old["ID"] == CreatedNew["DetailID"]) {
                    flag_Exist = true;
                    if (formatNumber(old["PriceText"]) != formatNumber(CreatedNew["Price"])) {
                        let change = tabTag + " - Đơn giá {0} -> {1} <br>".format(formatNumber(old["PriceText"]), formatNumber(CreatedNew["Price"]));
                        listChange.push(change);
                    }
                    if (old["Quantity"] != CreatedNew["Quantity"]) {
                        let change = tabTag + " - Số lượng {0} -> {1} <br>".format(old["Quantity"], CreatedNew["Quantity"]);
                        listChange.push(change);
                    }

                }
                if (listChange.length > 0) {
                    let resultChange = masterStr;
                    $.each(listChange, function (index, value) {
                        return resultChange += value;
                    });
                    history.push(resultChange);
                }


            }
            if (flag_Exist == false) {
                let itemDelete = detailOld[i];
                let color = getOptionsText("Color", itemDelete["Color"]);
                let size = getOptionsText("Size", itemDelete["Size"]);

                let change = "Xóa chi tiết {0}: {1} ".format(size, color);
                history.push(change);
            }
        }
        return history;
    }
}

var customerCheck = function () {
    var isValid = false;
    let data = [];
    var message_array = [];
    Empyreal.Common.ClearMessage();
    // Check Detail
    data = ProductUpdate.GetDataDetail();
    if (!isValid) {
        // Empty
        if (data == [] || !data || data.length == 0) {
            isValid = true;
            let msg = "Chi tiết sản phẩm không được trống!";
            message_array.push(msg);
        }
    }
    //Check File Image: SizeFile
    let sizeImages = ProductUpdate.GetSizeImage();
    if (sizeImages > 20) {
        isValid = true;
        let msg = "Tổng dung lượng của Hình ảnh được thêm phải < 20mb!\nTổng hiện tại là {0}mb".format(sizeImages);
        message_array.push(msg);


    }

    if (isValid) {
        Empyreal.Common.ShowMessage(message_array);
    }

    return isValid;
}

function formatNumber(input) {
    let number = input.toString().split(',').join('');
    return number.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1.')
}

/**
 * use for ComboBox
 * @param {any} selTag
 */
function getOptionsText(selTagID, selectedIndex) {
    let element = $("#" + selTagID);
    var text = $(element).find("option[value ='" + selectedIndex + "']").text();
    return text;
}