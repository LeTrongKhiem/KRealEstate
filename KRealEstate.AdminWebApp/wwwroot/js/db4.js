$(function() {
    "use strict";

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

    var chart = [chart1];

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
});
