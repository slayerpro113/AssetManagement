$.ajax({
    type: 'GET',
    url: '/Manager/GetChartData',
    dataType: 'json',
    success: function (data) {
        var series = [
            {
                type: 'pie',
                allowPointSelect: true,
                keys: ['name', 'y', 'selected', 'sliced'],
                data: [
                    [data[0].name, data[0].y, false],
                    [data[1].name, data[1].y, false],
                    [data[2].name, data[2].y, true, true],
                    [data[3].name, data[3].y, false]
                ],

                showInLegend: true
            }
        ];
        createChart(series);
    }
});

function createChart(series) {

    Highcharts.chart('container', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        title: {
            text: null
        },
        tooltip: {
            pointFormat: '{series.name}: <b>{point.percentage:.1f}%</b>'
        },
        credits: {
            enabled: false
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || '#476482'

                    }
                }
            }
        },
        series: series
    });
}