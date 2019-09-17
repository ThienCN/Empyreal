$(document).ready(function () {

    OrderManager.Events();
});

var OrderManager = new function () {
    this.Events = function () {

        // SignalR
        var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

        connection.on("OrderSuccess", function () {
            $("#notification").addClass("fas fa-bell");
        });

        // Reconnect loop
        function start() {
            connection.start().catch(function (err) {
                setTimeout(function () {
                    start();
                }, 5000);
            });
        }

        connection.onclose(function () {
            start();
        });

        start();
        // End SignalR
    }
}