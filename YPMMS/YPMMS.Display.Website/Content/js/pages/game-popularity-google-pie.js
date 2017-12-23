



//PIE CHART FOR THE AVERAGE TAKEN PER AVAILABLE HOURS - REPORTS/////////////////////////

google.charts.load('current', {'packages':['corechart']});
      google.charts.setOnLoadCallback(drawChart);
      function drawChart() {

        var data = google.visualization.arrayToDataTable([
          ['Task', 'Hours per Day'],
          ['CashExploder',     11],
          ['iPub2',      2],
          ['Family Guy',  2],
          ['Deal or No Deal', 2],
          ['Super Croc',    7]
        ]);

        var options = {
          title: 'Best Performing Games',
          is3D:true,
          colors:['#f85a5a','#FFB82B','#FFFC2B','#a6d53e','#467894','#00B233','#1D89CC'],
          chartArea:{left: 0, right:0, bottom:0, top:40},
        };

        var chart = new google.visualization.PieChart(document.getElementById('piechart'));

        chart.draw(data, options);
      }
 
 //END OF CONTENT
  
