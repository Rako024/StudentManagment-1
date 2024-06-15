'use strict';

$(document).ready(function() {

	function generateData(baseval, count, yrange) {
		var i = 0;
		var series = [];
		while (i < count) {
			var x = Math.floor(Math.random() * (750 - 1 + 1)) + 1;;
			var y = Math.floor(Math.random() * (yrange.max - yrange.min + 1)) + yrange.min;
			var z = Math.floor(Math.random() * (75 - 15 + 1)) + 15;

			series.push([x, y, z]);
			baseval += 86400000;
			i++;
		}
		return series;
	}

    if($('#deals-chart').length > 0 ){
        var options = {
            series: [{
            name: "sales",
            colors: ['#FFC38F'],
            data: [{
              x: 'Inpipeline',
              y: 400,
              
            }, {
              x: 'Follow Up',
              y: 30
            }, {
              x: 'Schedule',
              y: 248
            }, {
              x: 'Conversation',
              y: 470
            }, {
              x: 'Won',
              y: 470
            },{
              x: 'Lost',
              y: 180
            }]
          }],
            chart: {
            type: 'bar',
            height: 250,
          },
          plotOptions: {
            bar: {
                borderRadius: 50,
                borderRadiusApplication: 'around',
            }
          },
          colors: ['#FFC38F'],
          xaxis: {
            type: 'category',
            group: {
              style: {
                fontSize: '7px',
                fontWeight: 700,
              },
              groups: [
                { title: '2019', cols: 4 },
                { title: '2020', cols: 4 }
              ]
            }
          },
          
          };
    
          var chart = new ApexCharts(document.querySelector("#deals-chart"), options);
          chart.render();
    }	

    if($('#leads-years').length > 0 ){
      var options = {
        series: [{
        name: 'XYZ MOTORS',
        data: dates
      }],
        chart: {
        type: 'area',
        stacked: false,
        height: 350,
        zoom: {
          type: 'x',
          enabled: true,
          autoScaleYaxis: true
        },
        toolbar: {
          autoSelected: 'zoom'
        }
      },
      dataLabels: {
        enabled: false
      },
      markers: {
        size: 0,
      },
      title: {
        text: 'Stock Price Movement',
        align: 'left'
      },
      fill: {
        type: 'gradient',
        gradient: {
          shadeIntensity: 1,
          inverseColors: false,
          opacityFrom: 0.5,
          opacityTo: 0,
          stops: [0, 90, 100]
        },
      },
      yaxis: {
        labels: {
          formatter: function (val) {
            return (val / 1000000).toFixed(0);
          },
        },
        title: {
          text: 'Price'
        },
      },
      xaxis: {
        type: 'datetime',
      },
      tooltip: {
        shared: false,
        y: {
          formatter: function (val) {
            return (val / 1000000).toFixed(0)
          }
        }
      }
      };

      var chart = new ApexCharts(document.querySelector("#lead-years"), options);
      chart.render();
    }

    // Leads-chart
    if($('#leads-chart').length > 0 ){
      var options = {
          series: [{
          name: "sales",
          colors: ['#BEA4F2'],
          data: [{
            x: 'Inpipeline',
            y: 400,
            
          }, {
            x: 'Follow Up',
            y: 30
          }, {
            x: 'Schedule',
            y: 248
          }, {
            x: 'Conversation',
            y: 470
          }, {
            x: 'Won',
            y: 470
          },{
            x: 'Lost',
            y: 180
          }]
        }],
          chart: {
          type: 'bar',
          height: 250,
        },
        plotOptions: {
          bar: {
              borderRadius: 50,
              borderRadiusApplication: 'around',
          }
        },
        colors: ['#BEA4F2'],
        xaxis: {
          type: 'category',
          group: {
            style: {
              fontSize: '7px',
              fontWeight: 700,
            },
            groups: [
              { title: '2019', cols: 4 },
              { title: '2020', cols: 4 }
            ]
          }
        },
        
        };
  
        var chart = new ApexCharts(document.querySelector("#leads-chart"), options);
        chart.render();
  }	

    if($('#last-chart').length > 0 ){
        var options = {
            series: [{
            data: [400, 220, 448,]
        }],
            chart: {
            type: 'bar',
            height: 150
        },
       
        plotOptions: {
            bar: {
            borderRadius: 50,
            horizontal: true,
            }
        },
        dataLabels: {
            enabled: false
        },
        borderRadius: 50,
        colors: ['#F96C85'],
        xaxis: {
            categories: ['Conversation', 'Follow Up', 'Inpipeline'
            ],
        }
        };

        var chart = new ApexCharts(document.querySelector("#last-chart"), options);
        chart.render();
    }

    if($('#last-chart-2').length > 0 ){
      var options = {
          series: [{
          data: [400, 430, 448,]
      }],
          chart: {
          type: 'bar',
          height: 150
      },
     
      plotOptions: {
          bar: {
          borderRadius: 50,
          horizontal: true,
          }
      },
      dataLabels: {
          enabled: false
      },
      borderRadius: 50,
      colors: ['#F96C85'],
      xaxis: {
          categories: ['Conversation', 'Follow Up', 'Inpipeline'
          ],
      }
      };

      var chart = new ApexCharts(document.querySelector("#last-chart-2"), options);
      chart.render();
  }
    if($('#won-chart').length > 0 ){
        var options = {
            series: [{
            data: [400, 122, 250]
        }],
            chart: {
            type: 'bar',
            height: 150
        },
        plotOptions: {
            bar: {
            borderRadius: 10,
            horizontal: true,
            }
        },
        dataLabels: {
            enabled: false
        },
        colors: ['#77D882'],
        xaxis: {
            categories: ['Conversation', 'Follow Up', 'Inpipeline'
            ],
        }
        };

        var chart = new ApexCharts(document.querySelector("#won-chart"), options);
        chart.render();
    }

    if($('#leads-won-chart').length > 0 ){
      var options = {
          series: [{
          data: [400, 430, 448,]
      }],
          chart: {
          type: 'bar',
          height: 150
      },
      plotOptions: {
          bar: {
          borderRadius: 10,
          horizontal: true,
          }
      },
      dataLabels: {
          enabled: false
      },
      colors: ['#77D882'],
      xaxis: {
          categories: ['Conversation', 'Follow Up', 'Inpipeline'
          ],
      }
      };

      var chart = new ApexCharts(document.querySelector("#leads-won-chart"), options);
      chart.render();
  }

    if($('#year-chart').length > 0 ){
        var options = {
            series: [{
            data: [34, 44, 54, 21, 12, 43, 33, 23, 66, 66, 58]
          }],
            chart: {
            type: 'line',
            height: 350
          },
          stroke: {
            curve: 'stepline',
          },
          dataLabels: {
            enabled: false
          },
          title: {
            align: 'left'
          },
          colors: ['#FF902F'],
          markers: {
            hover: {
              sizeOffset: 4
            }
          }
          };
  
          var chart = new ApexCharts(document.querySelector("#year-chart"), options);
          chart.render();
    }


	// Column chart
    if($('#sales_chart').length > 0 ){
    	var columnCtx = document.getElementById("sales_chart"),
    	columnConfig = {
    		colors: ['#7638ff', '#fda600'],
    		series: [
    			{
    			name: "Received",
    			type: "column",
    			data: [70, 150, 80, 180, 150, 175, 201, 60, 200, 120, 190, 160, 50]
    			},
    			{
    			name: "Pending",
    			type: "column",
    			data: [23, 42, 35, 27, 43, 22, 17, 31, 22, 22, 12, 16, 80]
    			}
    		],
    		chart: {
    			type: 'bar',
    			fontFamily: 'Poppins, sans-serif',
    			height: 350,
    			toolbar: {
    				show: false
    			}
    		},
    		plotOptions: {
    			bar: {
    				horizontal: false,
    				columnWidth: '60%',
    				endingShape: 'rounded'
    			},
    		},
    		dataLabels: {
    			enabled: false
    		},
    		stroke: {
    			show: true,
    			width: 2,
    			colors: ['transparent']
    		},
    		xaxis: {
    			categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct'],
    		},
    		yaxis: {
    			title: {
    				text: '$ (thousands)'
    			}
    		},
    		fill: {
    			opacity: 1
    		},
    		tooltip: {
    			y: {
    				formatter: function (val) {
    					return "$ " + val + " thousands"
    				}
    			}
    		}
    	};
    	var columnChart = new ApexCharts(columnCtx, columnConfig);
    	columnChart.render();
    }

	//Pie Chart
    if($('#invoice_chart').length > 0 ){
    	var pieCtx = document.getElementById("invoice_chart"),
    	pieConfig = {
    		colors: ['#7638ff', '#ff737b', '#fda600', '#1ec1b0'],
    		series: [55, 40, 20, 10],
    		chart: {
    			fontFamily: 'Poppins, sans-serif',
    			height: 320,
    			type: 'donut',
    		},
    		labels: ['Paid', 'Unpaid', 'Overdue', 'Draft'],
    		legend: {show: false},
    		responsive: [{
    			breakpoint: 480,
    			options: {
    				chart: {
    					width: 200,
                        height:200,
    				},
    				legend: {
    					position: 'bottom'
    				}
    			}
    		}]
    	};
    	var pieChart = new ApexCharts(pieCtx, pieConfig);
    	pieChart.render();
	}
	
	// Simple Line
    if($('#s-line').length > 0 ){
    var sline = {
      chart: {
        height: 350,
        type: 'line',
        zoom: {
          enabled: false
        },
        toolbar: {
          show: false,
        }
      },
      colors: ['rgb(255, 155, 68)'],
      dataLabels: {
        enabled: false
      },
      stroke: {
        curve: 'straight'
      },
      series: [{
        name: "Desktops",
        data: [10, 41, 35, 51, 49, 62, 69, 91, 148]
      }],
      title: {
        text: 'Product Trends by Month',
        align: 'left'
      },
      grid: {
        row: {
          colors: ['#f1f2f3', 'transparent'], // takes an array which will be repeated on columns
          opacity: 0.5
        },
      },
      xaxis: {
        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep'],
      }
    }

    var chart = new ApexCharts(
      document.querySelector("#s-line"),
      sline
    );

    chart.render();
    }


// Simple Line Area
 if($('#s-line-area').length > 0 ){
var sLineArea = {
    chart: {
        height: 350,
        type: 'area',
        toolbar: {
          show: false,
        }
    },
    colors: ['rgb(255, 155, 68)', 'rgb(252, 96, 117)'],
    dataLabels: {
        enabled: false
    },
    stroke: {
        curve: 'smooth'
    },
    series: [{
        name: 'series1',
        data: [31, 40, 28, 51, 42, 109, 100]
    }, {
        name: 'series2',
        data: [11, 32, 45, 32, 34, 52, 41]
    }],

    xaxis: {
        type: 'datetime',
        categories: ["2018-09-19T00:00:00", "2018-09-19T01:30:00", "2018-09-19T02:30:00", "2018-09-19T03:30:00", "2018-09-19T04:30:00", "2018-09-19T05:30:00", "2018-09-19T06:30:00"],                
    },
    tooltip: {
        x: {
            format: 'dd/MM/yy HH:mm'
        },
    }
}

var chart = new ApexCharts(
    document.querySelector("#s-line-area"),
    sLineArea
);

chart.render();
}

// Simple Column
if($('#s-col').length > 0 ){
var sCol = {
    chart: {
        height: 350,
        type: 'bar',
        toolbar: {
          show: false,
        }
    },
    plotOptions: {
        bar: {
            horizontal: false,
            columnWidth: '55%',
            endingShape: 'rounded'  
        },
    },
     colors: ['rgb(255, 155, 68)', 'rgb(252, 96, 117)'],
    dataLabels: {
        enabled: false
    },
    stroke: {
        show: true,
        width: 2,
        colors: ['transparent']
    },
    series: [{
        name: 'Net Profit',
        data: [44, 55, 57, 56, 61, 58, 63, 60, 66]
    }, {
        name: 'Revenue',
        data: [76, 85, 101, 98, 87, 105, 91, 114, 94]
    }],
    xaxis: {
        categories: ['Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct'],
    },
    yaxis: {
        title: {
            text: '$ (thousands)'
        }
    },
    fill: {
        opacity: 1

    },
    tooltip: {
        y: {
            formatter: function (val) {
                return "$ " + val + " thousands"
            }
        }
    }
}

var chart = new ApexCharts(
    document.querySelector("#s-col"),
    sCol
);

chart.render();
}


// Simple Column Stacked
if($('#s-col-stacked').length > 0 ){
var sColStacked = {
    chart: {
        height: 350,
        type: 'bar',
        stacked: true,
        toolbar: {
          show: false,
        }
    },
    // colors: ['#4361ee', '#888ea8', '#e3e4eb', '#d3d3d3'],
    responsive: [{
        breakpoint: 480,
        options: {
            legend: {
                position: 'bottom',
                offsetX: -10,
                offsetY: 0
            }
        }
    }],
    plotOptions: {
        bar: {
            horizontal: false,
        },
    },
    series: [{
        name: 'PRODUCT A',
        data: [44, 55, 41, 67, 22, 43]
    },{
        name: 'PRODUCT B',
        data: [13, 23, 20, 8, 13, 27]
    },{
        name: 'PRODUCT C',
        data: [11, 17, 15, 15, 21, 14]
    },{
        name: 'PRODUCT D',
        data: [21, 7, 25, 13, 22, 8]
    }],
    xaxis: {
        type: 'datetime',
        categories: ['01/01/2011 GMT', '01/02/2011 GMT', '01/03/2011 GMT', '01/04/2011 GMT', '01/05/2011 GMT', '01/06/2011 GMT'],
    },
    legend: {
        position: 'right',
        offsetY: 40
    },
    fill: {
        opacity: 1
    },
}

var chart = new ApexCharts(
    document.querySelector("#s-col-stacked"),
    sColStacked
);

chart.render();
}

// Simple Bar
if($('#s-bar').length > 0 ){
var sBar = {
    chart: {
        height: 350,
        type: 'bar',
        toolbar: {
          show: false,
        }
    },
    colors: ['rgb(252, 96, 117)'],
    plotOptions: {
        bar: {
            horizontal: true,
        }
    },
    dataLabels: {
        enabled: false
    },
    series: [{
        data: [400, 430, 448, 470, 540, 580, 690, 1100, 1200, 1380]
    }],
    xaxis: {
        categories: ['South Korea', 'Canada', 'United Kingdom', 'Netherlands', 'Italy', 'France', 'Japan', 'United States', 'China', 'Germany'],
    }
}

var chart = new ApexCharts(
    document.querySelector("#s-bar"),
    sBar
);

chart.render();
}

// Mixed Chart
if($('#mixed-chart').length > 0 ){
var options = {
  chart: {
    height: 350,
    type: 'line',
    toolbar: {
      show: false,
    }
  },
  colors: ['rgb(255, 155, 68)', 'rgb(252, 96, 117)'],
  series: [{
    name: 'Website Blog',
    type: 'column',
    data: [440, 505, 414, 671, 227, 413, 201, 352, 752, 320, 257, 160]
  }, {
    name: 'Social Media',
    type: 'line',
    data: [23, 42, 35, 27, 43, 22, 17, 31, 22, 22, 12, 16]
  }],
  stroke: {
    width: [0, 4]
  },
  title: {
    text: 'Traffic Sources'
  },
  labels: ['01 Jan 2001', '02 Jan 2001', '03 Jan 2001', '04 Jan 2001', '05 Jan 2001', '06 Jan 2001', '07 Jan 2001', '08 Jan 2001', '09 Jan 2001', '10 Jan 2001', '11 Jan 2001', '12 Jan 2001'],
  xaxis: {
    type: 'datetime'
  },
  yaxis: [{
    title: {
      text: 'Website Blog',
    },

  }, {
    opposite: true,
    title: {
      text: 'Social Media'
    }
  }]

}

var chart = new ApexCharts(
  document.querySelector("#mixed-chart"),
  options
);

chart.render();
}

// Donut Chart

if($('#donut-chart').length > 0 ){
var donutChart = {
    chart: {
        height: 350,
        type: 'donut',
        toolbar: {
          show: false,
        }
    },
    // colors: ['#4361ee', '#888ea8', '#e3e4eb', '#d3d3d3'],
    series: [44, 55, 41, 17],
    responsive: [{
        breakpoint: 480,
        options: {
            chart: {
                width: 200,
                height:300,
            },
            legend: {
                position: 'bottom'
            }
        }
    }]
}

var donut = new ApexCharts(
    document.querySelector("#donut-chart"),
    donutChart
);

donut.render();
}

// Radial Chart
if($('#radial-chart').length > 0 ){
var radialChart = {
    chart: {
        height: 350,
        type: 'radialBar',
        toolbar: {
          show: false,
        }
    },
    // colors: ['#4361ee', '#888ea8', '#e3e4eb', '#d3d3d3'],
    plotOptions: {
        radialBar: {
            dataLabels: {
                name: {
                    fontSize: '22px',
                },
                value: {
                    fontSize: '16px',
                },
                total: {
                    show: true,
                    label: 'Total',
                    formatter: function (w) {
                        return 249
                    }
                }
            }
        }
    },
    series: [44, 55, 67, 83],
    labels: ['Apples', 'Oranges', 'Bananas', 'Berries'],    
}

var chart = new ApexCharts(
    document.querySelector("#radial-chart"),
    radialChart
);

chart.render();
}	
	
if($('#working_chart').length > 0) {
    var options = {
        series: [{
        name: 'Break',
        data: [-50, -120, -80, -180, -80, -70, -100],
      }, {
        name: 'Hours',
        data: [200, 250, 200, 290, 220, 300, 250],
      }],
      colors: ['#FC133D', '#55CE63'],
        chart: {
        type: 'bar',
        height: 210,
        stacked: true,
        
        zoom: {
          enabled: true
        }
      },
      responsive: [{
        breakpoint: 280,
        options: {
          legend: {
            position: 'bottom',
            offsetY: 0
          }
        }
      }],
      plotOptions: {
        bar: {
            horizontal: false,
            borderRadius: 4,
            borderRadiusApplication: "end", // "around" / "end" 
            borderRadiusWhenStacked: "all", // "all"/"last"
            columnWidth: '30%',
            endingShape: 'rounded'
        },
      },
      dataLabels: {
      enabled: false
    },
      // stroke: {
      //     width: 5,
      //     colors: ['#fff']
      //   },
      yaxis: {
          min: -200,
          max: 300,
          tickAmount: 5,
        },
      xaxis: {
        categories: [' S ', 'M', 'T', 'W', 'T', 'F' , 'S'
        ],
      },
      legend: {show: false},
      fill: {
        opacity: 1
      }
      };
      var chart = new ApexCharts(document.querySelector("#working_chart"), options);
      chart.render();
}

if($('#leadchart').length > 0 ){
  var options = {
    series: [{
      name: "Session Duration",
      data: [45, 52, 38, 24, 33, 26, 21, 20, 6, 8, 15, 10]
    },
  ],
    chart: {
    height: 350,
    type: 'line',
    zoom: {
      enabled: false
    },
  },
  dataLabels: {
    enabled: false
  },
  stroke: {
    width: [5, 7, 5],
    curve: 'straight',
    dashArray: [0, 8, 5]
  },
  legend: {
    tooltipHoverFormatter: function(val, opts) {
      return val + ' - <strong>' + opts.w.globals.series[opts.seriesIndex][opts.dataPointIndex] + '</strong>'
    }
  },
  markers: {
    size: 0,
    hover: {
      sizeOffset: 6
    }
  },
  colors: ['#FFC38F'],
  xaxis: {
    categories: ['Jan', 'feb', 'march', 'april', 'may', 'jun', 'july', 'aug', 'sep',
      'oct', 'nov', 'dec'
    ],
  },

  tooltip: {
    y: [
      {
        title: {
          formatter: function (val) {
            return val + " (mins)"
          }
        }
      },
      {
        title: {
          formatter: function (val) {
            return val + " per session"
          }
        }
      },
      {
        title: {
          formatter: function (val) {
            return val;
          }
        }
      }
    ]
  },
  grid: {
    borderColor: '#f1f1f1',
  }
  };

  var chart = new ApexCharts(document.querySelector("#leadchart"), options);
  chart.render();

}

if($('#leadpiechart').length > 0 ){
  var options = {
    series: [44, 55, 13, 43],
    chart: {
    width: 450,
    type: 'pie',
  },
  labels: ['Inpipeline', 'Follow Up', 'Schedule Service', 'Conversation'],
  responsive: [{
    breakpoint: 480,
    options: {
      chart: {
        width: 275
      },
      legend: {
        position: 'right'
      }
    }
  }]
  };

  var chart = new ApexCharts(document.querySelector("#leadpiechart"), options);
  chart.render();

}

  
});