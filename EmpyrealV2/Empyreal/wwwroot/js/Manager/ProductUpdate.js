$(document).ready(function () {
    console.log("debug");
    ProductUpdate.Events();
});

var ProductUpdate = new function () {
    this.STT = 1;
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
        this.Input_DetailID = `<input type="hidden" id="ProductDetailID" name="ProductDetailID" value="0" />`;
        this.Input_PriceID = `<input type="hidden" id="PriceId" name="PriceId" value="0" />`;

    }

    this.Events = function () {

        //Button: Thêm chi tiết sản phẩm
        $("#btnDetail").click(function () {
            let STT = "0" + ProductUpdate.STT;
            var Container = "<fieldset><legend></legend><div id='DetailForm" + STT + "' class='detail-form col-xs-12'></div></fieldset>";
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
            // Gắn sự kiện Delete cho Controll
            var btnDelete = $(typeGroup.find("#btnDeteleDetail"));
            btnDelete.bind("click", ProductUpdate.Controlls.DeteteDetail);
            //T
            ProductUpdate.STT++;


        });

        //Image Review
        $("#Files").unbind("change");
            
        $("#Files").bind("change", function () {
            Empyreal.Events.ImagePreview(this, 'div.gallery');
        });

        //Button: Cập nhật sản phẩm
        $("#" + ProductUpdate.btnSubmit).click(function () {
            $('#' + ProductUpdate.formSubmit).submit();
        });

    };
    this.Controlls = new function () {
        this.DeteteDetail = function (e) {

            $(this).parents(':eq(3)').remove();
        };

    }

}
