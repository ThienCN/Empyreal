$(document).ready(function () {
    
    ProductManager.Events();
});

var ProductManager = new function () {
    this.PageSize = 10;
    this.Event_PageNumber = function () {
        $("#History .pagination .PagedList-skipToPrevious").remove();
        $("#History .pagination .PagedList-skipToNext").remove();
        $("#History .pagination a").attr("href","###");

        
        $("#History .pagination a").prop("onclick", null).off("click");
        $("#History .pagination a").bind("click", function HistoryPaged_Click(e) {
            let element = e.target;

            let page = element.text;
            let detail = "";
            let pageSize = ProductManager.PageSize;

            ProductManager.GetHistoryPartial(detail, page, pageSize);
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
                ProductManager.Event_PageNumber();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Some error get partial");
            }
        });
    }
    this.Events = function () {

        $("#historyPopup").click(function () {
            var data = {
                detail: "",
                page: "",
                pageSize: ProductManager.PageSize,
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
                    ProductManager.Event_PageNumber();
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Some error get partial");
                }
            });
        });
    }
}