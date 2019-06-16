$(document).ready(function () {
    ProductManager.Events();
});

var ProductManager = new function () {
    this.Events = function () {
        $("#historyPopup").click(function () {
            $.ajax({
                url: "/Product/EditHistoryPartial",
                type: "POST",
                datatype: "json",
                data: {
                    productID: 1
                },
                success: function (result) {
                    $mymodal = $("#historyModal");
                    //update the modal's body with the response received
                    $mymodal.find("div.modal-body").html(result);
                    // Show the modal
                    $mymodal.modal("show");
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    alert("Some error get partial");
                }
            });
        });
    }
}