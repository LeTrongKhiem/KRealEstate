$(function() {
    "use strict";

    /* ===== Knob chart initialization ===== */

    $(function() {

        $('.knob').each(function() {

            var elm = $(this);
            var perc = elm.attr("value");

            elm.knob();

            $({ value: 0 }).animate({ value: perc }, {
                duration: 1000,
                easing: 'swing',
                progress: function() {
                    elm.val(Math.ceil(this.value)).trigger('change')
                }
            });
        });
    });

    // ============================================================== 
    // Statistics Chart
    // ============================================================== 

    var chart1 = new Chartist.Line('.stat', {
        labels: [0, 5, 10, 15, 20, 25],
        series: [
            [40, 10, 33, 18, 27, 45],
            [10, 24, 37, 11, 30, 25]
        ]
    }, {
        high: 50,
        low: 0,
        height: '278px',
        showArea: false,
        fullWidth: true,
        axisY: {
            onlyInteger: true,
            showGrid: false,
            offset: 20,
        },
        plugins: [
            Chartist.plugins.tooltip()
        ]
    });

    // ============================================================== 
    // New Sales Chart
    // ============================================================== 

    var chart2 = new Chartist.Line('.ct-sales-chart', {
        series: [
            [2, 3, 4, 4, 3, 2, 2, 3, 4, 4.9, 5.5, 6, 6, 5, 4, 4, 5, 6, 7]
        ]
    }, {
        low: 0,
        showArea: true,
        fullWidth: true,
        height: '80px',
        plugins: [
            Chartist.plugins.tooltip()
        ]
    });

    // ============================================================== 
    // User quota Chart
    // ============================================================== 

    var chart3 = new Chartist.Bar('.ct-uq-chart', {
        series: [
            [10, 10, 10, 10, 10, 10, 10],
            [5, 3, 7, 6, 8, 2, 4]
        ]
    }, {
        high: 10,
        low: 0,
        stackBars: true,
        fullWidth: true,
        height: '80px',
        plugins: [
            Chartist.plugins.tooltip()
        ]
    }).on('draw', function(data) {
        if (data.type === 'bar') {
            data.element.attr({
                style: 'stroke-width: 7px'
            });
        }
    });

    /* ===== Statistics chart ===== */

    var chart5 = new Chartist.Line('.stat', {
        labels: [0, 5, 10, 15, 20, 25],
        series: [
            [40, 10, 33, 18, 27, 45],
            [10, 24, 37, 11, 30, 25]
        ]
    }, {
        high: 50,
        low: 0,
        height: '278px',
        showArea: false,
        fullWidth: true,
        axisY: {
            onlyInteger: true,
            showGrid: false,
            offset: 20,
        },
        plugins: [
            Chartist.plugins.tooltip()
        ]
    });

    /* ===== Visits chart ===== */

    var chart6 = new Chartist.Bar('.ct-visit', {
        labels: [1, 2, 3],
        series: [50, 70, 60]
    }, {
        distributeSeries: true,
        chartPadding: {
            left: -20,
            right: -10
        },
        axisX: {
            showLabel: true,
            showGrid: false
        },
        axisY: {
            showLabel: false,
            showGrid: false
        },
        plugins: [
            Chartist.plugins.tooltip()
        ]
    });

    /* ===== Revenue chart ===== */

    var chart7 = new Chartist.Line('.ct-revenue', {
        labels: [0, 1, 2, 3, 4, 5, 6, 7],
        series: [
            [0, 3, 5, 3, 2, 4, 7, 6]
        ]
    }, {
        chartPadding: {
            left: -20,
            top: 10,
        },
        low: 1,
        showPoint: true,
        height: '100px',
        fullWidth: true,
        axisX: {
            showLabel: true,
            showGrid: true
        },
        axisY: {
            showLabel: false,
            showGrid: false
        },
        showArea: true,
        plugins: [
            Chartist.plugins.tooltip()
        ]
    });



    $("#earning").easyPieChart({
        barColor: "#4da8db",
        trackColor: !1,
        scaleColor: !1,
        scaleLength: 0,
        lineCap: "square",
        lineWidth: 12,
        size: 96,
        rotate: 180,
        animate: { duration: 2e3, enabled: !0 }
    });
    $("#pending").easyPieChart({
        barColor: "#4db7df",
        trackColor: !1,
        scaleColor: !1,
        scaleLength: 0,
        lineCap: "square",
        lineWidth: 12,
        size: 74,
        rotate: 180,
        animate: { duration: 2e3, enabled: !0 }
    });
    $("#booking").easyPieChart({
        barColor: "#4ccfe4",
        trackColor: !1,
        scaleColor: !1,
        scaleLength: 0,
        lineCap: "square",
        lineWidth: 12,
        size: 50,
        rotate: 180,
        animate: { duration: 2e3, enabled: !0 }
    });

    // ============================================================== 
    // Sales Difference chart
    // ============================================================== 
    var chart8 = new Chartist.Line('.ct-sd-chart', {
        labels: [1, 2, 3, 4, 5, 6, 7, 8],
        series: [
            [5, 6, 7, 8, 5, 3, 5, 4],
            [3, 7, 5, 7, 2, 6, 8, 3]
        ]
    }, {
        high: 10,
        low: 0,
        height: '230px',
        showArea: true,
        fullWidth: true,
        chartPadding: 0,
        axisX: {
            showLabel: false,
            divisor: 2,
            showGrid: false,
            offset: 0
        },
        axisY: {
            showLabel: false,
            showGrid: false,
            offset: 0
        },
        plugins: [
            Chartist.plugins.tooltip()
        ]
    });

    // ============================================================== 
    // Item earning chart
    // ============================================================== 
    var chart9 = new Chartist.Line('.ct-ie-chart', {
        labels: [1, 2, 3, 4, 5],
        series: [
            [8, 15, 9, 18, 10],
            [15, 9, 20, 9, 17]
        ]
    }, {
        high: 25,
        low: 0,
        showArea: true,
        lineSmooth: Chartist.Interpolation.simple({
            divisor: 10
        }),
        height: '273px',
        fullWidth: true,
        chartPadding: 0,
        plugins: [
            Chartist.plugins.tooltip()
        ],
        axisX: {
            showLabel: false,
            divisor: 2,
            showGrid: false,
            offset: 0
        },
        axisY: {
            showLabel: false,
            showGrid: false,
            offset: 0
        }
    });

    // ============================================================== 
    // Sales chart
    // ==============================================================
    var chart10 = new Chartist.Bar('.chartist-sales-chart', {
        labels: ['a', 'b', 'c', 'd', 'e', 'f', 'g'],
        series: [
            [5, 3, 3, 2.5, 4, 3.5, 3],
            [4, 4, 2, 3, 5, 2, 4]
        ]
    }, {
        high: 5,
        low: 0,
        height: '236px',
        fullWidth: true,
        chartPadding: 0,
        axisX: {
            showLabel: true,
            showGrid: false,
        },
        axisY: {
            showLabel: false,
            showGrid: false,
            offset: 0
        },
        plugins: [
            Chartist.plugins.tooltip()
        ]
    });



    var chart = [chart1, chart2, chart3, chart5, chart6, chart7, chart8, chart9, chart10];

    for (var i = 0; i < chart.length; i++) {
        chart[i].on('draw', function(data) {
            if (data.type === 'line' || data.type === 'area') {
                data.element.animate({
                    d: {
                        begin: 500 * data.index,
                        dur: 500,
                        from: data.path.clone().scale(1, 0).translate(0, data.chartRect.height()).stringify(),
                        to: data.path.clone().stringify(),
                        easing: Chartist.Svg.Easing.easeInOutElastic
                    }
                });
            }
            if (data.type === 'bar') {
                data.element.animate({
                    y2: {
                        dur: 500,
                        from: data.y1,
                        to: data.y2,
                        easing: Chartist.Svg.Easing.easeInOutElastic
                    },
                    opacity: {
                        dur: 500,
                        from: 0,
                        to: 1,
                        easing: Chartist.Svg.Easing.easeInOutElastic
                    }
                });
            }
        });
    }

    Morris.Donut({
        element: 'order-status-chart',
        data: [{
            label: "Total Orders",
            value: 120

        }, {
            label: "Pending Orders",
            value: 50
        }, {
            label: "Delivered Orders",
            value: 70
        }],
        resize: true,
        colors: ['#0283cc', '#e74a25', '#2ecc71']
    });

    // ============================================================== 
    // calendar
    // ==============================================================

    /*
        date store today date.
        d store today date.
        m store current month.
        y store current year.
    */
    var date = new Date();
    var d = date.getDate();
    var m = date.getMonth();
    var y = date.getFullYear();

    /*
        Initialize fullCalendar and store into variable.
        Why in variable?
        Because doing so we can use it inside other function.
        In order to modify its option later.
    */

    var calendar = $('#calendar').fullCalendar({
        /*
            header option will define our calendar header.
            left define what will be at left position in calendar
            center define what will be at center position in calendar
            right define what will be at right position in calendar
        */
        header: {
            left: 'prev',
            center: 'title',
            right: 'next'
        },
        firstDay: 1,
        handleWindowResize: true,
        fixedWeekCount: false,
        /*
            editable: true allow user to edit events.
        */
        editable: true,
        /*
            events is the main option for calendar.
            for demo we have added predefined events in json object.
        */
        events: [{
            title: 'Birthday Party',
            start: new Date(y, m, d + 12, 22, 0),
            textColor: '#00bbd9'
        }, {
            title: 'Lunch',
            start: new Date(y, m, d + 22, 12, 30),
            textColor: '#e74a25'
        }, {
            title: 'Conference',
            start: new Date(y, m, d + 22, 18, 30),
            textColor: '#00bbd9'
        }, {
            title: 'Meeting',
            start: new Date(y, m, d + 27, 15, 30),
            textColor: '#2ecc71'
        }, {
            title: 'Appointment',
            start: new Date(y, m, d + 17, 17, 30),
            allDay: false,
            textColor: '#0283cc'
        }, {
            title: 'Car Loan',
            start: new Date(y, m, d + 17, 10, 0),
            allDay: false,
            textColor: '#e74a25'
        }, {
            title: 'Appointment',
            start: new Date(y, m, d, 10, 0),
            allDay: false,
            textColor: '#fff'
        }, {
            title: 'Car Loan',
            start: new Date(y, m, d, 17, 30),
            allDay: false,
            textColor: '#fff'
        }, {
            title: 'Party Time',
            start: new Date(y, m, d + 9, 17, 30),
            allDay: false,
            textColor: '#2ecc71'
        }],
        eventColor: 'transparent'
    });
    $(".fc-content").prepend('<i class="fa fa-circle m-r-5 font-10"></i>');
});
