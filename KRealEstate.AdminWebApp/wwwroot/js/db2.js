$(function() {
    "use strict";
    // ============================================================== 
    // Sales Difference chart
    // ============================================================== 
    var chart1 = new Chartist.Line('.ct-sd-chart', {
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
    var chart2 = new Chartist.Line('.ct-ie-chart', {
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
    var chart3 = new Chartist.Bar('.chartist-sales-chart', {
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
