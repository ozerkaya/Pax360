!function($) {
    "use strict";

    var ChartC3 = function() {};

    ChartC3.prototype.init = function () {
        // Çubuk grafiği oluşturma
        c3.generate({
            bindto: '#chart',
            data: {
                columns: [
                    ['data1', 30, 20, 50, 40, 60, 50],
                    ['data2', 200, 130, 90, 240, 130, 220],
                    ['data3', 300, 200, 160, 400, 250, 250]
                ],
                type: 'bar',
                colors: {
                    data1: '#ebeff2',
                    data2: '#4bd396',
                    data3: '#f5707a'
                }
            }
        });

        // Kombine grafik oluşturma
        c3.generate({
            bindto: '#combine-chart',
            data: {
                columns: [
                    ['data1', 30, 20, 50, 40, 60, 50],
                    ['data2', 200, 130, 90, 240, 130, 220],
                    ['data3', 300, 200, 160, 400, 250, 250],
                    ['data4', 200, 130, 90, 240, 130, 220],
                    ['data5', 130, 120, 150, 140, 160, 150]
                ],
                types: {
                    data1: 'bar',
                    data2: 'bar',
                    data3: 'spline',
                    data4: 'line',
                    data5: 'bar'
                },
                colors: {
                    data1: '#7fc1fc',
                    data2: '#ebeff2',
                    data3: '#36404a',
                    data4: '#fb6d9d',
                    data5: '#5fbeaa'
                },
                groups: [
                    ['data1', 'data2']
                ]
            },
            axis: {
                x: {
                    type: 'categorized'
                }
            }
        });

        // Döndürülmüş grafik
        c3.generate({
            bindto: '#roated-chart',
            data: {
                columns: [
                    ['data1', 30, 200, 100, 400, 150, 250],
                    ['data2', 50, 20, 10, 40, 15, 25]
                ],
                types: {
                    data1: 'bar'
                },
                colors: {
                    data1: '#3ac9d6',
                    data2: '#f51167'
                }
            },
            axis: {
                rotated: true,
                x: {
                    type: 'categorized'
                }
            }
        });

        // Yığılmış grafik
        c3.generate({
            bindto: '#chart-stacked',
            data: {
                columns: [
                    ['data1', 30, 20, 50, 40, 60, 50],
                    ['data2', 200, 130, 90, 240, 130, 220]
                ],
                types: {
                    data1: 'area-spline',
                    data2: 'area-spline'
                },
                colors: {
                    data1: '#ff9800',
                    data2: '#8d6e63'
                }
            }
        });

        // Donut grafik
        c3.generate({
            bindto: '#donut-chart',
            data: {
                columns: [
                    ['data1', 46],
                    ['data2', 24]
                ],
                type: 'donut'
            },
            donut: {
                title: "Candidates",
                width: 30,
                label: {
                    show: false
                }
            },
            color: {
                pattern: ["#26a69a", "#ebeff2"]
            }
        });

        // Pasta grafik
        c3.generate({
            bindto: '#pie-chart',
            data: {
                columns: [
                    ['Lulu', 46],
                    ['Olaf', 24],
                    ['Item 3', 30]
                ],
                type: 'pie'
            },
            color: {
                pattern: ["#ebeff2", "#4bd396", "#f5707a"]
            },
            pie: {
                label: {
                    show: false
                }
            }
        });

        // Çizgi bölgeleri grafiği
        c3.generate({
            bindto: '#line-regions',
            data: {
                columns: [
                    ['data1', 30, 200, 100, 400, 150, 250],
                    ['data2', 50, 20, 10, 40, 15, 25]
                ],
                regions: {
                    'data1': [{'start': 1, 'end': 2, 'style': 'dashed'}, {'start': 3}],
                    'data2': [{'end': 3}]
                },
                colors: {
                    data1: '#ff9800',
                    data2: '#6b5fb5'
                }
            }
        });

        // Dağılım grafiği
        c3.generate({
            bindto: '#scatter-plot',
            data: {
                xs: {
                    setosa: 'setosa_x',
                    versicolor: 'versicolor_x'
                },
                columns: [
                    ["setosa_x", 3.5, 3.0, 3.2],
                    ["versicolor_x", 3.2, 3.2, 3.1],
                    ["setosa", 0.2, 0.2, 0.2],
                    ["versicolor", 1.4, 1.5, 1.5]
                ],
                type: 'scatter'
            },
            color: {
                pattern: ["#188ae2", "#f5707a"]
            },
            axis: {
                x: {
                    label: 'Sepal.Width',
                    tick: {
                        fit: false
                    }
                },
                y: {
                    label: 'Petal.Width'
                }
            }
        });
    };

    // ChartC3 nesnesini başlatma
    $.ChartC3 = new ChartC3, $.ChartC3.Constructor = ChartC3;

}(window.jQuery);

// Başlatma
(function($) {
    "use strict";
    $.ChartC3.init();
})(window.jQuery);
