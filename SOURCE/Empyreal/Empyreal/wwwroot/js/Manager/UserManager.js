
function DisableUser(element) {
    console.log("1");
    $this = element;
    userId = $this.closest('tr').find('td:nth-child(2)').text();

    $.ajax({
        url: "/User/DisableUser",
        type: "POST",
        datatype: "json",
        data: { Id: userId },
        success: function (data) {
            if (data.isSuccess) {
                ShowToast(true, data.message);
                $this.parent().append('<button type="button" class="control-danger table-controls" title="Cho phép hoạt động" onclick="EnableUser($(this));"><i class="fas fa-unlock-alt"></i></button>');
                $this.closest('tr').find('td:nth-child(7)').html('<span class="label label-danger">Ngưng hoạt động</span>');
                $this.remove();

            }
            else {
                ShowToast(false, data.message);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Some error update user");
        }
    });
}

function EnableUser(element) {
    console.log("2");
    $this = element;
    userId = $this.closest('tr').find('td:nth-child(2)').text();

    $.ajax({
        url: "/User/EnableUser",
        type: "POST",
        datatype: "json",
        data: { Id: userId },
        success: function (data) {
            if (data.isSuccess) {
                ShowToast(true, data.message);
                $this.parent().append('<button type="button" class="control-danger table-controls" title="Ngưng hoạt động" onclick="DisableUser($(this));"><i class="fas fa-lock"></i></button>');
                $this.closest('tr').find('td:nth-child(7)').html('<span class="label label-success">Hoạt động</span>');
                $this.remove();
            }
            else {
                ShowToast(false, data.message);
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert("Some error update user");
        }
    });
}

function ShowToast(bool, message) {
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