
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

    Highcharts.chart('Chart', {
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
            pointFormat: '<b>{point.percentage:.1f}%</b>'
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
//--------------3D Chart------------------
$.ajax({
    type: 'GET',
    url: '/Manager/Get3DChartData',
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
                    [data[2].name, data[2].y, false],
                    [data[3].name, data[3].y, false],
                    [data[4].name, data[4].y, false],
                    [data[5].name, data[5].y, true, true],
                    [data[6].name, data[6].y, false],
                    [data[7].name, data[7].y, false]
                ],

                showInLegend: true
            }
        ];
        create3DChart(series);
    }
});

function create3DChart(series) {
    Highcharts.chart('3dChart', {
        chart: {
            type: 'pie',
            options3d: {
                enabled: true,
                alpha: 45
            }
        },
        title: {
            text: null
        },
        tooltip: {
            pointFormat: '<b>{point.percentage:.1f}%</b>'
        },
        credits: {
            enabled: false
        },
        plotOptions: {
            pie: {
                innerSize: 100,
                depth: 45,
                dataLabels: {
                    enabled: true,
                    format: '<b>{point.name}</b>: {point.percentage:.1f} %',
                    style: {
                        color: (Highcharts.theme && Highcharts.theme.contrastTextColor) || '#476482'
                    }
                }
            }
        },
        series:series
    });
}