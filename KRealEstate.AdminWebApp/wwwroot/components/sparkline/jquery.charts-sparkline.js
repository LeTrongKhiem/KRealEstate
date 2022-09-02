  $(document).ready(function () {
      var sparklineLogin = function () {
          $("#sparkline1").sparkline([5, 6, 2, 8, 9, 4, 7, 10, 11, 12, 10], {
              type: 'bar',
              height: '45',
              barWidth: 7,
              barSpacing: 4,
              barColor: '#2ecc71'

          });

          $('#sparkline2').sparkline([20, 40, 30], {
              type: 'pie',
              width: '50',
              height: '45',
              resize: true,
              sliceColors: ['#00bbd9', '#4a23ad', '#d93c17']
          });


          $('#sparkline3').sparkline([5, 6, 2, 9, 4, 7, 10, 12], {
              type: 'bar',
              height: '164',
              barWidth: '7',
              resize: true,
              barSpacing: '5',
              barColor: '#d93c17'
          });


          $("#sparkline4").sparkline([0, 23, 43, 35, 44, 45, 56, 37, 40, 45, 56, 7, 10], {
              type: 'line',
              width: '120',
              height: '45',
              lineColor: '#2ecc71',
              fillColor: 'transparent',
              spotColor: '#2ecc71',
              minSpotColor: undefined,
              maxSpotColor: undefined,
              highlightSpotColor: undefined,
              highlightLineColor: undefined
          });

          $('#sparkline5').sparkline([15, 23, 55, 35, 54, 45, 66, 47, 30], {
              type: 'line',
              width: '100%',
              height: '160',
              chartRangeMax: 50,
              resize: true,
              lineColor: '#00bbd9',
              fillColor: 'rgba(19, 218, 254, 0.3)',
              highlightLineColor: 'rgba(0,0,0,.1)',
              highlightSpotColor: 'rgba(0,0,0,.2)',
          });

          $('#sparkline5').sparkline([0, 13, 10, 14, 15, 10, 18, 20, 0], {
              type: 'line',
              width: '100%',
              height: '160',
              chartRangeMax: 40,
              lineColor: '#4a23ad',
              fillColor: 'rgba(97, 100, 193, 0.3)',
              composite: true,
              resize: true,
              highlightLineColor: 'rgba(0,0,0,.1)',
              highlightSpotColor: 'rgba(0,0,0,.2)',
          });
          $('#sparkline6').sparkline([5, 6, 2, 8, 9, 4, 7, 10, 11, 12, 10], {
              type: 'bar',
              height: '45',
              barWidth: '7',
              barSpacing: '4',
              barColor: '#00bbd9'
          });
          $("#sparkline7").sparkline([0, 2, 8, 6, 8, 5, 6, 4, 8, 6, 4, 2], {
              type: 'line',
              width: '100%',
              height: '50',
              lineColor: '#ffb136',
              fillColor: '#ffb136',
              highlightLineColor: 'rgba(0, 0, 0, 0.2)',
              highlightSpotColor: '#4f4f4f'
          });
          $("#sparkline8").sparkline([2, 4, 4, 6, 8, 5, 6, 4, 8, 6, 6, 2], {
              type: 'line',
              width: '100%',
              height: '50',
              lineColor: '#2ecc71',
              fillColor: '#2ecc71',
              maxSpotColor: '#2ecc71',
              highlightLineColor: 'rgba(0, 0, 0, 0.2)',
              highlightSpotColor: '#2ecc71'
          });
          $("#sparkline9").sparkline([0, 2, 8, 6, 8, 5, 6, 4, 8, 6, 6, 2], {
              type: 'line',
              width: '100%',
              height: '50',
              lineColor: '#00bbd9',
              fillColor: '#00bbd9',
              minSpotColor: '#00bbd9',
              maxSpotColor: '#00bbd9',
              highlightLineColor: 'rgba(0, 0, 0, 0.2)',
              highlightSpotColor: '#00bbd9'
          });
          $("#sparkline10").sparkline([2, 4, 4, 6, 8, 5, 6, 4, 8, 6, 6, 2], {
              type: 'line',
              width: '100%',
              height: '50',
              lineColor: '#4a23ad',
              fillColor: '#4a23ad',
              maxSpotColor: '#4a23ad',
              highlightLineColor: 'rgba(0, 0, 0, 0.2)',
              highlightSpotColor: '#4a23ad'
          });
          $('#sparkline11').sparkline([20, 40, 30], {
              type: 'pie',
              height: '200',
              resize: true,
              sliceColors: ['#00bbd9', '#e74a25', '#2ecc71']
          });

          $("#sparkline12").sparkline([5, 6, 2, 8, 9, 4, 7, 10, 11, 12, 10, 4, 7, 10], {
              type: 'bar',
              height: '200',
              barWidth: 10,
              barSpacing: 7,
              barColor: '#2ecc71'
          });

          $('#sparkline13').sparkline([5, 6, 2, 9, 4, 7, 10, 12, 4, 7, 10], {
              type: 'bar',
              height: '200',
              barWidth: '10',
              resize: true,
              barSpacing: '7',
              barColor: '#e74a25'
          });
          $('#sparkline13').sparkline([5, 6, 2, 9, 4, 7, 10, 12, 4, 7, 10], {
              type: 'line',
              height: '200',
              lineColor: '#e74a25',
              fillColor: 'transparent',
              composite: true,
              highlightLineColor: 'rgba(0,0,0,.1)',
              highlightSpotColor: 'rgba(0,0,0,.2)'
          });
          $("#sparkline14").sparkline([0, 23, 43, 35, 44, 45, 56, 37, 40, 45, 56, 7, 10], {
              type: 'line',
              width: '100%',
              height: '200',
              lineColor: '#fff',
              fillColor: 'transparent',
              spotColor: '#fff',
              minSpotColor: undefined,
              maxSpotColor: undefined,
              highlightSpotColor: undefined,
              highlightLineColor: undefined
          });
          $('#sparkline15').sparkline([5, 6, 2, 8, 9, 4, 7, 10, 11, 12, 10, 9, 4, 7], {
              type: 'bar',
              height: '200',
              barWidth: '10',
              barSpacing: '10',
              barColor: '#00bbd9'
          });
          $('#sparkline16').sparkline([15, 23, 55, 35, 54, 45, 66, 47, 30], {
              type: 'line',
              width: '100%',
              height: '200',
              chartRangeMax: 50,
              resize: true,
              lineColor: '#00bbd9',
              fillColor: 'rgba(19, 218, 254, 0.3)',
              highlightLineColor: 'rgba(0,0,0,.1)',
              highlightSpotColor: 'rgba(0,0,0,.2)',
          });

          $('#sparkline16').sparkline([0, 13, 10, 14, 15, 10, 18, 20, 0], {
              type: 'line',
              width: '100%',
              height: '200',
              chartRangeMax: 40,
              lineColor: '#4a23ad',
              fillColor: 'rgba(97, 100, 193, 0.3)',
              composite: true,
              resize: true,
              highlightLineColor: 'rgba(0,0,0,.1)',
              highlightSpotColor: 'rgba(0,0,0,.2)',
          });

          $('#sparklinedashdb').sparkline([0, 5, 6, 10, 9, 12, 4, 9, 4, 2, 7, 9, 6, 2], {
              type: 'bar',
              height: '50',
              barWidth: '5',
              resize: true,
              barSpacing: '15',
              barColor: '#00bbd9'
          });

          $('#sparklinedash').sparkline([0, 5, 6, 10, 9, 12, 4, 9], {
              type: 'bar',
              height: '30',
              barWidth: '4',
              resize: true,
              barSpacing: '5',
              barColor: '#2ecc71'
          });

          $('#sparklinedash2').sparkline([0, 5, 6, 10, 9, 12, 4, 9], {
              type: 'bar',
              height: '30',
              barWidth: '4',
              resize: true,
              barSpacing: '5',
              barColor: '#4a23ad'
          });
          $('#sparklinedash3').sparkline([0, 5, 6, 10, 9, 12, 4, 9], {
              type: 'bar',
              height: '30',
              barWidth: '4',
              resize: true,
              barSpacing: '5',
              barColor: '#00bbd9'
          });
          $('#sparklinedash4').sparkline([0, 5, 6, 10, 9, 12, 4, 9], {
              type: 'bar',
              height: '30',
              barWidth: '4',
              resize: true,
              barSpacing: '5',
              barColor: '#e74a25'
          });


      }
      var sparkResize;

      $(window).resize(function (e) {
          clearTimeout(sparkResize);
          sparkResize = setTimeout(sparklineLogin, 500);
      });
      sparklineLogin();

  });
