google.charts.load('current', { 'packages': ['corechart', 'gauge'] });
google.charts.setOnLoadCallback(drawChart);

function drawChart() {
    function getlabels(min, max, count) {
        var tempArr = [],
            gap = max / (count - 1);

        for (var i = min; i <= max; i = i + gap) {
            tempArr.push(i.toString())
        }

        return tempArr;
    }

    // Line Chart
    var data = google.visualization.arrayToDataTable([
        ['Year', 'Output Frequency', 'DC Bus Voltage', 'O/P Current', 'Input Power', 'O/P Voltage'],
        ['2013', 1000, 400, 1000, 400, 400],
        ['2014', 1170, 460, 1170, 460, 340],
        ['2015', 660, 1120, 670, 760, 700],
        ['2016', 1030, 540, 170, 860, 1279]
    ]);

    var options = {
        title: 'Company Performance',
        hAxis: { title: 'Year', titleTextStyle: { color: '#333' } },
        vAxis: { minValue: 0 },
        pointSize: 5
    };

    var chart = new google.visualization.AreaChart(document.getElementById('AllDetails'));
    chart.draw(data, options);

    // Chart 1
    var Chart_1_data = google.visualization.arrayToDataTable([['Label', 'Value'], ['HZ', 39.01]]);

    var mTicks_1 = getlabels(0, 60, 6),
        Chart_1_options = {
        width: 800, height: 240,
        min: 0, max: 60,
        redFrom: 0, redTo: 12,
        yellowFrom: 24, yellowTo: 36,
        greenFrom: 48, greenTo: 60,
        majorTicks: mTicks_1,
        minorTicks: 30
    };

    var chart_1 = new google.visualization.Gauge(document.getElementById('Chart_1'));
    chart_1.draw(Chart_1_data, Chart_1_options);

    // Chart 2
    var Chart_2_data = google.visualization.arrayToDataTable([['Label', 'Value'], ['V', 574]]);

    var mTicks_2 = getlabels(0, 1000, 6),
        Chart_2_options = {
        width: 800, height: 240,
        min: 0, max: 1000,
        redFrom: 0, redTo: 200,
        yellowFrom: 400, yellowTo: 600,
        greenFrom: 800, greenTo: 1000,
        majorTicks: mTicks_2,
        minorTicks: 30
    };

    var Chart_2 = new google.visualization.Gauge(document.getElementById('Chart_2'));
    Chart_2.draw(Chart_2_data, Chart_2_options);

    // Chart 3
    var Chart_3_data = google.visualization.arrayToDataTable([['Label', 'Value'], ['A', 0]]);

    var mTicks_3 = getlabels(0, 50, 6),
        Chart_3_options = {
        width: 800, height: 240,
        min: 0, max: 50,
        redFrom: 0, redTo: 10,
        yellowFrom: 20, yellowTo: 30,
        greenFrom: 40, greenTo: 50,
        majorTicks: mTicks_3,
        minorTicks: 30
    };

    var Chart_3 = new google.visualization.Gauge(document.getElementById('Chart_3'));
    Chart_3.draw(Chart_3_data, Chart_3_options);

    // Chart 4
    var Chart_4_data = google.visualization.arrayToDataTable([['Label', 'Value'], ['KW', 0.03]]);

    var mTicks_4 = getlabels(0, 30, 6),
        Chart_4_options = {
        width: 800, height: 240,
        min: 0, max: 30,
        redFrom: 0, redTo: 6,
        yellowFrom: 12, yellowTo: 18,
        greenFrom: 24, greenTo: 30,
        majorTicks: mTicks_4,
        minorTicks: 30
    };

    var Chart_4 = new google.visualization.Gauge(document.getElementById('Chart_4'));
    Chart_4.draw(Chart_4_data, Chart_4_options);

    // Chart 5
    var Chart_5_data = google.visualization.arrayToDataTable([['Label', 'Value'], ['V', 416]]);

    var mTicks_5 = getlabels(0, 500, 6),
        Chart_5_options = {
        width: 800, height: 240,
        min: 0, max: 500,
        redFrom: 0, redTo: 100,
        yellowFrom: 200, yellowTo: 300,
        greenFrom: 400, greenTo: 500,
        majorTicks: mTicks_5,
        minorTicks: 30
    };

    var Chart_5 = new google.visualization.Gauge(document.getElementById('Chart_5'));
    Chart_5.draw(Chart_5_data, Chart_5_options);

    // Set Values Using...
    setInterval(function () {
        Chart_1_data.setValue(0, 1, 40 + Math.round(60 * Math.random()));
        chart_1.draw(Chart_1_data, Chart_1_options);
    }, 13000);
}