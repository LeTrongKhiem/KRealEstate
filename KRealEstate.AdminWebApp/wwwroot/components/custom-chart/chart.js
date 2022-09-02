$(document).ready(function() {
    "use strict";

    $(".complete-chart").each(function() {
        var id = $(this).attr("id");
        createchart(id);
    });

    function createchart(id) {

        var el = document.getElementById(id); // get canvas

        var options = {
            percent: el.getAttribute('data-percent') || 50,
            size: el.getAttribute('data-size') || 160,
            lineWidth: el.getAttribute('data-line') || 5,
            rotate: el.getAttribute('data-rotate') || 0,
            color: el.getAttribute('data-color') || '#000',
            textColor: el.getAttribute('data-text-color') || '#000'
        }

        var canvas = document.createElement('canvas');
        var h1 = document.createElement('h1');
        h1.setAttribute('class', 'per-number');
        h1.innerHTML = options.percent + '<span class="per-icon">&#37;</span>';
        h1.style.color = options.textColor;

        if (typeof(G_vmlCanvasManager) !== 'undefined') {
            G_vmlCanvasManager.initElement(canvas);
        }

        var ctx = canvas.getContext('2d');
        canvas.width = canvas.height = options.size;

        el.appendChild(h1);
        el.appendChild(canvas);

        ctx.translate(options.size / 2, options.size / 2); // change center
        ctx.rotate((-1 / 2 + options.rotate / 180) * Math.PI); // rotate -90 deg

        //imd = ctx.getImageData(0, 0, 240, 240);
        var radius = (options.size - options.lineWidth) / 2;

        var drawCircle = function(color, lineWidth, percent) {
            percent = Math.min(Math.max(0, percent || 1), 1);
            ctx.beginPath();
            ctx.arc(0, 0, radius, 0, Math.PI * 2 * percent, false);
            ctx.strokeStyle = color;
            ctx.lineCap = 'round'; // butt, round or square
            ctx.lineWidth = lineWidth
            ctx.stroke();
        };

        drawCircle('#e5ebec', options.lineWidth, 100 / 100);
        drawCircle(options.color, options.lineWidth, options.percent / 100);
    }
});
