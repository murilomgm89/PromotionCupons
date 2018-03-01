
$(function () {
    var data = [];
    var data2 = [];
    var pieChartData = eval($('#pieChartData').val());

    var pieChartData2 = eval($('#pieChartData2').val());

    if (pieChartData) {
        for (i = 0; i < pieChartData.length; i++) {
            var item = pieChartData[i];
            data.push({
                "label": item[0] + ": <strong>" + item[1] + "</strong>",
                "data": item[1]
            });
        }

        $.plot($("#pie"), data,
        {
            series: {
                pie: {
                    show: true,
                    innerRadius: 0.5,
                    radius: 1,
                    label: {
                        show: true,
                        radius: 2 / 3,
                        formatter: function (label, series) {
                            return '<div style="font-size:11px;text-align:center;color:white;"><br/>' + Math.round(series.percent) + '%</div>';
                        },
                        threshold: 0.1
                    }
                }
            },
            legend: {
                show: true,
                noColumns: 1, // number of colums in legend table
                labelFormatter: null, // fn: string -> string
                labelBoxBorderColor: "#000", // border color for the little label boxes
                container: null, // container (as jQuery object) to put legend in, null means default on top of graph
                position: "ne", // position of default legend container within plot
                margin: [5, 10], // distance from grid edge to default legend container within plot
                backgroundColor: "#ffffff", // null means auto-detect
                backgroundOpacity: 1 // set to 0 to avoid background
            },
            grid: {
                hoverable: true,
                clickable: true
            },
            colors: ["#6db6ee", "#ee7951"]
        });
    }

    if (pieChartData2) {
        for (i = 0; i < pieChartData2.length; i++) {
            var item = pieChartData2[i];
            data2.push({
                "label": item[0] + ": <strong>" + item[1] + "</strong>",
                "data": item[1]
            });
        }

        $.plot($("#pie2"), data2,
        {
            series: {
                pie: {
                    show: true,
                    innerRadius: 0.5,
                    radius: 1,
                    label: {
                        show: true,
                        radius: 2 / 3,
                        formatter: function (label, series) {
                            return '<div style="font-size:11px;text-align:center;color:white;"><br/>' + Math.round(series.percent) + '%</div>';
                        },
                        threshold: 0.1
                    }
                }
            },
            legend: {
                show: true,
                noColumns: 1, // number of colums in legend table
                labelFormatter: null, // fn: string -> string
                labelBoxBorderColor: "#000", // border color for the little label boxes
                container: null, // container (as jQuery object) to put legend in, null means default on top of graph
                position: "ne", // position of default legend container within plot
                margin: [5, 10], // distance from grid edge to default legend container within plot
                backgroundColor: "#ffffff", // null means auto-detect
                backgroundOpacity: 1 // set to 0 to avoid background
            },
            grid: {
                hoverable: true,
                clickable: true
            },
            colors: ["#6db6ee", "#ee7951", "#ff0000"]
        });
    }
});
    