$(document).ready(function () {
    
	//$("#slcFilled").change(function() {       
    //    $("#slcFilled option:selected").each(function () {
    //        if (this.value == 1) {
    //           LoadFilled(data1);
    //        }
    //        else if (this.value == 2) {
	//			LoadFilled(data2);
    //        }else{
	//			LoadFilled(data3);
	//		}
    //    });    
    //});
	
    
    var Promotion = eval($("#lineChartPromotion").val());	
	var ticks = eval($('#lineChartTicks').val());

    var data1 = [
        { 
            label: "Participações", 
            data: Promotion,
            color: '#1aacf0'             
        }
    ];		
	var ticksPoint = ticks;
	
	if (Promotion) {
	    LoadFilled(data1);
	}
	
   function LoadFilled (data){	   
	   $.plot($("#filled_blue"), data, {
			xaxis: {          
				mode: null,
				ticks: ticksPoint,
				tickLength: 1,
				axisLabel: 'Month',
				axisLabelUseCanvas: true,
				axisLabelFontSizePixels: 30,
				axisLabelPadding: 6
			},
			yaxis: {
				axisLabel: 'Amount',
				axisLabelUseCanvas: true,
				minTickSize: 1,
				tickDecimals: 0,
				axisLabelFontSizePixels: 8,
				autoscaleMargin: 0.01,
				axisLabelPadding: 5,
				tickLength: "full"
			},
			series: {
				lines: {
					show: true, 
					fill: true,
					fillColor: { colors: [ { opacity: 0.2 }, { opacity: 0.2 } ] },
					lineWidth: 1.5
				},
				points: {
					show: true,
					radius: 2.5,
					fill: true,
					fillColor: "#ffffff",
					symbol: "circle",
					lineWidth: 1.1
				}
			},
		   grid: { hoverable: true, clickable: true },
			legend: {
				show: false
			}
		});
   }
   //var SerializedPersonsChartData = eval($("#lineChartPersonsChartData").val());

   //$.plot($("#cpf_evolution"), [{
   //    label: "Cadastros",
   //    data: SerializedPersonsChartData,
   //    color: '#1aacf0'
   //}], {
   //    xaxis: {
   //        mode: null,
   //        ticks: ticksPoint,
   //        tickLength: 1,
   //        axisLabel: 'Month',
   //        axisLabelUseCanvas: true,
   //        axisLabelFontSizePixels: 30,
   //        axisLabelPadding: 6
   //    },
   //    yaxis: {
   //        axisLabel: 'Amount',
   //        axisLabelUseCanvas: true,
   //        minTickSize: 1,
   //        tickDecimals: 0,
   //        axisLabelFontSizePixels: 8,
   //        autoscaleMargin: 0.01,
   //        axisLabelPadding: 5,
   //        tickLength: "full"
   //    },
   //    series: {
   //        lines: {
   //            show: true,
   //            fill: true,
   //            fillColor: { colors: [{ opacity: 0.2 }, { opacity: 0.2 }] },
   //            lineWidth: 1.5
   //        },
   //        points: {
   //            show: true,
   //            radius: 2.5,
   //            fill: true,
   //            fillColor: "#ffffff",
   //            symbol: "circle",
   //            lineWidth: 1.1
   //        }
   //    },
   //    grid: { hoverable: true, clickable: true },
   //    legend: {
   //        show: false
   //    }
   //});

    function showTooltip(x, y, contents) {
        $('<div id="tooltip" class="chart-tooltip">' + contents + '</div>').css( {
            position: 'absolute',
            display: 'none',
            top: y - 46,
            left: x - 9,
            'z-index': '9999',
            opacity: 0.9
        }).appendTo("body").fadeIn(200);
    }

    var previousPoint = null;
    $("#filled_blue").bind("plothover", function (event, pos, item) {
        $("#x").text(pos.x.toFixed(2));
        $("#y").text(pos.y.toFixed(2));

        if ($("#filled_blue").length > 0) {
            if (item) {
                if (previousPoint != item.dataIndex) {
                    previousPoint = item.dataIndex;
                    
                    $("#tooltip").remove();
                    var x = item.datapoint[0].toFixed(0),
                        y = item.datapoint[1].toFixed(0);
                    
                    showTooltip(item.pageX, item.pageY,
                                item.series.label + ": " + "<strong>" + y + "</strong>");
                }
            }
            else {
                $("#tooltip").remove();
                previousPoint = null;            
            }
        }
    });

    //$("#cpf_evolution").bind("plothover", function (event, pos, item) {
    //    $("#x").text(pos.x.toFixed(2));
    //    $("#y").text(pos.y.toFixed(2));

    //    if ($("#cpf_evolution").length > 0) {
    //        if (item) {
    //            if (previousPoint != item.dataIndex) {
    //                previousPoint = item.dataIndex;

    //                $("#tooltip").remove();
    //                var x = item.datapoint[0].toFixed(0),
    //                    y = item.datapoint[1].toFixed(0);

    //                showTooltip(item.pageX, item.pageY,
    //                            item.series.label + ": " + "<strong>" + y + "</strong>");
    //            }
    //        }
    //        else {
    //            $("#tooltip").remove();
    //            previousPoint = null;
    //        }
    //    }
    //});

    $("#filled_blue").bind("plotclick", function (event, pos, item) {
        if (item) {
            $("#clickdata").text("You clicked point " + item.dataIndex + " in " + item.series.label + ".");
            plot.highlight(item.series, item.datapoint);
        }
    });
});