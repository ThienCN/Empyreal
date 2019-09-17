$(document).ready(function () {
    Profile.Events();
});

var Profile = new function () {
    this.Events = function () {
        console.log("yes");
        // Change password
        $("#is-change-pass").on("click", function () {
            $this = $(this).find("input[name='is-change-pass']");
            if ($this.attr("data-animation") == 0) {
                $this.attr("data-animation", "1");
                $("#password-change").slideDown(500);
                $("input[name='IsChangePass']").val("true");
                $(".icheckbox_minimal-grey").addClass("checked");
            }
            else {
                $this.attr("data-animation", "0");
                $("#password-change").slideUp(500);
                $("input[name='IsChangePass']").val("false");
                $(".icheckbox_minimal-grey").removeClass("checked");
            }
        });

        if ($("input[name='IsChangePass']").val() == "true") {
            $(".icheckbox_minimal-grey").addClass("checked");
            $("input[name='is-change-pass']").prop("checked", false);
            $("input[name='is-change-pass']").attr("data-animation", "1");
            $("#password-change").slideDown(500);
        }
    }
}