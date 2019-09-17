$(document).ready(function () {
    SearchPaged.Events();
});


var SearchPaged = new function () {
    this.PAGESIZE = 20;
    this.Event_PageNumber = function () {
        $("#product-search .pagination .PagedList-skipToPrevious").remove();
        $("#product-search .pagination .PagedList-skipToNext").remove();
        $("#product-search .pagination a").attr("href", "###");

        $("#product-search .pagination a").prop("onclick", null).off("click");
        $("#product-search .pagination a").bind("click", function SearchPaged_Click(e) {
            let element = e.target;

            let page = element.text;
            let detail = $("#title").val();
            let pageSize = SearchPaged.PAGESIZE;
                
            SearchPaged.GetSearchPartial(detail, page, pageSize);
        });
    };

    this.GetSearchPartial = function (detail, page, pageSize) {

        var data = {
            KeySearch: detail,
            page: page,
            pageSize: pageSize,
        };
        $.ajax({
            url: "/Search/GetSearchPartial",
            async: false, // !important: Xử lý đồng bộ
            type: "Get",
            dataType: "html",
            data: data,
            contentType: 'application/json; charset=utf-8',
            success: function (result) {
                var container = $("#product-search");
                //update the modal's body with the response received
                container.html(result);
                // Show the modal
                SearchPaged.Event_PageNumber();
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Some error get partial");
            }
        });
    };
    /**
     *  Use to call All events when page start
     * */
    this.Events = function () {

        //
        //$("#slider").slider();

        //
        let detail = $("#title").val();
        let pageSize = SearchPaged.PAGESIZE;
        SearchPaged.GetSearchPartial(detail, 1, pageSize);

    }
}