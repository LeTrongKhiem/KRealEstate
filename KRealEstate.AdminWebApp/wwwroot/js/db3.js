$(function() {
    "use strict";

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
});
