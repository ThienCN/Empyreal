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
    this.Layout = new function () {
        this.QuantityForm = `<div><label class="small-label">Số lượng</label>
                                    <div class="select-wrap">
                                        <input type="number" class="form-control" name="Quantity" id="Quantity" value="1" />
                                    </div></div>`;
        this.PriceForm = `<div><label class="small-label">Đơn giá (đồng)</label>
                                    <div class="select-wrap">
                                        <input type="number" class="form-control" name="PriceText" id="PriceText" value="0" "/>
                                    </div></div>`;
        this.DeleteForm = `<div class="icon-padding ">
                                        <button class="main-btn icon-btn-delete-detail" id="btnDeteleDetail" title="Xóa chi tiết sản phẩm" type="button"><i class="fas fa-minus"></i></button>
                                    </div>`;
        this.Input_DetailID = `<input type="hidden" id="ID" name="ProductDetailID" value="0" />`;
        this.Input_Delete_DetailID = `<input type="hidden" id="DeleteDetailID" name="DeleteDetailID" value="{0}" />`;
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
    this.Events = function () {

        // Button: Xóa hình ảnh
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

        //Button: Xóa chi tiết sản phẩm
        $(".icon-btn-edit-small").click(function () {
            var formDetail = $(this).parents(':eq(2)');

            var detailID = formDetail.find("#ID").val();
            console.log(detailID);

            if (detailID > 0) {
                var result = confirm("Sản phẩm này đã tồn tại trong kho.\nBạn vẫn muốn xóa sản phẩm này???");
                if (result == true) {

                    // Input has value = DetailID
                    var element = ProductUpdate.Layout.Input_Delete_DetailID.format(detailID);
                    console.log(element);
                    $("#" + ProductUpdate.FORM).append(element);
                    $(formDetail).remove();
                }
                else {
                    // Do nothings
                };
            } else {
                $(this).parents(':eq(2)').remove();
            }
        });

        //Button: Thêm chi tiết sản phẩm
        $("#btnDetail").click(function () {
            let STT = "0" + ProductUpdate.STT;
            var Container = "<fieldset><legend></legend><div id='DetailForm" + STT + "' class='detail-form col-xs-12 product-detail-group'></div></fieldset>";
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

            // Container Form
            var formContainer = "<div class='form-group row' id='FormGroup" + STT + "'></div>";
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
                let change = "Thêm chi tiết: Màu: {0} - Kích thước: {1}".format(CreatedNew["Color"], CreatedNew["Size"]);
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

                if (old["ID"] == CreatedNew["DetailID"]) {
                    flag_Exist = true;
                    if (old["Color"] != CreatedNew["Color"]) {
                        let change = "Chỉnh sửa chi tiết: Màu sắc {0} -> {1}".format(old["Color"], CreatedNew["Color"]);
                        history.push(change);
                    }
                    if (old["PriceText"] != CreatedNew["Price"]) {
                        let change = "Chỉnh sửa chi tiết: Đơn giá {0} -> {1}".format(old["PriceText"], CreatedNew["Price"]);
                        history.push(change);
                    }
                    if (old["Quantity"] != CreatedNew["Quantity"]) {
                        let change = "Chỉnh sửa chi tiết: Số lượng {0} -> {1}".format(old["Quantity"], CreatedNew["Quantity"]);
                        history.push(change);
                    }
                    if (old["Size"] != CreatedNew["Size"]) {
                        let change = "Chỉnh sửa chi tiết: Số lượng {0} -> {1}".format(old["Size"], CreatedNew["Size"]);
                        history.push(change);
                    }
                }
            }
            if (flag_Exist == false) {
                let change = "Xóa chi tiết: {0} ".format(detailOld[i]["ID"]);
                history.push(change);
            }
        }
        return history;
    }
}

var customerCheck = function() {
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