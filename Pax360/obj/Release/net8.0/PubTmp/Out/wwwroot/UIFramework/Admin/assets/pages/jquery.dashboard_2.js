(function($) {
	"use strict";

	var FlotChart = function() {
		this.$body = $("body");
		this.$realData = [];
	};

	// creates plot graph
	FlotChart.prototype.createPlotGraph = function(selector, data1, data2, data3, labels, colors, borderColor, bgColor) {
		// shows tooltip
		function showTooltip(x, y, contents) {
			$('<div id="tooltip" class="tooltipflot">' + contents + '</div>').css({
				position: 'absolute',
				top: y + 5,
				left: x + 5
			}).appendTo("body").fadeIn(200);
		}

		// Create the plot
		$.plot($(selector), [{
			data: data1,
			label: labels[0],
			color: colors[0]
		}, {
			data: data2,
			label: labels[1],
			color: colors[1]
		}, {
			data: data3,
			label: labels[2],
			color: colors[2]
		}], {
			series: {
				lines: {
					show: true,
					fill: true,
					lineWidth: 2,
					fillColor: {
						colors: [{
							opacity: 0
						}, {
							opacity: 0.5
						}, {
							opacity: 0.6
						}]
					}
				},
				points: {
					show: false
				},
				shadowSize: 0
			},
			grid: {
				hoverable: true,
				clickable: true,
				borderColor: borderColor,
				tickColor: "#f9f9f9",
				borderWidth: 1,
				labelMargin: 10,
				backgroundColor: bgColor
			},
			legend: {
				position: "ne",
				margin: [0, -24],
				noColumns: 0,
				labelBoxBorderColor: null,
				labelFormatter: function(label, series) {
					// just add some space to labels
					return '' + label + '&nbsp;&nbsp;';
				},
				width: 30,
				height: 2
			},
			yaxis: {
				axisLabel: "Daily Visits",
				tickColor: '#f5f5f5',
				font: {
					color: '#bdbdbd'
				}
			},
			xaxis: {
				axisLabel: "Last Days",
				tickColor: '#f5f5f5',
				font: {
					color: '#bdbdbd'
				}
			},
			tooltip: true,
			tooltipOpts: {
				content: '%s: Value of %x is %y',
				shifts: {
					x: -60,
					y: 25
				},
				defaultTheme: false
			}
		});
	};

	// Create an instance
	$(document).ready(function() {
		var flotChart = new FlotChart();
		flotChart.createPlotGraph('#chart-placeholder', [[1, 2], [2, 3], [3, 4]], [[1, 5], [2, 6]], [[1, 7], [2, 8]], ['Data 1', 'Data 2', 'Data 3'], ['#FF0000', '#00FF00', '#0000FF'], '#ccc', '#fff');
	});

})(jQuery);
