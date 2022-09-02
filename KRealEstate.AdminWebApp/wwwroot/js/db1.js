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

    /* ===== Statistics chart ===== */

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

    /* ===== Visits chart ===== */

    var chart2 = new Chartist.Bar('.ct-visit', {
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

    var chart3 = new Chartist.Line('.ct-revenue', {
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

    var chart = [chart1, chart2, chart3];

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
});
