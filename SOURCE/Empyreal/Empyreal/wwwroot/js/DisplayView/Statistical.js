$(document).ready(function () {
    Statistical.Events();
});

var Statistical = new function () {
    this.Events = function () {
        // Get Statistical
        Statistical.GetStatistical();

        // SignalR
        var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

        connection.on("ReloadStatistical", function () {
            Statistical.GetStatistical();
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
    
    // Get Statistical
    this.GetStatistical = function () {
        $.ajax({
            url: "/Statistical/GetAll",
            type: "POST",
            datatype: "json",
            async: false,
            data: { type: "" },
            success: function (result) {
                if (result.isSuccess) {
                    var newuser = Statistical.NumberWithCommas(result.statistical[0].newUser);
                    $("#stats-newuser").text(newuser);
                    var revenue = result.statistical[0].revenue.toLocaleString("vi-VN", { style: "currency", currency: "VND" })
                    $("#stats-revenue").text(revenue);
                    var orders = Statistical.NumberWithCommas(result.statistical[0].orders);
                    $("#stats-orders").text(orders);

                    var labels = new Array();
                    var monthlyrevenue = new Array();
                    var numberOfOrders = new Array();
                    for (var i = 0; i < result.statistical.length; i++) {
                        labels.push("Tháng " + result.statistical[i].month);
                        monthlyrevenue.push(result.statistical[i].monthlyRevenue);
                        numberOfOrders.push(result.statistical[i].numberOfOrders);
                    }

                    $("#myChart1").remove();
                    $('#chart-parent1').append('<canvas id="myChart1" height="100"></canvas>');

                    // Draw chart
                    var ctx1 = document.getElementById('myChart1').getContext('2d');
                    var myChart = new Chart(ctx1, {
                        type: 'line',
                        data: {
                            labels: labels,
                            datasets: [{
                                label: 'Doanh thu',
                                data: monthlyrevenue,
                                backgroundColor: [
                                    'rgba(54, 162, 235, 0.2)'
                                ],
                                borderColor: [
                                    'rgba(54, 162, 235, 0.6)'
                                ]
                            }
                                //    ,
                                //{
                                //    label: 'Số lượng đơn hàng',
                                //    data: numberOfOrders,
                                //    backgroundColor: [
                                //        'rgba(54, 162, 235, 0.2)'
                                //    ],
                                //    borderColor: [
                                //        'rgba(54, 162, 235, 1)'
                                //    ]
                                //    }
                            ]
                        },
                        options: {
                            title: {
                                display: true,
                                text: 'Thống kê doanh thu'
                            },
                            tooltips: {
                                callbacks: {
                                    label: function (tooltipItem, data) {
                                        var datasetLabel = data.datasets[tooltipItem.datasetIndex].label || 'Others';
                                        return datasetLabel + ': ' + tooltipItem.yLabel.toLocaleString("vi-VN", { style: "currency", currency: "VND" });
                                    }
                                }
                            },
                            scales: {
                                yAxes: [{
                                    ticks: {
                                        beginAtZero: true,
                                        steps: 10,
                                        stepValue: 2000000,
                                        max: 20000000,
                                        callback: function (label, index, labels) {
                                            return Intl.NumberFormat().format(label);
                                        }
                                    }
                                }]
                            }
                        }
                    });

                    $("#myChart2").remove();
                    $('#chart-parent2').append('<canvas id="myChart2" height="100"></canvas>');

                    var ctx2 = document.getElementById('myChart2').getContext('2d');
                    var myChart2 = new Chart(ctx2, {
                        type: 'line',
                        data: {
                            labels: labels,
                            datasets: [{
                                label: 'Số lượng đơn hàng',
                                data: numberOfOrders,
                                backgroundColor: [
                                    'rgba(255, 159, 64, 0.2)'
                                ],
                                borderColor: [
                                    'rgba(255, 159, 64, 0.6)'
                                ]
                            }]
                        },
                        options: {
                            title: {
                                display: true,
                                text: 'Thống kê số lượng đơn hàng'
                            },
                            scales: {
                                yAxes: [{
                                    display: true,
                                    scaleLabel: {
                                        display: true,
                                        labelString: 'Số lượng đơn hàng'
                                    },
                                    ticks: {
                                        beginAtZero: true,
                                        stepSize: 5,
                                        callback: function (label, index, labels) {
                                            return Intl.NumberFormat().format(label);
                                        }
                                    }
                                }]
                            }
                        }
                    });
                    //document.getElementById('myChart2').height = 1000;
                }
                else {
                    alert(result.message);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Some error get statistical");
            }
        });

        $.ajax({
            url: "/Statistical/StatisticalBestSeller",
            type: "POST",
            datatype: "json",
            data: { type: "" },
            async: false,
            success: function (result) {
                if (result.isSuccess) {
                    var labels = new Array();
                    var data = new Array();
                    for (var i = 0; i < result.statisticalBestSeller.length; i++) {
                        labels.push(result.statisticalBestSeller[i].name);
                        data.push(result.statisticalBestSeller[i].catalogId);
                    }
                    console.log(data);

                    $("#myChart3").remove();
                    $('#chart-parent3').append('<canvas id="myChart3" height="100"></canvas>');

                    // Draw chart
                    var ctx1 = document.getElementById('myChart3').getContext('2d');
                    var myChart = new Chart(ctx1, {
                        type: 'bar',
                        data: {
                            labels: labels,
                            datasets: [{
                                label: 'Số lượng đã bán',
                                data: data,
                                backgroundColor: [
                                    'rgba(54, 162, 235, 0.6)',
                                    'rgba(255, 206, 86, 0.6)',
                                    'rgba(75, 192, 192, 0.6)',
                                    'rgba(153, 102, 255, 0.6)',
                                    'rgba(255, 159, 64, 0.6)'
                                ]
                            }]
                        },
                        options: {
                            legend: { display: false },
                            title: {
                                display: true,
                                text: 'Sản phẩm bán chạy trong ngày'
                            },
                            scales: {
                                xAxes: [{
                                    gridLines: {
                                        display: false
                                    }
                                }],
                                yAxes: [{
                                    display: true,
                                    scaleLabel: {
                                        display: true,
                                        labelString: 'Số lượng đã bán'
                                    },
                                    ticks: {
                                        beginAtZero: true,
                                        callback: function (value) { if (value % 1 === 0) { return value; } }
                                    }
                                }]
                            }
                        }
                    });
                }
                else {
                    alert(result.message);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {
                alert("Some error get statistical");
            }
        });
    }

    this.NumberWithCommas = function (text) {
        return text.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }
}